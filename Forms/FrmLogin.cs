using DevExpress.XtraEditors;
using System.Drawing;
using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using VibeMap.Utils;

namespace VibeMap.Forms
{
    public partial class FrmLogin : XtraForm
    {
        public FrmLogin()
        {
            InitializeComponent();
            ThemeManager.ApplyGlobalBackground(this);
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
            ThemeManager.SetupPasswordPeek(txtPassword);
            LayoutHelper.CenterControl(pnlCard);
            this.Resize += (s, e) => LayoutHelper.CenterControl(pnlCard);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
        }

        // Method kept minimal if needed by designer, but logic moved to ThemeManager
        private void ApplyCosmicBackground() { }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                XtraMessageBox.Show(
                    "Kullanıcı adı ve şifre boş bırakılamaz.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string passwordHash = HashPassword(password);

            using (var con = DbConnection.GetUserConnection())
            {
                con.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT PasswordHash FROM Users WHERE Username = @u", con))
                {
                    cmd.Parameters.AddWithValue("@u", username);

                    var result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        XtraMessageBox.Show(
                            "Bu kullanıcı adına ait kayıt bulunamadı.",
                            "Hata",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    if (result.ToString() != passwordHash)
                    {
                        XtraMessageBox.Show(
                            "Hatalı şifre.",
                            "Hata",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }
                }
            }

            // ✅ GİRİŞ BAŞARILI → ANA SAYFA
            OpenHomeForm(username);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FrmRegister frm = new FrmRegister())
            {
                frm.ShowDialog();
            }
            this.Show();
        }

        private void OpenHomeForm(string username)
        {
            this.Hide();
            using (FrmHome frmHome = new FrmHome(username))
            {
                frmHome.ShowDialog();
            }
            // If we reach here, either FrmHome was closed or we're logging out.
            // Application.Exit() in FrmHome.OnFormClosing will handle the process termination.
            this.Close(); 
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();

                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ThemeManager.ToggleTheme(this);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
        }
    }
}
