using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FrtTicketVendingMachine
{
    public class RoundedButton : Button
    {
        private float _thickness = 3;  // Slightly thicker for better touch visibility
        public float Thickness
        {
            get => _thickness;
            set
            {
                _thickness = value;
                _pen = new Pen(_borderColor, Thickness);
                Invalidate();
            }
        }

        private Color _borderColor = Color.FromArgb(70, 70, 70); // Default to neutral gray
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                _pen = new Pen(_borderColor, Thickness);
                Invalidate();
            }
        }

        private int _radius = 25;  // Larger radius for touch targets
        public int Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Invalidate();
            }
        }

        private Pen _pen;

        public RoundedButton() : base()
        {
            _pen = new Pen(BorderColor, Thickness);
            DoubleBuffered = true;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;

            // Touchscreen optimizations
            SetStyle(ControlStyles.Selectable, false);  // Remove focus rectangle
            TabStop = false;  // Prevent keyboard tab selection
        }

        private GraphicsPath GetRoundedPath(Rectangle rect)
        {
            int radius = Radius;
            GraphicsPath path = new GraphicsPath();

            // Ensure radius doesn't exceed half the button size
            radius = Math.Min(radius, Math.Min(rect.Width / 2, rect.Height / 2));

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = GetRoundedPath(rect))
            {
                // Determine colors for current state
                Color bgColor = GetBackgroundColor();
                Color borderColor = GetBorderColor();
                Color textColor = GetTextColor();

                // Fill background
                using (var brush = new SolidBrush(bgColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Draw border
                if (Thickness > 0)
                {
                    using (var borderPen = new Pen(borderColor, Thickness))
                    {
                        e.Graphics.DrawPath(borderPen, path);
                    }
                }

                // Update region for clickable area
                Region = new Region(path);
            }

            // Draw text with proper padding
            DrawButtonText(e);
        }

        private Color GetBackgroundColor()
        {
            if (!Enabled)
                return Color.FromArgb(220, 220, 220); // Light gray when disabled

            return BackColor;
        }

        private Color GetBorderColor()
        {
            if (!Enabled)
                return Color.FromArgb(150, 150, 150); // Medium gray when disabled

            return BorderColor;
        }

        private Color GetTextColor()
        {
            if (!Enabled)
                return Color.FromArgb(100, 100, 100); // Dark gray when disabled

            return ForeColor;
        }

        private void DrawButtonText(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle textRect = new Rectangle(
                Padding.Left,
                Padding.Top,
                Width - Padding.Horizontal,
                Height - Padding.Vertical);

            using (var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            })
            {
                using (var brush = new SolidBrush(GetTextColor()))
                {
                    // Add subtle text shadow for better readability on touchscreens
                    if (Enabled)
                    {
                        using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                        {
                            Rectangle shadowRect = textRect;
                            shadowRect.Offset(1, 1);
                            e.Graphics.DrawString(Text, Font, shadowBrush, shadowRect, format);
                        }
                    }

                    e.Graphics.DrawString(Text, Font, brush, textRect, format);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        // Touchscreen optimizations
        protected override bool ShowFocusCues => false;
        protected override void OnGotFocus(EventArgs e) => base.OnGotFocus(e);
        protected override void OnLostFocus(EventArgs e) => base.OnLostFocus(e);
    }
}