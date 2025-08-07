using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrtTicketVendingMachine
{
    public static class SimpleConfig
    {
        private static Dictionary<string, string> _config = new Dictionary<string, string>();

        public static void Load(string filename = "config.txt")
        {
            if (File.Exists(filename))
            {
                _config = File.ReadAllLines(filename)
                    .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#") && line.Contains("="))
                    .ToDictionary(
                        line => line.Split('=')[0].Trim(),
                        line => string.Join("=", line.Split('=').Skip(1)).Trim()
                    );
            }
        }

        public static string Get(string key, string defaultValue = "")
        {
            return _config.ContainsKey(key) ? _config[key] : defaultValue;
        }
    }
}