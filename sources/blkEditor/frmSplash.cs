using phatvu1294;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace blkEditor
{
    public partial class frmSplash : Form
    {
        /***************************************************************/
        /*    Các thành phần toàn cục   */
        /***************************************************************/
        private Utilities utils = new Utilities();
        private string currVersion = string.Empty;

        /***************************************************************/
        /*    Hàm khởi tạo Form     */
        /***************************************************************/
        public frmSplash()
        {
            currVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(2) +
                "." + utils.GetLinkerTimestampUtc(Assembly.GetExecutingAssembly()).ToString("yyMMdd");
            InitializeComponent();
        }

        /***************************************************************/
        /*    Hàm và sự kiện Form      */
        /***************************************************************/
        private void frmSplash_Load(object sender, System.EventArgs e)
        {
            try
            {
                lblSplashVersion.Text += " " + currVersion;
            }
            catch { }
            finally { }
        }
    }
}
