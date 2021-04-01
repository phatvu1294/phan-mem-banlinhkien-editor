using phatvu1294;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace blkEditor
{
    public partial class frmAbout : Form
    {
        /***************************************************************/
        /*    Các thành phần toàn cục   */
        /***************************************************************/
        private Utilities utils = new Utilities();
        private string currentVersion = string.Empty;

        /***************************************************************/
        /*    Hàm get instance của form     */
        /***************************************************************/
        private static frmAbout _instance;
        public static frmAbout Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmAbout();
                }
                return _instance;
            }
        }

        /***************************************************************/
        /*    Hàm khởi tạo form     */
        /***************************************************************/
        public frmAbout()
        {
            currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(2) +
                "." + utils.GetLinkerTimestampUtc(Assembly.GetExecutingAssembly()).ToString("yyMMdd");
            InitializeComponent();
        }

        /***************************************************************/
        /*    Hàm và sự kiện cho form   */
        /***************************************************************/
        /* Sự kiện khi form được load */
        private void frmAbout_Load(object sender, EventArgs e)
        {
            try
            {
                lblAbout.Text = "BanLinhKien Editor\r\n";
                lblAbout.Text += "Phiên bản " + currentVersion + "\r\n";
                lblAbout.Text += "Bản quyền © 2019 - 2021 Vũ Phát";
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút OK được nhấn */
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
            finally { }
        }
    }
}
