using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VibeMap.Utils;
using VibeMap.DataAccess;
using System.Data.SQLite;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace VibeMap.Forms
{
    public partial class FrmContentList : XtraForm
    {
        private string _username;
        private string _statusFilter;

        public FrmContentList(string username, string statusFilter, string headerTitle)
        {
            InitializeComponent();
            _username = username;
            _statusFilter = statusFilter;
            lblHeader.Text = headerTitle;
            this.Text = "VibeMap | " + headerTitle;

            ThemeManager.ApplyGlobalBackground(this);
            ApplyCosmicBackground();
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            LoadData();
        }

        private void ApplyCosmicBackground()
        {
            gridContents.BackColor = Color.Transparent;
            gvContents.Appearance.Empty.BackColor = Color.Transparent;
            gvContents.Appearance.Row.BackColor = Color.FromArgb(150, 30, 30, 50); // Semi-transparent rows
            gvContents.Appearance.Row.ForeColor = Color.White;
        }

        private void LoadData()
        {
            try
            {
                using (var con = DbConnection.GetUserConnection())
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand("SELECT Id, Title, Category FROM UserActions WHERE Username = @u AND Status = @s ORDER BY Timestamp DESC", con))
                    {
                        cmd.Parameters.AddWithValue("@u", _username);
                        cmd.Parameters.AddWithValue("@s", _statusFilter);
                        
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gridContents.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Veri yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteRep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = gvContents.GetFocusedDataRow();
            if (row == null) return;

            int id = Convert.ToInt32(row["Id"]);
            string title = row["Title"].ToString();

            if (XtraMessageBox.Show($"'{title}' ögesini silmek istediğinize emin misiniz?", "Silmeyi Onayla", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var con = DbConnection.GetUserConnection())
                    {
                        con.Open();
                        using (var cmd = new SQLiteCommand("DELETE FROM UserActions WHERE Id = @id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadData(); // Refresh list
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Öge silinirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMoveRep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = gvContents.GetFocusedDataRow();
            if (row == null) return;

            int id = Convert.ToInt32(row["Id"]);
            
            // Create a context menu for target lists
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Daha Sonra Listesine Taşı", null, (s, ev) => MoveItem(id, "Daha Sonra"));
            menu.Items.Add("Aktivite Geçmişine Taşı", null, (s, ev) => MoveItem(id, "İzlenenler"));
            menu.Items.Add("Çöp'e Taşı", null, (s, ev) => MoveItem(id, "Çöp"));

            // Show menu at cursor position
            menu.Show(Cursor.Position);
        }

        private void MoveItem(int id, string newStatus)
        {
            try
            {
                using (var con = DbConnection.GetUserConnection())
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand("UPDATE UserActions SET Status = @s, Timestamp = @tm WHERE Id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@s", newStatus);
                        cmd.Parameters.AddWithValue("@tm", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData(); // Refresh list
                XtraMessageBox.Show("Öge başarıyla taşındı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Öge taşınırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
