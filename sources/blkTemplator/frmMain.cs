using phatvu1294;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace blkTemplator
{
    public partial class frmMain : Form
    {
        /***************************************************************/
        /*    Các thành phần toàn cục     */
        /***************************************************************/

        /* Biến phiên bản */
        private string currentVersion = string.Empty;

        /* Biến class phatvu1294 */
        private ConfigParam configData = new ConfigParam();
        private Configuration config = new Configuration();
        private GoogleDrive gDrive = new GoogleDrive();
        private Category category = new Category();
        private Utilities utils = new Utilities();

        /* Biến thành phần toàn cục */
        private List<CategoryData> superCategorySyncList = new List<CategoryData>();
        private List<CategoryData> superCategoryList = new List<CategoryData>();
        private List<string> categorySyncList = new List<string>();
        private List<string> categoryList = new List<string>();
        private List<string> detailList = new List<string>();
        private List<string> specList = new List<string>();
        private List<string> keywordList = new List<string>();
        private List<string> detailCustomList = new List<string>();
        private List<string> specCustomList = new List<string>();
        private List<string> keywordCustomList = new List<string>();

        /* Data source combo box */
        private List<CategoryData> categoryLevel1List = new List<CategoryData>();
        private List<CategoryData> categoryLevel2List = new List<CategoryData>();
        private List<CategoryData> categoryLevel3List = new List<CategoryData>();

        /* Biến dữ liệu khác */
        private int categoryIndex = 0;
        private int categoryLevel1Index = 0;
        private int categoryLevel2Index = 0;
        private int categoryLevel3Index = 0;
        private string fileCategoryXmlPath = string.Empty;
        private string fileTemplateXmlPath = string.Empty;
        private string fileTemplateExcelPath = string.Empty;

        /* Cấu trúc các tham số xuất file Excel */
        public struct ImportExportExcelParam
        {
            public string fileXml;
            public string fileExcel;
        }

        /***************************************************************/
        /*    Sự kiện khởi tạo Form     */
        /***************************************************************/
        public frmMain()
        {
            try
            {
                currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(2) +
                    "." + utils.GetLinkerTimestampUtc(Assembly.GetExecutingAssembly()).ToString("yyMMdd");

                /* Lấy thông số cấu hình */
                config.getBlkTemplatorConfiguration(ref configData);
                config.getBlkTemplatorConfigurationContent(ref configData);

                /* Lấy thông số của drive */
                gDrive.getDriveParamFromFileContent(configData.driveParamContent);

                /* Lấy thông số đăng nhập */
                frmLogin.Instance.login.getLoginParamFromFileContent(configData.loginParamContent);

                /* Khởi tạo các thành phần */
                InitializeComponent();
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện Form     */
        /***************************************************************/
        /* Sự kiện khi Form được load */
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                /* Tiêu đề Form */
                this.Text += " " + currentVersion;

                /* Phím tắt mặc định */
                mniOpenTemplate.ShortcutKeys = Keys.Control | Keys.O;
                mniSaveTemplate.ShortcutKeys = Keys.Control | Keys.S;
                mniGetTemplateFromDrive.ShortcutKeys = Keys.Control | Keys.G;
                mniUploadTemplateToDrive.ShortcutKeys = Keys.Control | Keys.U;
                mniExit.ShortcutKeys = Keys.Control | Keys.Q;
                mniHomepage.ShortcutKeys = Keys.None;
                mniManual.ShortcutKeys = Keys.F1;
                mniAbout.ShortcutKeys = Keys.Control | Keys.F1;
                mniLogout.Visible = false;
                cbxCategoryLevel1.Enabled = false;
                cbxCategoryLevel2.Enabled = false;

                /* Thanh trạng thái mặc định */
                tslPath.Text = string.Empty;
                tslPath.Visible = false;
                tspImportExport.Visible = false;
                tslStatus.Visible = false;

                /* Hiển thị thông báo */
                displayMessageHidden("Sẵn sàng");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được hiển thị */
        private void frmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                displayMessageHidden("Vui lòng đăng nhập");
                componentAllState(false);

                frmLogin.Instance.ShowDialog();

                while (frmLogin.Instance.loginState == LoginState.failure)
                {
                    displayMessageHidden("Vui lòng đăng nhập");
                    frmLogin.Instance.ShowDialog();
                }

                displayMessageHidden("Đăng nhập thành công");

                if (frmLogin.Instance.permission == LoginPermission.admin)
                {
                    this.Text += " - " + frmLogin.Instance.username + " (Quản trị viên)";
                }
                else if (frmLogin.Instance.permission == LoginPermission.user)
                {
                    this.Text += " - " + frmLogin.Instance.username + " (Người dùng thường)";
                }

                componentStateWhenFileClosed();
                mniLogout.Visible = true;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form đang được đóng */
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.Exit();
                Process.GetCurrentProcess().Kill();
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện liên quan đến tệp mẫu      */
        /***************************************************************/
        /* Hàm load tệp category xml */
        private void loadCategorySyncFromXmlFile(string fileXml)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fileXml);
                XmlNodeList node = xdoc.DocumentElement.ChildNodes;

                for (int i = 0; i < node.Count; i++)
                {
                    categorySyncList.Add(string.Empty);
                }

                for (int i = 0; i < node.Count; i++)
                {
                    if (node[i].Attributes["name"] != null)
                    {
                        categorySyncList[i] = node[i].Attributes["name"].InnerText;
                    }
                }
            }
            catch
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn tệp danh mục cần mở";
                ofd.Filter = "XML file|*.xml";

                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    fileCategoryXmlPath = ofd.FileName;
                    loadCategorySyncFromXmlFile(fileCategoryXmlPath);
                }
            }
            finally { }
        }

        /* Hàm đóng tệp template xml */
        private void closeTemplateXmlFile(bool displayMessage)
        {
            try
            {
                /* Đặt lại danh sách */
                categorySyncList = new List<string>();
                categoryList = new List<string>();
                detailList = new List<string>();
                specList = new List<string>();
                keywordList = new List<string>();
                detailCustomList = new List<string>();
                specCustomList = new List<string>();
                keywordCustomList = new List<string>();

                /* Đặt lại danh mục */
                categoryLevel1List = new List<CategoryData>();
                categoryLevel2List = new List<CategoryData>();
                categoryLevel3List = new List<CategoryData>();

                /* Xoá nội dung */
                cbxCategoryLevel1.DataSource = null;
                cbxCategoryLevel2.DataSource = null;
                cbxCategoryLevel1.Enabled = false;
                cbxCategoryLevel2.Enabled = false;

                /* Đặt lại chỉ mục */
                categoryLevel1Index = 0;
                categoryLevel2Index = 0;
                categoryLevel3Index = 0;

                /* Xoá nội dung */
                trvCategory.Nodes.Clear();
                txtProductDetail.Clear();
                txtProductSpec.Clear();
                txtProductKeyword.Clear();
                txtProductDetailCustom.Clear();
                txtProductSpecCustom.Clear();
                txtProductKeywordCustom.Clear();
                trvCategory.HideSelection = false;

                /* Ẩn toàn bộ thông báo */
                tslPath.Text = string.Empty;
                tslPath.Visible = false;
                tspImportExport.Visible = false;
                tslStatus.Visible = false;

                /* Đặt lại trạng thái nút mới mở */
                componentStateWhenFileClosed();

                /* Đặt lại đường dẫn */
                fileTemplateXmlPath = string.Empty;
                fileCategoryXmlPath = string.Empty;

                /* Nếu hiển thị tin nhắn */
                if (displayMessage == true)
                {
                    displayMessageHidden("Tệp mẫu đã được đóng");
                }
            }
            catch { }
            finally { }
        }

        /* Hàm mở template từ tệp Xml */
        private void openTemplateFromXmlFile(string fileXml, bool displayMessage)
        {
            try
            {
                componentStateWhenFileClosed();
                trvCategory.Nodes.Clear();
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fileXml);
                XmlNodeList node = xdoc.DocumentElement.ChildNodes;

                for (int i = 0; i < node.Count; i++)
                {
                    categoryList.Add(string.Empty);
                    detailList.Add(string.Empty);
                    specList.Add(string.Empty);
                    keywordList.Add(string.Empty);
                    detailCustomList.Add(string.Empty);
                    specCustomList.Add(string.Empty);
                    keywordCustomList.Add(string.Empty);
                }

                for (int i = 0; i < node.Count; i++)
                {
                    if (node[i].Attributes["name"] != null)
                    {
                        categoryList[i] = node[i].Attributes["name"].InnerText;
                        detailList[i] = node[i].ChildNodes[0] != null ? node[i].ChildNodes[0].InnerText : string.Empty;
                        specList[i] = node[i].ChildNodes[1] != null ? node[i].ChildNodes[1].InnerText : string.Empty;
                        keywordList[i] = node[i].ChildNodes[2] != null ? node[i].ChildNodes[2].InnerText : string.Empty;
                        detailCustomList[i] = node[i].ChildNodes[3] != null ? node[i].ChildNodes[3].InnerText : string.Empty;
                        specCustomList[i] = node[i].ChildNodes[4] != null ? node[i].ChildNodes[4].InnerText : string.Empty;
                        keywordCustomList[i] = node[i].ChildNodes[5] != null ? node[i].ChildNodes[5].InnerText : string.Empty;
                    }
                }

                loadCategoryLevel1();
                componentAllState(true);

                if (displayMessage == true)
                {
                    if (!string.IsNullOrEmpty(fileTemplateXmlPath))
                    {
                        tslPath.Text = fileTemplateXmlPath;
                        tslPath.Visible = true;
                        displayMessageHidden("Tệp mẫu đã được mở");
                    }
                    else
                    {
                        tslPath.Text = string.Empty;
                        tslPath.Visible = false;
                        displayMessageHidden("Không thể mở tệp mẫu");
                    }
                }
            }
            catch
            {
                componentStateWhenFileClosed();

                if (displayMessage == true)
                {
                    tslPath.Text = string.Empty;
                    tslPath.Visible = false;
                    displayMessageHidden("Không thể mở tệp mẫu");
                }
            }
            finally { }
        }

        /* Hàm lưu template ra tệp Xml */
        private void createTemplateEmptyToXmlFile(string fileXml)
        {
            try
            {
                StringBuilder xmlStr = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                xmlStr.Append("<root>\r\n");
                xmlStr.Append("\t<row name=\"" + System.Net.WebUtility.HtmlEncode("- Danh mục -") + "\">\r\n");
                xmlStr.Append("\t\t<col name=\"detail\">\r\n");
                xmlStr.Append("<![CDATA[]]>\r\n");
                xmlStr.Append("\t\t</col>\r\n");
                xmlStr.Append("\t\t<col name=\"spec\">\r\n");
                xmlStr.Append("<![CDATA[]]>\r\n");
                xmlStr.Append("\t\t</col>\r\n");
                xmlStr.Append("\t\t<col name=\"keyword\">\r\n");
                xmlStr.Append("<![CDATA[]]>\r\n");
                xmlStr.Append("\t\t</col>\r\n");
                xmlStr.Append("\t\t<col name=\"detailCustom\">\r\n");
                xmlStr.Append("<![CDATA[]]>\r\n");
                xmlStr.Append("\t\t</col>\r\n");
                xmlStr.Append("\t\t<col name=\"specCustom\">\r\n");
                xmlStr.Append("<![CDATA[]]>\r\n");
                xmlStr.Append("\t\t</col>\r\n");
                xmlStr.Append("\t\t<col name=\"keywordCustom\">\r\n");
                xmlStr.Append("<![CDATA[]]>\r\n");
                xmlStr.Append("\t\t</col>\r\n");
                xmlStr.Append("\t</row>\r\n");
                xmlStr.Append("\r\n");
                xmlStr.Append("</root>");

                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlStr.ToString());
                xdoc.Save(fileXml);
            }
            catch { }
            finally { }
        }

        /* Hàm lưu template ra tệp Xml */
        private void saveTemplateToXmlFile(string fileXml, bool displayMessage)
        {
            try
            {
                StringBuilder xmlStr = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                xmlStr.Append("<root>\r\n");

                for (int i = 0; i < categoryList.Count; i++)
                {
                    if (!string.IsNullOrEmpty(categoryList[i]))
                    {
                        xmlStr.Append("\t<row name=\"" + System.Net.WebUtility.HtmlEncode(categoryList[i]) + "\">\r\n");
                        xmlStr.Append("\t\t<col name=\"detail\">\r\n");
                        xmlStr.Append("<![CDATA[" + detailList[i] + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"spec\">\r\n");
                        xmlStr.Append("<![CDATA[" + specList[i] + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"keyword\">\r\n");
                        xmlStr.Append("<![CDATA[" + keywordList[i] + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"detailCustom\">\r\n");
                        xmlStr.Append("<![CDATA[" + detailCustomList[i] + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"specCustom\">\r\n");
                        xmlStr.Append("<![CDATA[" + specCustomList[i] + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"keywordCustom\">\r\n");
                        xmlStr.Append("<![CDATA[" + keywordCustomList[i] + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t</row>\r\n");
                        xmlStr.Append("\r\n");
                    }
                }
                xmlStr.Append("</root>");

                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlStr.ToString());
                xdoc.Save(fileXml);

                if (displayMessage == true)
                {
                    displayMessageHidden("Tệp mẫu đã được lưu");
                }
            }
            catch
            {
                if (displayMessage == true)
                {
                    displayMessageHidden("Không thể lưu tệp mẫu");
                }
            }
            finally { }
        }

        /* Hàm nhập template từ tệp Excel */
        private void importTemplateFromExcelFile(string fileXml, string fileExcel)
        {
            try
            {
                closeTemplateXmlFile(false);
                ImportExportExcelParam importExportExcelParam;
                importExportExcelParam.fileXml = fileXml;
                importExportExcelParam.fileExcel = fileExcel;
                bgwImportFromExcel.RunWorkerAsync(importExportExcelParam);
            }
            catch { }
            finally { }
        }

        /* Hàm xuất template ra tệp Excel */
        private void exportTemplateToExcelFile(string fileXml, string fileExcel)
        {
            try
            {
                ImportExportExcelParam importExportExcelParam;
                importExportExcelParam.fileXml = fileXml;
                importExportExcelParam.fileExcel = fileExcel;
                bgwExportToExcel.RunWorkerAsync(importExportExcelParam);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi treeview click chuột xuống */
        private void trvCategory_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (frmLogin.Instance.permission == LoginPermission.admin)
                {
                    if (e.Button != MouseButtons.Right) return;

                    TreeNode nodeHere = trvCategory.GetNodeAt(e.X, e.Y);
                    trvCategory.SelectedNode = nodeHere;

                    if (nodeHere == null) return;

                    cmsCategoryAction.Show(trvCategory, new Point(e.X, e.Y));
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện sau khi treeview được lựa chọn */
        private void trvCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                /* Danh mục cấp 3 */
                if (e.Node.Text.Contains("-- -- ") && e.Node.Text.Contains("-- "))
                {
                    categoryIndex = categoryLevel3List[e.Node.Index - 2].beginIndex;
                }
                /* Danh mục cấp 2 */
                else if (!e.Node.Text.Contains("-- -- ") && e.Node.Text.Contains("-- "))
                {
                    categoryIndex = categoryLevel2List[cbxCategoryLevel2.SelectedIndex].beginIndex;
                }
                /* Danh mục cấp 1 */
                else
                {
                    categoryIndex = categoryLevel1List[cbxCategoryLevel1.SelectedIndex].beginIndex;
                }

                txtProductDetail.Text = detailList[categoryIndex];
                txtProductSpec.Text = specList[categoryIndex];
                txtProductKeyword.Text = keywordList[categoryIndex];
                txtProductDetailCustom.Text = detailCustomList[categoryIndex];
                txtProductSpecCustom.Text = specCustomList[categoryIndex];
                txtProductKeywordCustom.Text = keywordCustomList[categoryIndex];
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi txtProduct thay đổi */
        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RichTextBox txt = (RichTextBox)sender;
                int position = txt.SelectionStart;

                if (txt.Name.Equals(txtProductDetail.Name))
                {
                    detailList[categoryIndex] = txt.Text;
                    txtProductDetail.Text = detailList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductSpec.Name))
                {
                    specList[categoryIndex] = txt.Text;
                    txtProductSpec.Text = specList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductKeyword.Name))
                {
                    keywordList[categoryIndex] = txt.Text;
                    txtProductKeyword.Text = keywordList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductDetailCustom.Name))
                {
                    detailCustomList[categoryIndex] = txt.Text;
                    txtProductDetailCustom.Text = detailCustomList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductSpecCustom.Name))
                {
                    specCustomList[categoryIndex] = txt.Text;
                    txtProductSpecCustom.Text = specCustomList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductKeywordCustom.Name))
                {
                    keywordCustomList[categoryIndex] = txt.Text;
                    txtProductKeywordCustom.Text = keywordCustomList[categoryIndex];
                }

                txt.SelectionStart = position;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi txtProduct được thay đổi */
        private void txtProduct_Validated(object sender, EventArgs e)
        {
            try
            {
                RichTextBox txt = (RichTextBox)sender;
                int position = txt.SelectionStart;

                if (txt.Name.Equals(txtProductDetail.Name))
                {
                    detailList[categoryIndex] = txt.Text;
                    txtProductDetail.Text = detailList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductSpec.Name))
                {
                    specList[categoryIndex] = txt.Text;
                    txtProductSpec.Text = specList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductKeyword.Name))
                {
                    keywordList[categoryIndex] = txt.Text;
                    txtProductKeyword.Text = keywordList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductDetailCustom.Name))
                {
                    detailCustomList[categoryIndex] = txt.Text;
                    txtProductDetailCustom.Text = detailCustomList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductSpecCustom.Name))
                {
                    specCustomList[categoryIndex] = txt.Text;
                    txtProductSpecCustom.Text = specCustomList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductKeywordCustom.Name))
                {
                    keywordCustomList[categoryIndex] = txt.Text;
                    txtProductKeywordCustom.Text = keywordCustomList[categoryIndex];
                }

                txt.SelectionStart = position;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi txtProduct được thay đổi */
        private void txtProduct_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                RichTextBox txt = (RichTextBox)sender;
                int position = txt.SelectionStart;

                if (txt.Name.Equals(txtProductDetail.Name))
                {
                    detailList[categoryIndex] = txt.Text;
                    txtProductDetail.Text = detailList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductSpec.Name))
                {
                    specList[categoryIndex] = txt.Text;
                    txtProductSpec.Text = specList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductKeyword.Name))
                {
                    keywordList[categoryIndex] = txt.Text;
                    txtProductKeyword.Text = keywordList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductDetailCustom.Name))
                {
                    detailCustomList[categoryIndex] = txt.Text;
                    txtProductDetailCustom.Text = detailCustomList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductSpecCustom.Name))
                {
                    specCustomList[categoryIndex] = txt.Text;
                    txtProductSpecCustom.Text = specCustomList[categoryIndex];
                }

                else if (txt.Name.Equals(txtProductKeywordCustom.Name))
                {
                    keywordCustomList[categoryIndex] = txt.Text;
                    txtProductKeywordCustom.Text = keywordCustomList[categoryIndex];
                }

                txt.SelectionStart = position;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi combobox level 1 chọn item */
        private void cbxCategoryLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadCategoryLevel2();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi combobox level 2 chọn item */
        private void cbxCategoryLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadCategoryLevel3();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đồng bộ hoá danh mục được nhấn */
        private void btnSyncCategory_Click(object sender, EventArgs e)
        {
            try
            {
                /* Hiển thị hộp thoại */
                DialogResult dr = MessageBox.Show(new Form() { TopMost = true }, "Tệp mẫu sẽ được tạo mới hoặc lưu lại (nếu mở) sau khi\r\nđồng bộ. Bạn có muốn thực hiện điều này hay không?", "Thông báo",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    string folderPath1 = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    if (categoryList.Count < 1)
                    {
                        folderPath1 = Path.Combine(folderPath1, "BanLinhKien Editor", "templates");
                        Directory.CreateDirectory(folderPath1);
                        fileTemplateXmlPath = folderPath1 + "\\new_templates_from_website.xml";
                        createTemplateEmptyToXmlFile(fileTemplateXmlPath);
                        openTemplateFromXmlFile(fileTemplateXmlPath, false);
                    }

                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    folderPath = Path.Combine(folderPath, "BanLinhKien Editor", "categorys");
                    Directory.CreateDirectory(folderPath);
                    fileCategoryXmlPath = folderPath + "\\categorys.xml";
                    loadCategorySyncFromXmlFile(fileCategoryXmlPath);

                    /* Chạy tiến trình nền */
                    bgwSyncCategory.RunWorkerAsync();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút tải tệp lên Drive được nhấn */
        private void btnUploadTemplateToDrive_Click(object sender, EventArgs e)
        {
            try
            {
                /* Hiển thị hộp thoại */
                DialogResult dr = MessageBox.Show(new Form() { TopMost = true }, "Tệp mẫu phải được lưu trước khi tải lên Drive.\r\nBạn có muốn thực hiện điều này hay không?", "Thông báo",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    categoryLevel1Index = cbxCategoryLevel1.SelectedIndex;
                    categoryLevel2Index = cbxCategoryLevel2.SelectedIndex;
                    categoryLevel3Index = trvCategory.SelectedNode.Index;

                    /* Lưu tệp mẫu */
                    saveTemplateToXmlFile(fileTemplateXmlPath, false);

                    /* Thực thi tiến trình nền tải lên Drive */
                    bgwUploadToDrive.RunWorkerAsync();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lấy tệp mẫu từ Drive được nhấn */
        private void btnGetTemplateFromDrive_Click(object sender, EventArgs e)
        {
            try
            {
                /* Hiển thị hộp thoại */
                DialogResult dr = MessageBox.Show(new Form() { TopMost = true }, "Tệp mẫu sẽ được tải xuống trước khi có thể chỉnh\r\nsửa. Bạn có muốn thực hiện điều này hay không?", "Thông báo",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    saveTemplateToXmlFile(fileTemplateXmlPath, false);
                    closeTemplateXmlFile(false);
                    bgwGetFromDrive.RunWorkerAsync();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút import từ Excel được nhấp chọn */
        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn tệp Excel cẩn mở";
                ofd.Filter = "Excel file|*.xls";

                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    fileTemplateExcelPath = ofd.FileName;
                    importTemplateFromExcelFile(fileTemplateXmlPath, fileTemplateExcelPath);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút export sang Excel được nhấp chọn */
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Lưu tệp mẫu Excel";
                sfd.Filter = "Excel file|*.xls";
                sfd.FileName = "template_" + DateTime.Now.ToString("yyyyMMdd");

                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    categoryLevel1Index = cbxCategoryLevel1.SelectedIndex;
                    categoryLevel2Index = cbxCategoryLevel2.SelectedIndex;
                    categoryLevel3Index = trvCategory.SelectedNode.Index;

                    saveTemplateToXmlFile(fileTemplateXmlPath, false);
                    exportTemplateToExcelFile(fileTemplateXmlPath, sfd.FileName);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút mở Template được nhấp chọn */
        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn tệp mẫu cần mở";
                ofd.Filter = "XML file|*.xml";

                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    saveTemplateToXmlFile(fileTemplateXmlPath, false);
                    fileTemplateXmlPath = ofd.FileName;
                    openTemplateFromXmlFile(fileTemplateXmlPath, true);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đóng tệp mẫu được nhấn */
        private void btnCloseTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                /* Hiển thị hộp thoại */
                DialogResult dr = MessageBox.Show(new Form() { TopMost = true }, "Bạn có muốn lưu tệp mẫu này trước khi đóng hay không?", "Thông báo",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    saveTemplateToXmlFile(fileTemplateXmlPath, false);
                }

                /* Đóng tệp mẫu */
                closeTemplateXmlFile(true);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lưu tệp mẫu được nhấp chọn */
        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                /* Lưu vị trí con trỏ của hai textview */
                int position1 = txtProductDetail.SelectionStart;
                int position2 = txtProductSpec.SelectionStart;
                int position3 = txtProductKeyword.SelectionStart;
                int position4 = txtProductDetailCustom.SelectionStart;
                int position5 = txtProductSpecCustom.SelectionStart;
                int position6 = txtProductKeywordCustom.SelectionStart;

                categoryLevel1Index = cbxCategoryLevel1.SelectedIndex;
                categoryLevel2Index = cbxCategoryLevel2.SelectedIndex;
                categoryLevel3Index = trvCategory.SelectedNode.Index;

                /* Lưu và mở lại tệp template */
                saveTemplateToXmlFile(fileTemplateXmlPath, true);
                openTemplateFromXmlFile(fileTemplateXmlPath, false);

                /* Khôi phục lại vị trí */
                txtProductDetail.SelectionStart = position1;
                txtProductSpec.SelectionStart = position2;
                txtProductKeyword.SelectionStart = position3;
                txtProductDetailCustom.SelectionStart = position4;
                txtProductSpecCustom.SelectionStart = position5;
                txtProductKeywordCustom.SelectionStart = position6;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lưu tệp mẫu như là được nhấp chọn */
        private void btnSaveTemplateAs_Click(object sender, EventArgs e)
        {
            try
            {
                int position1 = txtProductDetail.SelectionStart;
                int position2 = txtProductSpec.SelectionStart;
                int position3 = txtProductKeyword.SelectionStart;
                int position4 = txtProductDetailCustom.SelectionStart;
                int position5 = txtProductSpecCustom.SelectionStart;
                int position6 = txtProductKeywordCustom.SelectionStart;

                categoryLevel1Index = cbxCategoryLevel1.SelectedIndex;
                categoryLevel2Index = cbxCategoryLevel2.SelectedIndex;
                categoryLevel3Index = trvCategory.SelectedNode.Index;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Lưu tệp mẫu như là...";
                sfd.Filter = "XML file|*.xml";
                sfd.FileName = Path.GetFileName(fileTemplateXmlPath);

                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    fileTemplateXmlPath = sfd.FileName;
                    saveTemplateToXmlFile(fileTemplateXmlPath, true);
                    openTemplateFromXmlFile(fileTemplateXmlPath, false);
                }

                txtProductDetail.SelectionStart = position1;
                txtProductSpec.SelectionStart = position2;
                txtProductKeyword.SelectionStart = position3;
                txtProductDetailCustom.SelectionStart = position4;
                txtProductSpecCustom.SelectionStart = position5;
                txtProductKeywordCustom.SelectionStart = position6;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đẩy tính năng thực thi */
        private void btnAppendDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductDetailCustom.Text.Trim()))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                    builder.AppendLine();
                    builder.Append(txtProductDetailCustom.Text);

                    txtProductDetail.AppendText(builder.ToString());
                    txtProductDetailCustom.Clear();
                }
                else
                {
                    displayMessageHidden("Không có gì để đẩy");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đẩy thông số thực thi */
        private void btnApendSpec_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductSpecCustom.Text.Trim()))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                    builder.AppendLine();
                    builder.Append(txtProductSpecCustom.Text);

                    txtProductSpec.AppendText(builder.ToString());
                    txtProductSpecCustom.Clear();
                }
                else
                {
                    displayMessageHidden("Không có gì để đẩy");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đẩy từ khoá thực thi */
        private void btnAppendKeyword_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductKeywordCustom.Text.Trim()))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine();
                    builder.AppendLine();
                    builder.Append(txtProductKeywordCustom.Text);

                    txtProductKeyword.AppendText(builder.ToString());
                    txtProductKeywordCustom.Clear();
                }
                else
                {
                    displayMessageHidden("Không có gì để đẩy");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nhập tệp excel quá trình thay đổi */
        private void bgwImportFromExcel_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    componentAllState(false);
                    tspImportExport.Visible = true;
                    tspImportExport.Value = e.ProgressPercentage;
                    displayMessage("Đang nhập tệp Excel: " + e.ProgressPercentage.ToString() + "%");
                }
                else if (e.ProgressPercentage == 100)
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessageHidden("Tệp Excel đã được nhập dưới dạng tệp mẫu");
                }
                else
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessage("Không thể nhập tệp Excel");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nhập tệp excel chế độ nền */
        private void bgwImportFromExcel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                saveTemplateToXmlFile(fileTemplateXmlPath, false);

                bgwImportFromExcel.ReportProgress(0);

                string _fileXml = ((ImportExportExcelParam)e.Argument).fileXml;
                string _fileExcel = ((ImportExportExcelParam)e.Argument).fileExcel;

                Excel.Application oExcel = null;
                Excel.Workbook oBook = null;
                Excel.Worksheet oSheet = null;
                Excel.Range oRange = null;

                oExcel = new Excel.Application();
                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                oExcel.UserControl = false;

                oBook = oExcel.Workbooks.Open(fileTemplateExcelPath);
                oSheet = oBook.Sheets[1];
                oRange = oSheet.UsedRange;

                int rowCount = oRange.Rows.Count;
                int colCount = oRange.Columns.Count;

                StringBuilder xmlStr = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                xmlStr.Append("<root>\r\n");

                for (int i = 2; i <= rowCount; i++)
                {
                    if ((oRange.Cells[i, 1] != null && oRange.Cells[i, 1].Value2 != null) &&
                        (oRange.Cells[i, 2] != null && oRange.Cells[i, 2].Value2 != null) &&
                        (oRange.Cells[i, 3] != null && oRange.Cells[i, 3].Value2 != null) &&
                        (oRange.Cells[i, 4] != null && oRange.Cells[i, 4].Value2 != null) &&
                        (oRange.Cells[i, 5] != null && oRange.Cells[i, 5].Value2 != null) &&
                        (oRange.Cells[i, 6] != null && oRange.Cells[i, 6].Value2 != null) &&
                        (oRange.Cells[i, 7] != null && oRange.Cells[i, 7].Value2 != null))
                    {
                        xmlStr.Append("\t<row name=\"" + System.Net.WebUtility.HtmlEncode(oRange.Cells[i, 1].Value2.ToString()) + "\">\r\n");
                        xmlStr.Append("\t\t<col name=\"detail\">\r\n");
                        xmlStr.Append("<![CDATA[" + oRange.Cells[i, 2].Value2.ToString() + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"spec\">\r\n");
                        xmlStr.Append("<![CDATA[" + oRange.Cells[i, 3].Value2.ToString() + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"keyword\">\r\n");
                        xmlStr.Append("<![CDATA[" + oRange.Cells[i, 4].Value2.ToString() + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"detailCustom\">\r\n");
                        xmlStr.Append("<![CDATA[" + oRange.Cells[i, 5].Value2.ToString() + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"specCustom\">\r\n");
                        xmlStr.Append("<![CDATA[" + oRange.Cells[i, 6].Value2.ToString() + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t\t<col name=\"keywordCustom\">\r\n");
                        xmlStr.Append("<![CDATA[" + oRange.Cells[i, 7].Value2.ToString() + "]]>\r\n");
                        xmlStr.Append("\t\t</col>\r\n");
                        xmlStr.Append("\t</row>\r\n");
                        xmlStr.Append("\r\n");
                    }

                    double percent = Math.Round((Convert.ToDouble(i - 2) / Convert.ToDouble(rowCount - 2)) * 98);
                    int progress = Convert.ToInt32(percent) + 1;
                    bgwImportFromExcel.ReportProgress(progress);
                }

                bgwImportFromExcel.ReportProgress(99);

                xmlStr.Append("</root>");
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlStr.ToString());
                _fileXml = Path.ChangeExtension(_fileExcel, ".xml");
                xdoc.Save(_fileXml);

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Marshal.ReleaseComObject(oRange);
                Marshal.ReleaseComObject(oSheet);

                oBook.Close();
                Marshal.ReleaseComObject(oBook);

                oExcel.Quit();
                Marshal.ReleaseComObject(oExcel);

                e.Result = _fileXml;

                bgwImportFromExcel.ReportProgress(100);
            }
            catch
            {
                bgwImportFromExcel.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi nhập tệp excel thực thi xong */
        private void bgwImportFromExcel_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                componentAllState(true);
                tspImportExport.Value = 0;
                tspImportExport.Visible = false;

                fileTemplateXmlPath = e.Result.ToString();
                openTemplateFromXmlFile(fileTemplateXmlPath, true);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi xuất tệp excel quá trình thay đổi */
        private void bgwExportToExcel_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    componentAllState(false);
                    tspImportExport.Visible = true;
                    tspImportExport.Value = e.ProgressPercentage;
                    displayMessage("Đang xuất tệp Excel: " + e.ProgressPercentage.ToString() + "%");
                }
                else if (e.ProgressPercentage == 100)
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessageHidden("Tệp Excel đã được xuất");
                }
                else
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessage("Không thể xuất tệp Excel");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi xuất tệp excel chế độ nền */
        private void bgwExportToExcel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                saveTemplateToXmlFile(fileTemplateXmlPath, false);

                bgwExportToExcel.ReportProgress(0);

                string _fileXml = ((ImportExportExcelParam)e.Argument).fileXml;
                string _fileExcel = ((ImportExportExcelParam)e.Argument).fileExcel;

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(_fileXml);
                XmlNodeList node = xdoc.DocumentElement.ChildNodes;

                Excel.Application oExcel = null;
                Excel.Workbook oBook = null;
                Excel.Sheets oSheetsColl = null;
                Excel.Worksheet oSheet = null;
                Excel.Range oRange = null;

                object oMissing = Missing.Value;
                oExcel = new Excel.Application();
                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                oExcel.UserControl = false;

                oBook = oExcel.Workbooks.Add(oMissing);
                oSheetsColl = oExcel.Worksheets;
                oSheet = (Excel.Worksheet)oSheetsColl.get_Item(1);
                oSheet.Name = "Danh mục mẫu";

                oRange = (Excel.Range)oSheet.Cells[1, 1];
                oRange.NumberFormat = "@";
                oRange.Value2 = "Tên danh mục";
                oRange.Font.Color = ColorTranslator.ToOle(Color.White);
                oRange.Interior.Color = ColorTranslator.ToOle(Color.Blue);
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                oRange = (Excel.Range)oSheet.Cells[1, 2];
                oRange.NumberFormat = "@";
                oRange.Value2 = "Tính năng";
                oRange.Font.Color = ColorTranslator.ToOle(Color.White);
                oRange.Interior.Color = ColorTranslator.ToOle(Color.Blue);
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                oRange = (Excel.Range)oSheet.Cells[1, 3];
                oRange.NumberFormat = "@";
                oRange.Value2 = "Thông số kỹ thuật/Bảng thông số";
                oRange.Font.Color = ColorTranslator.ToOle(Color.White);
                oRange.Interior.Color = ColorTranslator.ToOle(Color.Blue);
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                oRange = (Excel.Range)oSheet.Cells[1, 4];
                oRange.NumberFormat = "@";
                oRange.Value2 = "Từ khoá";
                oRange.Font.Color = ColorTranslator.ToOle(Color.White);
                oRange.Interior.Color = ColorTranslator.ToOle(Color.Blue);
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                oRange = (Excel.Range)oSheet.Cells[1, 5];
                oRange.NumberFormat = "@";
                oRange.Value2 = "Tính năng tuỳ chỉnh";
                oRange.Font.Color = ColorTranslator.ToOle(Color.White);
                oRange.Interior.Color = ColorTranslator.ToOle(Color.Blue);
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                oRange = (Excel.Range)oSheet.Cells[1, 6];
                oRange.NumberFormat = "@";
                oRange.Value2 = "Thông số kỹ thuật/Bảng thông số tuỳ chỉnh";
                oRange.Font.Color = ColorTranslator.ToOle(Color.White);
                oRange.Interior.Color = ColorTranslator.ToOle(Color.Blue);
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                oRange = (Excel.Range)oSheet.Cells[1, 7];
                oRange.NumberFormat = "@";
                oRange.Value2 = "Từ khoá tuỳ chỉnh";
                oRange.Font.Color = ColorTranslator.ToOle(Color.White);
                oRange.Interior.Color = ColorTranslator.ToOle(Color.Blue);
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                for (int i = 0; i < node.Count; i++)
                {
                    oRange = (Excel.Range)oSheet.Cells[i + 2, 1];
                    oRange.NumberFormat = "@";
                    oRange.Value2 = node[i].Attributes["name"].InnerText;
                    oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                    oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    oRange = (Excel.Range)oSheet.Cells[i + 2, 2];
                    oRange.NumberFormat = "@";
                    oRange.Value2 = node[i].ChildNodes[0].InnerText;
                    oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                    oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    oRange = (Excel.Range)oSheet.Cells[i + 2, 3];
                    oRange.NumberFormat = "@";
                    oRange.Value2 = node[i].ChildNodes[1].InnerText;
                    oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                    oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    oRange = (Excel.Range)oSheet.Cells[i + 2, 4];
                    oRange.NumberFormat = "@";
                    oRange.Value2 = node[i].ChildNodes[2].InnerText;
                    oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                    oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    oRange = (Excel.Range)oSheet.Cells[i + 2, 5];
                    oRange.NumberFormat = "@";
                    oRange.Value2 = node[i].ChildNodes[3].InnerText;
                    oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                    oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    oRange = (Excel.Range)oSheet.Cells[i + 2, 6];
                    oRange.NumberFormat = "@";
                    oRange.Value2 = node[i].ChildNodes[4].InnerText;
                    oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                    oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    oRange = (Excel.Range)oSheet.Cells[i + 2, 7];
                    oRange.NumberFormat = "@";
                    oRange.Value2 = node[i].ChildNodes[5].InnerText;
                    oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                    oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    oRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    double percent = Math.Round((Convert.ToDouble(i) / Convert.ToDouble(node.Count - 1)) * 98);
                    int progress = Convert.ToInt32(percent) + 1;
                    bgwExportToExcel.ReportProgress(progress);
                }

                bgwExportToExcel.ReportProgress(99);

                oSheet.Rows.AutoFit();
                oSheet.Columns.AutoFit();

                oBook.SaveAs(_fileExcel, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Marshal.ReleaseComObject(oRange);
                Marshal.ReleaseComObject(oSheet);

                oBook.Close();
                Marshal.ReleaseComObject(oBook);

                oExcel.Quit();
                Marshal.ReleaseComObject(oExcel);

                bgwExportToExcel.ReportProgress(100);
            }
            catch
            {
                bgwExportToExcel.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi xuất tệp excel thực thi xong */
        private void bgwExportToExcel_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                componentAllState(true);
                tspImportExport.Value = 0;
                tspImportExport.Visible = false;
                openTemplateFromXmlFile(fileTemplateXmlPath, false);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền tải lên Drive thay đổi */
        private void bgwUploadToDrive_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    componentAllState(false);
                    tspImportExport.Visible = true;
                    tspImportExport.Value = e.ProgressPercentage;
                    displayMessage("Đang tải lên tệp mẫu: " + e.ProgressPercentage.ToString() + "%");
                }
                else if (e.ProgressPercentage == 100)
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessageHidden("Tệp mẫu đã được tải lên");
                }
                else
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessageHidden("Không thể tải lên tệp mẫu");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tải lên Drive thực thi */
        private void bgwUploadToDrive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                bgwUploadToDrive.ReportProgress(0);

                /* Lấy Id của tệp */
                string[] text = configData.urlTemplates.Split(new string[] { "/uc?id=" }, StringSplitOptions.RemoveEmptyEntries);
                string fileId = text[1];

                bgwUploadToDrive.ReportProgress(40);

                /* Kiểm tra credential người dùng đăng nhập vào photoss */
                gDrive.getUserCredentialFromFileContent(
                    gDrive.driveParam.credentialsPhotosContent,
                    "credentials_photos.json");
                gDrive.getDriveService();

                bgwUploadToDrive.ReportProgress(60);

                /* Cập nhật tệp template */
                fileId = gDrive.updateFileToDrive(fileId, fileTemplateXmlPath, "application/xml");

                if (!string.IsNullOrEmpty(fileId))
                {
                    bgwUploadToDrive.ReportProgress(80);

                    /* Chia sẻ tệp tin */
                    gDrive.shareableFileFolderDrive(fileId);

                    bgwUploadToDrive.ReportProgress(100);
                }
                else
                {
                    bgwUploadToDrive.ReportProgress(-1);
                }
            }
            catch
            {
                bgwUploadToDrive.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi tiến trình nền tải lên Drive thực thi hoàn thành */
        private void bgwUploadToDrive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                componentAllState(true);
                tspImportExport.Value = 0;
                tspImportExport.Visible = false;
                openTemplateFromXmlFile(fileTemplateXmlPath, false);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình lấy tệp mẫu thay đổi */
        private void bgwGetFromDrive_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    componentAllState(false);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessage("Đang lấy tệp mẫu từ Drive...");
                }
                else if (e.ProgressPercentage == 100)
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessageHidden("Tệp mẫu đã được lấy từ Drive");
                }
                else
                {
                    componentAllState(true);
                    tspImportExport.Value = 0;
                    tspImportExport.Visible = false;
                    displayMessageHidden("Không thể lấy tệp mẫu từ Drive");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình lấy tệp mẫu từ drive thực thi */
        private void bgwGetFromDrive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                /* Lấy tệp mẫu sử dụng Google Drive */
                /************************************/
                bgwGetFromDrive.ReportProgress(0);

                /* Lấy Id của tệp */
                string[] text = configData.urlTemplates.Split(new string[] { "/uc?id=" }, StringSplitOptions.RemoveEmptyEntries);
                string fileId = text[1];

                bgwGetFromDrive.ReportProgress(25);

                /* Kiểm tra credential người dùng đăng nhập vào photoss */
                gDrive.getUserCredentialFromFileContent(
                    gDrive.driveParam.credentialsPhotosContent,
                    "credentials_photos.json");
                gDrive.getDriveService();

                bgwGetFromDrive.ReportProgress(50);

                /* Tệp lưu tạm thời */
                fileTemplateXmlPath = Path.GetTempPath() + "templates.xml";

                bgwGetFromDrive.ReportProgress(75);

                /* Tải tệp từ drive */
                gDrive.downloadFileFromDrive(fileId, fileTemplateXmlPath);

                bgwGetFromDrive.ReportProgress(100);

                /* Lấy tệp mẫu sử dụng Http Web */
                /********************************/
                //bgwGetFromDrive.ReportProgress(0);
                //config.getBlkTemplatesContent(ref configData);
                //XmlDocument xdoc = new XmlDocument();
                //xdoc.LoadXml(configData.templatesContent);
                //bgwGetFromDrive.ReportProgress(50);
                //fileTemplateXmlPath = Path.GetTempPath() + "templates.xml";
                //xdoc.Save(fileTemplateXmlPath);
                //bgwGetFromDrive.ReportProgress(100);
            }
            catch
            {
                bgwUploadToDrive.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi tiến trình lấy tệp mẫu từ drive thực thi xong */
        private void bgwGetFromDrive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                /* Mở tệp mẫu từ Drive */
                componentAllState(true);
                tspImportExport.Value = 0;
                tspImportExport.Visible = false;
                openTemplateFromXmlFile(fileTemplateXmlPath, true);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền đồng bộ thay đổi */
        private void bgwSyncCategory_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    componentAllState(false);
                    displayMessage("Đang đồng bộ danh mục...");

                }
                else if (e.ProgressPercentage == 100)
                {
                    componentAllState(true);
                    displayMessageHidden("Đồng bộ danh mục thành công");
                }
                else
                {
                    componentAllState(true);
                    displayMessageHidden("Không thể đồng bộ danh mục");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền đồng bộ chủ đề hoạt động */
        private void bgwSyncCategory_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                bgwSyncCategory.ReportProgress(0);

                /* Đồng bộ danh mục sản phẩm */
                category.synchronizeCategory(ref categorySyncList, ref categoryList,
                    ref detailList, ref specList, ref keywordList,
                    ref detailCustomList, ref specCustomList, ref keywordCustomList);

                bgwSyncCategory.ReportProgress(100);
            }
            catch
            {
                bgwSyncCategory.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi tiến trình nền đồng bộ hoàn thành */
        private void bgwSyncCategory_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                componentAllState(true);
                tspImportExport.Value = 0;
                tspImportExport.Visible = false;
                saveTemplateToXmlFile(fileTemplateXmlPath, false);
                openTemplateFromXmlFile(fileTemplateXmlPath, false);
                tslPath.Text = fileTemplateXmlPath;
                tslPath.Visible = true;
                displayMessageHidden("Đồng bộ danh mục thành công");
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện menu   */
        /***************************************************************/
        /* Sự kiện khi menu Exit được nhấp chọn */
        private void mniExit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
                bgwExportToExcel.Dispose();
                bgwImportFromExcel.Dispose();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu trang chủ được nhấp chọn */
        private void mniHomepage_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(configData.urlHomepage);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút Hướng dẫn được nhấp chọn */
        private void mniManual_Click(object sender, EventArgs e)
        {
            try
            {
                string helpFileName = "help.chm";

                if (File.Exists(helpFileName))
                {
                    Help.ShowHelp(this, helpFileName);
                }
                else
                {
                    Process.Start(configData.urlHelppage);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu thông tin được nhấp chọn */
        private void mniAbout_Click(object sender, EventArgs e)
        {
            try
            {
                frmAbout.Instance.ShowDialog();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu đăng xuất được click */
        private void mniLogout_Click(object sender, EventArgs e)
        {
            try
            {
                mniLogout.Visible = false;

                closeTemplateXmlFile(false);
                this.Text = "BanLinhKien Templator " + currentVersion;

                displayMessageHidden("Vui lòng đăng nhập");
                componentAllState(false);

                frmLogin.Instance.ShowDialog();

                while (frmLogin.Instance.loginState == LoginState.failure)
                {
                    displayMessageHidden("Vui lòng đăng nhập");
                    frmLogin.Instance.ShowDialog();
                }

                displayMessageHidden("Đăng nhập thành công");

                if (frmLogin.Instance.permission == LoginPermission.admin)
                {
                    this.Text += " - " + frmLogin.Instance.username + " (Quản trị viên)";
                }
                else if (frmLogin.Instance.permission == LoginPermission.user)
                {
                    this.Text += " - " + frmLogin.Instance.username + " (Người dùng thường)";
                }

                componentStateWhenFileClosed();
                mniLogout.Visible = true;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu Xoá chủ đề được nhấp chọn */
        private void mniDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode nodeSelected = trvCategory.SelectedNode;

                /* Nếu là danh mục cấp 3 thì xoá 1 */
                if (nodeSelected.Text.Contains("-- -- ") && nodeSelected.Text.Contains("-- "))
                {
                    try
                    {
                        categoryList.RemoveAt(categoryIndex);
                        detailList.RemoveAt(categoryIndex);
                        specList.RemoveAt(categoryIndex);
                        keywordList.RemoveAt(categoryIndex);
                    }
                    catch { }
                }
                /* Nếu là danh mục cấp 2 thì xoá */
                else if (!nodeSelected.Text.Contains("-- -- ") && nodeSelected.Text.Contains("-- "))
                {
                    CategoryData item2 = categoryLevel2List[cbxCategoryLevel2.SelectedIndex];
                    for (int i = item2.beginIndex; i <= item2.endIndex; i++)
                    {
                        try
                        {
                            categoryList.RemoveAt(item2.beginIndex);
                            detailList.RemoveAt(item2.beginIndex);
                            specList.RemoveAt(item2.beginIndex);
                            keywordList.RemoveAt(item2.beginIndex);
                        }
                        catch { }
                    }
                }
                /* Nếu là danh mục cấp 1 thì xoá */
                else
                {
                    CategoryData item1 = categoryLevel1List[cbxCategoryLevel1.SelectedIndex];
                    for (int i = item1.beginIndex; i <= item1.endIndex; i++)
                    {
                        try
                        {
                            categoryList.RemoveAt(item1.beginIndex);
                            detailList.RemoveAt(item1.beginIndex);
                            specList.RemoveAt(item1.beginIndex);
                            keywordList.RemoveAt(item1.beginIndex);
                        }
                        catch { }
                    }
                }

                categoryLevel1Index = cbxCategoryLevel1.SelectedIndex;
                categoryLevel2Index = cbxCategoryLevel2.SelectedIndex;
                categoryLevel3Index = trvCategory.SelectedNode.Index;
                saveTemplateToXmlFile(fileTemplateXmlPath, false);
                openTemplateFromXmlFile(fileTemplateXmlPath, false);

            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện khác   */
        /***************************************************************/

        /* Hàm load danh mục cấp 1 vào combobox */
        private void loadCategoryLevel1()
        {
            try
            {
                /* Tải danh mục vào combobox level 1 */
                categoryLevel1List = new List<CategoryData>();
                categoryLevel2List = new List<CategoryData>();
                categoryLevel3List = new List<CategoryData>();

                TreeNode nodeSelected = new TreeNode();

                superCategoryList = category.getSuperCategoryFromList(categoryList);
                foreach (CategoryData item1 in superCategoryList)
                {
                    if (item1.level.Equals(1))
                    {
                        categoryLevel1List.Add(item1);
                    }
                }

                cbxCategoryLevel1.DataSource = null;
                cbxCategoryLevel2.DataSource = null;
                cbxCategoryLevel1.Enabled = false;
                cbxCategoryLevel2.Enabled = false;

                if (categoryLevel1List.Count > 0)
                {
                    cbxCategoryLevel1.Enabled = true;
                    cbxCategoryLevel1.DataSource = categoryLevel1List.Count > 0 ? categoryLevel1List.ConvertAll(x => x.name.ToString()).ToList() : null;

                    try
                    {
                        cbxCategoryLevel1.SelectedIndex = categoryLevel1Index;
                    }
                    catch
                    {
                        try
                        {
                            cbxCategoryLevel1.SelectedIndex = 0;
                        }
                        catch
                        {
                            cbxCategoryLevel1.DataSource = null;
                            cbxCategoryLevel2.DataSource = null;
                            cbxCategoryLevel1.Enabled = false;
                            cbxCategoryLevel2.Enabled = false;
                        }
                    }
                }

                try
                {
                    nodeSelected = trvCategory.Nodes[categoryLevel3Index];
                    trvCategory.SelectedNode = nodeSelected;
                }
                catch
                {
                    try
                    {
                        nodeSelected = trvCategory.Nodes[trvCategory.Nodes.Count - 1];
                        trvCategory.SelectedNode = nodeSelected;
                    }
                    catch
                    {
                        nodeSelected = trvCategory.Nodes[0];
                        trvCategory.SelectedNode = nodeSelected;
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Hàm load danh mục cấp 2 vào combobox */
        private void loadCategoryLevel2()
        {
            try
            {
                trvCategory.Nodes.Clear();
                categoryLevel2List = new List<CategoryData>();
                categoryLevel3List = new List<CategoryData>();

                /* Thêm danh mục cấp 1 vừa chọn vào treeview */
                CategoryData level1Selected = categoryLevel1List[cbxCategoryLevel1.SelectedIndex];
                trvCategory.Nodes.Add(level1Selected.name);
                TreeNode nodeSelected = trvCategory.Nodes[0];
                trvCategory.SelectedNode = nodeSelected;

                /* Lọc danh mục cấp 2 của cấp 1*/
                for (int i = level1Selected.beginIndex; i <= level1Selected.endIndex; i++)
                {
                    if (superCategoryList[i].level.Equals(2))
                    {
                        CategoryData item2 = superCategoryList[i];
                        categoryLevel2List.Add(item2);
                    }
                }

                /* Thêm danh sách cấp 2 vào combobox */
                cbxCategoryLevel2.DataSource = null;
                cbxCategoryLevel2.Enabled = false;

                if (categoryLevel2List.Count > 0)
                {
                    cbxCategoryLevel2.Enabled = true;
                    cbxCategoryLevel2.DataSource = categoryLevel2List.ConvertAll(x => x.name.ToString()).ToList();

                    try
                    {
                        cbxCategoryLevel2.SelectedIndex = categoryLevel2Index;
                    }
                    catch
                    {
                        try
                        {
                            cbxCategoryLevel2.SelectedIndex = 0;
                        }
                        catch
                        {
                            cbxCategoryLevel2.DataSource = null;
                            cbxCategoryLevel2.Enabled = false;
                        }
                    }
                }

                try
                {
                    nodeSelected = trvCategory.Nodes[categoryLevel3Index];
                    trvCategory.SelectedNode = nodeSelected;
                }
                catch
                {
                    try
                    {
                        nodeSelected = trvCategory.Nodes[trvCategory.Nodes.Count - 1];
                        trvCategory.SelectedNode = nodeSelected;
                    }
                    catch
                    {
                        nodeSelected = trvCategory.Nodes[0];
                        trvCategory.SelectedNode = nodeSelected;
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Hàm load danh mục cấp 3 */
        private void loadCategoryLevel3()
        {
            try
            {
                trvCategory.Nodes.Clear();
                categoryLevel3List = new List<CategoryData>();

                /* Thêm danh mục cấp 1 vừa chọn vào treeview */
                CategoryData level1Selected = categoryLevel1List[cbxCategoryLevel1.SelectedIndex];
                trvCategory.Nodes.Add(level1Selected.name);
                TreeNode nodeSelected = trvCategory.Nodes[0];
                trvCategory.SelectedNode = nodeSelected;

                /* Thêm danh mục cấp 2 vừa chọn vào treeview */
                CategoryData level2Selected = categoryLevel2List[cbxCategoryLevel2.SelectedIndex];
                trvCategory.Nodes.Add(level2Selected.name);
                nodeSelected = trvCategory.Nodes[1];
                trvCategory.SelectedNode = nodeSelected;

                /* Lọc danh sách cấp 3 của cấp 2 */
                for (int i = level2Selected.beginIndex; i <= level2Selected.endIndex; i++)
                {
                    if (superCategoryList[i].level.Equals(3))
                    {
                        CategoryData item3 = superCategoryList[i];
                        categoryLevel3List.Add(item3);
                    }
                }

                /* Thêm danh sách cấp 3 vào treeview */
                foreach (CategoryData item in categoryLevel3List)
                {
                    trvCategory.Nodes.Add(item.name);
                }

                try
                {
                    nodeSelected = trvCategory.Nodes[categoryLevel3Index];
                    trvCategory.SelectedNode = nodeSelected;
                }
                catch
                {
                    try
                    {
                        nodeSelected = trvCategory.Nodes[trvCategory.Nodes.Count - 1];
                        trvCategory.SelectedNode = nodeSelected;
                    }
                    catch
                    {
                        nodeSelected = trvCategory.Nodes[0];
                        trvCategory.SelectedNode = nodeSelected;
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Hàm cho phép toàn bộ nút nhấn */
        private void componentAllState(bool state)
        {
            try
            {
                if (state.Equals(true))
                {
                    btnUploadTemplateToDrive.Enabled = true;
                    mniUploadTemplateToDrive.Enabled = btnUploadTemplateToDrive.Enabled;
                    btnGetTemplateFromDrive.Enabled = true;
                    mniGetTemplateFromDrive.Enabled = btnGetTemplateFromDrive.Enabled;
                    btnImportFromExcel.Enabled = true;
                    mniImportFormExcel.Enabled = btnImportFromExcel.Enabled;
                    btnExportToExcel.Enabled = true;
                    mniExportToExcel.Enabled = btnExportToExcel.Enabled;
                    btnOpenTemplate.Enabled = true;
                    mniOpenTemplate.Enabled = btnOpenTemplate.Enabled;
                    btnCloseTemplate.Enabled = true;
                    mniCloseTemplate.Enabled = btnCloseTemplate.Enabled;
                    btnSaveTemplate.Enabled = true;
                    mniSaveTemplate.Enabled = btnSaveTemplate.Enabled;
                    btnSaveTemplateAs.Enabled = true;
                    mniSaveTemplateAs.Enabled = btnSaveTemplateAs.Enabled;
                    btnSyncCategory.Enabled = true;
                    mniSyncCategory.Enabled = btnSyncCategory.Enabled;

                    trvCategory.Enabled = true;
                    txtProductDetail.Enabled = true;
                    txtProductSpec.Enabled = true;
                    txtProductKeyword.Enabled = true;
                    txtProductDetailCustom.Enabled = true;
                    txtProductSpecCustom.Enabled = true;
                    txtProductKeywordCustom.Enabled = true;

                    txtProductDetail.BackColor = SystemColors.Control;
                    txtProductSpec.BackColor = SystemColors.Control;
                    txtProductKeyword.BackColor = SystemColors.Control;
                    txtProductDetailCustom.BackColor = SystemColors.Control;
                    txtProductSpecCustom.BackColor = SystemColors.Control;
                    txtProductKeywordCustom.BackColor = SystemColors.Control;

                    if (frmLogin.Instance.permission == LoginPermission.admin)
                    {
                        txtProductDetail.ReadOnly = false;
                        txtProductSpec.ReadOnly = false;
                        txtProductKeyword.ReadOnly = false;
                        txtProductDetail.BackColor = SystemColors.Window;
                        txtProductSpec.BackColor = SystemColors.Window;
                        txtProductKeyword.BackColor = SystemColors.Window;
                        txtProductDetailCustom.BackColor = SystemColors.Window;
                        txtProductSpecCustom.BackColor = SystemColors.Window;
                        txtProductKeywordCustom.BackColor = SystemColors.Window;

                        if (trvCategory.GetNodeCount(true) > 0 ) btnAppendDetail.Enabled = true;
                        mniAppendDetail.Enabled = btnAppendDetail.Enabled;
                        if (trvCategory.GetNodeCount(true) > 0) btnApendSpec.Enabled = true;
                        mniAppendSpec.Enabled = btnApendSpec.Enabled;
                        if (trvCategory.GetNodeCount(true) > 0) btnAppendKeyword.Enabled = true;
                        mniAppendKeyword.Enabled = btnAppendKeyword.Enabled;
                        if (trvCategory.GetNodeCount(true) > 0) mniDeleteCategory1.Enabled = true;
                        mnuEdit.Visible = true;
                    }
                    else if (frmLogin.Instance.permission == LoginPermission.user)
                    {
                        txtProductDetail.ReadOnly = true;
                        txtProductSpec.ReadOnly = true;
                        txtProductKeyword.ReadOnly = true;
                        txtProductDetail.BackColor = SystemColors.Control;
                        txtProductSpec.BackColor = SystemColors.Control;
                        txtProductKeyword.BackColor = SystemColors.Control;
                        txtProductDetailCustom.BackColor = SystemColors.Control;
                        txtProductSpecCustom.BackColor = SystemColors.Control;
                        txtProductKeywordCustom.BackColor = SystemColors.Control;

                        btnAppendDetail.Enabled = false;
                        mniAppendDetail.Enabled = btnAppendDetail.Enabled;
                        btnApendSpec.Enabled = false;
                        mniAppendSpec.Enabled = btnApendSpec.Enabled;
                        btnAppendKeyword.Enabled = false;
                        mniAppendKeyword.Enabled = btnAppendKeyword.Enabled;
                        mniDeleteCategory1.Enabled = false;
                        mnuEdit.Visible = false;
                    }
                }
                else
                {
                    btnUploadTemplateToDrive.Enabled = false;
                    mniUploadTemplateToDrive.Enabled = btnUploadTemplateToDrive.Enabled;
                    btnGetTemplateFromDrive.Enabled = false;
                    mniGetTemplateFromDrive.Enabled = btnGetTemplateFromDrive.Enabled;
                    btnImportFromExcel.Enabled = false;
                    mniImportFormExcel.Enabled = btnImportFromExcel.Enabled;
                    btnExportToExcel.Enabled = false;
                    mniExportToExcel.Enabled = btnExportToExcel.Enabled;
                    btnOpenTemplate.Enabled = false;
                    mniOpenTemplate.Enabled = btnOpenTemplate.Enabled;
                    btnCloseTemplate.Enabled = false;
                    mniCloseTemplate.Enabled = btnCloseTemplate.Enabled;
                    btnSaveTemplate.Enabled = false;
                    mniSaveTemplate.Enabled = btnSaveTemplate.Enabled;
                    btnSaveTemplateAs.Enabled = false;
                    mniSaveTemplateAs.Enabled = btnSaveTemplateAs.Enabled;
                    btnSyncCategory.Enabled = false;
                    mniSyncCategory.Enabled = btnSyncCategory.Enabled;

                    trvCategory.Enabled = false;
                    cbxCategoryLevel1.Enabled = false;
                    cbxCategoryLevel2.Enabled = false;

                    txtProductDetail.Enabled = false;
                    txtProductDetailCustom.Enabled = false;
                    txtProductSpec.Enabled = false;
                    txtProductSpecCustom.Enabled = false;
                    txtProductKeyword.Enabled = false;
                    txtProductKeywordCustom.Enabled = false;

                    btnAppendDetail.Enabled = false;
                    mniAppendDetail.Enabled = btnAppendDetail.Enabled;
                    btnApendSpec.Enabled = false;
                    mniAppendSpec.Enabled = btnApendSpec.Enabled;
                    btnAppendKeyword.Enabled = false;
                    mniAppendKeyword.Enabled = btnAppendKeyword.Enabled;
                    mniDeleteCategory1.Enabled = false;
                }
            }
            catch { }
            finally { }
        }

        /* Hàm trạng thái các nút nút dành cho khi tệp chưa mở */
        private void componentStateWhenFileClosed()
        {
            try
            {
                btnSyncCategory.Enabled = true;
                mniSyncCategory.Enabled = btnSyncCategory.Enabled;
                btnUploadTemplateToDrive.Enabled = false;
                mniUploadTemplateToDrive.Enabled = btnUploadTemplateToDrive.Enabled;
                btnGetTemplateFromDrive.Enabled = true;
                mniGetTemplateFromDrive.Enabled = btnGetTemplateFromDrive.Enabled;
                btnImportFromExcel.Enabled = true;
                mniImportFormExcel.Enabled = btnImportFromExcel.Enabled;
                btnExportToExcel.Enabled = false;
                mniExportToExcel.Enabled = btnExportToExcel.Enabled;
                btnOpenTemplate.Enabled = true;
                mniOpenTemplate.Enabled = btnOpenTemplate.Enabled;
                btnCloseTemplate.Enabled = false;
                mniCloseTemplate.Enabled = btnCloseTemplate.Enabled;
                btnSaveTemplate.Enabled = false;
                mniSaveTemplate.Enabled = btnSaveTemplate.Enabled;
                btnSaveTemplateAs.Enabled = false;
                mniSaveTemplateAs.Enabled = btnSaveTemplateAs.Enabled;

                trvCategory.Enabled = false;
                cbxCategoryLevel1.Enabled = false;
                cbxCategoryLevel2.Enabled = false;

                txtProductDetail.Enabled = false;
                txtProductDetailCustom.Enabled = false;
                txtProductSpec.Enabled = false;
                txtProductSpecCustom.Enabled = false;
                txtProductKeyword.Enabled = false;
                txtProductKeywordCustom.Enabled = false;

                btnAppendDetail.Enabled = false;
                mniAppendDetail.Enabled = btnAppendDetail.Enabled;
                btnApendSpec.Enabled = false;
                mniAppendSpec.Enabled = btnApendSpec.Enabled;
                btnAppendKeyword.Enabled = false;
                mniAppendKeyword.Enabled = btnAppendKeyword.Enabled;
                mniDeleteCategory1.Enabled = false;

                if (frmLogin.Instance.permission == LoginPermission.admin)
                {
                    btnExportToExcel.Visible = true;
                    mniExportToExcel.Visible = btnExportToExcel.Visible;
                    btnImportFromExcel.Visible = true;
                    mniImportFormExcel.Visible = btnImportFromExcel.Visible;
                    mniFileSep3.Visible = true;
                    mnuEdit.Visible = true;

                    txtProductDetail.ReadOnly = false;
                    txtProductSpec.ReadOnly = false;
                    txtProductKeyword.ReadOnly = false;
                    txtProductDetail.BackColor = SystemColors.Window;
                    txtProductSpec.BackColor = SystemColors.Window;
                    txtProductKeyword.BackColor = SystemColors.Window;
                    txtProductDetailCustom.BackColor = SystemColors.Window;
                    txtProductSpecCustom.BackColor = SystemColors.Window;
                    txtProductKeywordCustom.BackColor = SystemColors.Window;
                }
                else if (frmLogin.Instance.permission == LoginPermission.user)
                {
                    btnExportToExcel.Visible = false;
                    mniExportToExcel.Visible = btnExportToExcel.Visible;
                    btnImportFromExcel.Visible = false;
                    mniImportFormExcel.Visible = btnImportFromExcel.Visible;
                    mniFileSep3.Visible = false;
                    mnuEdit.Visible = false;

                    txtProductDetail.ReadOnly = true;
                    txtProductSpec.ReadOnly = true;
                    txtProductKeyword.ReadOnly = true;
                    txtProductDetail.BackColor = SystemColors.Control;
                    txtProductSpec.BackColor = SystemColors.Control;
                    txtProductKeyword.BackColor = SystemColors.Control;
                    txtProductDetailCustom.BackColor = SystemColors.Control;
                    txtProductSpecCustom.BackColor = SystemColors.Control;
                    txtProductKeywordCustom.BackColor = SystemColors.Control;
                }
            }
            catch { }
            finally { }
        }

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
