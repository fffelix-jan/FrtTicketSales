using K4os.Compression.LZ4;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// FrtTicket 0.3.0 (.NET Framework 4.8 Edition)
/// Provides methods for encoding and decoding FRT (Falloway Rapid Transit) tickets
/// with cryptographic signatures and obfuscation for QR code generation.
/// Backported for .NET Framework 4.8 compatibility.
/// </summary>
public static class FrtTicket
{
    // Constants
    public static readonly byte[] MagicBytes = Encoding.UTF8.GetBytes("FRT"); // 3-byte header
    public const string TimeZoneId = "China Standard Time"; // Change to your metro's timezone

    public static byte[] XorObfuscate(byte[] data, byte[] key)
    {
        byte[] result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
            result[i] = (byte)(data[i] ^ key[i % key.Length]);
        return result;
    }

    /// <summary>
    /// Encodes ticket data into a secure, obfuscated string suitable for QR code generation.
    /// </summary>
    /// <param name="ticketNumber">Unique identifier for the ticket (12-18 digits recommended).</param>
    /// <param name="valueCents">Ticket value in cents (must be non-negative).</param>
    /// <param name="issuingStation">3-letter station code where the ticket was issued.</param>
    /// <param name="issueDateTime">Exact timestamp of ticket issuance.</param>
    /// <param name="ticketType">
    /// Ticket type identifier:
    /// <list type="bullet">
    ///   <item><description>0 - Full Fare</description></item>
    ///   <item><description>1 - Student</description></item>
    ///   <item><description>2 - Senior</description></item>
    ///   <item><description>3 - Free Exit</description></item>
    ///   <item><description>4 - Day Pass</description></item>
    ///   <item><description>255 - Debug</description></item>
    /// </list>
    /// </param>
    /// <param name="signingKey">ECDsa private key for cryptographic signing.</param>
    /// <param name="xorObfuscatorKey">
    /// Secret key for XOR obfuscation (minimum 16 bytes recommended).
    /// Must match the key used for decoding.
    /// </param>
    /// <returns>
    /// URL-safe Base64 encoded string containing:
    /// <list type="number">
    ///   <item><description>3-byte "FRT" magic header</description></item>
    ///   <item><description>LZ4-compressed ticket data</description></item>
    ///   <item><description>ECDSA signature</description></item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if signingKey or xorObfuscatorKey is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if issuingStation is not 3 characters or valueCents is negative.
    /// </exception>
    public static string EncodeTicket(
        long ticketNumber,
        int valueCents,
        string issuingStation,
        DateTime issueDateTime,
        byte ticketType,
        ECDsa signingKey,
        byte[] xorObfuscatorKey)
    {
        if (signingKey == null)
            throw new ArgumentNullException(nameof(signingKey));
        if (xorObfuscatorKey == null)
            throw new ArgumentNullException(nameof(xorObfuscatorKey));
        if (string.IsNullOrEmpty(issuingStation) || issuingStation.Length != 3)
            throw new ArgumentException("Issuing station must be exactly 3 characters", nameof(issuingStation));
        if (valueCents < 0)
            throw new ArgumentException("Value cannot be negative", nameof(valueCents));

        // 1. Create the ticket string
        string ticketString =
            $"{ticketNumber}|{valueCents}|{issuingStation}|{issueDateTime:yyyyMMddHHmmss}|{ticketType}";

        // 2. Compress the ticket data
        byte[] dataBytes = Encoding.UTF8.GetBytes(ticketString);
        byte[] compressed = LZ4Pickler.Pickle(dataBytes, LZ4Level.L00_FAST);

        // 3. Add magic bytes header
        byte[] dataWithHeader = new byte[MagicBytes.Length + compressed.Length];
        Buffer.BlockCopy(MagicBytes, 0, dataWithHeader, 0, MagicBytes.Length);
        Buffer.BlockCopy(compressed, 0, dataWithHeader, MagicBytes.Length, compressed.Length);

        // 4. Sign the data (including magic bytes) - .NET Framework compatible
        byte[] signature = SignDataCompatible(signingKey, dataWithHeader);

        // 5. Combine header + compressed data + signature
        byte[] combined = new byte[dataWithHeader.Length + signature.Length];
        Buffer.BlockCopy(dataWithHeader, 0, combined, 0, dataWithHeader.Length);
        Buffer.BlockCopy(signature, 0, combined, dataWithHeader.Length, signature.Length);

        // 6. XOR obfuscate
        byte[] obfuscated = XorObfuscate(combined, xorObfuscatorKey);

        // 7. Convert to URL-safe Base64
        return Convert.ToBase64String(obfuscated)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    /// <summary>
    /// Decodes and validates an FRT ticket string, verifying both the XOR obfuscation key
    /// and cryptographic signature.
    /// </summary>
    /// <param name="encodedTicket">
    /// URL-safe Base64 string produced by <see cref="EncodeTicket"/>.
    /// </param>
    /// <param name="signingKey">ECDsa public key for signature verification.</param>
    /// <param name="xorObfuscatorKey">
    /// Secret XOR key that must match the one used during encoding.
    /// </param>
    /// <param name="ticketNumber">Output: Decoded ticket number.</param>
    /// <param name="valueCents">Output: Ticket value in cents.</param>
    /// <param name="issuingStation">Output: 3-letter issuing station code.</param>
    /// <param name="issueDateTime">Output: Ticket issuance timestamp.</param>
    /// <param name="ticketType">Output: Numeric ticket type identifier.</param>
    /// <returns>
    /// True if the ticket is valid (correct XOR key, valid signature, and properly formatted),
    /// false otherwise. Output parameters are only valid when returning true.
    /// </returns>
    public static bool TryDecodeTicket(
        string encodedTicket,
        ECDsa signingKey,
        byte[] xorObfuscatorKey,
        out long ticketNumber,
        out int valueCents,
        out string issuingStation,
        out DateTime issueDateTime,
        out byte ticketType)
    {
        ticketNumber = 0;
        valueCents = 0;
        issuingStation = string.Empty;
        issueDateTime = DateTime.MinValue;
        ticketType = 0;

        try
        {
            if (signingKey == null || xorObfuscatorKey == null)
                return false;

            // 1. Restore Base64
            string padded = encodedTicket
                .Replace('-', '+')
                .Replace('_', '/')
                .PadRight(encodedTicket.Length + (4 - encodedTicket.Length % 4) % 4, '=');

            // 2. Convert from Base64
            byte[] obfuscated = Convert.FromBase64String(padded);

            // 3. De-XOR the ticket
            byte[] combined = XorObfuscate(obfuscated, xorObfuscatorKey);

            // 4. Try to extract and verify signature - handle variable signature lengths
            if (!TryExtractAndVerifySignature(combined, signingKey, out byte[] signedData))
                return false;

            // 5. Verify magic bytes (XOR key check)
            if (signedData.Length < MagicBytes.Length)
                return false;

            for (int i = 0; i < MagicBytes.Length; i++)
            {
                if (signedData[i] != MagicBytes[i])
                {
                    return false; // XOR key verification failed
                }
            }

            // 6. Extract compressed data (after magic bytes)
            byte[] compressed = new byte[signedData.Length - MagicBytes.Length];
            Buffer.BlockCopy(signedData, MagicBytes.Length, compressed, 0, compressed.Length);

            // 7. Decompress
            byte[] dataBytes = LZ4Pickler.Unpickle(compressed);
            string ticketData = Encoding.UTF8.GetString(dataBytes);

            // 8. Parse
            string[] parts = ticketData.Split('|');
            if (parts.Length != 5) return false;

            // Parse fields
            ticketNumber = long.Parse(parts[0]);
            valueCents = int.Parse(parts[1]);
            issuingStation = parts[2];
            issueDateTime = DateTime.ParseExact(parts[3], "yyyyMMddHHmmss", null);
            ticketType = byte.Parse(parts[4]);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Creates an ECDsa public key from Base64-encoded key data.
    /// Compatible with .NET Framework 4.8.
    /// </summary>
    /// <param name="base64PublicKey">Base64-encoded public key</param>
    /// <returns>ECDsa instance for verification</returns>
    public static ECDsa CreatePublicKeyFromBase64(string base64Der)
    {
        byte[] derBytes = Convert.FromBase64String(base64Der);

        var spki = SubjectPublicKeyInfo.GetInstance(Asn1Object.FromByteArray(derBytes));
        var bcKey = (ECPublicKeyParameters)PublicKeyFactory.CreateKey(spki);

        var q = bcKey.Q.Normalize();
        byte[] x = q.AffineXCoord.GetEncoded();
        byte[] y = q.AffineYCoord.GetEncoded();

        var ecParams = new ECParameters
        {
            Curve = ECCurve.NamedCurves.nistP256,
            Q = new ECPoint { X = x, Y = y }
        };

        var ecdsa = ECDsa.Create();
        ecdsa.ImportParameters(ecParams);
        return ecdsa;
    }

    /// <summary>
    /// Signs data using ECDsa in a .NET Framework 4.8 compatible way
    /// </summary>
    private static byte[] SignDataCompatible(ECDsa signingKey, byte[] data)
    {
        try
        {
            // Use the available SignData method
            return signingKey.SignData(data, HashAlgorithmName.SHA256);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to sign data: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Verifies signature using ECDsa in a .NET Framework 4.8 compatible way
    /// </summary>
    private static bool VerifySignatureCompatible(ECDsa signingKey, byte[] data, byte[] signature)
    {
        try
        {
            return signingKey.VerifyData(data, signature, HashAlgorithmName.SHA256);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Tries to extract and verify signature from combined data.
    /// Handles variable signature lengths.
    /// </summary>
    private static bool TryExtractAndVerifySignature(byte[] combined, ECDsa signingKey, out byte[] signedData)
    {
        signedData = null;

        // Try different signature lengths - ECDSA signatures can vary
        // For P-256, try common lengths
        int[] possibleSigLengths = { 64, 70, 71, 72, 73, 74, 68, 69, 75, 76, 77, 78 };

        foreach (int sigLength in possibleSigLengths)
        {
            if (combined.Length <= sigLength)
                continue;

            int dataLength = combined.Length - sigLength;
            byte[] candidateData = new byte[dataLength];
            byte[] candidateSignature = new byte[sigLength];

            Buffer.BlockCopy(combined, 0, candidateData, 0, dataLength);
            Buffer.BlockCopy(combined, dataLength, candidateSignature, 0, sigLength);

            if (VerifySignatureCompatible(signingKey, candidateData, candidateSignature))
            {
                signedData = candidateData;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Creates an ECDsa key from raw P-256 point coordinates
    /// </summary>
    private static ECDsa CreateECDsaFromRawBytes(byte[] keyBytes)
    {
        if (keyBytes.Length != 65 || keyBytes[0] != 0x04)
            throw new ArgumentException("Invalid raw key format");

        try
        {
            // Extract X and Y coordinates
            byte[] x = new byte[32];
            byte[] y = new byte[32];
            Buffer.BlockCopy(keyBytes, 1, x, 0, 32);
            Buffer.BlockCopy(keyBytes, 33, y, 0, 32);

            // Create ECParameters for P-256
            var ecParams = new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                Q = new ECPoint
                {
                    X = x,
                    Y = y
                }
            };

            var ecdsa = ECDsa.Create();
            ecdsa.ImportParameters(ecParams);
            return ecdsa;
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Failed to create ECDsa from raw bytes: {ex.Message}", ex);
        }
    }
}