using blkEditor.Properties;
using phatvu1294;
using System;
using System.Windows.Forms;

namespace blkEditor
{
    public partial class frmGetData : Form
    {
        /***************************************************************/
        /*    Thành phần toàn cục   */
        /***************************************************************/
        /* Biến class phatvu1294 */
        private ProductWeb pWeb = new ProductWeb();
        private ProductWebData pWebData = new ProductWebData();

        /* Biến dữ liệu html nhận được */
        public string htmlData = string.Empty;
        public bool confirmGetLink = false;

        /***************************************************************/
        /*    Hàm get instance của form     */
        /***************************************************************/
        private static frmGetData _instance;
        public static frmGetData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmGetData();
                }
                return _instance;
            }
        }

        /***************************************************************/
        /*    Hàm khởi tạo form     */
        /***************************************************************/
        public frmGetData()
        {
            InitializeComponent();
        }

        /***************************************************************/
        /*    Hàm và sự kiện form   */
        /***************************************************************/
        /* Sự kiện khi form được load */
        private void frmGetLink_Load(object sender, EventArgs e)
        {
            try
            {
                chkAddToListImage.Checked = Settings.Default.chkAddToListImage;
                chkGetImageProfile.Checked = Settings.Default.chkGetImageProfile;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi Form được hiển thị */
        private void frmGetLink_Shown(object sender, EventArgs e)
        {
            try
            {
                confirmGetLink = false;
                htmlData = string.Empty;
                txtLinkAddressName.Clear();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được đóng */
        private void frmGetLink_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Settings.Default.chkAddToListImage = chkAddToListImage.Checked;
                Settings.Default.chkGetImageProfile = chkGetImageProfile.Checked;
                Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện lấy dữ liệu   */
        /***************************************************************/
        /* Sự kiện khi textbox địa chỉ được nhấn */
        private async void txtLinkAddressName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Return))
                {
                    string text = txtLinkAddressName.Text.Trim();

                    if (Uri.IsWellFormedUriString(text, UriKind.RelativeOrAbsolute))
                    {
                        htmlData = pWeb.getHtmlUsingHttpWebClient(text);
                    }
                    else
                    {
                        pWebData = await pWeb.getProductDataFromWebSearch(text);
                        htmlData = pWeb.getHtmlUsingHttpWebClient(pWebData.productLink);
                    }

                    confirmGetLink = true;
                    this.Close();
                }
            }
            catch { }
            finally { }
        }

        /* sự kiện khi nút lấy dữ liệu được nhấn */
        private void btnGetDataDone_Click(object sender, EventArgs e)
        {
            try
            {
                htmlData = pWeb.getHtmlUsingHttpWebClient(txtLinkAddressName.Text.Trim());

                confirmGetLink = true;
                this.Close();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi check box lấy ảnh từ ảnh đại điện được nhấp chọn */
        private void chkGetImageProfile_Click(object sender, EventArgs e)
        {
            try
            {
                /* Lưu vào Settings */
                Properties.Settings.Default.chkGetImageProfile = chkGetImageProfile.Checked;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi checkbox thêm ảnh vào danh sách chỉnh sửa được nhấp chọn */
        private void chkAddToListImage_Click(object sender, EventArgs e)
        {
            try
            {
                /* Lưu vào Settings */
                Properties.Settings.Default.chkAddToListImage = chkAddToListImage.Checked;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }
    }
}
