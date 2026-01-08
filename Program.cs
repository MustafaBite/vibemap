using System;
using System.Windows.Forms;
using VibeMap.DataAccess;
using VibeMap.Forms;

namespace VibeMap
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DatabaseInitializer.Initialize();

                Application.Run(new FrmLogin());
            }
            catch (Exception ex)
            {
                string errorMsg = "KRİTİK HATA: " + ex.Message + "\n\n" + ex.StackTrace;
                if (ex.InnerException != null)
                {
                    errorMsg += "\n\nİç Hata: " + ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                }
                System.IO.File.WriteAllText("startup_error.log", errorMsg);
                MessageBox.Show(errorMsg, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
