using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;

namespace VibeMap.Utils
{
    public static class ThemeManager
    {
        public enum ThemeMode { Light, Dark }
        public static ThemeMode CurrentTheme { get; private set; } = ThemeMode.Dark;

        // Dark Theme Colors (Cosmic Night)
        private static readonly Color DarkBg = Color.FromArgb(11, 14, 20); // Deep Space
        private static readonly Color DarkCard = Color.FromArgb(180, 28, 32, 40); // 180 = Semi-transparent
        private static readonly Color DarkText = Color.White;
        private static readonly Color DarkAccentStart = Color.FromArgb(58, 155, 237); // #3a9bed
        private static readonly Color DarkAccentEnd = Color.FromArgb(36, 94, 207);   // #245ecf

        // Light Theme Colors (Cosmic Day)
        private static readonly Color LightBg = Color.FromArgb(240, 242, 245); // Soft Lavender
        private static readonly Color LightCard = Color.FromArgb(200, 255, 255, 255);
        private static readonly Color LightText = Color.FromArgb(40, 40, 50);
        private static readonly Color LightAccentStart = Color.FromArgb(58, 155, 237); // Consistent blue
        private static readonly Color LightAccentEnd = Color.FromArgb(36, 94, 207);

        public static void ApplyTheme(Form form, ThemeMode mode)
        {
            CurrentTheme = mode;
            Color bg = mode == ThemeMode.Dark ? DarkBg : LightBg;
            Color text = mode == ThemeMode.Dark ? DarkText : LightText;

            foreach (Control ctrl in form.Controls)
            {
                ApplyToControl(ctrl, mode);
            }

            // DevExpress Specific (Global Theme adjustment)
            UserLookAndFeel.Default.SetSkinStyle(mode == ThemeMode.Dark ? "Visual Studio 2013 Dark" : "Visual Studio 2013 Light");
        }

        private static void ApplyToControl(Control ctrl, ThemeMode mode)
        {
            Color cardBg = mode == ThemeMode.Dark ? DarkCard : LightCard;
            Color text = mode == ThemeMode.Dark ? DarkText : LightText;
            Color accentStart = mode == ThemeMode.Dark ? DarkAccentStart : LightAccentStart;
            Color accentEnd = mode == ThemeMode.Dark ? DarkAccentEnd : LightAccentEnd;

            if (ctrl is SimpleButton btn)
            {
                btn.Appearance.BackColor = accentStart;
                btn.Appearance.BackColor2 = accentEnd;
                btn.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
                btn.Appearance.ForeColor = Color.White;
                btn.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.Appearance.Options.UseBackColor = true;
                btn.Appearance.Options.UseForeColor = true;
                btn.Appearance.Options.UseFont = true;
                btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            }
            else if (ctrl is TextEdit txt)
            {
                // Solid colors for input fields to ensure text visibility
                txt.Properties.Appearance.BackColor = mode == ThemeMode.Dark 
                    ? Color.FromArgb(255, 30, 32, 40) // Opaque dark
                    : Color.White;
                
                txt.Properties.Appearance.ForeColor = text;
                txt.Properties.Appearance.Font = new Font("Segoe UI", 10);
                txt.Properties.Appearance.Options.UseBackColor = true;
                txt.Properties.Appearance.Options.UseForeColor = true;
                txt.Properties.Appearance.Options.UseFont = true;
                txt.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            }
            else if (ctrl is LabelControl lbl)
            {
                lbl.Appearance.ForeColor = text;
                lbl.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lbl.Appearance.Options.UseForeColor = true;
                lbl.Appearance.Options.UseFont = true;
            }
            else if (ctrl is PanelControl pnl)
            {
                if (pnl.Tag?.ToString() == "KeepTransparent")
                {
                    pnl.Appearance.BackColor = Color.Transparent;
                    pnl.Appearance.Options.UseBackColor = true;
                }
                else
                {
                    pnl.Appearance.BackColor = cardBg;
                    pnl.Appearance.Options.UseBackColor = true;
                }
                pnl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            }

            // Recursive for nested controls
            if (ctrl.HasChildren)
            {
                foreach (Control child in ctrl.Controls)
                {
                    ApplyToControl(child, mode);
                }
            }
        }

        public static void ToggleTheme(Form form)
        {
            ApplyTheme(form, CurrentTheme == ThemeMode.Dark ? ThemeMode.Light : ThemeMode.Dark);
        }

        public static void SetupPasswordPeek(ButtonEdit edit)
        {
            var btn = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph);
            btn.Caption = "👁";
            edit.Properties.Buttons.Clear(); // Remove default if any
            edit.Properties.Buttons.Add(btn);
            edit.Properties.ButtonClick += (s, e) =>
            {
                edit.Properties.UseSystemPasswordChar = !edit.Properties.UseSystemPasswordChar;
                btn.Caption = edit.Properties.UseSystemPasswordChar ? "👁" : "🔒";
            };
        }

        public static void ApplyGlobalBackground(Form form)
        {
            try
            {
                string bgPath = @"C:\Users\MSİ\.gemini\antigravity\brain\703709a7-ea6d-4f33-b648-39208d596c61\dark_blue_cosmic_texture_png_1767212904117.png";
                if (System.IO.File.Exists(bgPath))
                {
                    form.BackgroundImage = Image.FromFile(bgPath);
                    form.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch { /* Fallback to default theme colors */ }
        }
    }
}
