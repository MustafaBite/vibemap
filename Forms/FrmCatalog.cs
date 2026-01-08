using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VibeMap.Utils;
using VibeMap.Services;
using VibeMap.DataAccess;
using System.Data.SQLite;

namespace VibeMap.Forms
{
    public partial class FrmCatalog : XtraForm
    {
        private string _username;

        public FrmCatalog(string username)
        {
            InitializeComponent();
            _username = username;
            
            ThemeManager.ApplyGlobalBackground(this);
            ApplyCosmicBackground(); // Keep for grid specific logic
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            LoadData();
        }

        private void ApplyCosmicBackground()
        {
            gridCatalog.BackColor = Color.Transparent;
            gvCatalog.Appearance.Empty.BackColor = Color.Transparent;
            gvCatalog.Appearance.Row.BackColor = Color.FromArgb(150, 30, 30, 50);
            gvCatalog.Appearance.Row.ForeColor = Color.White;
            gvCatalog.Appearance.GroupRow.BackColor = Color.FromArgb(180, 50, 50, 80);
            gvCatalog.Appearance.GroupRow.ForeColor = Color.Orange;
        }

        private Dictionary<string, Image> _imageCache = new Dictionary<string, Image>();

        private void LoadData()
        {
            var allRecs = RecommendationService.GetAllRecommendations();
            gridCatalog.DataSource = allRecs;
            
            gvCatalog.CustomUnboundColumnData += GvCatalog_CustomUnboundColumnData;
        }

        private void GvCatalog_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "ImageObject" && e.IsGetData)
            {
                var rec = e.Row as Recommendation;
                if (rec == null || string.IsNullOrEmpty(rec.ImagePath)) return;

                if (_imageCache.ContainsKey(rec.ImagePath))
                {
                    e.Value = _imageCache[rec.ImagePath];
                }
                else
                {
                    // For a smooth experience, we'd do this async, but for a dev prototype, 
                    // a simple cache + try/catch is usually enough to "WOW" the initial view.
                    try
                    {
                        if (rec.ImagePath.StartsWith("http"))
                        {
                            using (var wc = new System.Net.WebClient())
                            {
                                byte[] data = wc.DownloadData(rec.ImagePath);
                                using (var ms = new System.IO.MemoryStream(data))
                                {
                                    var img = Image.FromStream(ms);
                                    _imageCache[rec.ImagePath] = img;
                                    e.Value = img;
                                }
                            }
                        }
                        else if (System.IO.File.Exists(rec.ImagePath))
                        {
                            var img = Image.FromFile(rec.ImagePath);
                            _imageCache[rec.ImagePath] = img;
                            e.Value = img;
                        }
                    }
                    catch { /* Fallback */ }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvCatalog_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            // If user clicks a data row (not a group row or button), show detailed view
            if (e.HitInfo.InDataRow && !e.HitInfo.InRowCell) 
            {
                ShowDetail(e.RowHandle);
            }
        }

        private void ShowDetail(int rowHandle)
        {
            var rec = gvCatalog.GetRow(rowHandle) as Recommendation;
            if (rec != null)
            {
                using (var frm = new FrmRecommendation(_username, rec.Category, rec.Title, rec.Description, rec.ImagePath, rec.ActionLink))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void btnAddRep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var rec = gvCatalog.GetFocusedRow() as Recommendation;
            if (rec == null) return;

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Daha Sonra'ya Ekle", null, (s, ev) => SaveAction(rec, "Daha Sonra"));
            menu.Items.Add("Aktivite Geçmişine Ekle", null, (s, ev) => SaveAction(rec, "İzlenenler"));
            menu.Show(Cursor.Position);
        }

        private void btnDeleteRep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var rec = gvCatalog.GetFocusedRow() as Recommendation;
            if (rec == null) return;

            if (XtraMessageBox.Show($"'{rec.Title}' ögesini çöpe atmak istiyor musunuz?", "Onayla", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveAction(rec, "Çöp");
            }
        }

        private void SaveAction(Recommendation rec, string status)
        {
            try
            {
                using (var con = DbConnection.GetUserConnection())
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand(con))
                    {
                        // Clean existing
                        cmd.CommandText = "DELETE FROM UserActions WHERE Username = @u AND Title = @t";
                        cmd.Parameters.AddWithValue("@u", _username);
                        cmd.Parameters.AddWithValue("@t", rec.Title);
                        cmd.ExecuteNonQuery();

                        // Insert new
                        cmd.CommandText = "INSERT INTO UserActions (Username, Category, Title, Status, Timestamp) VALUES (@u, @c, @t, @s, @tm)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@u", _username);
                        cmd.Parameters.AddWithValue("@c", rec.Category);
                        cmd.Parameters.AddWithValue("@t", rec.Title);
                        cmd.Parameters.AddWithValue("@s", status);
                        cmd.Parameters.AddWithValue("@tm", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
                XtraMessageBox.Show($"'{rec.Title}' başarıyla işaretlendi: {status}.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
