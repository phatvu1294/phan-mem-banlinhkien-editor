using Gecko;
using Gecko.DOM;
using Gecko.Events;
using phatvu1294;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using File = System.IO.File;

namespace blkEditor
{
    public partial class frmMain : Form
    {
        /***************************************************************/
        /*    Các thành phần toàn cục   */
        /***************************************************************/
        /* Kiểu dữ liệu tự tạo */
        public enum GeckoWebName
        {
            geckoPreview,
            geckoNhanhVNWeb,
            geckoTVKhoWeb,
        }

        /* Cấu trúc các tham số update hình ảnh */
        public struct UpdateImageAfterParam
        {
            public string bitmapAfterTitle;
            public Bitmap bitmapBefore;
            public int bitmapBeforeClientWidth;
            public int bitmapBeforeClientHeight;
            public bool rotate;
            public float contrast;
            public int style;
            public int xZero;
            public int yZero;
            public double scale;
        }

        /* Cấu trúc trả về update hình ảnh */
        public struct UpdateImageAfterReturn
        {
            public Bitmap bitmapBefore;
            public Bitmap bitmapAfter;
        }

        /* Cấu trúc tham số gửi lên drive */
        public struct UploadToDriveParam
        {
            public string bitmapTitle;
            public Bitmap bitmapAfter;
        }

        /* Cấu trúc tham số lấy dữ liệu html */
        public struct GetDataHtmlParam
        {
            public string htmlData;
            public bool addToListImage;
            public bool getImageProfile;
            public HtmlEditorData htmlEditorData;
        }

        /* Biến class phatvu1294 */
        private Configuration config = new Configuration();
        private Theme theme = new Theme();
        private Utilities utils = new Utilities();
        private GoogleDrive gDrive = new GoogleDrive();
        private HtmlEditor hEditor = new HtmlEditor();
        private ProductWeb pWeb = new ProductWeb();
        private Category category = new Category();

        /* Biến của ứng dụng */
        private string currentVersion = string.Empty;

        /* Biến configuration */
        private ConfigParam configData = new ConfigParam();

        /* Biến số từ HTML */
        private HtmlEditorData htmlEditorData = new HtmlEditorData();
        private long wordLength = 0;

        /* Biến xử lý hình ảnh */
        private Bitmap bmpBefore, bmpTemplate, bmpComboOld, bmpCombo;
        private bool rotate = false;
        private float contrast = 1f;
        private int style = 1;
        private long quality = 100L;
        private int xOriginal = 0, yOriginal = 0;
        private int xWork = 0, yWork = 0;
        private double scale = 1f;
        public int imgPickerIndex = 0;
        public ImageList imgPickerList = new ImageList();
        private List<List<Point>> drawPolygons = new List<List<Point>>();
        private List<Point> newPolygon = null;
        private Point newPoint;
        private Point startPointMove, startPointScale;

        /* Biến sản phẩm web */
        private List<string> categoryList = new List<string>();
        private List<CategoryData> superCategoryList = new List<CategoryData>();
        private List<CategoryData> categoryLevel1List = new List<CategoryData>();
        private List<CategoryData> categoryLevel2List = new List<CategoryData>();
        private List<CategoryData> categoryLevel3List = new List<CategoryData>();
        private int loginTVKhoStep = 1;
        private int editByProductNameStep = 0;
        private int addImageByProductNameStep = 0;

        /* Biến zalo web*/
        private List<string> zaloGroupList = new List<string>();

        /* Biến hàm ngẫu nhiên */
        private static Random random = new Random();

        /* Biến tempates */
        private XmlDocument templateDoc = new XmlDocument();

        /* Biến Form */
        private bool firstLoadCategory = true;
        private int hintIndex = -1;

        /***************************************************************/
        /*    Hàm dùng chung   */
        /***************************************************************/
        /* Đọc ngày giờ từ bản project */
        public static DateTime GetLinkerTimestampUtc(Assembly assembly)
        {
            var location = assembly.Location;

            return GetLinkerTimestampUtc(location);
        }

        /* Đọc ngày giờ từ đường dẫn */
        public static DateTime GetLinkerTimestampUtc(string filePath)
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            var bytes = new byte[2048];

            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Read(bytes, 0, bytes.Length);
            }

            var headerPos = BitConverter.ToInt32(bytes, peHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(bytes, headerPos + linkerTimestampOffset);
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return dt.AddSeconds(secondsSince1970);
        }

        /***************************************************************/
        /*    Khởi tạo form chính   */
        /***************************************************************/
        public frmMain()
        {
            try
            {
                currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(2) +
                    "." + GetLinkerTimestampUtc(Assembly.GetExecutingAssembly()).ToString("yyMMdd");

                /* Kiểm tra phiên bản và đọc thông số từ Web */
                config.getBlkEditorConfiguration(ref configData, currentVersion);
                config.getBlkEditorConfigurationContent(ref configData);

                /* Khởi tạo Gecko Web với UserAgent đặt trước */
                Xpcom.Initialize(Application.StartupPath + "\\Firefox");
                GeckoPreferences.User["general.useragent.override"] = configData.userAgent;

                /* Đọc tham số Drive */
                gDrive.getDriveParamFromFileContent(configData.driveParamContent);
                frmGetDrive.Instance.gDrive.getDriveParamFromFileContent(configData.driveParamContent);

                /* Đọc tham số sản phẩm Web */
                pWeb.getWebParamFromFileContent(configData.webParamContent);

                /* Đọc tham số Editor */
                hEditor.getEditorParamFromFileContent(configData.editorParamContent);

                /* Đọc tham số MQTT */
                frmRequest.Instance.mqtt.getMqttParamFromFileContent(configData.mqttParamContent);

                /* Kết nối Mqtt */
                frmRequest.Instance.mqttClient = new MqttClient(frmRequest.Instance.mqtt.mqttParam.mqttBroker);
                frmRequest.Instance.mqttClient.ConnectionClosed += clientConnectionClosed;
                frmRequest.Instance.mqttClient.MqttMsgPublishReceived += clientMqttMsgPublishReceived;
                Task.Run(() => frmRequest.Instance.mqtt.tryReconnectAsync(frmRequest.Instance.mqttClient, frmRequest.Instance.tokenSource.Token));

                /* Khởi tạo các thành phần */
                InitializeComponent();

                /* Đặt giao diện mới cho các thành phần */
                theme.changeComponent(this, typeof(TabPage));
                theme.changeComponent(this, typeof(PictureBox));
                theme.changeComponent(this, typeof(DataGridView));
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện cho form và tab    */
        /***************************************************************/
        /* Hàm khi HTML Editor được lựa chọn */
        private void menuSelected(int index)
        {
            /* Hiện ảnh các menu */
            mnuFileHTMLEditor.Visible = (index != 1 ? true : false);
            mnuFileImageEditor.Visible = (index == 1 ? true : false);
            mnuTool.Visible = true;
            mnuHTMLEditor.Visible = (index == 0 ? true : false);
            mnuImageEditor.Visible = (index == 1 ? true : false);
            mnuProductWeb.Visible = (index == 3 ? true : false);
            mnuHelp.Visible = true;
            mniOpenHTML.Visible = (index == 0 ? true : false);
            mniSaveHTML.Visible = (index == 0 ? true : false);
            mniFileHTMLEditorSep1.Visible = (index == 0 ? true : false);

            /* Chọn tab của tool */
            mniHTMLEditor.Checked = (index == 0 ? true : false);
            mniImageEditor.Checked = (index == 1 ? true : false);
            mniPreview.Checked = (index == 2 ? true : false);
            mniProductWeb.Checked = (index == 3 ? true : false);
            mniNhanhWeb.Checked = (tabProductWeb.SelectedIndex == 0 ? true : false);
            mniTVKhoWeb.Checked = (tabProductWeb.SelectedIndex == 1 ? true : false);

            /* Đặt phím tắt */
            /****************/
            /* menu tệp html editor */
            mniOpenHTML.ShortcutKeys = (index == 0 ? Keys.Control | Keys.O : Keys.None);
            mniSaveHTML.ShortcutKeys = (index == 0 ? Keys.Control | Keys.S : Keys.None);
            mniHTMLEditorExit.ShortcutKeys = (index == 0 ? Keys.Control | Keys.Q : Keys.None);

            /* menu tệp chỉnh sửa ảnh */
            mniOpenListImage.ShortcutKeys = (index == 1 ? Keys.Control | Keys.O : Keys.None);
            mniClearListImage.ShortcutKeys = Keys.None;
            mniSaveImage.ShortcutKeys = (index == 1 ? Keys.Control | Keys.S : Keys.None);
            mniUploadAddLink.ShortcutKeys = (index == 1 ? Keys.Control | Keys.U : Keys.None);
            mniFileImageEditorExit.ShortcutKeys = (index == 1 ? Keys.Control | Keys.Q : Keys.None);

            /* menu công cụ */
            mniHTMLEditor.ShortcutKeys = Keys.Control | Keys.D1;
            mniImageEditor.ShortcutKeys = Keys.Control | Keys.D2;
            mniPreview.ShortcutKeys = Keys.Control | Keys.D3;
            mniProductWeb.ShortcutKeys = Keys.Control | Keys.D4;
            mniBlkTemplator.ShortcutKeys = Keys.Control | Keys.B;

            /* menu html editor */
            mniCopyHTML.ShortcutKeys = (index == 0 ? Keys.Control | Keys.H : Keys.None);
            mniGetData.ShortcutKeys = (index == 0 ? Keys.Control | Keys.G : Keys.None);
            mniClearContent.ShortcutKeys = Keys.None;
            mniCopyProductName.ShortcutKeys = Keys.None;
            mniGenerateKeyword.ShortcutKeys = Keys.None;
            mniClearProductKeyword.ShortcutKeys = Keys.None;
            mniClearProductPicture.ShortcutKeys = Keys.None;
            mniClearProductVideo.ShortcutKeys = Keys.None;
            mniResetCategory.ShortcutKeys = Keys.None;

            /* menu chỉnh sửa ảnh */
            mniOpenListImageDrive.ShortcutKeys = (index == 1 ? Keys.Control | Keys.G : Keys.None);
            mniCaptureRequestList.ShortcutKeys = (index == 1 ? Keys.Control | Keys.L : Keys.None);
            mniClearImage.ShortcutKeys = (index == 1 ? Keys.Control | Keys.X : Keys.None);
            mniRotateImage.ShortcutKeys = (index == 1 ? Keys.Control | Keys.R : Keys.None);
            mniDecImageContrast.ShortcutKeys = (index == 1 ? Keys.Control | Keys.D8 : Keys.None);
            mniIncImageContrast.ShortcutKeys = (index == 1 ? Keys.Control | Keys.D9 : Keys.None);
            mniClearImageCrop.ShortcutKeys = Keys.None;
            mniDecImageScale.ShortcutKeys = (index == 1 ? Keys.Control | Keys.OemMinus : Keys.None);
            mniIncImageScale.ShortcutKeys = (index == 1 ? Keys.Control | Keys.Oemplus : Keys.None);
            mniMoveImageLeft.ShortcutKeys = (index == 1 ? Keys.Control | Keys.Left : Keys.None);
            mniMoveImageRight.ShortcutKeys = (index == 1 ? Keys.Control | Keys.Right : Keys.None);
            mniMoveImageUp.ShortcutKeys = (index == 1 ? Keys.Control | Keys.Up : Keys.None);
            mniMoveImageDown.ShortcutKeys = (index == 1 ? Keys.Control | Keys.Down : Keys.None);
            mniImageOrginalScale.ShortcutKeys = (index == 1 ? Keys.Control | Keys.D0 : Keys.None);
            mniAddImageCombo.ShortcutKeys = (index == 1 ? Keys.Control | Keys.Space : Keys.None);
            mniDecImageScale.ShortcutKeyDisplayString = (index == 1 ? "Ctrl+-" : "");
            mniIncImageScale.ShortcutKeyDisplayString = (index == 1 ? "Ctrl++" : "");

            /* menu sản phẩm web */
            mniSaveDataWeb.ShortcutKeys = (index == 3 ? Keys.Control | Keys.D : Keys.None);
            mniUpdateDataWeb.ShortcutKeys = (index == 3 ? Keys.Control | Keys.U : Keys.None);
            mniReloadNhanhVNPage.ShortcutKeys = Keys.None;
            mniProductList.ShortcutKeys = Keys.None;
            mniAddProduct.ShortcutKeys = Keys.None;
            mniEditByProductName.ShortcutKeys = Keys.None;
            mniFilterByProductName.ShortcutKeys = Keys.None;
            mniAddImageByProductName.ShortcutKeys = Keys.None;
            mniGetProductCode.ShortcutKeys = Keys.None;
            mniNhanhWeb.ShortcutKeys = Keys.None;
            mniTVKhoWeb.ShortcutKeys = Keys.None;

            /* menu trợ giúp */
            mniHomepage.ShortcutKeys = Keys.None;
            mniManual.ShortcutKeys = Keys.F1;
            mniAbout.ShortcutKeys = Keys.Control | Keys.F1;
        }

        /* Sự kiện khi form được load */
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                /* Xử lý của form */
                this.Text += " " + currentVersion;
                firstLoadCategory = true;
                displayMessageHidden("Sẵn sàng");

                /* Đọc thông số từ Settings */
                txtProductName.Text = Properties.Settings.Default.txtProductName;
                txtProductDetail.Text = Properties.Settings.Default.txtProductDetail;
                txtProductSpec.Text = Properties.Settings.Default.txtProductSpec;
                hEditor.getProductSpecTableHtmlFromSettings(Properties.Settings.Default.dgvProductSpecTable, ref htmlEditorData);
                txtProductNote.Text = Properties.Settings.Default.txtProductNote;
                rbtGuaranteeNone.Checked = Properties.Settings.Default.rbtGuaranteeNone;
                rbtGuarantee6Month.Checked = Properties.Settings.Default.rbtGuarantee6Month;
                rbtGuarantee12Month.Checked = Properties.Settings.Default.rbtGuarantee12Month;
                rbtGuaranteeSupport.Checked = Properties.Settings.Default.rbtGuaranteeSupport;
                chkGuaranteeDiamond.Checked = Properties.Settings.Default.chkGuaranteeDiamond;
                txtProductKeyword.Text = Properties.Settings.Default.txtProductKeyword;
                txtProductPicture.Text = Properties.Settings.Default.txtProductPicture;
                txtProductVideo.Text = Properties.Settings.Default.txtProductVideo;
                chkUserName.Checked = Properties.Settings.Default.chkUserName;
                txtUserName.Text = Properties.Settings.Default.txtUserName;
                chkUserDate.Checked = Properties.Settings.Default.chkUserDate;
                txtProductRelate.Text = Properties.Settings.Default.txtProductRelate;
                txtProductDownload.Text = Properties.Settings.Default.txtProductDownload;
                rbtAddHtmlToDescription.Checked = Properties.Settings.Default.rbtAddHtmlToDescription;
                rbtAddHtmlToContent.Checked = Properties.Settings.Default.rbtAddHtmlToContent;
                style = Properties.Settings.Default.style;

                /* Chọn tab mặc định */
                tabMain.SelectedIndex = 0;
                tabProductWeb.SelectedIndex = 0;

                /* Xử lý html mặc định */
                txtProductName.Enabled = true;
                btnCopyProductName.Enabled = true;
                btnResetCategory.Enabled = false;
                cbxCategoryLevel3.DataSource = null;
                cbxCategoryLevel3.Text = null;
                cbxCategoryLevel2.DataSource = null;
                cbxCategoryLevel2.Text = null;
                cbxCategoryLevel1.DataSource = null;
                cbxCategoryLevel1.Text = null;
                cbxCategoryLevel3.Enabled = false;
                cbxCategoryLevel2.Enabled = false;
                cbxCategoryLevel1.Enabled = false;
                txtProductDetail.Enabled = true;
                txtProductSpec.Enabled = true;
                dgvProductSpecTable.Enabled = true;
                txtProductNote.Enabled = true;
                txtProductDownload.Enabled = true;
                txtProductRelate.Enabled = true;
                if (rbtGuarantee6Month.Checked || rbtGuarantee12Month.Checked)
                {
                    chkGuaranteeDiamond.Enabled = true;
                }
                else
                {
                    chkGuaranteeDiamond.Checked = false;
                    chkGuaranteeDiamond.Enabled = false;
                }
                txtProductKeyword.Enabled = true;
                btnGenerateKeyword.Enabled = true;
                btnClearProductKeyword.Enabled = true;
                txtProductPicture.Enabled = true;
                btnClearProductPicture.Enabled = true;
                txtProductVideo.Enabled = true;
                btnClearProductVideo.Enabled = true;
                if (chkUserName.Checked)
                {
                    txtUserName.Enabled = true;
                }
                else
                {
                    txtUserName.Enabled = false;
                }
                gwbNhanhVNWeb.Enabled = true;
                gwbNhanhVNWeb.NoDefaultContextMenu = true;
                gwbTVKhoWeb.Enabled = true;
                gwbTVKhoWeb.NoDefaultContextMenu = true;
                mniResetCategory.Enabled = btnResetCategory.Enabled;

                /* Xử lý ảnh mặc định */
                btnOpenListImage.Enabled = true;
                btnOpenListImageDrive.Enabled = true;
                btnClearListImage.Enabled = false;
                btnCaptureRequestList.Enabled = true;
                btnClearImage.Enabled = false;
                btnRotateImage.Enabled = false;
                btnDecImageContrast.Enabled = false;
                btnIncImageContrast.Enabled = false;
                btnClearImageCrop.Enabled = false;
                btnDecImageScale.Enabled = false;
                btnIncImageScale.Enabled = false;
                btnMoveImageLeft.Enabled = false;
                btnMoveImageRight.Enabled = false;
                btnMoveImageUp.Enabled = false;
                btnMoveImageDown.Enabled = false;
                btnImageOriginalScale.Enabled = false;
                btnAddImageCombo.Enabled = false;
                chkAddFrameTitle.Enabled = false;
                btnSaveImage.Enabled = false;
                btnUploadAddLink.Enabled = false;
                if (style == 1)
                {
                    chkAddFrameTitle.Checked = true;
                    mniAddFrameTitle.Checked = true;
                }
                else if (style == 0)
                {
                    chkAddFrameTitle.Checked = false;
                    mniAddFrameTitle.Checked = false;
                }
                contrast = 1f;
                xOriginal = 0;
                yOriginal = 0;
                scale = 1f;
                picImageEditorBefore.Enabled = false;
                picImageEditorAfter.Enabled = false;
                mniOpenListImage.Enabled = btnOpenListImage.Enabled;
                mniOpenListImageDrive.Enabled = btnOpenListImageDrive.Enabled;
                mniClearListImage.Enabled = btnClearListImage.Enabled;
                mniCaptureRequestList.Enabled = btnCaptureRequestList.Enabled;
                mniClearImage.Enabled = btnClearImage.Enabled;
                mniRotateImage.Enabled = btnRotateImage.Enabled;
                mniDecImageContrast.Enabled = btnDecImageContrast.Enabled;
                mniIncImageContrast.Enabled = btnIncImageContrast.Enabled;
                mniClearImageCrop.Enabled = btnClearImageCrop.Enabled;
                mniDecImageScale.Enabled = btnDecImageScale.Enabled;
                mniIncImageScale.Enabled = btnIncImageScale.Enabled;
                mniMoveImageLeft.Enabled = btnMoveImageLeft.Enabled;
                mniMoveImageRight.Enabled = btnMoveImageRight.Enabled;
                mniMoveImageUp.Enabled = btnMoveImageUp.Enabled;
                mniMoveImageDown.Enabled = btnMoveImageDown.Enabled;
                mniImageOrginalScale.Enabled = btnImageOriginalScale.Enabled;
                mniAddImageCombo.Enabled = btnAddImageCombo.Enabled;
                mniAddFrameTitle.Enabled = chkAddFrameTitle.Enabled;
                mniSaveImage.Enabled = btnSaveImage.Enabled;
                mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;

                /* Xem trước */
                gwbPreview.Enabled = true;
                gwbPreview.NoDefaultContextMenu = true;

                /* Sản phẩm Web mặc định */
                btnReloadNhanhVNPage.Enabled = true;
                btnProductList.Enabled = false;
                btnAddProduct.Enabled = false;
                btnEditByProductName.Enabled = false;
                btnFilterByProductName.Enabled = false;
                btnAddImageByProductName.Enabled = false;
                btnUpdateDataWeb.Enabled = false;
                btnSaveDataWeb.Enabled = false;
                btnGetProductCode.Enabled = false;
                txtProductCode.Enabled = false;
                txtProductPrice.Enabled = true;
                rbtAddHtmlToDescription.Enabled = false;
                rbtAddHtmlToContent.Enabled = false;
                btnCancelNavigating.Visible = false;
                mniReloadNhanhVNPage.Enabled = btnReloadNhanhVNPage.Enabled;
                mniProductList.Enabled = btnProductList.Enabled;
                mniAddProduct.Enabled = btnAddProduct.Enabled;
                mniFilterByProductName.Enabled = btnFilterByProductName.Enabled;
                mniAddImageByProductName.Enabled = btnAddImageByProductName.Enabled;
                mniUpdateDataWeb.Enabled = btnUpdateDataWeb.Enabled;
                mniSaveDataWeb.Enabled = btnSaveDataWeb.Enabled;
                mniGetProductCode.Enabled = btnGetProductCode.Enabled;
                gwbNhanhVNWeb.Enabled = true;
                gwbTVKhoWeb.Enabled = true;

                /* Đặt trạng thái timer */
                tmrHideMessage.Enabled = true;
                tmrTVKhoSearch.Enabled = false;
                tmrTVKhoLogin.Enabled = true;
                tmrMonitorUI.Enabled = true;

                /* thanh status bar mặc định */
                tslWordLength.Visible = true;
                btnCancelNavigating.Visible = false;
                tslStatus.Visible = false;

                /* Chọn tab mặc định */
                menuSelected(tabMain.SelectedIndex);

                /* Cập nhật HTML */
                updateComponentToContentHtml();

                /* Cập nhật web browser */
                geckoWebBrowserLoad(GeckoWebName.geckoPreview, hEditor.contentHtml.ToString());
                geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItemAdd);
                geckoWebBrowserLoad(GeckoWebName.geckoTVKhoWeb, pWeb.webParam.urlTVKhoItem);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được hiển thị */
        private void frmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                /* Lấy tệp mẫu sử dụng Google Drive */
                /************************************/
                /* Lấy Id của tệp */
                string[] text = configData.urlTemplates.Split(new string[] { "/uc?id=" }, StringSplitOptions.RemoveEmptyEntries);
                string fileId = text[1];

                /* Kiểm tra credential người dùng đăng nhập vào photoss */
                gDrive.getUserCredentialFromFileContent(
                    gDrive.driveParam.credentialsPhotosContent,
                    "credentials_photos.json");
                gDrive.getDriveService();

                /* Tệp lưu tạm thời */
                string fileXmlTemplates = Path.GetTempPath() + "templates.xml";

                /* Tải tệp từ drive */
                gDrive.downloadFileFromDrive(fileId, fileXmlTemplates);

                /* Load xml */
                templateDoc.Load(fileXmlTemplates);

                /* Lấy tệp mẫu sử dụng Http Web */
                /********************************/
                //config.getBlkTemplatesContent(ref configData);
                //templateDoc.LoadXml(configData.templatesContent);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được đóng */
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Đóng tất cả client */
                frmRequest.Instance.mqttClient.Disconnect();
                frmRequest.Instance.tokenSource.Cancel();
                frmRequest.Instance.tokenSource.Dispose();

                /* Lưu vào Settings */
                Properties.Settings.Default.txtProductName = txtProductName.Text.Trim();
                Properties.Settings.Default.txtProductDetail = txtProductDetail.Text;
                Properties.Settings.Default.txtProductSpec = txtProductSpec.Text;
                Properties.Settings.Default.dgvProductSpecTable = hEditor.productSpecTableHtml.ToString();
                Properties.Settings.Default.txtProductNote = txtProductNote.Text;
                Properties.Settings.Default.rbtGuaranteeNone = rbtGuaranteeNone.Checked;
                Properties.Settings.Default.rbtGuarantee6Month = rbtGuarantee6Month.Checked;
                Properties.Settings.Default.rbtGuarantee12Month = rbtGuarantee12Month.Checked;
                Properties.Settings.Default.rbtGuaranteeSupport = rbtGuaranteeSupport.Checked;
                Properties.Settings.Default.chkGuaranteeDiamond = chkGuaranteeDiamond.Checked;
                Properties.Settings.Default.txtProductKeyword = txtProductKeyword.Text;
                Properties.Settings.Default.txtProductPicture = txtProductPicture.Text;
                Properties.Settings.Default.txtProductVideo = txtProductVideo.Text;
                Properties.Settings.Default.chkUserName = chkUserName.Checked;
                Properties.Settings.Default.txtUserName = txtUserName.Text;
                Properties.Settings.Default.chkUserDate = chkUserDate.Checked;
                Properties.Settings.Default.style = ((chkAddFrameTitle.Checked) ? 1 : 0);
                Properties.Settings.Default.txtProductRelate = txtProductRelate.Text;
                Properties.Settings.Default.txtProductDownload = txtProductDownload.Text;
                Properties.Settings.Default.cbxCategoryLevel1 = cbxCategoryLevel1.SelectedIndex;
                Properties.Settings.Default.cbxCategoryLevel2 = cbxCategoryLevel2.SelectedIndex;
                Properties.Settings.Default.cbxCategoryLevel3 = cbxCategoryLevel3.SelectedIndex;
                Properties.Settings.Default.rbtAddHtmlToDescription = rbtAddHtmlToDescription.Checked;
                Properties.Settings.Default.rbtAddHtmlToContent = rbtAddHtmlToContent.Checked;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện thay đổi kích thước form */
        private void frmMain_Resize(object sender, EventArgs e)
        {
            try
            {
                drawPolygons = new List<List<Point>>();

                if (picImageEditorAfter.Image != null)
                {
                    rotate = false;
                    updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi item của tab được tạo */
        private void tabMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string tabName = tabMain.TabPages[e.Index].Text;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                if (e.Index == tabMain.SelectedIndex)
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                }
                e.Graphics.DrawString(tabName, this.Font, Brushes.Black, tabMain.GetTabRect(e.Index), stringFormat);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tab được thay đổi */
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                menuSelected(tabMain.SelectedIndex);

                /* Hiển thị gợi nhớ và cập nhật */
                if (tabMain.SelectedIndex == 1)
                {
                    if (this.Controls.Contains(ucHintText.Instance))
                    {
                        this.Controls.Remove(ucHintText.Instance);
                    }

                    if (picImageEditorAfter.Image != null)
                    {
                        rotate = false;
                        updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                    }
                }
                else if (tabMain.SelectedIndex == 2)
                {
                    if (this.Controls.Contains(ucHintText.Instance))
                    {
                        this.Controls.Remove(ucHintText.Instance);
                    }

                    updateComponentToContentHtml();
                    geckoWebBrowserLoad(GeckoWebName.geckoPreview, hEditor.contentHtml.ToString());
                }
                else if (tabMain.SelectedIndex == 3)
                {
                    if (this.Controls.Contains(ucHintText.Instance))
                    {
                        this.Controls.Remove(ucHintText.Instance);
                    }

                    updateComponentToContentHtml();
                }

                /* menu tệp html */
                mniOpenHTML.Enabled = btnOpenHTML.Enabled;
                mniSaveHTML.Enabled = btnSaveHTML.Enabled;

                /* menu tệp chỉnh sửa ảnh */
                mniOpenListImage.Enabled = btnOpenListImage.Enabled;
                mniClearListImage.Enabled = btnClearListImage.Enabled;
                mniSaveImage.Enabled = btnSaveImage.Enabled;
                mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;

                /* menu chỉnh sửa html */
                mniCopyHTML.Enabled = btnCopyHTML.Enabled;
                mniGetData.Enabled = btnGetData.Enabled;
                mniClearContent.Enabled = btnClearContent.Enabled;
                mniCopyProductName.Enabled = btnCopyProductName.Enabled;
                mniGenerateKeyword.Enabled = btnGenerateKeyword.Enabled;
                mniClearProductKeyword.Enabled = btnClearProductKeyword.Enabled;
                mniClearProductPicture.Enabled = btnClearProductPicture.Enabled;
                mniClearProductVideo.Enabled = btnClearProductVideo.Enabled;
                mniResetCategory.Enabled = btnResetCategory.Enabled;

                /* menu chỉnh sửa ảnh */
                mniClearImage.Enabled = btnClearImage.Enabled;
                mniRotateImage.Enabled = btnRotateImage.Enabled;
                mniDecImageContrast.Enabled = btnDecImageContrast.Enabled;
                mniIncImageContrast.Enabled = btnIncImageContrast.Enabled;
                mniClearImageCrop.Enabled = btnClearImageCrop.Enabled;
                mniDecImageScale.Enabled = btnDecImageScale.Enabled;
                mniIncImageScale.Enabled = btnIncImageScale.Enabled;
                mniMoveImageLeft.Enabled = btnMoveImageLeft.Enabled;
                mniMoveImageRight.Enabled = btnMoveImageRight.Enabled;
                mniMoveImageUp.Enabled = btnMoveImageUp.Enabled;
                mniMoveImageDown.Enabled = btnMoveImageDown.Enabled;
                mniImageOrginalScale.Enabled = btnImageOriginalScale.Enabled;
                mniAddImageCombo.Enabled = btnAddImageCombo.Enabled;
                mniAddFrameTitle.Enabled = chkAddFrameTitle.Enabled;
                mniSaveImage.Enabled = btnSaveImage.Enabled;
                mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;

                /* menu sản phẩm web */
                mniSaveDataWeb.Enabled = btnSaveDataWeb.Enabled;
                mniUpdateDataWeb.Enabled = btnUpdateDataWeb.Enabled;
                mniReloadNhanhVNPage.Enabled = btnReloadNhanhVNPage.Enabled;
                mniProductList.Enabled = btnProductList.Enabled;
                mniAddProduct.Enabled = btnAddProduct.Enabled;
                mniEditByProductName.Enabled = btnEditByProductName.Enabled;
                mniFilterByProductName.Enabled = btnFilterByProductName.Enabled;
                mniAddImageByProductName.Enabled = btnAddImageByProductName.Enabled;
                mniGetProductCode.Enabled = btnGetProductCode.Enabled;
                mniNhanhWeb.Enabled = gwbNhanhVNWeb.Enabled;
                mniTVKhoWeb.Enabled = gwbTVKhoWeb.Enabled;
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Sự kiện MQTT  */
        /***************************************************************/
        /* Sự kiện khi MQTT mất kết nối */
        private void clientConnectionClosed(object sender, EventArgs e)
        {
            try
            {
                Task.Run(() => frmRequest.Instance.mqtt.tryReconnectAsync(frmRequest.Instance.mqttClient, frmRequest.Instance.tokenSource.Token));
            }
            catch { }
            finally { }
        }

        /* Sự kiện gọi về mỗi khi có dữ liệu từ MQTT */
        private void clientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    /* Xoá dữ liệu */
                    frmRequest.Instance.lbxProductNameRequestList.Items.Clear();
                    frmRequest.Instance.lbxProductNameDoneList.Items.Clear();

                    /* Xử lý dữ liệu nhận được từ MQTT */
                    MqttData mqttData = new MqttData();
                    frmRequest.Instance.mqtt.processDataFromServer(e, ref mqttData);

                    /* Gán ngược dữ liệu về */
                    frmRequest.Instance.requestProductNameList = mqttData.requestProductNameList;
                    frmRequest.Instance.requestProductCodeList = mqttData.requestProductCodeList;
                    frmRequest.Instance.requestProductLocationList = mqttData.requestProductLocationList;
                    frmRequest.Instance.doneProductNameList = mqttData.doneProductNameList;
                    frmRequest.Instance.doneProductCodeList = mqttData.doneProductCodeList;
                    frmRequest.Instance.doneProductLocationList = mqttData.doneProductLocationList;

                    /* Cập nhật lại listbox yêu cầu và hoàn thành */
                    foreach (string item in frmRequest.Instance.requestProductNameList)
                    {
                        frmRequest.Instance.lbxProductNameRequestList.Items.Add(item);
                    }

                    foreach (string item in frmRequest.Instance.doneProductNameList)
                    {
                        frmRequest.Instance.lbxProductNameDoneList.Items.Add(item);
                    }

                    /* Đặt vị trị hiện hành của listbox yêu cầu */
                    if (frmRequest.Instance.lbxProductNameRequestList.Items.Count > 0)
                    {
                        if (frmRequest.Instance.requestProductNameIndex < frmRequest.Instance.lbxProductNameRequestList.Items.Count)
                        {
                            frmRequest.Instance.lbxProductNameRequestList.SelectedIndex = frmRequest.Instance.requestProductNameIndex;
                        }
                        else
                        {
                            frmRequest.Instance.lbxProductNameRequestList.SelectedIndex = frmRequest.Instance.lbxProductNameRequestList.Items.Count - 1;
                        }
                    }

                    /* Đặt vị trị hiện hành của listbox hoàn thành */
                    if (frmRequest.Instance.lbxProductNameDoneList.Items.Count > 0)
                    {
                        if (frmRequest.Instance.doneProductNameIndex < frmRequest.Instance.lbxProductNameDoneList.Items.Count)
                        {
                            frmRequest.Instance.lbxProductNameDoneList.SelectedIndex = frmRequest.Instance.doneProductNameIndex;
                        }
                        else
                        {
                            frmRequest.Instance.lbxProductNameDoneList.SelectedIndex = frmRequest.Instance.lbxProductNameDoneList.Items.Count - 1;
                        }
                    }

                    displayMessageHidden("Danh sách đã được cập nhật");
                });
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện HTML   */
        /***************************************************************/
        /* Hàm lấy dữ liệu từ HTML */
        private void getDataFromHtml()
        {
            try
            {
                GetDataHtmlParam getDataHtmlParam = new GetDataHtmlParam();
                getDataHtmlParam.htmlData = frmGetData.Instance.htmlData;
                getDataHtmlParam.addToListImage = frmGetData.Instance.chkAddToListImage.Checked;
                getDataHtmlParam.getImageProfile = frmGetData.Instance.chkGetImageProfile.Checked;
                getDataHtmlParam.htmlEditorData = htmlEditorData;
                bgwGetDataFromHtml.RunWorkerAsync(getDataHtmlParam);
            }
            catch { }
            finally { }
        }

        /* Hàm cập nhật toàn bộ dữ liệu sang nội dung Html */
        private void updateComponentToContentHtml()
        {
            try
            {
                htmlEditorData.txtProductName = txtProductName;
                htmlEditorData.txtProductDetail = txtProductDetail;
                htmlEditorData.txtProductSpec = txtProductSpec;
                htmlEditorData.dgvProductSpecTable = dgvProductSpecTable;
                htmlEditorData.txtProductNote = txtProductNote;
                htmlEditorData.txtProductDownload = txtProductDownload;
                htmlEditorData.txtProductRelate = txtProductRelate;
                htmlEditorData.rbtGuaranteeNone = rbtGuaranteeNone;
                htmlEditorData.rbtGuaranteeSupport = rbtGuaranteeSupport;
                htmlEditorData.rbtGuarantee6Month = rbtGuarantee6Month;
                htmlEditorData.rbtGuarantee12Month = rbtGuarantee12Month;
                htmlEditorData.chkGuaranteeDiamond = chkGuaranteeDiamond;
                htmlEditorData.txtProductKeyword = txtProductKeyword;
                htmlEditorData.txtProductPicture = txtProductPicture;
                htmlEditorData.txtProductVideo = txtProductVideo;
                htmlEditorData.chkUserName = chkUserName;
                htmlEditorData.txtUserName = txtUserName;
                htmlEditorData.chkUserDate = chkUserDate;
                hEditor.updateProductNameHtml(ref htmlEditorData);
                hEditor.updateProductDetailHtml(ref htmlEditorData);
                hEditor.updateProductSpecHtml(ref htmlEditorData);
                hEditor.updateProductSpecTableHtml(ref htmlEditorData);
                hEditor.updateProductNoteHtml(ref htmlEditorData);
                hEditor.updateProductDownloadHtml(ref htmlEditorData);
                hEditor.updateProductRelateHtml(ref htmlEditorData);
                hEditor.updateProductGuaranteeHtml(ref htmlEditorData);
                hEditor.updateProductKeywordHtml(ref htmlEditorData);
                hEditor.updateProductPictureHtml(ref htmlEditorData);
                hEditor.updateProductVideoHtml(ref htmlEditorData);
                hEditor.updateUserInfo(ref htmlEditorData);
                hEditor.updateContentHtml(ref htmlEditorData);
                rbtGuaranteeNone = htmlEditorData.rbtGuaranteeNone;
                rbtGuaranteeSupport = htmlEditorData.rbtGuaranteeSupport;
                rbtGuarantee6Month = htmlEditorData.rbtGuarantee6Month;
                rbtGuarantee12Month = htmlEditorData.rbtGuarantee12Month;
                chkGuaranteeDiamond = htmlEditorData.chkGuaranteeDiamond;
                chkUserName = htmlEditorData.chkUserName;
                chkUserDate = htmlEditorData.chkUserDate;
                wordLength = hEditor.getWordLengthFromHtml(hEditor.contentHtml.ToString());
                tslWordLength.Text = "Số lượng từ: " + wordLength.ToString();
            }
            catch { }
            finally { }
        }

        /* Hàm cập nhật toàn bộ dữ liệu từ html sang nội dung */
        private void updateContentHtmlToComponent()
        {
            try
            {
                /* Cập nhật HTML */
                txtProductName = htmlEditorData.txtProductName;
                txtProductDetail = htmlEditorData.txtProductDetail;
                txtProductSpec = htmlEditorData.txtProductSpec;
                dgvProductSpecTable = htmlEditorData.dgvProductSpecTable;
                txtProductNote = htmlEditorData.txtProductNote;
                txtProductDownload = htmlEditorData.txtProductDownload;
                txtProductRelate = htmlEditorData.txtProductRelate;
                rbtGuaranteeNone = htmlEditorData.rbtGuaranteeNone;
                rbtGuaranteeSupport = htmlEditorData.rbtGuaranteeSupport;
                rbtGuarantee6Month = htmlEditorData.rbtGuarantee6Month;
                rbtGuarantee12Month = htmlEditorData.rbtGuarantee12Month;
                chkGuaranteeDiamond = htmlEditorData.chkGuaranteeDiamond;
                txtProductKeyword = htmlEditorData.txtProductKeyword;
                txtProductPicture = htmlEditorData.txtProductPicture;
                txtProductVideo = htmlEditorData.txtProductVideo;
                chkUserName = htmlEditorData.chkUserName;
                txtUserName = htmlEditorData.txtUserName;
                chkUserDate = htmlEditorData.chkUserDate;
                hEditor.updateContentHtml(ref htmlEditorData);
            }
            catch { }
            finally { }
        }

        /* Hàm lấy nội dung mẫu XML */
        public void getTemplateByCategoryFromXmlDoc(XmlDocument xdoc)
        {
            try
            {
                string categoryLevel1Name = string.Empty;
                string categoryLevel2Name = string.Empty;
                string categoryLevel3Name = string.Empty;

                if (cbxCategoryLevel1.Items.Count > 0)
                {
                    categoryLevel1Name = cbxCategoryLevel1.Items[cbxCategoryLevel1.SelectedIndex].ToString();
                }

                if (cbxCategoryLevel2.Items.Count > 1)
                {
                    categoryLevel2Name = cbxCategoryLevel2.Items[cbxCategoryLevel2.SelectedIndex].ToString();
                }

                if (cbxCategoryLevel3.Items.Count > 1)
                {
                    categoryLevel3Name = cbxCategoryLevel3.Items[cbxCategoryLevel3.SelectedIndex].ToString();
                }

                int categoryLevel1Index = -1;
                int categoryLevel2Index = -1;
                int categoryLevel3Index = -1;

                XmlNodeList node = xdoc.DocumentElement.ChildNodes;

                if (cbxCategoryLevel1.Items.Count > 0)
                {
                    for (int i = 0; i < node.Count; i++)
                    {
                        if (node[i].Attributes["name"].InnerText.Equals(categoryLevel1Name) && cbxCategoryLevel1.Enabled)
                        {
                            categoryLevel1Index = i;
                            break;
                        }
                    }
                }

                if (cbxCategoryLevel2.Items.Count > 1)
                {
                    for (int j = categoryLevel1Index; j < node.Count; j++)
                    {
                        if (node[j].Attributes["name"].InnerText.Equals(categoryLevel2Name) && cbxCategoryLevel2.Enabled)
                        {
                            categoryLevel2Index = j;
                            break;
                        }
                    }
                }

                if (cbxCategoryLevel3.Items.Count > 1)
                {
                    for (int k = categoryLevel2Index; k < node.Count; k++)
                    {
                        if (node[k].Attributes["name"].InnerText.Equals(categoryLevel3Name))
                        {
                            categoryLevel3Index = k;
                            break;
                        }
                    }
                }

                if (categoryLevel1Index >= 0 && categoryLevel2Index >= 0 && categoryLevel3Index >= 0)
                {
                    hintIndex = categoryLevel3Index;
                }
                else if (categoryLevel1Index >= 0 && categoryLevel2Index >= 0)
                {
                    hintIndex = categoryLevel2Index;
                }
                else if (categoryLevel1Index >= 0)
                {
                    hintIndex = categoryLevel1Index;
                }
                else
                {
                    hintIndex = -1;
                }
            }
            catch { }
            finally { }
        }

        /* Hàm chèn ký tự đầu dòng */
        public void insertFirstBullet(RichTextBox txt, int index)
        {
            try
            {
                var insertText = "- ";
                txt.Text = txt.Text.Insert(index, insertText);
                txt.SelectionStart = index + insertText.Length;
            }
            catch { }
            finally { }
        }

        /* Hàm tải hình ảnh và thêm vào danh sách chỉnh sửa */
        private void downloadAddToListImage(List<string> listLink)
        {
            try
            {
                foreach (string link in listLink)
                {
                    string fileName = utils.convertNameToFileName(txtProductName.Text.Trim(), "jpg");
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    folderPath = Path.Combine(folderPath, "BanLinhKien Editor", "images", DateTime.Now.ToString("yyyy-MM-dd"));
                    Directory.CreateDirectory(folderPath);
                    string filePath = folderPath + "\\" + fileName;

                    MemoryStream ms = pWeb.getMemoryStreamUsingHttpWebClient(link);
                    using (var img = Image.FromStream(ms))
                    {
                        img.Save(filePath, ImageFormat.Jpeg);
                        imgPickerList.ImageSize = new Size(100, 75);
                        lsvListImagePicker.LargeImageList = imgPickerList;

                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            MemoryStream msf = new MemoryStream();
                            fs.CopyTo(msf);
                            msf.Seek(0, SeekOrigin.Begin);
                            imgPickerList.Images.Add(Image.FromStream(msf));
                            lsvListImagePicker.Items.Add(filePath, imgPickerIndex);
                            imgPickerIndex++;
                        }

                        btnClearListImage.Enabled = true;
                        mniClearListImage.Enabled = btnClearListImage.Enabled;
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tên sp được thay đổi */
        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                txtProductCode.Clear();
                frmGetDrive.Instance.filterProductName = txtProductName.Text.Trim();

                /* Lưu vào Settings */
                Properties.Settings.Default.txtProductName = txtProductName.Text.Trim();
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sư kiện khi nút copy tên sản phẩm được nhấn */
        private void btnCopyProductName_Click(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                if (!string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    Clipboard.SetText(txtProductName.Text.Trim());
                    displayMessageHidden("Tên sản phẩm đã được sao chép");
                }
                else
                {
                    displayMessageHidden("Không thể sao chép");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đặt lại danh mục được nhấn */
        private void btnResetCategory_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
                cbxCategoryLevel1.SelectedIndex = 0;
                displayMessageHidden("Danh mục sản phẩm đã được đặt lại");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi chủ đề cấp 1 được thay đổi */
        private void cbxCategoryLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                categoryLevel2List = new List<CategoryData>();
                categoryLevel3List = new List<CategoryData>();

                CategoryData level1Selected = categoryLevel1List[cbxCategoryLevel1.SelectedIndex];

                CategoryData level2First = new CategoryData();
                level2First.name = "- Danh mục 1 -";
                level2First.beginIndex = level1Selected.beginIndex;
                level2First.endIndex = level1Selected.beginIndex;
                level2First.level = 2;
                level2First.parent = level1Selected.beginIndex;
                categoryLevel2List.Add(level2First);

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
                cbxCategoryLevel3.DataSource = null;
                cbxCategoryLevel2.Enabled = true;
                cbxCategoryLevel3.Enabled = false;
                cbxCategoryLevel2.DataSource = categoryLevel2List.ConvertAll(x => x.name.ToString()).ToList();
                btnResetCategory.Enabled = true;
                mniResetCategory.Enabled = btnResetCategory.Enabled;

                /* Thử lựa chọn danh sách mặc định */
                try
                {
                    cbxCategoryLevel2.SelectedIndex = 0;
                }
                /* Lỗi không có phần tử trong combobox */
                catch
                {
                    cbxCategoryLevel2.DataSource = null;
                    cbxCategoryLevel2.Enabled = false;
                }
                finally { }

                getTemplateByCategoryFromXmlDoc(templateDoc);

                /* Lưu vào settings */
                Properties.Settings.Default.cbxCategoryLevel1 = cbxCategoryLevel1.SelectedIndex;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi chủ đề cấp 2 được thay đổi */
        private void cbxCategoryLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                categoryLevel3List = new List<CategoryData>();

                CategoryData level2Selected = categoryLevel2List[cbxCategoryLevel2.SelectedIndex];

                CategoryData level3First = new CategoryData();
                level3First.name = "- Danh mục 2 -";
                level3First.beginIndex = level2Selected.beginIndex;
                level3First.endIndex = level2Selected.beginIndex;
                level3First.level = 3;
                level3First.parent = level2Selected.beginIndex;
                categoryLevel3List.Add(level3First);

                /* Lọc danh sách cấp 3 của cấp 2 */
                for (int i = level2Selected.beginIndex; i <= level2Selected.endIndex; i++)
                {
                    if (superCategoryList[i].level.Equals(3))
                    {
                        CategoryData item3 = superCategoryList[i];
                        categoryLevel3List.Add(item3);
                    }
                }

                /* Thêm danh sách cấp 2 vào combobox */
                cbxCategoryLevel3.DataSource = null;
                cbxCategoryLevel3.Enabled = true;
                cbxCategoryLevel3.DataSource = categoryLevel3List.ConvertAll(x => x.name.ToString()).ToList();
                btnResetCategory.Enabled = true;
                mniResetCategory.Enabled = btnResetCategory.Enabled;

                /* Thử lựa chọn danh sách mặc định */
                try
                {
                    cbxCategoryLevel3.SelectedIndex = 0;
                }
                /* Lỗi không có phần tử trong combobox */
                catch
                {
                    cbxCategoryLevel3.DataSource = null;
                    cbxCategoryLevel3.Enabled = false;
                }
                finally { }

                getTemplateByCategoryFromXmlDoc(templateDoc);

                /* Lưu vào settings */
                Properties.Settings.Default.cbxCategoryLevel2 = cbxCategoryLevel2.SelectedIndex;
                Properties.Settings.Default.Save();

            }
            catch { }
            finally { }
        }

        /* Sự kiện khi chủ đề cấp 3 được thay đổi */
        private void cbxCategoryLevel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getTemplateByCategoryFromXmlDoc(templateDoc);

                /* Lưu vào settings */
                Properties.Settings.Default.cbxCategoryLevel3 = cbxCategoryLevel3.SelectedIndex;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi chi tiết được thay đổi */
        private void txtProductDetail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào Settings */
                Properties.Settings.Default.txtProductDetail = txtProductDetail.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi thông số kỹ thuật được thay đổi */
        private void txtProductSpec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào Settings */
                Properties.Settings.Default.txtProductSpec = txtProductSpec.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi con trỏ chuột hover vào vùng datagirdview */
        private void dgvProductSpecTable_MouseHover(object sender, EventArgs e)
        {
            try
            {
                StringBuilder hintFromXml = new StringBuilder();
                XmlNodeList node = templateDoc.DocumentElement.ChildNodes;

                if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[1].InnerText.Trim()))
                {
                    hintFromXml.AppendLine(node[hintIndex].ChildNodes[1].InnerText.Trim());
                }
                if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[1].InnerText.Trim())
                    && !string.IsNullOrEmpty(node[hintIndex].ChildNodes[4].InnerText.Trim()))
                {
                    hintFromXml.AppendLine("--- ");
                    hintFromXml.AppendLine(node[hintIndex].ChildNodes[4].InnerText.Trim());
                }

                if (!string.IsNullOrEmpty(hintFromXml.ToString()))
                {
                    if (!this.Controls.Contains(ucHintText.Instance))
                    {
                        int lineCount = 0, lineHeight = 0;
                        Font font = new Font("Times New Roman", 12.0F);

                        Image fakeImage = new Bitmap(1, 1);
                        Graphics graphics = Graphics.FromImage(fakeImage);

                        string[] hintLine = Regex.Matches(hintFromXml.ToString(), @"[^\r\n]+").Cast<Match>().Select(m => m.Value).ToArray();

                        string hintText = string.Empty;
                        for (int i = 0; i < hintLine.Count(); i++)
                        {
                            SizeF size = graphics.MeasureString(hintLine[i], font);
                            lineHeight = (int)Math.Ceiling(size.Height);
                            int ratio = (int)Math.Ceiling(size.Width / (double)(ucHintText.Instance.getTemplateWidth));
                            lineCount += ratio;
                            hintText += hintLine[i].Replace("|", string.Empty) + "\r\n";
                        }

                        ucHintText.Instance.Width = txtProductSpec.Width + 75;
                        ucHintText.Instance.Height = lineCount * lineHeight + 6;
                        ucHintText.Instance.Top = txtProductSpec.Parent.Top + txtProductSpec.Parent.Parent.Top + txtProductSpec.Parent.Parent.Parent.Top + txtProductSpec.Top + 43 + 4;
                        ucHintText.Instance.Left = txtProductSpec.Parent.Left + txtProductSpec.Parent.Parent.Left + txtProductSpec.Parent.Parent.Parent.Left + txtProductSpec.Left + txtProductSpec.Width + 3;
                        ucHintText.Instance.Font = font;
                        ucHintText.Instance.setTemplateHint = hintText;
                        this.Controls.Add(ucHintText.Instance);
                    }

                    ucHintText.Instance.BringToFront();
                }
                else
                {
                    if (this.Controls.Contains(ucHintText.Instance))
                    {
                        this.Controls.Remove(ucHintText.Instance);
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi con trỏ chuột leave khỏi vùng datagirdview */
        private void dgvProductSpecTable_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (this.Controls.Contains(ucHintText.Instance))
                {
                    this.Controls.Remove(ucHintText.Instance);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi giá trị cell của table được thay đổi */
        private void dgvProductSpecTable_ValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                updateComponentToContentHtml();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi table được thay đổi */
        private void dgvProductSpecTable_Changed(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi người dùng can thiệp table được thay đổi */
        private void dgvProductSpecTable_UserRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.dgvProductSpecTable = hEditor.productSpecTableHtml.ToString();
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nhấn phím keydown */
        private void dgvProductSpecTable_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                StringBuilder hintText = new StringBuilder();
                XmlNodeList node = templateDoc.DocumentElement.ChildNodes;

                if ((e.Control && e.KeyCode == Keys.T))
                {
                    dgvProductSpecTable.Rows.Clear();

                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[1].InnerText.Trim()))
                    {
                        hintText.AppendLine(node[hintIndex].ChildNodes[1].InnerText.Trim());
                    }
                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[4].InnerText.Trim()))
                    {
                        hintText.AppendLine(node[hintIndex].ChildNodes[4].InnerText.Trim());
                    }

                    string[] lstText = Regex.Matches(hintText.ToString(), @"[^\r\n]+").Cast<Match>().Select(m => m.Value).ToArray();
                    for (int i = 0; i < lstText.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(lstText[i]))
                        {
                            string line = lstText[i];
                            string[] lines = Regex.Matches(line, @"[^|]+").Cast<Match>().Select(m => m.Value).ToArray();
                            string[] texts = Regex.Matches(lines[0], @"[^:]+").Cast<Match>().Select(m => m.Value).ToArray();
                            dgvProductSpecTable.Rows.Add(texts[0], ((texts.Count() >= 2) ? texts[1] : string.Empty));
                        }
                    }

                    updateComponentToContentHtml();

                    /* Lưu vào settings */
                    Properties.Settings.Default.dgvProductSpecTable = hEditor.productSpecTableHtml.ToString();
                    Properties.Settings.Default.Save();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa bảng thông số được nhấp chọn */
        private void btnClearProductSpecTable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductSpecTable.Rows.Count > 1)
                {
                    dgvProductSpecTable.Rows.Clear();
                    displayMessageHidden("Bảng thông số đã được xóa");
                }
                else
                {
                    displayMessageHidden("Không có gì để xóa");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi lưu ý được thay đổi */
        private void txtProductNote_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.txtProductNote = txtProductNote.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi download được thay đổi */
        private void txtProductDownload_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.txtProductDownload = txtProductDownload.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi SP liên quan được thay đổi */
        private void txtProductRelate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.txtProductRelate = txtProductRelate.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi bảo hành được thay đổi */
        private void rbtGuarantee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.rbtGuaranteeNone = rbtGuaranteeNone.Checked;
                Properties.Settings.Default.rbtGuaranteeSupport = rbtGuaranteeSupport.Checked;
                Properties.Settings.Default.rbtGuarantee6Month = rbtGuarantee6Month.Checked;
                Properties.Settings.Default.rbtGuarantee12Month = rbtGuarantee12Month.Checked;
                Properties.Settings.Default.chkGuaranteeDiamond = chkGuaranteeDiamond.Checked;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi từ khóa được thay đổi */
        private void txtProductKeyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.txtProductKeyword = txtProductKeyword.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút thêm từ khóa tự động được nhấn */
        private void btnGenerateKeyword_Click(object sender, EventArgs e)
        {
            try
            {
                string textStr = txtProductName.Text.Trim();
                string[] textArr = Regex.Matches(textStr, @"\w+").Cast<Match>().Select(m => m.Value).ToArray();

                List<string> wordList = new List<string>();
                string line = Environment.NewLine + "- ";

                if (!string.IsNullOrEmpty(textArr[0]))
                {
                    for (int i = 0; i < textArr.Length; i++)
                    {
                        line += textArr[i] + ((i < textArr.Length - 1) ? " " : string.Empty);
                    }

                    txtProductKeyword.AppendText(line);

                    if (!line.Equals(utils.convertToViNoSign(line)))
                    {
                        txtProductKeyword.AppendText(utils.convertToViNoSign(line));
                    }
                }

                if (textArr.Length > 1)
                {
                    line = Environment.NewLine + "- ";

                    for (int i = 0; i < textArr.Length; i++)
                    {
                        wordList.Add(textArr[i].Trim());
                        wordList.Add(utils.convertToViNoSign(textArr[i].Trim()));
                    }

                    wordList = wordList.Distinct().ToList();

                    for (int i = 0; i < wordList.Count; i++)
                    {
                        line += wordList[i] + (i == wordList.Count - 1 ? string.Empty : ", ");
                    }

                    txtProductKeyword.AppendText(line);
                }

                string text = txtProductKeyword.Text;

                for (int i = 0; i < txtProductKeyword.Lines.Count(); i++)
                {
                    if (text[0].Equals('\n')) text = text.Remove(0, 1);
                    if (text[0].Equals('\r') && text[1].Equals('\n')) text = text.Remove(0, 2);
                    if (text[text.Length - 1].Equals('\n')) text = text.Remove(text.Length - 1, 1);
                    if (text[text.Length - 2].Equals('\r') && text[text.Length - 1].Equals('\n')) text = text.Remove(text.Length - 2, 2);
                }

                txtProductKeyword.Text = text;
                updateComponentToContentHtml();

                displayMessageHidden("Đã thêm từ khóa tự động");
            }
            catch
            {
                displayMessageHidden("Không thể thêm từ khóa");
            }
            finally { }
        }

        /* Sự kiện khi nút xóa từ khóa được nhấn */
        private void btnClearProductKeyword_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductKeyword.Text))
                {
                    txtProductKeyword.Clear();
                    displayMessageHidden("Từ khóa sản phẩm đã được xóa");
                }
                else
                {
                    displayMessageHidden("Không có gì để xóa");
                }

                updateComponentToContentHtml();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi hình ảnh được thay đổi */
        private void txtProductPicture_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.txtProductPicture = txtProductPicture.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa link ảnh được nhấn */
        private void btnClearProductPicture_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductPicture.Text))
                {
                    txtProductPicture.Clear();
                    displayMessageHidden("Ảnh sản phẩm đã được xóa");
                }
                else
                {
                    displayMessageHidden("Không có gì để xóa");
                }

                updateComponentToContentHtml();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi video được thay đổi */
        private void txtProductVideo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                updateComponentToContentHtml();

                /* Lưu vào settings */
                Properties.Settings.Default.txtProductVideo = txtProductVideo.Text;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa video sản phẩm được nhấn */
        private void btnClearProductVideo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductVideo.Text))
                {
                    txtProductVideo.Clear();
                    displayMessageHidden("Video sản phẩm đã được xóa");
                }
                else
                {
                    displayMessageHidden("Không có gì để xóa");
                }
                updateComponentToContentHtml();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi thông tin người dùng được thay đổi */
        private void chkUserInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if ((CheckBox)sender == chkUserName)
                {
                    if (chkUserName.Checked == true)
                    {
                        txtUserName.Enabled = true;
                    }
                    else
                    {
                        txtUserName.Enabled = false;
                    }
                }
                updateComponentToContentHtml();

                /* Lưu vào Settings */
                if ((TextBox)sender == txtUserName)
                {
                    Properties.Settings.Default.txtUserName = txtUserName.Text;
                }
                else if ((CheckBox)sender == chkUserName)
                {
                    Properties.Settings.Default.chkUserName = chkUserName.Checked;
                }
                else if ((CheckBox)sender == chkUserDate)
                {
                    Properties.Settings.Default.chkUserDate = chkUserDate.Checked;
                }
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lấy dữ liệu từ Link được nhấp chọn */
        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                frmGetData.Instance.ShowDialog();

                if (frmGetData.Instance.confirmGetLink == true)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(frmGetData.Instance.htmlData))
                        {
                            rbtGuaranteeNone.Checked = true;
                            chkGuaranteeDiamond.Checked = false;
                            txtProductName.Clear();
                            txtProductDetail.Clear();
                            txtProductSpec.Clear();
                            txtProductNote.Clear();
                            txtProductDownload.Clear();
                            txtProductRelate.Clear();
                            txtProductKeyword.Clear();
                            txtProductPicture.Clear();
                            txtProductVideo.Clear();
                            dgvProductSpecTable.Rows.Clear();
                            getDataFromHtml();
                        }
                        else
                        {
                            displayMessageHidden("Không thể lấy được dữ liệu");
                        }
                    }
                    catch
                    {
                        displayMessageHidden("Không thể lấy được dữ liệu");
                    }
                    finally { }
                }
            }

            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa nội dung được nhấp chọn */
        private void btnClearContent_Click(object sender, EventArgs e)
        {
            try
            {
                rbtGuaranteeNone.Checked = true;
                chkGuaranteeDiamond.Checked = false;

                txtProductName.Clear();
                txtProductDetail.Clear();
                txtProductSpec.Clear();
                txtProductNote.Clear();
                txtProductDownload.Clear();
                txtProductRelate.Clear();
                txtProductKeyword.Clear();
                txtProductPicture.Clear();
                txtProductVideo.Clear();
                dgvProductSpecTable.Rows.Clear();

                updateComponentToContentHtml();
                displayMessageHidden("Toàn bộ nội dung đã được xóa");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút mở HTML được nhấp chọn */
        private void btnOpenHTML_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn tệp HTML cần chỉnh sửa";
                ofd.Filter = "HTML file|*.html;*.htm";
                DialogResult dr = ofd.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    rbtGuaranteeNone.Checked = true;
                    chkGuaranteeDiamond.Checked = false;
                    txtProductName.Clear();
                    txtProductDetail.Clear();
                    txtProductSpec.Clear();
                    txtProductNote.Clear();
                    txtProductDownload.Clear();
                    txtProductRelate.Clear();
                    txtProductKeyword.Clear();
                    txtProductPicture.Clear();
                    txtProductVideo.Clear();
                    dgvProductSpecTable.Rows.Clear();
                    hEditor.getDataFromHtml(File.ReadAllText(ofd.FileName, Encoding.UTF8), false, false, ref htmlEditorData);
                    updateContentHtmlToComponent();
                    displayMessageHidden("Tệp HTML đã được mở");
                }
            }
            catch
            {
                displayMessageHidden("Không thể mở tệp HTML");
            }
            finally { }
        }

        /* Sự kiện khi nút lưu HTML được nhấp chọn */
        private void btnSaveHTML_Click(object sender, EventArgs e)
        {
            try
            {
                if (wordLength >= 100)
                {
                    updateComponentToContentHtml();

                    string fileName = utils.convertNameToFileName(txtProductName.Text.Trim(), "html");

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "Lưu tệp HTML đã được chỉnh sửa";
                    sfd.Filter = "HTML file|*.html";
                    sfd.FileName = fileName;
                    DialogResult dr = sfd.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, hEditor.contentHtml.ToString(), Encoding.UTF8);
                        displayMessageHidden("Tệp HTML đã được lưu");
                    }
                }
                else
                {
                    MessageBox.Show("Để lưu tệp HTML thì số lượng từ tối\r\nthiếu phải là 100. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút sao chép HTML sang clipboard được nhấp chọn */
        private void btnCopyHTML_Click(object sender, EventArgs e)
        {
            try
            {
                if (wordLength >= 100)
                {
                    updateComponentToContentHtml();
                    Clipboard.SetText(hEditor.contentHtml.ToString());
                    displayMessageHidden("Mã HTML đã được sao chép");
                }
                else
                {
                    MessageBox.Show("Để sao chép HTML thì số lượng từ tối\r\nthiếu phải là 100. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi textbox nhấn phím tắt */
        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                StringBuilder hintText = new StringBuilder();
                XmlNodeList node = templateDoc.DocumentElement.ChildNodes;
                RichTextBox txt = (RichTextBox)sender;

                if ((e.Control && e.KeyCode == Keys.T))
                {
                    if (txt.Name.Equals(txtProductDetail.Name))
                    {
                        if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[0].InnerText.Trim()))
                        {
                            hintText.AppendLine(node[hintIndex].ChildNodes[0].InnerText.Trim());
                        }
                        if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[0].InnerText.Trim())
                            && !string.IsNullOrEmpty(node[hintIndex].ChildNodes[3].InnerText.Trim()))
                        {
                            hintText.AppendLine("--- ");
                            hintText.AppendLine(node[hintIndex].ChildNodes[3].InnerText.Trim());

                        }
                    }
                    else if (txt.Name.Equals(txtProductSpec.Name))
                    {
                        if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[1].InnerText.Trim()))
                        {
                            hintText.AppendLine(node[hintIndex].ChildNodes[1].InnerText.Trim());
                        }
                        if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[1].InnerText.Trim())
                            && !string.IsNullOrEmpty(node[hintIndex].ChildNodes[4].InnerText.Trim()))
                        {
                            hintText.AppendLine("--- ");
                            hintText.AppendLine(node[hintIndex].ChildNodes[4].InnerText.Trim());

                        }
                    }
                    else if (txt.Name.Equals(txtProductKeyword.Name))
                    {
                        if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[2].InnerText.Trim()))
                        {
                            hintText.AppendLine(node[hintIndex].ChildNodes[2].InnerText.Trim());
                        }
                        if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[2].InnerText.Trim())
                            && !string.IsNullOrEmpty(node[hintIndex].ChildNodes[5].InnerText.Trim()))
                        {
                            hintText.AppendLine("--- ");
                            hintText.AppendLine(node[hintIndex].ChildNodes[5].InnerText.Trim());

                        }
                    }

                    string[] lstText = Regex.Matches(hintText.ToString(), @"[^\r\n]+").Cast<Match>().Select(m => m.Value).ToArray();

                    for (int i = 0; i < lstText.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(lstText[i]))
                        {
                            string line = lstText[i];
                            string[] lines = Regex.Matches(line, @"[^|]+").Cast<Match>().Select(m => m.Value).ToArray();
                            txt.AppendText(lines[0] + ((i < lstText.Count() - 1) ? "\r\n" : string.Empty));
                        }
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi con trỏ chuột hover vào vùng textbox */
        private void txtProduct_MouseHover(object sender, EventArgs e)
        {
            try
            {
                StringBuilder hintFromXml = new StringBuilder();
                XmlNodeList node = templateDoc.DocumentElement.ChildNodes;
                RichTextBox txt = (RichTextBox)sender;

                if (txt.Name.Equals(txtProductDetail.Name))
                {
                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[0].InnerText.Trim()))
                    {
                        hintFromXml.AppendLine(node[hintIndex].ChildNodes[0].InnerText.Trim());
                    }
                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[0].InnerText.Trim())
                        && !string.IsNullOrEmpty(node[hintIndex].ChildNodes[3].InnerText.Trim()))
                    {
                        hintFromXml.AppendLine("--- ");
                        hintFromXml.AppendLine(node[hintIndex].ChildNodes[3].InnerText.Trim());

                    }
                }
                else if (txt.Name.Equals(txtProductSpec.Name))
                {
                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[1].InnerText.Trim()))
                    {
                        hintFromXml.AppendLine(node[hintIndex].ChildNodes[1].InnerText.Trim());
                    }
                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[1].InnerText.Trim())
                        && !string.IsNullOrEmpty(node[hintIndex].ChildNodes[4].InnerText.Trim()))
                    {
                        hintFromXml.AppendLine("--- ");
                        hintFromXml.AppendLine(node[hintIndex].ChildNodes[4].InnerText.Trim());

                    }
                }
                else if (txt.Name.Equals(txtProductKeyword.Name))
                {
                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[2].InnerText.Trim()))
                    {
                        hintFromXml.AppendLine(node[hintIndex].ChildNodes[2].InnerText.Trim());
                    }
                    if (!string.IsNullOrEmpty(node[hintIndex].ChildNodes[2].InnerText.Trim())
                        && !string.IsNullOrEmpty(node[hintIndex].ChildNodes[5].InnerText.Trim()))
                    {
                        hintFromXml.AppendLine("--- ");
                        hintFromXml.AppendLine(node[hintIndex].ChildNodes[5].InnerText.Trim());

                    }
                }

                if (!string.IsNullOrEmpty(hintFromXml.ToString()))
                {
                    if (!this.Controls.Contains(ucHintText.Instance))
                    {
                        int lineCount = 0, lineHeight = 0;
                        Font font = new Font("Times New Roman", 12.0F);

                        Image fakeImage = new Bitmap(1, 1);
                        Graphics graphics = Graphics.FromImage(fakeImage);

                        string[] hintLine = Regex.Matches(hintFromXml.ToString(), @"[^\r\n]+").Cast<Match>().Select(m => m.Value).ToArray();

                        string hintText = string.Empty;
                        for (int i = 0; i < hintLine.Count(); i++)
                        {
                            SizeF size = graphics.MeasureString(hintLine[i], font);
                            lineHeight = (int)Math.Ceiling(size.Height);
                            int ratio = (int)Math.Ceiling(size.Width / (double)(ucHintText.Instance.getTemplateWidth));
                            lineCount += ratio;
                            hintText += hintLine[i].Replace("|", string.Empty) + Environment.NewLine;
                        }

                        if (txt.Name.Equals(txtProductDetail.Name) || txt.Name.Equals(txtProductSpec.Name))
                        {
                            ucHintText.Instance.Width = txt.Width + 75;
                            ucHintText.Instance.Height = lineCount * lineHeight + 12;
                            ucHintText.Instance.Top = txt.Parent.Top + txt.Parent.Parent.Top + txt.Parent.Parent.Parent.Top + txt.Top + 43 + 4;
                            ucHintText.Instance.Left = txt.Parent.Left + txt.Parent.Parent.Left + txt.Parent.Parent.Parent.Left + txt.Left + 3 + txt.Width;
                        }
                        else if (txt.Name.Equals(txtProductKeyword.Name))
                        {
                            ucHintText.Instance.Width = txt.Width + 75;
                            ucHintText.Instance.Height = lineCount * lineHeight + 12;
                            ucHintText.Instance.Top = txt.Parent.Top + txt.Parent.Parent.Top + txt.Parent.Parent.Parent.Top + txt.Top + 43 + 4;
                            ucHintText.Instance.Left = txt.Parent.Left + txt.Parent.Parent.Left + txt.Parent.Parent.Parent.Left + txt.Left + 3 - ucHintText.Instance.Width;
                        }
                        ucHintText.Instance.Font = font;
                        ucHintText.Instance.setTemplateHint = hintText;
                        this.Controls.Add(ucHintText.Instance);
                    }

                    ucHintText.Instance.BringToFront();
                }
                else
                {
                    if (this.Controls.Contains(ucHintText.Instance))
                    {
                        this.Controls.Remove(ucHintText.Instance);
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi con trỏ chuột leave khỏi vùng vùng textbox */
        private void txtProduct_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (this.Controls.Contains(ucHintText.Instance))
                {
                    this.Controls.Remove(ucHintText.Instance);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi textbox nhấn enter */
        private void txtProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                RichTextBox txt = sender as RichTextBox;

                if (e.KeyChar == Convert.ToChar(Keys.Return))
                {
                    insertFirstBullet(txt, txt.SelectionStart);
                }

                var pos = txt.GetLineFromCharIndex(txt.SelectionStart);
                string line = txt.Lines[pos];

                if (!line[0].Equals('-') && !line[1].Equals('-') && txt.SelectionStart == 2)
                {
                    insertFirstBullet(txt, 0);
                    txt.SelectionStart = 4;
                }

            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền đọc dữ liệu từ HTML làm việc */
        private void bgwGetDataHtml_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    string _html = ((GetDataHtmlParam)e.Argument).htmlData;
                    bool _getImageProfile = ((GetDataHtmlParam)e.Argument).getImageProfile;
                    bool _addToListImage = ((GetDataHtmlParam)e.Argument).addToListImage;
                    HtmlEditorData _htmlEditorData = ((GetDataHtmlParam)e.Argument).htmlEditorData;
                    hEditor.getDataFromHtml(_html, _addToListImage, _getImageProfile, ref _htmlEditorData);
                    e.Result = _htmlEditorData;
                });
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình đọc dữ liệu từ HTML hoàn thành làm việc */
        private void bgwGetDataHtml_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                htmlEditorData = (HtmlEditorData)e.Result;

                if (frmGetData.Instance.chkAddToListImage.Checked == true)
                {
                    downloadAddToListImage(hEditor.linkGetList);
                }

                updateContentHtmlToComponent();
                displayMessageHidden("Đã lấy được dữ liệu");
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện liên quan đến ảnh      */
        /***************************************************************/
        /* Hàm get encoder ảnh */
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        /* Hàm chống lag ảnh */
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /* Hàm cập nhật ảnh kết quả */
        private void updateImageAfter(bool rotate, float contrast, int xZero, int yZero)
        {
            try
            {
                UpdateImageAfterParam updateImageAfterParam;
                updateImageAfterParam.bitmapAfterTitle = txtProductName.Text.Trim();
                updateImageAfterParam.bitmapBefore = (Bitmap)picImageEditorBefore.Image.Clone();
                updateImageAfterParam.bitmapBeforeClientWidth = picImageEditorBefore.ClientSize.Width;
                updateImageAfterParam.bitmapBeforeClientHeight = picImageEditorBefore.ClientSize.Height;
                updateImageAfterParam.rotate = rotate;
                updateImageAfterParam.contrast = contrast;
                updateImageAfterParam.style = style;
                updateImageAfterParam.xZero = xZero;
                updateImageAfterParam.yZero = yZero;
                updateImageAfterParam.scale = scale;

                if (bgwUpdateImageAfter.IsBusy)
                {
                    bgwUpdateImageAfter.CancelAsync();
                }
                else
                {
                    bgwUpdateImageAfter.RunWorkerAsync(updateImageAfterParam);
                }

                while (bgwUpdateImageAfter.IsBusy)
                {
                    Application.DoEvents();
                }
            }
            catch { }
            finally { }
        }

        /* Hàm lấy tỷ lệ và độ lệch của ảnh so với khung */
        private void getImageFactorOffset(int wImage, int hImage, int wFrame, int hFrame, ref double factor, ref int xOffset, ref int yOffset)
        {
            try
            {
                double wfactor = (double)wImage / wFrame;
                double hfactor = (double)hImage / hFrame;
                factor = Math.Max(wfactor, hfactor);
                xOffset = (int)((wFrame - wImage / factor) / 2);
                yOffset = (int)((hFrame - hImage / factor) / 2);
            }
            catch { }
            finally { }
        }

        /* Hàm lấy tỷ lệ của ảnh so với khung */
        private void getImageFactor(int wImage, int hImage, int wFrame, int hFrame, ref double factor)
        {
            try
            {
                double wfactor = (double)wImage / wFrame;
                double hfactor = (double)hImage / hFrame;
                factor = Math.Max(wfactor, hfactor);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lựa chọn hình ảnh được nhấp chọn */
        private void btnOpenListImage_Click(object sender, EventArgs e)
        {
            try
            {
                imgPickerList.ImageSize = new Size(100, 75);
                lsvListImagePicker.LargeImageList = imgPickerList;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn danh sách ảnh cần chỉnh sửa";
                ofd.Multiselect = true;
                ofd.Filter = "Image file|*.jpg;*.png;*.gif;*.bmp";
                DialogResult dr = ofd.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    string[] files = ofd.FileNames;
                    foreach (string fileName in files)
                    {
                        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            MemoryStream ms = new MemoryStream();
                            fs.CopyTo(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            imgPickerList.Images.Add(Image.FromStream(ms));
                            lsvListImagePicker.Items.Add(fileName, imgPickerIndex);
                            imgPickerIndex++;
                        }
                    }
                    btnClearListImage.Enabled = true;
                    mniClearListImage.Enabled = btnClearListImage.Enabled;
                    displayMessageHidden("Danh sách ảnh đã được tạo");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa danh sách ảnh được nhấp chọn */
        private void btnClearListImage_Click(object sender, EventArgs e)
        {
            try
            {
                imgPickerIndex = 0;
                imgPickerList.Images.Clear();
                lsvListImagePicker.Clear();
                contrast = 1f;
                btnClearListImage.Enabled = false;
                mniClearListImage.Enabled = btnClearListImage.Enabled;
                displayMessageHidden("Danh sách ảnh đã được xóa");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi hình ảnh được nhấp chọn */
        private void lsvListImagePicker_Click(object sender, EventArgs e)
        {
            try
            {
                if (lsvListImagePicker.FocusedItem.Index == -1)
                    return;

                picImageEditorBefore.Enabled = true;
                picImageEditorAfter.Enabled = true;

                btnClearImage.Enabled = true;
                btnRotateImage.Enabled = true;
                btnDecImageContrast.Enabled = true;
                btnIncImageContrast.Enabled = true;
                btnClearImageCrop.Enabled = true;
                btnDecImageScale.Enabled = true;
                btnIncImageScale.Enabled = true;
                btnMoveImageLeft.Enabled = true;
                btnMoveImageRight.Enabled = true;
                btnMoveImageUp.Enabled = true;
                btnMoveImageDown.Enabled = true;
                btnImageOriginalScale.Enabled = true;
                btnAddImageCombo.Enabled = true;
                chkAddFrameTitle.Enabled = true;
                btnSaveImage.Enabled = true;
                if (bgwUploadToDrive.IsBusy)
                {
                    btnUploadAddLink.Enabled = false;
                }
                else
                {
                    btnUploadAddLink.Enabled = true;
                }

                mniClearImage.Enabled = btnClearImage.Enabled;
                mniRotateImage.Enabled = btnRotateImage.Enabled;
                mniDecImageContrast.Enabled = btnDecImageContrast.Enabled;
                mniIncImageContrast.Enabled = btnIncImageContrast.Enabled;
                mniClearImageCrop.Enabled = btnClearImageCrop.Enabled;
                mniDecImageScale.Enabled = btnDecImageScale.Enabled;
                mniIncImageScale.Enabled = btnIncImageScale.Enabled;
                mniMoveImageLeft.Enabled = btnMoveImageLeft.Enabled;
                mniMoveImageRight.Enabled = btnMoveImageRight.Enabled;
                mniMoveImageUp.Enabled = btnMoveImageUp.Enabled;
                mniMoveImageDown.Enabled = btnMoveImageDown.Enabled;
                mniImageOrginalScale.Enabled = btnImageOriginalScale.Enabled;
                mniAddImageCombo.Enabled = btnAddImageCombo.Enabled;
                mniAddFrameTitle.Enabled = chkAddFrameTitle.Enabled;
                mniSaveImage.Enabled = btnSaveImage.Enabled;
                mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;

                ListViewItem item = lsvListImagePicker.Items[lsvListImagePicker.FocusedItem.Index];
                ImageFormat imageFormat = ImageFormat.Jpeg;

                switch (Path.GetExtension(item.Text))
                {
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;

                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;

                    case ".gif":
                        imageFormat = ImageFormat.Gif;
                        break;
                }

                using (FileStream fs = new FileStream(item.Text, FileMode.Open, FileAccess.Read))
                {
                    MemoryStream msf = new MemoryStream();
                    fs.CopyTo(msf);
                    msf.Seek(0, SeekOrigin.Begin);
                    bmpBefore = (Bitmap)Image.FromStream(msf);
                }

                using (Bitmap bmp = bmpBefore)
                {
                    ImageCodecInfo encoder = GetEncoder(imageFormat);
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                    using (MemoryStream mso = new MemoryStream())
                    {
                        bmp.Save(mso, encoder, encoderParameters);
                        bmpBefore = (Bitmap)Image.FromStream(mso);
                    }
                }

                bmpTemplate = new Bitmap(Application.StartupPath + "\\style.png");

                double _factor = 1f;
                getImageFactor(bmpBefore.Width, bmpBefore.Height, 1080, 720, ref _factor);
                if (_factor < 1f) _factor = 1f;

                bmpBefore = new Bitmap(bmpBefore, new Size(Convert.ToInt32(bmpBefore.Width / _factor), Convert.ToInt32(bmpBefore.Height / _factor)));
                picImageEditorBefore.Image = bmpBefore;

                drawPolygons = new List<List<Point>>();
                contrast = 1f;
                xOriginal = 0;
                yOriginal = 0;
                scale = 1f;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);

                displayMessageHidden("Ảnh đã được chọn");
            }
            catch { }
            finally { };
        }

        /* Sự kiện khi nút xóa ảnh được nhấp chọn */
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            try
            {
                bmpComboOld = null;
                drawPolygons = new List<List<Point>>();
                rotate = false;
                contrast = 1f;
                xOriginal = 0;
                yOriginal = 0;
                scale = 1f;

                picImageEditorBefore.Image = null;
                picImageEditorAfter.Image = null;
                picImageEditorBefore.Enabled = false;
                picImageEditorAfter.Enabled = false;

                btnClearImage.Enabled = false;
                btnRotateImage.Enabled = false;
                btnDecImageContrast.Enabled = false;
                btnIncImageContrast.Enabled = false;
                btnClearImageCrop.Enabled = false;
                btnDecImageScale.Enabled = false;
                btnIncImageScale.Enabled = false;
                btnMoveImageLeft.Enabled = false;
                btnMoveImageRight.Enabled = false;
                btnMoveImageUp.Enabled = false;
                btnMoveImageDown.Enabled = false;
                btnImageOriginalScale.Enabled = false;
                btnAddImageCombo.Enabled = false;
                chkAddFrameTitle.Enabled = false;
                btnSaveImage.Enabled = false;
                btnUploadAddLink.Enabled = false;

                mniClearImage.Enabled = btnClearImage.Enabled;
                mniRotateImage.Enabled = btnRotateImage.Enabled;
                mniDecImageContrast.Enabled = btnDecImageContrast.Enabled;
                mniIncImageContrast.Enabled = btnIncImageContrast.Enabled;
                mniClearImageCrop.Enabled = btnClearImageCrop.Enabled;
                mniDecImageScale.Enabled = btnDecImageScale.Enabled;
                mniIncImageScale.Enabled = btnIncImageScale.Enabled;
                mniMoveImageLeft.Enabled = btnMoveImageLeft.Enabled;
                mniMoveImageRight.Enabled = btnMoveImageRight.Enabled;
                mniMoveImageUp.Enabled = btnMoveImageUp.Enabled;
                mniMoveImageDown.Enabled = btnMoveImageDown.Enabled;
                mniImageOrginalScale.Enabled = btnImageOriginalScale.Enabled;
                mniAddImageCombo.Enabled = btnAddImageCombo.Enabled;
                mniAddFrameTitle.Enabled = chkAddFrameTitle.Enabled;
                mniSaveImage.Enabled = btnSaveImage.Enabled;
                mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;

                displayMessageHidden("Ảnh đã được xóa");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xoay ảnh được nhấp chọn */
        private void btnRotateImage_Click(object sender, EventArgs e)
        {
            try
            {
                rotate = true;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                rotate = false;
                displayMessageHidden("Ảnh đã được xoay");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút giảm độ tương phản được nhấp chọn */
        private void btnDecImageContrast_Click(object sender, EventArgs e)
        {
            try
            {
                contrast -= 0.05f;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Độ tương phản " + Math.Round(contrast * 100).ToString());
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút tăng độ tương phản được nhấp chọn */
        private void btnIncImageContrast_Click(object sender, EventArgs e)
        {
            try
            {
                contrast += 0.05f;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Độ tương phản " + Math.Round(contrast * 100).ToString());
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa vùng cắt ảnh được nhấp chọn */
        private void btnClearImageCrop_Click(object sender, EventArgs e)
        {
            try
            {
                drawPolygons = new List<List<Point>>();
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Vùng cắt ảnh đã được xóa");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút giảm tỷ lệ ảnh được nhấp chọn */
        private void btnDecImageScale_Click(object sender, EventArgs e)
        {
            try
            {
                scale -= 0.1f;
                if (scale < 0) scale = 0;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Tỷ lệ ảnh " + Math.Round(scale * 100).ToString() + "%");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút tăng tỷ lệ ảnh được nhấp chọn */
        private void btnIncImageScale_Click(object sender, EventArgs e)
        {
            try
            {
                scale += 0.1f;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Tỷ lệ ảnh " + Math.Round(scale * 100).ToString() + "%");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút di chuyển ảnh sang trái được nhấp chọn */
        private void btnMoveImageLeft_Click(object sender, EventArgs e)
        {
            try
            {
                xOriginal -= 10;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Ảnh đã được di chuyển sang trái");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút di chuyển ảnh sang phải được nhấp chọn */
        private void btnMoveImageRight_Click(object sender, EventArgs e)
        {
            try
            {
                xOriginal += 10;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Ảnh đã được di chuyển sang phải");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút di chuyển ảnh sang lên trên được nhấp chọn */
        private void btnMoveImageUp_Click(object sender, EventArgs e)
        {
            try
            {
                yOriginal -= 10;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Ảnh đã được di chuyển lên trên");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút di chuyển ảnh xuống dưới được nhấp chọn */
        private void btnMoveImageDown_Click(object sender, EventArgs e)
        {
            try
            {
                yOriginal += 10;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Ảnh đã được di chuyển xuống dưới");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút khôi phục tỷ lệ ảnh và vị trí được nhấp chọn */
        private void btnImageOriginalScale_Click(object sender, EventArgs e)
        {
            try
            {
                xOriginal = 0;
                yOriginal = 0;
                scale = 1f;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                displayMessageHidden("Ảnh đã được khôi phục tỷ lệ và vị trí");
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút thêm ảnh combo được nhấp chọn */
        private void btnAddImageCombo_Click(object sender, EventArgs e)
        {
            try
            {
                bmpComboOld = bmpCombo;
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi checkbox thêm khung và tiêu đề được nhấp chọn */
        private void chkAddFrameTitle_Click(object sender, EventArgs e)
        {
            try
            {
                if (picImageEditorAfter.Image != null)
                {
                    if (style == 0)
                    {
                        style = 1;
                        chkAddFrameTitle.Checked = true;
                        mniAddFrameTitle.Checked = chkAddFrameTitle.Checked;
                        displayMessageHidden("Ảnh đã được thêm khung và tiêu đề");
                    }
                    else
                    {
                        style = 0;
                        chkAddFrameTitle.Checked = false;
                        mniAddFrameTitle.Checked = chkAddFrameTitle.Checked;
                        displayMessageHidden("Ảnh đã loại bỏ khung và tiêu đề");
                    }

                    rotate = false;
                    updateImageAfter(rotate, contrast, xOriginal, yOriginal);

                    Properties.Settings.Default.style = style;
                    Properties.Settings.Default.Save();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lưu ảnh được nhấp chọn */
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Lưu ảnh đã được chỉnh sửa";
                sfd.Filter = "Image file|*.jpg;*.png;*.gif;*.bmp";

                string fileName = utils.convertNameToFileName(txtProductName.Text.Trim(), "jpg");

                sfd.FileName = fileName;
                ImageFormat format = ImageFormat.Jpeg;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    switch (Path.GetExtension(sfd.FileName))
                    {
                        case ".png":
                            format = ImageFormat.Png;
                            break;

                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;

                        case ".gif":
                            format = ImageFormat.Gif;
                            break;
                    }

                    picImageEditorAfter.Image.Save(sfd.FileName, format);
                    displayMessageHidden("Ảnh đã được lưu");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi con trỏ ảnh xem trước được nhấp xuống */
        private void picImageEditorBefore_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (picImageEditorBefore.Image != null)
                {
                    if (newPolygon != null)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            if (newPolygon.Count > 2)
                            {
                                drawPolygons.Add(newPolygon);
                                rotate = false;
                                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                            }

                            newPolygon = null;
                            tslStatus.Text = string.Empty;
                        }
                        else
                        {
                            if (newPolygon[newPolygon.Count - 1] != e.Location)
                            {
                                newPolygon.Add(e.Location);
                            }
                        }
                    }
                    else
                    {
                        drawPolygons = new List<List<Point>>();
                        newPolygon = new List<Point>();
                        newPoint = e.Location;
                        newPolygon.Add(e.Location);
                    }

                    picImageEditorBefore.Invalidate();
                }
                else
                {
                    drawPolygons = new List<List<Point>>();
                    newPolygon = new List<Point>();
                    newPoint = e.Location;
                    newPolygon.Add(e.Location);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi con trỏ ảnh xem trước được di chuyển */
        private void picImageEditorBefore_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (picImageEditorBefore.Image != null)
                {
                    picImageEditorBefore.Cursor = Cursors.Cross;
                    if (newPolygon == null) return;
                    newPoint = e.Location;
                    picImageEditorBefore.Invalidate();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi ảnh xem trước được vẽ */
        private void picImageEditorBefore_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (picImageEditorBefore.Image != null)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    foreach (List<Point> polygon in drawPolygons)
                    {
                        e.Graphics.DrawPolygon(Pens.Black, polygon.ToArray());
                    }

                    if (newPolygon != null)
                    {
                        if (newPolygon.Count > 1)
                        {
                            e.Graphics.DrawLines(Pens.Black, newPolygon.ToArray());
                        }

                        if (newPolygon.Count > 0)
                        {
                            using (Pen dashed_pen = new Pen(Color.Black))
                            {
                                dashed_pen.DashPattern = new float[] { 3, 3 };
                                e.Graphics.DrawLine(dashed_pen,
                                                    newPolygon[newPolygon.Count - 1],
                                                    newPoint);
                            }

                            displayMessage("Nhấp chuột phải để hoàn tất");
                        }
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi ảnh kết quả khi nhấp chuột */
        private void picImageEditorAfter_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    picImageEditorAfter.Cursor = Cursors.SizeAll;
                    startPointMove = new Point(e.X, e.Y);
                    xWork = xOriginal;
                    yWork = yOriginal;
                    rotate = false;
                    updateImageAfter(rotate, contrast, xWork, yWork);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    picImageEditorAfter.Cursor = Cursors.NoMove2D;
                    startPointScale = new Point(e.X, e.Y);
                    rotate = false;
                    updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi ảnh kết quả được di chuyển */
        private void picImageEditorAfter_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    double factor1 = 1f;
                    int xOffset1 = 0;
                    int yOffset1 = 0;

                    getImageFactorOffset(picImageEditorAfter.Image.Width,
                                         picImageEditorAfter.Image.Height,
                                         picImageEditorAfter.ClientSize.Width,
                                         picImageEditorAfter.ClientSize.Height,
                                         ref factor1, ref xOffset1, ref yOffset1);

                    xWork = xOriginal + (int)((e.X - startPointMove.X) * factor1);
                    yWork = yOriginal + (int)((e.Y - startPointMove.Y) * factor1);

                    rotate = false;
                    updateImageAfter(rotate, contrast, xWork, yWork);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    int xOffset = e.X - startPointScale.X;
                    int yOffset = e.Y - startPointScale.Y;

                    if (yOffset < 0)
                    {
                        scale += 0.025f;

                        rotate = false;
                        updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                    }

                    else if (yOffset > 0)
                    {
                        scale -= 0.025f;
                        if (scale < 0) scale = 0;

                        rotate = false;
                        updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                    }

                    startPointScale.X = e.X;
                    startPointScale.Y = e.Y;
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi ảnh kết quả nhả chuột phải */
        private void picImageEditorAfter_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    xOriginal = xWork;
                    yOriginal = yWork;
                }

                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
                picImageEditorAfter.Cursor = Cursors.Default;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tách vùng được thay đổi */
        private void spcImageEditor_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            try
            {
                drawPolygons = new List<List<Point>>();
                rotate = false;
                updateImageAfter(rotate, contrast, xOriginal, yOriginal);
            }
            catch { }
            finally { }
        }

        /* Sự kiện tiến trình nền cập nhật hình ảnh */
        private void bgwUpdateImageAfter_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (bgwUpdateImageAfter.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    string _bitmapAfterTitle = ((UpdateImageAfterParam)e.Argument).bitmapAfterTitle;
                    Bitmap _bitmapBefore = ((UpdateImageAfterParam)e.Argument).bitmapBefore;
                    int _bitmapBeforeClientWidth = ((UpdateImageAfterParam)e.Argument).bitmapBeforeClientWidth;
                    int _bitmapBeforeClientHeight = ((UpdateImageAfterParam)e.Argument).bitmapBeforeClientHeight;
                    bool _rotate = ((UpdateImageAfterParam)e.Argument).rotate;
                    float _contrast = ((UpdateImageAfterParam)e.Argument).contrast;
                    int _style = ((UpdateImageAfterParam)e.Argument).style;
                    int _xZero = ((UpdateImageAfterParam)e.Argument).xZero;
                    int _yZero = ((UpdateImageAfterParam)e.Argument).yZero;
                    double _scale = ((UpdateImageAfterParam)e.Argument).scale;

                    if (_bitmapBefore != null)
                    {
                        /* Cập nhật xoay */
                        Bitmap bmpRotate = _bitmapBefore;
                        if (rotate == true)
                        {
                            bmpRotate.RotateFlip(RotateFlipType.Rotate180FlipX);
                            bmpRotate.RotateFlip(RotateFlipType.Rotate90FlipX);
                        }

                        /* Cập nhật tương phản */
                        Bitmap bmpContrast = new Bitmap(bmpRotate.Width, bmpRotate.Height);
                        ImageAttributes ia = new ImageAttributes();
                        ColorMatrix cmpicture = new ColorMatrix(new float[][] { new float[] { _contrast, 0f, 0f, 0f, 0f }, new float[] { 0f, _contrast, 0f, 0f, 0f }, new float[] { 0f, 0f, _contrast, 0f, 0f }, new float[] { 0f, 0f, 0f, 1f, 0f }, new float[] { 0.001f, 0.001f, 0.001f, 0f, 1f } });
                        ia.SetColorMatrix(cmpicture);
                        Graphics grps = Graphics.FromImage(bmpContrast);
                        grps.DrawImage(bmpRotate, new Rectangle(0, 0, bmpRotate.Width, bmpRotate.Height), 0, 0,
                                       bmpRotate.Width, bmpRotate.Height, GraphicsUnit.Pixel, ia);
                        grps.Dispose();

                        /* Cập nhật vùng polygon */
                        double factor1 = 1f;
                        int xOffset1 = 0;
                        int yOffset1 = 0;
                        getImageFactorOffset(_bitmapBefore.Width,
                                             _bitmapBefore.Height,
                                             _bitmapBeforeClientWidth,
                                             _bitmapBeforeClientHeight,
                                             ref factor1, ref xOffset1, ref yOffset1);

                        List<List<Point>> workPolygons = new List<List<Point>>();

                        foreach (List<Point> drawPolygon in drawPolygons)
                        {
                            List<Point> workPolygon = new List<Point>();

                            foreach (Point drawPoint in drawPolygon)
                            {
                                int cx = (int)((drawPoint.X - xOffset1) * factor1);
                                int cy = (int)((drawPoint.Y - yOffset1) * factor1);
                                workPolygon.Add(new Point(cx, cy));
                            }

                            workPolygons.Add(workPolygon);
                        }

                        /* Cắt ảnh theo vùng */
                        foreach (List<Point> workPolygon in workPolygons)
                        {
                            GraphicsPath gp = new GraphicsPath();
                            gp.AddPolygon(workPolygon.ToArray());
                            Bitmap bmp1 = new Bitmap(bmpContrast.Width, bmpContrast.Height);
                            Bitmap bmp0 = bmpContrast;

                            using (Graphics G = Graphics.FromImage(bmp1))
                            {
                                G.Clip = new Region(gp);
                                G.DrawImage(bmp0, 0, 0);
                                bmpContrast = bmp1;
                            }

                            gp.Dispose();
                        }

                        double factor2 = 1f;
                        int xOffset2 = 0;
                        int yOffset2 = 0;
                        int xResize = 0;
                        int yResize = 0;

                        Bitmap bmpPolygon = new Bitmap(bmpTemplate.Width, bmpTemplate.Height, PixelFormat.Format32bppArgb);
                        getImageFactorOffset(bmpContrast.Width, bmpContrast.Height, bmpTemplate.Width, bmpTemplate.Height, ref factor2, ref xOffset2, ref yOffset2);
                        xResize = (int)((bmpTemplate.Width - bmpContrast.Size.Width / factor2 * scale) / 2) + _xZero;
                        yResize = (int)((bmpTemplate.Height - bmpContrast.Size.Height / factor2 * scale) / 2) + _yZero;

                        using (Graphics graphics = Graphics.FromImage(bmpPolygon))
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, bmpTemplate.Width, bmpTemplate.Height));

                            if (bmpComboOld != null)
                            {
                                graphics.DrawImage(bmpComboOld, new Rectangle(new Point(0, 0), new Size(bmpComboOld.Width, bmpComboOld.Height)), new Rectangle(new Point(0, 0), new Size(bmpComboOld.Width, bmpComboOld.Height)), GraphicsUnit.Pixel);
                            }

                            graphics.DrawImage(bmpContrast, new Rectangle(new Point(xResize, yResize), new Size((int)(bmpContrast.Size.Width / factor2 * scale), (int)(bmpContrast.Size.Height / factor2 * scale))),
                                               new Rectangle(new Point(0, 0), new Size(bmpContrast.Size.Width, bmpContrast.Size.Height)), GraphicsUnit.Pixel);
                        }

                        /* cập nhật ảnh full */
                        bmpCombo = bmpPolygon;
                        Bitmap bmpFull = new Bitmap(bmpCombo.Width, bmpCombo.Height, PixelFormat.Format32bppArgb);

                        /* viết chữ */
                        using (Graphics graphics = Graphics.FromImage(bmpFull))
                        {
                            graphics.DrawImage(bmpCombo, new Rectangle(new Point(0, 0), new Size(bmpCombo.Width, bmpCombo.Height)), new Rectangle(new Point(0, 0), new Size(bmpCombo.Width, bmpCombo.Height)), GraphicsUnit.Pixel);

                            if (style == 1)
                            {
                                graphics.DrawImage(bmpTemplate, new Rectangle(new Point(), bmpTemplate.Size),
                                                   new Rectangle(new Point(), bmpTemplate.Size), GraphicsUnit.Pixel);

                                Font timesFont = new Font("Times New Roman", 28, FontStyle.Bold);
                                string text = _bitmapAfterTitle;
                                string[] words = Regex.Matches(text, @"\S+").Cast<Match>().Select(m => m.Value).ToArray();

                                if (words.Length <= 7 && words.Length > 0)
                                {
                                    string textCenter = string.Empty;
                                    for (int i = 0; i < words.Length; i++) textCenter += words[i] + ' ';

                                    var path = new GraphicsPath();
                                    Rectangle bounds = new Rectangle(145, 30, 565, 90);

                                    //graphics.DrawRectangle(Pens.Black, bounds);

                                    path.AddString(textCenter, timesFont.FontFamily, (int)timesFont.Style, 28, new Point(0, 0), StringFormat.GenericTypographic);
                                    var area = Rectangle.Round(path.GetBounds());
                                    var offset = new Point(bounds.Left + (bounds.Width / 2 - area.Width / 2) - area.Left, bounds.Top + (bounds.Height / 2 - area.Height / 2) - area.Top);
                                    var translate = new Matrix();
                                    translate.Translate(offset.X, offset.Y);
                                    path.Transform(translate);
                                    graphics.FillPath(Brushes.White, path);
                                }
                                else if (words.Length > 7)
                                {
                                    string textTop = string.Empty;
                                    string textBottom = string.Empty;
                                    int wordBreak = words.Length / 2;

                                    for (int i = 0; i < wordBreak; i++) textTop += words[i] + ' ';
                                    for (int i = wordBreak; i < words.Length; i++) textBottom += words[i] + ' ';

                                    var pathTop = new GraphicsPath();
                                    var pathBottom = new GraphicsPath();

                                    Rectangle boundsTop = new Rectangle(145, 30, 565, 45);
                                    Rectangle boundsBottom = new Rectangle(145, 75, 565, 45);

                                    //graphics.DrawRectangle(Pens.Black, boundsTop);
                                    //graphics.DrawRectangle(Pens.Black, boundsBottom);

                                    pathTop.AddString(textTop, timesFont.FontFamily, (int)timesFont.Style, 28, new Point(0, 0), StringFormat.GenericTypographic);
                                    pathBottom.AddString(textBottom, timesFont.FontFamily, (int)timesFont.Style, 28, new Point(0, 0), StringFormat.GenericTypographic);

                                    var areaTop = Rectangle.Round(pathTop.GetBounds());
                                    var areaBottom = Rectangle.Round(pathBottom.GetBounds());

                                    var offsetTop = new Point(boundsTop.Left + (boundsTop.Width / 2 - areaTop.Width / 2) - areaTop.Left, boundsTop.Top + (boundsTop.Height / 2 - areaTop.Height / 2) - areaTop.Top);
                                    var offsetBottom = new Point(boundsBottom.Left + (boundsBottom.Width / 2 - areaBottom.Width / 2) - areaBottom.Left, boundsBottom.Top + (boundsBottom.Height / 2 - areaBottom.Height / 2) - areaBottom.Top);

                                    var translateTop = new Matrix();
                                    var translateBottom = new Matrix();

                                    translateTop.Translate(offsetTop.X, offsetTop.Y);
                                    translateBottom.Translate(offsetBottom.X, offsetBottom.Y);

                                    pathTop.Transform(translateTop);
                                    pathBottom.Transform(translateBottom);

                                    graphics.FillPath(Brushes.White, pathTop);
                                    graphics.FillPath(Brushes.White, pathBottom);
                                }
                            }
                        }

                        /* trả về kết quả */
                        UpdateImageAfterReturn updateImageAfterReturn;
                        updateImageAfterReturn.bitmapBefore = bmpRotate;
                        updateImageAfterReturn.bitmapAfter = bmpFull;
                        e.Result = updateImageAfterReturn;
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình cập nhật hình ảnh hoàn thành làm việc */
        private void bgwUpdateImageAfter_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    picImageEditorAfter.Image = ((UpdateImageAfterReturn)e.Result).bitmapAfter;
                    picImageEditorBefore.Image = ((UpdateImageAfterReturn)e.Result).bitmapBefore;
                }
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện Google Drive     */
        /***************************************************************/
        /* Sự kiện khi nút chọn danh sách ảnh từ Drive được nhấp chọn */
        private void btnOpenListImageDrive_Click(object sender, EventArgs e)
        {
            try
            {
                frmGetDrive.Instance.gDrive.getUserCredentialFromFileContent(
                    gDrive.driveParam.credentialsRawContent,
                    "credentials_raw.json");
                frmGetDrive.Instance.gDrive.getDriveService();

                frmGetDrive.Instance.filterProductName = txtProductName.Text.Trim();
                frmGetDrive.Instance.txtFilterFileDriveByName.Text = string.Empty;
                frmGetDrive.Instance.ShowDialog();

                if (frmGetDrive.Instance.isGetDriveDone == true)
                {
                    if (frmGetDrive.Instance.lstFilePathSelected.Count > 0)
                    {
                        imgPickerList.ImageSize = new Size(100, 75);
                        lsvListImagePicker.LargeImageList = imgPickerList;

                        foreach (string fileName in frmGetDrive.Instance.lstFilePathSelected)
                        {
                            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                            {
                                MemoryStream ms = new MemoryStream();
                                fs.CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                imgPickerList.Images.Add(Image.FromStream(ms));
                                lsvListImagePicker.Items.Add(fileName, imgPickerIndex);
                                imgPickerIndex++;
                            }
                        }

                        btnClearListImage.Enabled = true;
                        mniClearListImage.Enabled = btnClearListImage.Enabled;
                        displayMessageHidden("Danh sách ảnh đã được tạo");
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút nhấn tải lên và thêm link được nhấp chọn */
        private void btnUploadAddLink_Click(object sender, EventArgs e)
        {
            try
            {
                btnUploadAddLink.Enabled = false;
                mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;

                UploadToDriveParam uploadToDriveParam;

                uploadToDriveParam.bitmapTitle = txtProductName.Text.Trim();
                uploadToDriveParam.bitmapAfter = (Bitmap)picImageEditorAfter.Image.Clone();
                bgwUploadToDrive.RunWorkerAsync(uploadToDriveParam);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút danh sách yêu cầu chụp được nhấp chọn */
        private void btnCaptureRequestList_Click(object sender, EventArgs e)
        {
            try
            {
                frmRequest.Instance.ShowDialog();

                if (!string.IsNullOrEmpty(frmRequest.Instance.requestProductName.Trim()))
                {
                    tabMain.SelectedIndex = 0;
                    txtProductName.Text = frmRequest.Instance.requestProductName.Trim();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình tải lên và thêm link được cập nhật */
        private void bgwUploadToDrive_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage < 0)
                {
                    displayMessageHidden("Không thể tải lên và thêm link");
                    btnUploadAddLink.Enabled = true;
                    mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;
                }
                else if (e.ProgressPercentage == 0)
                {
                    displayMessage("Đang xác thực tài khoản");
                }
                else if (e.ProgressPercentage == 20)
                {
                    displayMessage("Đang kiểm tra thư mục");
                }
                else if (e.ProgressPercentage == 30)
                {
                    displayMessage("Đang tạo thư mục mới");
                }
                else if (e.ProgressPercentage == 40)
                {
                    displayMessage("Bắt đầu tải lên");
                }
                else if (e.ProgressPercentage == 60)
                {
                    displayMessage("Đang chia sẻ công khai");
                }
                else if (e.ProgressPercentage == 80)
                {
                    displayMessage("Đang lấy và thêm link");
                }
                else if (e.ProgressPercentage == 100)
                {
                    displayMessageHidden("Đã tải lên và thêm link");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình tải lên và thêm link được làm việc */
        private void bgwUploadToDrive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string _bitmapTitle = ((UploadToDriveParam)e.Argument).bitmapTitle;
                Bitmap _bitmapAfter = ((UploadToDriveParam)e.Argument).bitmapAfter;

                bgwUploadToDrive.ReportProgress(0);
                gDrive.getUserCredentialFromFileContent(
                    gDrive.driveParam.credentialsPhotosContent,
                    "credentials_photos.json");
                gDrive.getDriveService();

                string fileName = utils.convertNameToFileName(_bitmapTitle, "jpg");

                string filePath = Path.GetTempPath() + fileName;
                _bitmapAfter.Save(filePath, ImageFormat.Jpeg);
                string fileLink = string.Empty;

                bgwUploadToDrive.ReportProgress(20);
                string folderName = "Ảnh sản phẩm mới " + DateTime.Now.ToString("MM yyyy");
                string folderId = gDrive.checkFolderExists(folderName);

                if (string.IsNullOrEmpty(folderId))
                {
                    bgwUploadToDrive.ReportProgress(30);
                    folderId = gDrive.createFolderDrive(folderName);
                }

                bgwUploadToDrive.ReportProgress(40);
                var newFileId = gDrive.uploadFileToDrive(folderId, fileName, filePath, "image/jpeg");

                bgwUploadToDrive.ReportProgress(60);
                gDrive.shareableFileFolderDrive(newFileId);

                bgwUploadToDrive.ReportProgress(80);
                fileLink = gDrive.getShareableLinkDrive(newFileId);
                string[] param = fileLink.Split(new[] { "/file/d/", "/view", "/edit" }, StringSplitOptions.None);
                fileLink = param[0] + "/uc?id=" + param[1];
                e.Result = fileLink;

                bgwUploadToDrive.ReportProgress(100);
            }
            catch
            {
                bgwUploadToDrive.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi tiến trình tải lên và thêm link hoàn thành làm việc */
        private void bgwUploadToDrive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    txtProductPicture.AppendText("\r\n" + e.Result);
                    string text = txtProductPicture.Text;

                    for (int i = 0; i < txtProductPicture.Lines.Count(); i++)
                    {
                        if (text[0].Equals('\n')) text = text.Remove(0, 1);
                        if (text[0].Equals('\r') && text[1].Equals('\n')) text = text.Remove(0, 2);
                        if (text[text.Length - 1].Equals('\n')) text = text.Remove(text.Length - 1, 1);
                        if (text[text.Length - 2].Equals('\r') && text[text.Length - 1].Equals('\n')) text = text.Remove(text.Length - 2, 2);
                    }

                    txtProductPicture.Text = text;
                    updateComponentToContentHtml();
                    geckoWebBrowserLoad(GeckoWebName.geckoPreview, hEditor.contentHtml.ToString());

                    btnUploadAddLink.Enabled = true;
                    mniUploadAddLink.Enabled = btnUploadAddLink.Enabled;
                }
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện sản phẩm Web   */
        /***************************************************************/
        /* Hàm lưu danh mục ra tệp Xml */
        private void saveCategoryToXmlFile(string fileXml)
        {
            try
            {
                StringBuilder xmlStr = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                xmlStr.Append("<root>\r\n");

                for (int i = 0; i < categoryList.Count; i++)
                {
                    if (!string.IsNullOrEmpty(categoryList[i]))
                    {
                        xmlStr.Append("\t<row name=\"" + System.Net.WebUtility.HtmlEncode(categoryList[i]) + "\"></row>\r\n");
                    }
                }
                xmlStr.Append("</root>");

                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlStr.ToString());
                xdoc.Save(fileXml);
            }
            catch { }
            finally { }
        }

        /* Hàm yêu cầu mã phần mềm */
        private void requestProductCode()
        {
            try
            {
                if (gwbTVKhoWeb.Url.ToString().Contains("/products"))
                {
                    txtProductCode.Clear();
                    txtProductCode.Enabled = false;
                    btnGetProductCode.Enabled = false;
                    mniGetProductCode.Enabled = btnGetProductCode.Enabled;

                    int i = 0;
                    int searchInputIndex = 0;
                    GeckoInputElement searchInputElement = (GeckoInputElement)gwbTVKhoWeb.Document.GetElementById("mat-input-" + i.ToString());

                    while (searchInputElement == null)
                    {
                        ++i;
                        searchInputElement = (GeckoInputElement)gwbTVKhoWeb.Document.GetElementById("mat-input-" + i.ToString());
                        if (i > 250)
                        {
                            i = 0;
                            break;
                        }
                    }

                    searchInputIndex = i;

                    try
                    {
                        GeckoInputElement searchInputName = (GeckoInputElement)gwbTVKhoWeb.Document.GetElementById("mat-input-" + searchInputIndex.ToString());

                        string nameStr = txtProductName.Text.Trim();
                        nameStr = Regex.Replace(nameStr, @"(\[.*\])", string.Empty);
                        string[] nameArr = Regex.Matches(nameStr, @"\S+").Cast<Match>().Select(m => m.Value).ToArray();
                        nameStr = string.Empty;

                        for (int j = 0; j < nameArr.Count(); j++)
                        {
                            nameStr += nameArr[j] + ((j != nameArr.Count() - 1) ? " " : string.Empty);
                        }

                        searchInputName.Click();
                        searchInputName.Value = nameStr;
                        gwbTVKhoWeb.Window.WindowUtils.SendNativeKeyEvent(0, 0, 0, "\r", "\r");
                    }
                    catch { }
                    finally { }

                    tmrTVKhoSearch.Enabled = true;
                }
            }
            catch { }
            finally { }
        }

        /* Hàm cập nhật html data */
        private void updateProductWeb()
        {
            /* Điền ảnh sản phẩm từ link trực tiếp*/
            try
            {
                try
                {
                    string fileName = utils.convertNameToFileNameSimplify(txtProductName.Text.Trim(), "jpg");

                    List<string> LinkList = new List<string>();

                    for (int i = 0; i < txtProductPicture.Lines.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(txtProductPicture.Lines[i]))
                        {
                            LinkList.Add(txtProductPicture.Lines[i]);
                        }
                    }

                    using (Gecko.AutoJSContext js = new Gecko.AutoJSContext(gwbNhanhVNWeb.Window))
                    {
                        MemoryStream ms = pWeb.getMemoryStreamUsingHttpWebClient(LinkList.First());

                        byte[] imgBuffer = ms.ToArray();
                        string strBuffer = BitConverter.ToString(imgBuffer).Replace("-", string.Empty);
                        string script = "var dt = new ClipboardEvent('').clipboardData || new DataTransfer();";
                        script += "var buffStr = '" + strBuffer + "';";
                        script += "var buffArr = new Uint8Array(buffStr.length / 2);";
                        script += "for (var i = 0; i < buffStr.length; i += 2) {buffArr[i / 2] = parseInt(buffStr.substring(i, i + 2), 16)}";
                        script += "var file = new File([buffArr], '" + fileName + "', {type: 'image/jpeg', lastModified: Date.now()});";
                        script += "dt.items.add(file);";
                        script += "var fileUpload = document.getElementById('imageUpload');";
                        script += "fileUpload.files = dt.files;";
                        string result = string.Empty;
                        js.EvaluateScript(script, out result);
                    }
                }
                catch
                {
                    displayMessageHidden("Không thể thêm hình ảnh");
                }
                finally { }
            }
            catch { }
            finally { }

            /* Điền tên sản phẩm */
            try
            {
                GeckoInputElement inputProductName = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("name");

                string result = string.Empty;

                if (!string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    result = txtProductName.Text.Trim();
                }

                inputProductName.Value = result;
            }
            catch { }
            finally { }

            /* Điền tên khác */
            try
            {
                GeckoInputElement inputProductOtherName = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("otherName");

                string textStr = txtProductName.Text.Trim();
                string[] textArr = Regex.Matches(textStr, @"\w+").Cast<Match>().Select(m => m.Value).ToArray();

                List<string> wordList = new List<string>();
                string result = string.Empty;

                if (!string.IsNullOrEmpty(textStr))
                {
                    wordList.Add(textStr);
                    wordList.Add(utils.convertToViNoSign(textStr));
                }

                if (textArr.Length > 1)
                {
                    for (int i = 0; i < textArr.Length; i++)
                    {
                        wordList.Add(textArr[i].Trim());
                        wordList.Add(utils.convertToViNoSign(textArr[i].Trim()));
                    }
                }

                wordList = wordList.Distinct().ToList();

                for (int i = 0; i < wordList.Count; i++)
                {
                    result += wordList[i] + (i == wordList.Count - 1 ? string.Empty : ", ");
                }

                inputProductOtherName.Value = result;
            }
            catch { }
            finally { }

            /* Điền từ khóa Meta, tags  */
            try
            {
                GeckoInputElement inputProductMetaKeyword = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("metaKeywords");
                GeckoInputElement inputProductMetaDescription = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("metaDescription");
                GeckoInputElement inputProductMetaTitle = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("metaTitle");
                GeckoInputElement inputProductTags = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("tags");

                string textStr = txtProductName.Text.Trim();
                string[] textArr = Regex.Matches(textStr, @"\w+").Cast<Match>().Select(m => m.Value).ToArray();

                List<string> wordList = new List<string>();
                string result = string.Empty;

                if (!string.IsNullOrEmpty(textStr))
                {
                    wordList.Add(textStr);
                    wordList.Add(utils.convertToViNoSign(textStr));
                }

                if (textArr.Length > 1)
                {
                    for (int i = 0; i < textArr.Length; i++)
                    {
                        wordList.Add(textArr[i].Trim());
                        wordList.Add(utils.convertToViNoSign(textArr[i].Trim()));
                    }
                }

                wordList = wordList.Distinct().ToList();

                for (int i = 0; i < wordList.Count; i++)
                {
                    result += wordList[i] + (i == wordList.Count - 1 ? string.Empty : ", ");
                }

                inputProductMetaKeyword.Value = result;
                inputProductMetaTitle.Value = textStr;
                inputProductTags.Value = result;

                if (!string.IsNullOrEmpty(htmlEditorData.txtProductDetail.Text))
                {
                    inputProductMetaDescription.Value = htmlEditorData.txtProductDetail.Text;
                }
                else
                {
                    inputProductMetaDescription.Value = textStr;
                }
            }
            catch { }
            finally { }

            /* Điền mã và mã vạch sản phẩm */
            try
            {
                GeckoInputElement inputProductCode = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("code");
                GeckoInputElement inputProductBarCode = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("barcode");

                string result = string.Empty;

                if (!string.IsNullOrEmpty(txtProductCode.Text))
                {
                    result = txtProductCode.Text.Trim();
                }

                inputProductCode.Value = result;
                inputProductBarCode.Value = result;
            }
            catch { }
            finally { }

            /* Điền giá sản phẩm */
            try
            {
                GeckoInputElement inputProductPrice = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("price");

                try
                {
                    string result = string.Empty;

                    if (!string.IsNullOrEmpty(txtProductPrice.Text))
                    {
                        result = long.Parse(txtProductPrice.Text).ToString();
                    }

                    inputProductPrice.Value = result;
                }
                catch { }
                finally { }
            }
            catch { }
            finally { }

            /* Điền danh mục sản phẩm */
            try
            {
                string categoryLevel1Name = string.Empty;
                string categoryLevel2Name = string.Empty;
                string categoryLevel3Name = string.Empty;

                if (cbxCategoryLevel1.Items.Count > 0)
                {
                    categoryLevel1Name = cbxCategoryLevel1.Items[cbxCategoryLevel1.SelectedIndex].ToString();
                }

                if (cbxCategoryLevel2.Items.Count > 1)
                {
                    categoryLevel2Name = cbxCategoryLevel2.Items[cbxCategoryLevel2.SelectedIndex].ToString();
                }

                if (cbxCategoryLevel3.Items.Count > 1)
                {
                    categoryLevel3Name = cbxCategoryLevel3.Items[cbxCategoryLevel3.SelectedIndex].ToString();
                }

                int categoryLevel1Index = -1;
                int categoryLevel2Index = -1;
                int categoryLevel3Index = -1;

                if (cbxCategoryLevel1.Items.Count > 0)
                {
                    categoryLevel1Index = categoryList.FindIndex(0, x => x.StartsWith(categoryLevel1Name));
                }

                if (cbxCategoryLevel2.Items.Count > 1)
                {
                    categoryLevel2Index = categoryList.FindIndex(categoryLevel1Index, x => x.StartsWith(categoryLevel2Name));
                }

                if (cbxCategoryLevel3.Items.Count > 1)
                {
                    categoryLevel3Index = categoryList.FindIndex(categoryLevel2Index, x => x.StartsWith(categoryLevel3Name));
                }

                GeckoSelectElement selectProductCategory = (GeckoSelectElement)gwbNhanhVNWeb.Document.GetElementById("categoryId");
                GeckoElement selectProductCategoryContains = (GeckoElement)gwbNhanhVNWeb.Document.GetElementById("select2-categoryId-container");

                if (categoryLevel1Index >= 0 && categoryLevel2Index >= 0 && categoryLevel3Index >= 0)
                {
                    selectProductCategory.SelectedIndex = categoryLevel3Index;
                    selectProductCategoryContains.TextContent = categoryList[categoryLevel3Index];
                }
                else if (categoryLevel1Index >= 0 && categoryLevel2Index >= 0)
                {
                    selectProductCategory.SelectedIndex = categoryLevel2Index;
                    selectProductCategoryContains.TextContent = categoryList[categoryLevel2Index];
                }
                else if (categoryLevel1Index >= 0)
                {
                    selectProductCategory.SelectedIndex = categoryLevel1Index;
                    selectProductCategoryContains.TextContent = categoryList[categoryLevel1Index];
                }
            }
            catch { }
            finally { }

            /* Điền mã HTML */
            try
            {
                updateComponentToContentHtml();

                if (rbtAddHtmlToContent.Checked)
                {
                    GeckoTextAreaElement textAeraContent = (GeckoTextAreaElement)gwbNhanhVNWeb.Document.GetElementById("content");
                    textAeraContent.Value = hEditor.contentHtml.ToString();
                }
                else if (rbtAddHtmlToDescription.Checked)
                {
                    GeckoTextAreaElement textAeraDescription = (GeckoTextAreaElement)gwbNhanhVNWeb.Document.GetElementById("description");
                    textAeraDescription.Value = hEditor.contentHtml.ToString();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút tải lại trang Nhanh.vn được nhấp chọn */
        private void btnReloadNhanhVNPage_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
                editByProductNameStep = 0;
                addImageByProductNameStep = 0;
                geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, gwbNhanhVNWeb.Url.ToString());
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút danh sách sản phẩm được nhấn */
        private void btnProductList_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
                editByProductNameStep = 0;
                addImageByProductNameStep = 0;
                geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItem);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút thêm sản phẩm mới được nhấn */
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
                editByProductNameStep = 0;
                addImageByProductNameStep = 0;
                geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItemAdd);
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút sửa sản phẩm theo tên được nhấn */
        private void btnEditByProductName_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
                addImageByProductNameStep = 0;

                if (!string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItemSearch + WebUtility.UrlEncode(txtProductName.Text.Trim()));
                    editByProductNameStep = 1;
                }
                else
                {
                    editByProductNameStep = 0;
                    displayMessageHidden("Tên sản phẩm không được bỏ trống");
                }
            }
            catch
            {
                editByProductNameStep = 0;
                displayMessageHidden("Không thể tìm được sản phẩm");
            }
            finally { }
        }

        /* Sự kiện khi nút lọc theo tên sản phẩm được nhấn */
        private void btnFilterByProductName_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
                editByProductNameStep = 0;
                addImageByProductNameStep = 0;

                if (!string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItemSearch + WebUtility.UrlEncode(txtProductName.Text.Trim()));
                }
                else
                {
                    displayMessageHidden("Tên sản phẩm không được bỏ trống");
                }
            }
            catch
            {
                displayMessageHidden("Không thể lọc được sản phẩm");
            }
            finally { }
        }

        /* Sự kiện khi nút thêm ảnh theo tên được nhấn */
        private void btnAddImageByProductName_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
                editByProductNameStep = 0;

                if (!string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItemSearch + WebUtility.UrlEncode(txtProductName.Text.Trim()));
                    addImageByProductNameStep = 1;
                }
                else
                {
                    addImageByProductNameStep = 0;
                    displayMessageHidden("Tên sản phẩm không được bỏ trống");
                }
            }
            catch
            {
                addImageByProductNameStep = 0;
                displayMessageHidden("Không thể tìm được sản phẩm");
            }
            finally { }
        }

        /* Sự kiện khi nút lấy mã PM được nhấn */
        private void btnGetProductCode_Click(object sender, EventArgs e)
        {
            try
            {
                requestProductCode();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi textbox giá bán thay đổi */
        private void txtProductPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblProductPrice.Text = string.Format("{0:#,0}", double.Parse(txtProductPrice.Text));
            }
            catch
            {
                lblProductPrice.Text = string.Empty;
            }
            finally { }
        }

        /* Sự kiện khi thêm vào khung được nhấp chọn */
        private void rbtAddHtmlToFrame_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;

                /* Lưu vào setting */
                Properties.Settings.Default.rbtAddHtmlToContent = rbtAddHtmlToContent.Checked;
                Properties.Settings.Default.rbtAddHtmlToDescription = rbtAddHtmlToDescription.Checked;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút cập nhật dữ liệu Form được nhấn */
        private void btnUpdateDataWeb_Click(object sender, EventArgs e)
        {
            try
            {
                if (gwbNhanhVNWeb.Url.ToString().Contains("/product/item/edit") || gwbNhanhVNWeb.Url.ToString().Contains("/product/item/add"))
                {
                    tabProductWeb.SelectedIndex = 0;
                    editByProductNameStep = 0;
                    addImageByProductNameStep = 0;

                    if (wordLength >= 100)
                    {
                        updateProductWeb();
                        displayMessageHidden("Dữ liệu Web đã được cập nhật");
                    }
                    else
                    {
                        MessageBox.Show("Để cập nhật dữ liệu Web thì số lượng từ\r\ntối thiếu phải là 100. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lưu dữ liệu Web được nhấn */
        private void btnSaveDataWeb_Click(object sender, EventArgs e)
        {
            try
            {
                if (gwbNhanhVNWeb.Url.ToString().Contains("/product/item/edit") || gwbNhanhVNWeb.Url.ToString().Contains("/product/item/add"))
                {
                    tabProductWeb.SelectedIndex = 0;
                    editByProductNameStep = 0;
                    addImageByProductNameStep = 0;
                    GeckoInputElement inputProductName = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("name");
                    inputProductName.Form.Submit();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi web browser nhanh VN tải thành công */
        private void gwbNhanhVNWeb_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            try
            {
                btnProductList.Enabled = true;
                btnAddProduct.Enabled = true;
                btnEditByProductName.Enabled = true;
                btnFilterByProductName.Enabled = true;
                btnAddImageByProductName.Enabled = true;

                mniProductList.Enabled = btnProductList.Enabled;
                mniAddProduct.Enabled = btnAddProduct.Enabled;
                mniEditByProductName.Enabled = btnEditByProductName.Enabled;
                mniFilterByProductName.Enabled = btnFilterByProductName.Enabled;
                mniAddImageByProductName.Enabled = btnAddImageByProductName.Enabled;

                displayMessageHidden("Tải url thành công");

                /* Tự động đăng nhập vào nhanh.VN */
                if (gwbNhanhVNWeb.Url.ToString().Contains("/user/signin"))
                {
                    /*
                    GeckoInputElement inputUsername = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("username");
                    GeckoInputElement inputPassword = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("password");

                    if (inputUsername != null && inputPassword != null)
                    {
                        inputUsername.Value = "tranthanhnam";
                        inputPassword.Value = "minhha123";
                        inputPassword.Form.Submit();
                    }
                    */

                    displayMessageHidden("Vui lòng đăng nhập Nhanh.vn Web");
                }

                /* Sửa sản phẩm theo tên */
                else if (gwbNhanhVNWeb.Url.ToString().Contains("product/item/edit"))
                {
                    try
                    {
                        GeckoInputElement inputProductPrice = (GeckoInputElement)gwbNhanhVNWeb.Document.GetElementById("price");
                        txtProductPrice.Text = inputProductPrice.Value;
                    }
                    catch { }
                    finally { }
                }

                /* Lọc sản phẩm theo tên */
                else if (gwbNhanhVNWeb.Url.ToString().Contains("/product/item/index"))
                {
                    if (editByProductNameStep == 1 || addImageByProductNameStep == 1)
                    {
                        /* Lọc lấy địa chỉ sửa */
                        GeckoElement tableProduct = (GeckoElement)gwbNhanhVNWeb.Document.GetElementById("dgProductStore");

                        if (tableProduct != null)
                        {
                            foreach (var node in tableProduct.ChildNodes)
                            {
                                if (node != null)
                                {
                                    if (node.NodeName.Trim() == "TBODY" || node.NodeName.Trim() == "tbody")
                                    {
                                        foreach (var node1 in node.ChildNodes)
                                        {
                                            if (node1 != null)
                                            {
                                                var node2 = node1.ChildNodes;

                                                for (uint i = 0; i < node2.Count(); i++)
                                                {
                                                    if (node2[i] != null)
                                                    {
                                                        if (node2[i].TextContent.Trim().Equals(txtProductName.Text.Trim()))
                                                        {
                                                            var node3 = node2[i].ParentNode.ChildNodes[1];

                                                            if (node3 != null)
                                                            {
                                                                foreach (var node4 in node3.ChildNodes)
                                                                {
                                                                    if (node4 != null)
                                                                    {
                                                                        if (node4.NodeName.Trim() == "A" || node4.NodeName.Trim() == "a")
                                                                        {
                                                                            if (editByProductNameStep == 1)
                                                                            {
                                                                                geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItemEdit + node4.TextContent.Trim());
                                                                                editByProductNameStep = 0;
                                                                                displayMessageHidden("Sản phẩm đã sẵn sàng để chỉnh sửa");
                                                                            }
                                                                            else if (addImageByProductNameStep == 1)
                                                                            {
                                                                                geckoWebBrowserLoad(GeckoWebName.geckoNhanhVNWeb, pWeb.webParam.urlProductItemDetail + node4.TextContent.Trim() + pWeb.webParam.productItemDetailGallery);
                                                                                addImageByProductNameStep = 2;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        editByProductNameStep = 0;
                                                                        addImageByProductNameStep = 0;
                                                                        displayMessageHidden("Không thể tìm được sản phẩm");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                editByProductNameStep = 0;
                                                                addImageByProductNameStep = 0;
                                                                displayMessageHidden("Không thể tìm được sản phẩm");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        editByProductNameStep = 0;
                                                        addImageByProductNameStep = 0;
                                                        displayMessageHidden("Không thể tìm được sản phẩm");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                editByProductNameStep = 0;
                                                addImageByProductNameStep = 0;
                                                displayMessageHidden("Không thể tìm được sản phẩm");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    editByProductNameStep = 0;
                                    addImageByProductNameStep = 0;
                                    displayMessageHidden("Không thể tìm được sản phẩm");
                                }
                            }
                        }
                        else
                        {
                            editByProductNameStep = 0;
                            addImageByProductNameStep = 0;
                            displayMessageHidden("Không thể tìm được sản phẩm");
                        }
                    }
                }

                /* Thêm ảnh sản phẩm theo tên */
                else if (gwbNhanhVNWeb.Url.ToString().Contains("/product/item/detail") && gwbNhanhVNWeb.Url.ToString().Contains("&tab=gallery"))
                {
                    if (addImageByProductNameStep == 2)
                    {
                        /* Upload ảnh từ link trực tiếp*/
                        List<string> LinkList = new List<string>();
                        for (int i = 0; i < txtProductPicture.Lines.Count(); i++)
                        {
                            if (!string.IsNullOrEmpty(txtProductPicture.Lines[i]))
                            {
                                LinkList.Add(txtProductPicture.Lines[i]);
                            }
                        }

                        using (Gecko.AutoJSContext js = new Gecko.AutoJSContext(gwbNhanhVNWeb.Window))
                        {
                            if (LinkList.Count > 1)
                            {
                                string script = "var dt = new ClipboardEvent('').clipboardData || new DataTransfer();";
                                script += "var buffStr;";
                                script += "var buffArr;";
                                script += "var file;";

                                for (int i = 1; i < LinkList.Count(); i++)
                                {
                                    string fileName = utils.convertNameToFileNameSimplify(txtProductName.Text.Trim(), "jpg");
                                    MemoryStream ms = pWeb.getMemoryStreamUsingHttpWebClient(LinkList[i]);

                                    byte[] imgBuffer = ms.ToArray();
                                    string strBuffer = BitConverter.ToString(imgBuffer).Replace("-", string.Empty);
                                    script += "buffStr = '" + strBuffer + "';";
                                    script += "buffArr = new Uint8Array(buffStr.length / 2);";
                                    script += "for (var i = 0; i < buffStr.length; i += 2) {buffArr[i / 2] = parseInt(buffStr.substring(i, i + 2), 16)}";
                                    script += "file = new File([buffArr], '" + fileName + "', {type: 'image/jpeg', lastModified: Date.now()});";
                                    script += "dt.items.add(file);";
                                }

                                script += "var fileUpload = document.getElementsByClassName('dz-hidden-input');";
                                script += "fileUpload[0].files = dt.files;";
                                script += "var event = document.createEvent('UIEvents');";
                                script += "event.initUIEvent('change', true, true);";
                                script += "fileUpload[0].dispatchEvent(event);";
                                string result = string.Empty;
                                js.EvaluateScript(script, out result);
                            }
                        }

                        addImageByProductNameStep = 0;
                        displayMessageHidden("Sản phẩm đã được thêm ảnh phụ");
                    }
                }

                /* Chủ đề sản phẩm */
                else if (gwbNhanhVNWeb.Url.ToString().Contains("/product/item/edit") || gwbNhanhVNWeb.Url.ToString().Contains("/product/item/add"))
                {
                    if (firstLoadCategory == true)
                    {
                        /* Đọc danh mục sản phẩm */
                        categoryList = new List<string>();

                        GeckoSelectElement selectProductCategory = (GeckoSelectElement)gwbNhanhVNWeb.Document.GetElementById("categoryId");
                        var nChild = selectProductCategory.GetElementsByTagName("option");

                        for (uint i = 0; i < nChild.Count(); i++)
                        {
                            categoryList.Add(nChild[i].TextContent);
                        }

                        categoryLevel1List = new List<CategoryData>();
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
                        cbxCategoryLevel3.DataSource = null;
                        cbxCategoryLevel1.Enabled = true;
                        cbxCategoryLevel2.Enabled = false;
                        cbxCategoryLevel3.Enabled = false;
                        cbxCategoryLevel1.DataSource = categoryLevel1List.ConvertAll(x => x.name.ToString()).ToList();

                        btnResetCategory.Enabled = true;
                        mniResetCategory.Enabled = btnResetCategory.Enabled;

                        /* Lưu danh mục sản phẩm ra tệp xml để đồng bộ */
                        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        folderPath = Path.Combine(folderPath, "BanLinhKien Editor", "categorys");
                        Directory.CreateDirectory(folderPath);
                        string fileXmlPath = folderPath + "\\categorys.xml";
                        saveCategoryToXmlFile(fileXmlPath);

                        cbxCategoryLevel1.SelectedIndex = Properties.Settings.Default.cbxCategoryLevel1;
                        cbxCategoryLevel2.SelectedIndex = Properties.Settings.Default.cbxCategoryLevel2;
                        cbxCategoryLevel3.SelectedIndex = Properties.Settings.Default.cbxCategoryLevel3;

                        firstLoadCategory = false;
                    }
                }
            }
            catch { }
            finally
            {
                btnCancelNavigating.Visible = false;
            }
        }

        /* Sự kiện khi web browser đang thực thi url */
        private void gwbNhanhVNWeb_Navigating(object sender, GeckoNavigatingEventArgs e)
        {
            try
            {
                btnProductList.Enabled = false;
                btnAddProduct.Enabled = false;
                btnEditByProductName.Enabled = false;
                btnFilterByProductName.Enabled = false;
                btnAddImageByProductName.Enabled = false;
                btnResetCategory.Enabled = false;

                mniProductList.Enabled = btnProductList.Enabled;
                mniAddProduct.Enabled = btnAddProduct.Enabled;
                mniEditByProductName.Enabled = btnEditByProductName.Enabled;
                mniFilterByProductName.Enabled = btnFilterByProductName.Enabled;
                mniAddImageByProductName.Enabled = btnAddImageByProductName.Enabled;
                mniResetCategory.Enabled = btnResetCategory.Enabled;

                btnCancelNavigating.Visible = true;
                displayMessage("Đang truy cập url: " + e.Uri.ToString());
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi timer TV Kho Search được tick */
        private void tmrTVKhoSearch_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrTVKhoSearch.Enabled = false;
                txtProductCode.Enabled = true;
                btnGetProductCode.Enabled = true;
                mniGetProductCode.Enabled = btnGetProductCode.Enabled;

                if (gwbTVKhoWeb.Url.ToString().Contains("/products"))
                {
                    if (!string.IsNullOrEmpty(txtProductName.Text.Trim()))
                    {
                        GeckoElementCollection nProductCode = gwbTVKhoWeb.Document.GetElementsByTagName("td");

                        for (uint i = 0; i < nProductCode.Count(); i++)
                        {
                            if (nProductCode[i] != null)
                            {
                                var nChild = nProductCode[i].ParentNode.ChildNodes;

                                foreach (var node in nChild)
                                {
                                    if (txtProductName.Text.Trim().Trim().Equals(node.TextContent.Trim()))
                                    {
                                        var nChild1 = node.ParentNode.ChildNodes;

                                        foreach (var node1 in nChild1)
                                        {
                                            if (node1.TextContent.Contains("LK_") || node1.TextContent.Contains("CNC_") || node1.TextContent.Contains("BB_"))
                                            {
                                                txtProductCode.Text = node1.TextContent;
                                                displayMessageHidden("Đã lấy được mã PM của sản phẩm");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi timer TV Kho Login được tick */
        private void tmrTVKhoLogin_Tick(object sender, EventArgs e)
        {
            try
            {
                if (gwbTVKhoWeb.Url.ToString().Contains("/login"))
                {
                    tmrTVKhoSearch.Enabled = false;

                    txtProductCode.Clear();
                    txtProductCode.Enabled = false;
                    btnGetProductCode.Enabled = false;
                    mniGetProductCode.Enabled = btnGetProductCode.Enabled;

                    int i = 0;
                    int loginInputIndex = 0;
                    GeckoInputElement loginInputElement = (GeckoInputElement)gwbTVKhoWeb.Document.GetElementById("mat-input-" + i.ToString());

                    while (loginInputElement == null)
                    {
                        i++;
                        loginInputElement = (GeckoInputElement)gwbTVKhoWeb.Document.GetElementById("mat-input-" + i.ToString());

                        if (i > 250)
                        {
                            i = 0;
                            break;
                        }
                    }
                    loginInputIndex = i;

                    switch (loginTVKhoStep)
                    {
                        case 2:
                            try
                            {
                                GeckoInputElement inputUsername = (GeckoInputElement)gwbTVKhoWeb.Document.GetElementById("mat-input-" + loginInputIndex.ToString());
                                inputUsername.Click();

                                foreach (char c in "admin@gmail.com")
                                {
                                    gwbTVKhoWeb.Window.WindowUtils.SendNativeKeyEvent(0, 0, 0, c.ToString(), c.ToString());
                                }
                            }
                            catch { }
                            finally { }
                            break;

                        case 4:
                            try
                            {
                                GeckoInputElement inputPassword = (GeckoInputElement)gwbTVKhoWeb.Document.GetElementById("mat-input-" + (loginInputIndex + 1).ToString());
                                inputPassword.Click();

                                foreach (char c in "minhha123")
                                {
                                    gwbTVKhoWeb.Window.WindowUtils.SendNativeKeyEvent(0, 0, 0, c.ToString(), c.ToString());
                                }
                            }
                            catch { }
                            finally { }
                            break;

                        case 6:
                            try
                            {
                                using (Gecko.AutoJSContext js = new Gecko.AutoJSContext(gwbTVKhoWeb.Window))
                                {
                                    string script = string.Empty;
                                    script += "var frm = document.getElementsByTagName('form')[0];";
                                    script += "var evt = document.createEvent('Event');";
                                    script += "evt.initEvent('submit', true, true);";
                                    script += "frm.dispatchEvent(evt);";
                                    string result = string.Empty;
                                    js.EvaluateScript(script, out result);
                                }
                            }
                            catch { }
                            finally { }
                            break;
                    }
                }
                else if (gwbTVKhoWeb.Url.ToString().Contains("/products"))
                {
                    if (tmrTVKhoSearch.Enabled == false)
                    {
                        txtProductCode.Enabled = true;
                        btnGetProductCode.Enabled = true;
                        mniGetProductCode.Enabled = btnGetProductCode.Enabled;
                    }
                }

                if (loginTVKhoStep++ > 20)
                {
                    loginTVKhoStep = 0;
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tab sản phẩm Web được thay đổi */
        private void tabProductWeb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabProductWeb.SelectedIndex == 0)
            {
                mniNhanhWeb.Checked = true;
                mniTVKhoWeb.Checked = false;
            }
            else if (tabProductWeb.SelectedIndex == 1)
            {
                mniTVKhoWeb.Checked = true;
                mniNhanhWeb.Checked = false;
            }
        }

        /***************************************************************/
        /*    Hàm và sự kiện tiến trình nền Web  */
        /***************************************************************/
        /* Hàm task load nội dung gecko webbrowser */
        public void geckoWebBrowserLoad(GeckoWebName geckoWebName, string content)
        {
            try
            {
                switch (geckoWebName)
                {
                    case GeckoWebName.geckoPreview:
                        bgwPreview.RunWorkerAsync(content);
                        break;

                    case GeckoWebName.geckoNhanhVNWeb:
                        bgwNhanhVNWeb.RunWorkerAsync(content);
                        break;

                    case GeckoWebName.geckoTVKhoWeb:
                        bgwTVKhoWeb.RunWorkerAsync(content);
                        break;
                }
            }
            catch { }
            finally { }
        }

        /* Hàm và sự kiện khi tiến trình nền xem trước được cập nhật */
        private void bgwPreview_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string content = (string)e.Argument;
                Invoke((MethodInvoker)delegate { gwbPreview.LoadHtml(content); });
            }
            catch { }
            finally { }
        }

        /* Hàm và sự kiện khi tiến trình nền nhanhvn được cập nhật */
        private void bgwNhanhVNWeb_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string content = (string)e.Argument;
                Invoke((MethodInvoker)delegate { gwbNhanhVNWeb.Navigate(content); });
            }
            catch { }
            finally { }
        }

        /* Hàm và sự kiện khi tiến trình nền tvkho được cập nhật */
        private void bgwTVKhoWeb_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string content = (string)e.Argument;
                Invoke((MethodInvoker)delegate { gwbTVKhoWeb.Navigate(content); });
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện danh mục lựa chọn  */
        /***************************************************************/
        /* Sự kiện khi menu thoát được nhấp chọn */
        private void mniExit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
                bgwGetDataFromHtml.Dispose();
                bgwNhanhVNWeb.Dispose();
                bgwPreview.Dispose();
                bgwTVKhoWeb.Dispose();
                bgwUpdateImageAfter.Dispose();
                bgwUploadToDrive.Dispose();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu chỉnh sửa HTML được nhấp chọn */
        private void mniHTMLEditor_Click(object sender, EventArgs e)
        {
            try
            {
                tabMain.SelectedIndex = 0;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu chỉnh sửa ảnh được nhấp chọn */
        private void mniImageEditor_Click(object sender, EventArgs e)
        {
            try
            {
                tabMain.SelectedIndex = 1;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu xem trước HTML được nhấp chọn */
        private void mniPreview_Click(object sender, EventArgs e)
        {
            try
            {
                tabMain.SelectedIndex = 2;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu sản phẩm Web được nhấp chọn */
        private void mniProductWeb_Click(object sender, EventArgs e)
        {
            try
            {
                tabMain.SelectedIndex = 3;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu TVkho được nhấp chọn */
        private void mniTVKhoWeb_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 1;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu nhanh được nhấp chọn */
        private void mniNhanhWeb_Click(object sender, EventArgs e)
        {
            try
            {
                tabProductWeb.SelectedIndex = 0;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi menu BLK Templator được nhấp chọn */
        private void mniBlkTemplator_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("blkTemplator.exe");
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

        /* Sự kiện khi menu hướng dẫn được nhấp chọn */
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
                tslStatus.Visible = false;
                tslStatus.Text = string.Empty;
                tmrHideMessage.Stop();
            }
            catch { }
            finally { }
        }

        /* Sự kiện timer theo dõi UI được tick */
        private void tmrMonitorUI_Tick(object sender, EventArgs e)
        {
            try
            {
                if (gwbNhanhVNWeb.Url.ToString().Contains("/product/item/edit") || gwbNhanhVNWeb.Url.ToString().Contains("/product/item/add"))
                {
                    rbtAddHtmlToDescription.Enabled = true;
                    rbtAddHtmlToContent.Enabled = true;

                    btnUpdateDataWeb.Enabled = true;
                    btnSaveDataWeb.Enabled = true;

                    mniUpdateDataWeb.Enabled = btnUpdateDataWeb.Enabled;
                    mniSaveDataWeb.Enabled = btnSaveDataWeb.Enabled;
                }
                else
                {
                    rbtAddHtmlToDescription.Enabled = false;
                    rbtAddHtmlToContent.Enabled = false;

                    btnUpdateDataWeb.Enabled = false;
                    btnSaveDataWeb.Enabled = false;

                    mniUpdateDataWeb.Enabled = btnUpdateDataWeb.Enabled;
                    mniSaveDataWeb.Enabled = btnSaveDataWeb.Enabled;
                }

                if (gwbTVKhoWeb.Url.ToString().Contains("/login"))
                {
                    txtProductCode.Clear();
                    txtProductCode.Enabled = false;
                    btnGetProductCode.Enabled = false;
                    mniGetProductCode.Enabled = btnGetProductCode.Enabled;
                }
                else if (gwbTVKhoWeb.Url.ToString().Contains("/products"))
                {
                    if (!tmrTVKhoSearch.Enabled)
                    {
                        txtProductCode.Enabled = true;
                        btnGetProductCode.Enabled = true;
                        mniGetProductCode.Enabled = btnGetProductCode.Enabled;
                    }
                }
                else
                {
                    tmrTVKhoSearch.Enabled = false;
                    txtProductCode.Clear();
                    txtProductCode.Enabled = false;
                    btnGetProductCode.Enabled = false;
                    mniGetProductCode.Enabled = btnGetProductCode.Enabled;
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút hủy truy cập được nhấn */
        private void btnCancelNavigating_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (tabProductWeb.SelectedIndex == 0)
                {
                    gwbNhanhVNWeb.Stop();
                    displayMessage(string.Empty);
                }
                else if (tabProductWeb.SelectedIndex == 1)
                {
                    gwbTVKhoWeb.Stop();
                    displayMessage(string.Empty);
                }

                btnCancelNavigating.Visible = false;
                txtProductPrice.Enabled = true;
                btnProductList.Enabled = true;
                btnAddProduct.Enabled = true;
                btnFilterByProductName.Enabled = true;
                btnEditByProductName.Enabled = true;
                btnAddImageByProductName.Enabled = true;
                btnUpdateDataWeb.Enabled = true;
                btnSaveDataWeb.Enabled = true;

                rbtAddHtmlToDescription.Enabled = true;
                rbtAddHtmlToContent.Enabled = true;

                mniProductList.Enabled = btnProductList.Enabled;
                mniAddProduct.Enabled = btnAddProduct.Enabled;
                mniFilterByProductName.Enabled = btnFilterByProductName.Enabled;
                mniEditByProductName.Enabled = btnEditByProductName.Enabled;
                mniAddImageByProductName.Enabled = btnAddImageByProductName.Enabled;
                mniUpdateDataWeb.Enabled = btnUpdateDataWeb.Enabled;
                mniSaveDataWeb.Enabled = btnSaveDataWeb.Enabled;

                editByProductNameStep = 0;
                addImageByProductNameStep = 0;
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm gecko webbrowser    */
        /***************************************************************/
        /* Sự kiện khi gecko webbrowser mở cửa sổ mới */
        private void geckoWebBrowser_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch { }
            finally { }
        }
    }
}
