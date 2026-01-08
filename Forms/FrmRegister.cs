using System;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using VibeMap.Utils;
using System.Data.SQLite;

namespace VibeMap.Forms
{
    public partial class FrmRegister : DevExpress.XtraEditors.XtraForm
    {
        public FrmRegister()
        {
            InitializeComponent();
            ThemeManager.ApplyGlobalBackground(this);
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
            ThemeManager.SetupPasswordPeek(txtPassword);
            ThemeManager.SetupPasswordPeek(txtPassword2);
            LayoutHelper.CenterControl(pnlCard);
            this.Resize += (s, e) => LayoutHelper.CenterControl(pnlCard);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
        }

        private void ApplyCosmicBackground() { }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Return to Login
        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ThemeManager.ToggleTheme(this);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string pass1 = txtPassword.Text.Trim();
            string pass2 = txtPassword2.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(pass1) ||
                string.IsNullOrWhiteSpace(pass2))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı");
                return;
            }

            if (pass1 != pass2)
            {
                MessageBox.Show("Şifreler eşleşmiyor.", "Uyarı");
                return;
            }

            if (pass1.Length != 8)
            {
                MessageBox.Show(
                    "Şifre tam olarak 8 karakter olmalıdır.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }




            string passwordHash = HashPassword(pass1);

            using (var con = DbConnection.GetUserConnection())
            {
                con.Open();

                // 1️⃣ Aynı kullanıcı adı var mı?
                using (var checkCmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM Users WHERE Username = @u", con))
                {
                    checkCmd.Parameters.AddWithValue("@u", username);

                    long count = (long)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show(
                            "Kullanıcı adı zaten mevcut. Başka bir isim deneyin.",
                            "Uyarı"
                        );
                        return;
                    }
                }

                // 2️⃣ Kullanıcıyı ekle (CreatedAt + ProfilePicturePath)
                using (var insertCmd = new SQLiteCommand(
                    "INSERT INTO Users (Username, PasswordHash, CreatedAt) VALUES (@u, @p, @c)", con))
                {
                    insertCmd.Parameters.AddWithValue("@u", username);
                    insertCmd.Parameters.AddWithValue("@p", passwordHash);
                    insertCmd.Parameters.AddWithValue("@c", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    insertCmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Kayıt başarılı! Şimdi giriş yapabilirsiniz.",
                "Bilgi"
            );

            this.Close(); // Login ekranına geri
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

        private void FrmRegister_Load(object sender, EventArgs e)
        {
        }
    }
}
