using phatvu1294;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace blkEditor
{
    public partial class frmGetDrive : Form
    {
        /***************************************************************/
        /*    Các thành phần toàn cục   */
        /***************************************************************/
        /* Cấu trúc dữ liệu File Drive Order By */
        public struct FileDriveOrderByData
        {
            public string name;
            public DriveOrderBy orderBy;
        }

        /* Cấu trúc dữ thông số File Drive cho tiến trình nền */
        public struct FileDriveListParam
        {
            public string filterName;
            public DriveOrderBy orderBy;
            public DriveDirOrderBy dirOrderBy;
        }

        /* Cấu trúc dữ trả về File Drive cho tiến trình nền */
        public struct FileDriveListReturn
        {
            public Image imgPicker;
            public string fileName;
            public string filePath;
            public string fileId;
            public int imgPickerIndex;
        }

        /* Các thành phần toàn cục */
        public GoogleDrive gDrive = new GoogleDrive();
        public string urlDriveThumbnail = string.Empty;
        private Utilities utils = new Utilities();

        /* Các thành phần toàn cục */
        public string filterProductName = string.Empty;
        public bool isGetDriveDone = false;
        private int dirOrderBy = 0;

        /* Các thành phần toàn cục */
        public List<string> lstFilePathSelected = new List<string>();
        private List<FileDriveOrderByData> lstComboboxOrderBy = new List<FileDriveOrderByData>();
        private List<int> lstFileIndexSelected = new List<int>();

        /***************************************************************/
        /*    Hàm get thể hiện của form     */
        /***************************************************************/
        private static frmGetDrive _instance;
        public static frmGetDrive Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmGetDrive();
                }

                return _instance;
            }
        }

        /***************************************************************/
        /*    Hàm khởi tạo form     */
        /***************************************************************/
        public frmGetDrive()
        {
            InitializeComponent();
        }

        /***************************************************************/
        /*    Hàm và sự kiện form   */
        /***************************************************************/
        /* Sự kiện khi form được load */
        private void frmGetDrive_Load(object sender, EventArgs e)
        {
            try
            {
                dirOrderBy = 0;

                lsvFileDriveList.HideSelection = false;
                lsvFileDriveList.Enabled = true;

                cbxFileDriveListOrderBy.Items.Clear();
                lstComboboxOrderBy = new List<FileDriveOrderByData>();
                FileDriveOrderByData comboboxOrderBySource = new FileDriveOrderByData();

                comboboxOrderBySource.name = "Sửa đổi lần cuối";
                comboboxOrderBySource.orderBy = DriveOrderBy.modifiedTime;
                lstComboboxOrderBy.Add(comboboxOrderBySource);
          
                comboboxOrderBySource.name = "Thời gian tạo";
                comboboxOrderBySource.orderBy = DriveOrderBy.createdTime;
                lstComboboxOrderBy.Add(comboboxOrderBySource);

                comboboxOrderBySource.name = "Kích thước";
                comboboxOrderBySource.orderBy = DriveOrderBy.quotaBytesUsed;
                lstComboboxOrderBy.Add(comboboxOrderBySource);

                comboboxOrderBySource.name = "Tên";
                comboboxOrderBySource.orderBy = DriveOrderBy.name;
                lstComboboxOrderBy.Add(comboboxOrderBySource);

                foreach (FileDriveOrderByData item in lstComboboxOrderBy)
                {
                    cbxFileDriveListOrderBy.Items.Add(item.name);
                }

                if (cbxFileDriveListOrderBy.Items.Count > 0) cbxFileDriveListOrderBy.SelectedIndex = Properties.Settings.Default.cbxFileDriveListOrderBy;

                btnImageDriveListDirOrderBy.BackgroundImage = Properties.Resources.sort_down;

                tslStatus.Visible = false;
                tspLoadDownloadFileDriveList.Visible = false;
                tmrHideMessage.Enabled = true;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được hiển thị */
        private void frmGetDrive_Shown(object sender, EventArgs e)
        {
            try
            {
                isGetDriveDone = false;
                lstFilePathSelected = new List<string>();
                lstFileIndexSelected = new List<int>();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được đóng */
        private void frmGetDrive_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                /* Lưu vào Settings */
                Properties.Settings.Default.cbxFileDriveListOrderBy = cbxFileDriveListOrderBy.SelectedIndex;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /***************************************************************/
        /*    Hàm và sự kiện get Drive   */
        /***************************************************************/
        /* Hàm load danh sách tệp theo tên */
        private void loadFileDriveListByName()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFilterFileDriveByName.Text.Trim()))
                {
                    lsvFileDriveList.Clear();
                    tspLoadDownloadFileDriveList.Visible = true;

                    FileDriveListParam loadFileDriveListParam;
                    loadFileDriveListParam.filterName = utils.convertNameToFileDriveName(txtFilterFileDriveByName.Text.Trim());
                    loadFileDriveListParam.orderBy = lstComboboxOrderBy[cbxFileDriveListOrderBy.SelectedIndex].orderBy;
                    loadFileDriveListParam.dirOrderBy = (DriveDirOrderBy)dirOrderBy;

                    if (bgwLoadImageDriveList.IsBusy)
                    {
                        bgwLoadImageDriveList.CancelAsync();
                    }
                    else
                    {
                        bgwLoadImageDriveList.RunWorkerAsync(loadFileDriveListParam);
                    }

                    while (bgwLoadImageDriveList.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút lọc danh sách ảnh theo tên được nhấp chọn */
        private void btnFilterFileDriveListByName_Click(object sender, EventArgs e)
        {
            try
            {
                txtFilterFileDriveByName.Text = filterProductName.Trim();

                if (txtFilterFileDriveByName.Text != string.Empty)
                {
                    loadFileDriveListByName();
                }
                else
                {
                    lsvFileDriveList.Clear();
                    displayMessageHidden("Tên sản phẩm không được bỏ trống");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi text box tên nhấn phím Enter */
        private void txtFilterFileDriveByProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtFilterFileDriveByName.Text.Trim() != string.Empty)
                    {
                        loadFileDriveListByName();
                    }
                    else
                    {
                        displayMessageHidden("Tên sản phẩm không được bỏ trống");
                    }
                }
                catch { }
                finally { }
            }
        }

        /* Sự kiện khi textbox lọc sản phẩm theo tên được thay đổi */
        private void txtFilterFileDriveByName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFilterFileDriveByName.Text.Trim()))
                {
                    lsvFileDriveList.Clear();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi combobox được thay đổi */
        private void cbxFileDriveListOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadFileDriveListByName();

                /* Lưu vào settings */
                Properties.Settings.Default.cbxFileDriveListOrderBy = cbxFileDriveListOrderBy.SelectedIndex;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đảo hướng sắp xếp được nhấn */
        private void btnImageDriveListDirOrderBy_Click(object sender, EventArgs e)
        {
            try
            {
                if (dirOrderBy == (int)DriveDirOrderBy.desc)
                {
                    dirOrderBy = (int)DriveDirOrderBy.asc;
                    btnImageDriveListDirOrderBy.BackgroundImage = Properties.Resources.sort_up;
                }
                else if (dirOrderBy == (int)DriveDirOrderBy.asc)
                {
                    dirOrderBy = (int)DriveDirOrderBy.desc;
                    btnImageDriveListDirOrderBy.BackgroundImage = Properties.Resources.sort_down;
                }
                loadFileDriveListByName();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi lựa chọn thành công được nhấp chọn */
        private void btnGetDriveDone_Click(object sender, EventArgs e)
        {
            try
            {
                isGetDriveDone = true;

                ListView.SelectedListViewItemCollection items = lsvFileDriveList.SelectedItems;
                lstFileIndexSelected = new List<int>();

                foreach (ListViewItem item in items)
                {
                    lstFileIndexSelected.Add(item.Index);
                }

                if (lstFileIndexSelected.Count > 0)
                {
                    bgwDownloadImageDrive.RunWorkerAsync();
                }
                else
                {
                    lstFilePathSelected = new List<string>();
                    lstFileIndexSelected = new List<int>();
                    this.Close();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền load danh sách tệp được thay đổi */
        private void bgwLoadImageDriveList_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    btnFilterFileDriveListByName.Enabled = false;
                    txtFilterFileDriveByName.Enabled = false;
                    cbxFileDriveListOrderBy.Enabled = false;
                    btnImageDriveListDirOrderBy.Enabled = false;
                    btnGetDriveDone.Enabled = false;
                    lsvFileDriveList.Enabled = false;
                    tspLoadDownloadFileDriveList.Visible = true;
                    tspLoadDownloadFileDriveList.Value = e.ProgressPercentage;
                    displayMessage("Đang tìm tệp với truy vấn \"" + txtFilterFileDriveByName.Text.Trim() + "\"");
                }
                else if (e.ProgressPercentage == 100)
                {
                    btnFilterFileDriveListByName.Enabled = true;
                    txtFilterFileDriveByName.Enabled = true;
                    cbxFileDriveListOrderBy.Enabled = true;
                    btnImageDriveListDirOrderBy.Enabled = true;
                    btnGetDriveDone.Enabled = true;
                    lsvFileDriveList.Enabled = true;
                    tspLoadDownloadFileDriveList.Value = 0;
                    tspLoadDownloadFileDriveList.Visible = false;
                    displayMessageHidden("Đã tìm thấy tệp truy vấn");
                }
                else
                {
                    btnFilterFileDriveListByName.Enabled = true;
                    txtFilterFileDriveByName.Enabled = true;
                    cbxFileDriveListOrderBy.Enabled = true;
                    btnImageDriveListDirOrderBy.Enabled = true;
                    btnGetDriveDone.Enabled = true;
                    lsvFileDriveList.Enabled = true;
                    tspLoadDownloadFileDriveList.Value = 0;
                    tspLoadDownloadFileDriveList.Visible = false;
                    displayMessageHidden("Không thể tìm thấy tệp");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền load danh sách tệp làm việc */
        private void bgwLoadImageDriveList_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<FileDriveListReturn> ret = new List<FileDriveListReturn>();
            try
            {
                bgwLoadImageDriveList.ReportProgress(0);

                string _filterName = ((FileDriveListParam)e.Argument).filterName;
                DriveOrderBy _orderBy = ((FileDriveListParam)e.Argument).orderBy;
                DriveDirOrderBy _dirOrderBy = ((FileDriveListParam)e.Argument).dirOrderBy;
                gDrive.getFileDriveListWithName(_filterName, _orderBy, _dirOrderBy);
                int _imgPickerIndex = 0;
                int i = 0;

                bgwLoadImageDriveList.ReportProgress(1);

                if (gDrive.fileDriveList.Count > 0)
                {
                    foreach (DriveData fileDrive in gDrive.fileDriveList)
                    {
                        string fileName = fileDrive.fileName.Trim();
                        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        folderPath = Path.Combine(folderPath, "BanLinhKien Editor", "thumbnails", DateTime.Now.ToString("yyyy-MM-dd"));
                        Directory.CreateDirectory(folderPath);
                        string filePath = folderPath + "\\" + fileName;

                        if (!File.Exists(filePath))
                        {
                            MemoryStream ms = gDrive.getMemoryStreamDriveThumbnail(fileDrive.fileId);

                            using (var img = Image.FromStream(ms))
                            {
                                img.Save(filePath, ImageFormat.Jpeg);
                            }
                        }

                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            MemoryStream msf = new MemoryStream();
                            fs.CopyTo(msf);
                            msf.Seek(0, SeekOrigin.Begin);

                            FileDriveListReturn loadFileDriveListReturn;
                            loadFileDriveListReturn.imgPicker = Image.FromStream(msf);
                            loadFileDriveListReturn.fileName = fileDrive.fileName;
                            loadFileDriveListReturn.filePath = filePath;
                            loadFileDriveListReturn.fileId = fileDrive.fileId;
                            loadFileDriveListReturn.imgPickerIndex = _imgPickerIndex;

                            ret.Add(loadFileDriveListReturn);
                            _imgPickerIndex++;
                        }

                        double percent = Math.Round((Convert.ToDouble(i) / Convert.ToDouble(gDrive.fileDriveList.Count)) * 97);
                        int progress = Convert.ToInt32(percent) + 2;
                        bgwLoadImageDriveList.ReportProgress(Convert.ToInt32(percent));
                        i++;
                    } 
                }

                bgwLoadImageDriveList.ReportProgress(100);

                e.Result = ret;
            }
            catch
            {
                bgwLoadImageDriveList.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi tiến trình nền load danh sách tệp làm việc hoàn thành */
        private void bgwLoadImageDriveList_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                ImageList imgPickerList = new ImageList();
                imgPickerList.ImageSize = new Size(100, 75);
                lsvFileDriveList.LargeImageList = imgPickerList;

                if (((List<FileDriveListReturn>)e.Result).Count > 0)
                {
                    foreach (FileDriveListReturn item in (List<FileDriveListReturn>)e.Result)
                    {
                        imgPickerList.Images.Add(item.imgPicker);
                        lsvFileDriveList.Items.Add(item.fileName, item.imgPickerIndex);
                    }

                    displayMessageHidden("Đã tìm thấy " + ((List<FileDriveListReturn>)e.Result).Count.ToString() + " tệp với truy vấn \"" + txtFilterFileDriveByName.Text.Trim() + "\"");
                }
                else
                {
                    displayMessageHidden("Không thể tìm được tệp");
                }

                btnFilterFileDriveListByName.Enabled = true;
                txtFilterFileDriveByName.Enabled = true;
                cbxFileDriveListOrderBy.Enabled = true;
                btnImageDriveListDirOrderBy.Enabled = true;
                btnGetDriveDone.Enabled = true;
                lsvFileDriveList.Enabled = true;
                tspLoadDownloadFileDriveList.Value = 0;
                tspLoadDownloadFileDriveList.Visible = false;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình tải ảnh được thay đổi */
        private void bgwDownloadImageDrive_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    btnFilterFileDriveListByName.Enabled = false;
                    txtFilterFileDriveByName.Enabled = false;
                    cbxFileDriveListOrderBy.Enabled = false;
                    btnImageDriveListDirOrderBy.Enabled = false;
                    btnGetDriveDone.Enabled = false;
                    lsvFileDriveList.Enabled = false;
                    tspLoadDownloadFileDriveList.Visible = true;
                    tspLoadDownloadFileDriveList.Value = e.ProgressPercentage;
                    displayMessage("Đang tải danh sách tệp vui lòng chờ");
                }
                else if (e.ProgressPercentage == 100)
                {
                    btnFilterFileDriveListByName.Enabled = true;
                    txtFilterFileDriveByName.Enabled = true;
                    cbxFileDriveListOrderBy.Enabled = true;
                    btnImageDriveListDirOrderBy.Enabled = true;
                    btnGetDriveDone.Enabled = true;
                    lsvFileDriveList.Enabled = true;
                    tspLoadDownloadFileDriveList.Value = 0;
                    tspLoadDownloadFileDriveList.Visible = false;
                    displayMessageHidden("Danh sách tệp đã được tải xuống");
                }
                else
                {
                    btnFilterFileDriveListByName.Enabled = true;
                    txtFilterFileDriveByName.Enabled = true;
                    cbxFileDriveListOrderBy.Enabled = true;
                    btnImageDriveListDirOrderBy.Enabled = true;
                    btnGetDriveDone.Enabled = true;
                    lsvFileDriveList.Enabled = true;
                    tspLoadDownloadFileDriveList.Value = 0;
                    tspLoadDownloadFileDriveList.Visible = false;
                    displayMessageHidden("Không thể tải được danh sách tệp");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền tải ảnh làm việc */
        private void bgwDownloadImageDrive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                bgwDownloadImageDrive.ReportProgress(0);
                lstFilePathSelected = new List<string>();

                int x = 0;
                foreach (int index in lstFileIndexSelected)
                {
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    folderPath = Path.Combine(folderPath, "BanLinhKien Editor", "raw", DateTime.Now.ToString("yyyy-MM-dd"));
                    Directory.CreateDirectory(folderPath);

                    string fileName = gDrive.fileDriveList[index].fileName;
                    string fileId = gDrive.fileDriveList[index].fileId;
                    string filePath = folderPath + "\\" + fileName;

                    if (!File.Exists(filePath))
                    {
                        gDrive.downloadFileFromDrive(fileId, filePath);
                    }

                    lstFilePathSelected.Add(filePath);

                    double percent = Math.Round((Convert.ToDouble(x) / Convert.ToDouble(gDrive.fileDriveList.Count)) * 99);
                    int progress = Convert.ToInt32(percent) + 1;
                    bgwDownloadImageDrive.ReportProgress(progress);
                    x++;
                }
                bgwDownloadImageDrive.ReportProgress(100);
            }
            catch
            {
                bgwDownloadImageDrive.ReportProgress(-1);
                lstFilePathSelected = new List<string>();
            }
            finally { }
        }

        /* Sự kiện khi tiến trình nền tải ảnh làm việc hoàn thành */
        private void bgwDownloadImageDrive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                btnFilterFileDriveListByName.Enabled = true;
                txtFilterFileDriveByName.Enabled = true;
                cbxFileDriveListOrderBy.Enabled = true;
                btnImageDriveListDirOrderBy.Enabled = true;
                btnGetDriveDone.Enabled = true;
                lsvFileDriveList.Enabled = true;
                tspLoadDownloadFileDriveList.Value = 0;
                tspLoadDownloadFileDriveList.Visible = false;
                this.Close();
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
    }
}
