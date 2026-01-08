using DevExpress.XtraEditors;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VibeMap.Utils;

namespace VibeMap.Forms
{
    public partial class FrmMood : XtraForm
    {
        private List<string> _selectedMoods = new List<string>();
        public string UserMood => string.Join(", ", _selectedMoods);

        private readonly string[] _allMoods = {
            "Mutlu / Keyifli",
            "Üzgün",
            "Yorgun",
            "Stresli",
            "Yalnız",
            "Sıkılmış",
            "Heyecanlı",
            "Öfkeli"
        };

        public FrmMood()
        {
            InitializeComponent();
            ThemeManager.ApplyGlobalBackground(this);
            this.Text = "VibeMap | Ruh Hali";
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
            LayoutHelper.CenterControl(pnlCard);
            this.Resize += (s, e) => LayoutHelper.CenterControl(pnlCard);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
        }

        private void ApplyCosmicBackground() { }

        private void FrmMood_Load(object sender, EventArgs e)
        {
            foreach (var m in _allMoods)
            {
                CheckButton btn = new CheckButton();
                btn.Text = m;
                btn.Size = new Size(110, 40);
                btn.Padding = new Padding(5);
                btn.Margin = new Padding(3);
                btn.CheckedChanged += MoodButton_CheckedChanged;
                btn.AllowFocus = false;
                flowMoods.Controls.Add(btn);
            }
        }

        private void MoodButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckButton btn)
            {
                string moodText = btn.Text;
                if (btn.Checked)
                {
                    if (_selectedMoods.Count >= 3)
                    {
                        btn.Checked = false; // Prevent selection
                        XtraMessageBox.Show("En fazla 3 ruh hali seçebilirsiniz!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    _selectedMoods.Add(moodText);
                }
                else
                {
                    _selectedMoods.Remove(moodText);
                }
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (_selectedMoods.Count == 0)
            {
                XtraMessageBox.Show("Lütfen en az bir ruh hali seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ThemeManager.ToggleTheme(this);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
        }
    }
}
