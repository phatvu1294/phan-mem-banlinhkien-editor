using phatvu1294;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blkTemplator
{
    public partial class frmLogin : Form
    {
        /***************************************************************/
        /*    Các thành phần toàn cục     */
        /***************************************************************/
        /* Biến class phatvu1294 */
        public Login login = new Login();        

        /* Các thành phần toàn cục */
        public LoginState loginState = LoginState.failure;
        public string username = string.Empty;
        public string password = string.Empty;
        public LoginPermission permission = LoginPermission.user; 

        /***************************************************************/
        /*    Hàm get instance của form     */
        /***************************************************************/
        private static frmLogin _instance;
        public static frmLogin Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmLogin();
                }
                return _instance;
            }
        }

        /***************************************************************/
        /*    Khởi tạo Form     */
        /***************************************************************/

        /* Khởi tạo form */
        public frmLogin()
        {
            InitializeComponent();
        }

        /***************************************************************/
        /*    Sự kiện Form     */
        /***************************************************************/
        /* Sự kiện khi form được load */
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                tslStatus.ForeColor = Color.Green;
                displayMessageHidden("Vui lòng đăng nhập");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được hiển thị */
        private void frmLogin_Shown(object sender, EventArgs e)
        {
            try
            {
                loginState = LoginState.failure;
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form đang đóng */
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (loginState.Equals(LoginState.failure))
                {
                    Application.Exit();
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Sự kiện các thành phần    */
        /***************************************************************/
        /* Sự kiện khi chuyển tới text box */
        private void txt_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                txt.SelectAll();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi txt mật khẩu ấn phím */
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    username = txtUsername.Text.Trim();
                    password = txtPassword.Text.Trim();

                    if (login.checkUsernameAndPassword(username, password, ref permission))
                    {
                        loginState = LoginState.success;
                        this.Close();
                    }
                    else
                    {
                        loginState = LoginState.failure;
                        tslStatus.ForeColor = Color.Red;
                        displayMessageHidden("Sai tên người dùng hoặc mật khẩu. Vui lòng thử lại");
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đăng nhập được nhấn */
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                username = txtUsername.Text.Trim();
                password = txtPassword.Text.Trim();

                if (login.checkUsernameAndPassword(username, password, ref permission))
                {
                    loginState = LoginState.success;
                    this.Close();
                }
                else
                {
                    loginState = LoginState.failure;
                    tslStatus.ForeColor = Color.Red;
                    displayMessageHidden("Sai tên người dùng hoặc mật khẩu. Vui lòng thử lại");
                }
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện khác   */
        /***************************************************************/
        /* Hàm hiển thị trạng thái với tự động ẩn */
        private void displayMessageHidden(string text)
        {
            try
            {
                tslStatus.Visible = true;
                tslStatus.Text = text;
                tmrHideMessage.Interval = 1500;
                tmrHideMessage.Start();
            }
            catch { }
            finally { }
        }

        /* Hàm hiển thị trạng thái */
        private void displayMessage(string text)
        {
            try
            {
                tmrHideMessage.Stop();
                tslStatus.Visible = true;
                tslStatus.Text = text;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi timer ẩn tin nhắn được tick */
        private void tmrHideMessage_Tick(object sender, EventArgs e)
        {
            try
            {
                tslStatus.Text = string.Empty;
                tslStatus.Visible = false;
                tmrHideMessage.Stop();
                tmrHideMessage.Interval = 0;
            }
            catch { }
            finally { }
        }
    }
}
