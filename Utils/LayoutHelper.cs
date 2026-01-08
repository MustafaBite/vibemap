using System.Drawing;
using System.Windows.Forms;

namespace VibeMap.Utils
{
    public static class LayoutHelper
    {
        /// <summary>
        /// Centers a control within its parent.
        /// </summary>
        public static void CenterControl(Control ctrl)
        {
            if (ctrl == null || ctrl.Parent == null) return;

            ctrl.Location = new Point(
                (ctrl.Parent.ClientSize.Width - ctrl.Width) / 2,
                (ctrl.Parent.ClientSize.Height - ctrl.Height) / 2
            );
        }

        /// <summary>
        /// Centers a control horizontally within its parent.
        /// </summary>
        public static void CenterHorizontal(Control ctrl, int? top = null)
        {
            if (ctrl == null || ctrl.Parent == null) return;

            ctrl.Left = (ctrl.Parent.ClientSize.Width - ctrl.Width) / 2;
            if (top.HasValue) ctrl.Top = top.Value;
        }
    }
}
