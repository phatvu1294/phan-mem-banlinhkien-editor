using phatvu1294;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using Excel = Microsoft.Office.Interop.Excel;

namespace blkEditor
{
    public partial class frmRequest : Form
    {
        /***************************************************************/
        /*    Các thành phần toàn cục   */
        /***************************************************************/
        /* Biến class phatvu1294 */
        public MQTT mqtt = new MQTT();

        /* Biến kết nối MQTT */
        public MqttClient mqttClient;
        public CancellationTokenSource tokenSource = new CancellationTokenSource();

        /* Biến lưu danh sách request và hoàn thành */
        public List<string> doneProductNameList = new List<string>();
        public List<string> doneProductCodeList = new List<string>();
        public List<string> doneProductLocationList = new List<string>();
        public List<string> requestProductNameList = new List<string>();
        public List<string> requestProductCodeList = new List<string>();
        public List<string> requestProductLocationList = new List<string>();

        /* Biến tên sản phẩm chỉnh sửa */
        public string requestProductName;
        public int requestProductNameIndex;
        public int doneProductNameIndex;

        /***************************************************************/
        /*    Hàm get instance của form     */
        /***************************************************************/
        private static frmRequest _instance;
        public static frmRequest Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmRequest();
                }
                return _instance;
            }
        }

        /***************************************************************/
        /*    Hàm khởi tạo form     */
        /***************************************************************/
        public frmRequest()
        {
            InitializeComponent();
        }

        /***************************************************************/
        /*    Hàm và sự kiện form     */
        /***************************************************************/
        /* Hàm nhập danh sách yêu cầu từ tệp Excel */
        private void importRequestListFromExcelFile(string fileExcel)
        {
            try
            {
                bgwImportFromExcel.RunWorkerAsync(fileExcel);
            }
            catch { }
            finally { }
        }

        /* Hàm public danh sách tới MQTT Server */
        private void publicProductNameRequestDoneList()
        {
            try
            {
                MqttData mqttData = new MqttData();
                mqttData.requestProductNameList = requestProductNameList;
                mqttData.requestProductCodeList = requestProductCodeList;
                mqttData.requestProductLocationList = requestProductLocationList;
                mqttData.doneProductNameList = doneProductNameList;
                mqttData.doneProductCodeList = doneProductCodeList;
                mqttData.doneProductLocationList = doneProductLocationList;
                mqtt.publicDataToServer(mqttClient, mqttData);
                txtAddProductNameRequest.Clear();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được load */
        private void frmRequest_Load(object sender, EventArgs e)
        {
            try
            {
                tmrMonitorUI.Enabled = true;
                tmrHideMessage.Enabled = true;
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được đóng */
        private void frmRequest_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                /* Lưu vào Settings */
                Properties.Settings.Default.lbxProductNameRequestList = requestProductNameIndex;
                Properties.Settings.Default.lbxProductNameDoneList = doneProductNameIndex;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi form được hiển thị */
        private void frmRequest_Shown(object sender, EventArgs e)
        {
            try
            {
                /* Đặt lại tên */
                requestProductName = string.Empty;

                /* Lưu vào Settings */
                requestProductNameIndex = Properties.Settings.Default.lbxProductNameRequestList;
                doneProductNameIndex = Properties.Settings.Default.lbxProductNameDoneList;

            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa danh sách yêu cầu được nhấp chọn */
        private void btnClearProductNameRequestList_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn\r\nbộ danh sách yêu cầu chụp không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    requestProductNameList = new List<string>();
                    requestProductCodeList = new List<string>();
                    requestProductLocationList = new List<string>();
                    lbxProductNameRequestList.Items.Clear();
                    publicProductNameRequestDoneList();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút nhập danh sách yêu cầu từ Excel */
        private void btnImportRequestListFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Chọn tệp Excel cẩn mở";
                ofd.Filter = "Excel file|*.xls";
                DialogResult dr = ofd.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    importRequestListFromExcelFile(ofd.FileName);
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa danh sách hoàn thành được nhấp chọn */
        private void btnClearProductNameDoneList_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn\r\nbộ danh sách hoàn thành không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    doneProductNameList = new List<string>();
                    doneProductCodeList = new List<string>();
                    doneProductLocationList = new List<string>();
                    lbxProductNameDoneList.Items.Clear();
                    publicProductNameRequestDoneList();
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi text box thêm sản phẩm yêu cầu được nhấn Enter */
        private void txtAddProductNameRequest_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(txtAddProductNameRequest.Text.Trim()))
                    {
                        requestProductNameList.Add(txtAddProductNameRequest.Text.Trim());
                        requestProductCodeList.Add("null");
                        requestProductLocationList.Add("null");
                        publicProductNameRequestDoneList();
                    }
                    else
                    {
                        displayMessageHidden("Tên sản phẩm không được bỏ trống");
                    }
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút thêm sản phẩm yêu cầu được nhấp chọn */
        private void btnAddProductNameRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAddProductNameRequest.Text.Trim()))
                {
                    requestProductNameList.Add(txtAddProductNameRequest.Text.Trim());
                    requestProductCodeList.Add("null");
                    requestProductLocationList.Add("null");
                    publicProductNameRequestDoneList();
                }
                else
                {
                    displayMessageHidden("Tên sản phẩm không được bỏ trống");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút đánh dấu là đã hoàn thành được nhấp chọn */
        private void btnMarkProductAsDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxProductNameRequestList.SelectedItem != null)
                {
                    /* Thêm vào danh sách hoàn thành */
                    doneProductNameList.Add(requestProductNameList[lbxProductNameRequestList.SelectedIndex]);
                    doneProductCodeList.Add(requestProductCodeList[lbxProductNameRequestList.SelectedIndex]);
                    doneProductLocationList.Add(requestProductLocationList[lbxProductNameRequestList.SelectedIndex]);

                    /* Xoá khỏi danh sách yêu cầu */
                    List<string> removeList = new List<string>();
                    foreach (string item in requestProductNameList)
                    {
                        if (item.Equals(lbxProductNameRequestList.Items[lbxProductNameRequestList.SelectedIndex]))
                        {
                            removeList.Add(item);
                        }
                    }
                    foreach (string item in removeList)
                    {
                        requestProductCodeList.RemoveAt(requestProductNameList.IndexOf(item));
                        requestProductLocationList.RemoveAt(requestProductNameList.IndexOf(item));
                        requestProductNameList.Remove(item);
                    }

                    /* Đẩy danh sách lên MQTT */
                    publicProductNameRequestDoneList();
                }
                else
                {
                    displayMessageHidden("Phải chọn tên sản phẩm trước");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa yêu cầu được nhấp chọn */
        private void btnClearProductNameRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxProductNameRequestList.SelectedItem != null)
                {
                    /* Xoá khỏi danh sách yêu cầu */
                    List<string> removeList = new List<string>();
                    foreach (string item in requestProductNameList)
                    {
                        if (item.Equals(lbxProductNameRequestList.Items[lbxProductNameRequestList.SelectedIndex]))
                        {
                            removeList.Add(item);
                        }
                    }
                    foreach (string item in removeList)
                    {
                        requestProductCodeList.RemoveAt(requestProductNameList.IndexOf(item));
                        requestProductLocationList.RemoveAt(requestProductNameList.IndexOf(item));
                        requestProductNameList.Remove(item);
                    }

                    /* Đẩy lên MQTT */
                    publicProductNameRequestDoneList();
                }
                else
                {
                    displayMessageHidden("Phải chọn tên sản phẩm trước");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút xóa hoàn thành được nhấp chọn */
        private void btnClearProductNameDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxProductNameDoneList.SelectedItem != null)
                {
                    /* Xoá khỏi danh sách hoàn thành */
                    List<string> removeList = new List<string>();
                    foreach (string item in doneProductNameList)
                    {
                        if (item.Equals(lbxProductNameDoneList.Items[lbxProductNameDoneList.SelectedIndex]))
                        {
                            removeList.Add(item);
                        }
                    }
                    foreach (string item in removeList)
                    {
                        doneProductCodeList.RemoveAt(doneProductNameList.IndexOf(item));
                        doneProductLocationList.RemoveAt(doneProductNameList.IndexOf(item));
                        doneProductNameList.Remove(item);
                    }

                    /* Đẩy lên MQTT */
                    publicProductNameRequestDoneList();
                }
                else
                {
                    displayMessageHidden("Phải chọn tên sản phẩm trước");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi chỉnh sửa nhanh sản phẩm được nhấp chọn */
        private void btnEditWithThisProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxProductNameDoneList.SelectedItem != null)
                {
                    requestProductName = lbxProductNameDoneList.Items[lbxProductNameDoneList.SelectedIndex].ToString().Trim();
                    this.Close();
                }
                else
                {
                    displayMessageHidden("Phải chọn tên sản phẩm trước");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi nút yêu cầu chụp lại được nhấp chọn */
        private void btnProductNameRequestAgain_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxProductNameDoneList.SelectedItem != null)
                {
                    /* Thêm vào danh sách yêu cầu */
                    requestProductNameList.Add(doneProductNameList[lbxProductNameDoneList.SelectedIndex]);
                    requestProductCodeList.Add(doneProductCodeList[lbxProductNameDoneList.SelectedIndex]);
                    requestProductLocationList.Add(doneProductLocationList[lbxProductNameDoneList.SelectedIndex]);

                    /* Xoá khỏi danh sách hoàn thành */
                    List<string> removeList = new List<string>();
                    foreach (string item in doneProductNameList)
                    {
                        if (item.Equals(doneProductNameList[lbxProductNameDoneList.SelectedIndex]))
                        {
                            removeList.Add(item);
                        }
                    }
                    foreach (string item in removeList)
                    {
                        doneProductCodeList.RemoveAt(doneProductNameList.IndexOf(item));
                        doneProductLocationList.RemoveAt(doneProductNameList.IndexOf(item));
                        doneProductNameList.Remove(item);
                    }

                    /* Đẩy lên MQTT */
                    publicProductNameRequestDoneList();

                }
                else
                {
                    displayMessageHidden("Phải chọn tên sản phẩm trước");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi sản phẩm yêu cầu được sao chép */
        private void lbxProductNameRequestList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && (e.KeyCode == Keys.C))
                {
                    Clipboard.SetText(lbxProductNameRequestList.SelectedItem.ToString());
                    displayMessageHidden("Tên sản phẩm \"" + lbxProductNameRequestList.SelectedItem.ToString() + "\" đã được sao chép");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi sản phẩm hoàn thành được sao chép */
        private void lbxProductNameDoneList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && (e.KeyCode == Keys.C))
                {
                    Clipboard.SetText(lbxProductNameDoneList.SelectedItem.ToString());
                    displayMessageHidden("Tên sản phẩm \"" + lbxProductNameDoneList.SelectedItem.ToString() + "\" đã được sao chép");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi listbox yêu cầu thay đổi */
        private void lbxProductNameRequestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                requestProductNameIndex = lbxProductNameRequestList.SelectedIndex;
                Properties.Settings.Default.lbxProductNameRequestList = requestProductNameIndex;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi listbox hoàn thành thay đổi */
        private void lbxProductNameDoneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                doneProductNameIndex = lbxProductNameDoneList.SelectedIndex;
                Properties.Settings.Default.lbxProductNameDoneList = doneProductNameIndex;
                Properties.Settings.Default.Save();
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi timer cập nhật dao diện được tick */
        private void tmrMonitorUI_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lbxProductNameRequestList.Items.Count > 0)
                {
                    btnMarkProductAsDone.Enabled = true;
                    btnClearProductNameRequestList.Enabled = true;
                    btnClearProductNameRequest.Enabled = true;
                }
                else
                {
                    btnMarkProductAsDone.Enabled = false;
                    btnClearProductNameRequestList.Enabled = false;
                    btnClearProductNameRequest.Enabled = false;
                }

                if (lbxProductNameDoneList.Items.Count > 0)
                {
                    btnEditWithThisProduct.Enabled = true;
                    btnClearProductNameDoneList.Enabled = true;
                    btnClearProductNameDone.Enabled = true;
                    btnProductNameRequestAgain.Enabled = true;
                }
                else
                {
                    btnEditWithThisProduct.Enabled = false;
                    btnClearProductNameDoneList.Enabled = false;
                    btnClearProductNameDone.Enabled = false;
                    btnProductNameRequestAgain.Enabled = false;
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền được thay đổi */
        private void bgwImportFromExcel_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage >= 0 && e.ProgressPercentage < 100)
                {
                    lbxProductNameRequestList.Enabled = false;
                    lbxProductNameDoneList.Enabled = false;

                    btnClearProductNameRequestList.Enabled = false;
                    txtAddProductNameRequest.Enabled = false;
                    btnAddProductNameRequest.Enabled = false;
                    btnClearProductNameDoneList.Enabled = false;
                    btnImportRequestListFromExcel.Enabled = false;
                    btnMarkProductAsDone.Enabled = false;
                    btnClearProductNameRequest.Enabled = false;
                    btnEditWithThisProduct.Enabled = false;
                    btnProductNameRequestAgain.Enabled = false;
                    btnClearProductNameDone.Enabled = false;
                    tspImport.Visible = true;
                    tspImport.Value = e.ProgressPercentage;
                    tmrMonitorUI.Enabled = false;
                    displayMessage("Đang nhập tệp Excel: " + e.ProgressPercentage.ToString() + "%");
                }
                else if (e.ProgressPercentage == 100)
                {
                    lbxProductNameRequestList.Enabled = true;
                    lbxProductNameDoneList.Enabled = true;

                    btnClearProductNameRequestList.Enabled = true;
                    txtAddProductNameRequest.Enabled = true;
                    btnAddProductNameRequest.Enabled = true;
                    btnClearProductNameDoneList.Enabled = true;
                    btnImportRequestListFromExcel.Enabled = true;
                    btnMarkProductAsDone.Enabled = true;
                    btnClearProductNameRequest.Enabled = true;
                    btnEditWithThisProduct.Enabled = true;
                    btnProductNameRequestAgain.Enabled = true;
                    btnClearProductNameDone.Enabled = true;

                    tspImport.Visible = false;
                    tspImport.Value = 0;
                    tmrMonitorUI.Enabled = true;

                    displayMessageHidden("Tệp Excel đã được nhập");
                }
                else
                {
                    lbxProductNameRequestList.Enabled = true;
                    lbxProductNameDoneList.Enabled = true;

                    btnClearProductNameRequestList.Enabled = true;
                    txtAddProductNameRequest.Enabled = true;
                    btnAddProductNameRequest.Enabled = true;
                    btnClearProductNameDoneList.Enabled = true;
                    btnImportRequestListFromExcel.Enabled = true;
                    btnMarkProductAsDone.Enabled = true;
                    btnClearProductNameRequest.Enabled = true;
                    btnEditWithThisProduct.Enabled = true;
                    btnProductNameRequestAgain.Enabled = true;
                    btnClearProductNameDone.Enabled = true;

                    tspImport.Visible = false;
                    tspImport.Value = 0;
                    tmrMonitorUI.Enabled = true;

                    displayMessageHidden("Không thể nhập tệp Excel");
                }
            }
            catch { }
            finally { }
        }

        /* Sự kiện khi tiến trình nền nhập từ Excel làm việc */
        private void bgwImportFromExcel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                bgwImportFromExcel.ReportProgress(0);
                string fileExcelPath = e.Argument.ToString();

                Excel.Application oExcel = null;
                Excel.Workbook oBook = null;
                Excel.Worksheet oSheet = null;
                Excel.Range oRange = null;

                oExcel = new Excel.Application();
                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                oExcel.UserControl = false;

                oBook = oExcel.Workbooks.Open(fileExcelPath);
                oSheet = oBook.Sheets[1];
                oRange = oSheet.UsedRange;

                int rowCount = oRange.Rows.Count;
                int colCount = oRange.Columns.Count;

                for (int i = 2; i <= rowCount; i++)
                {
                    if (oRange.Cells[i, 1] != null && !string.IsNullOrEmpty(oRange.Cells[i, 1].Value2))
                    {
                        requestProductNameList.Add(!string.IsNullOrEmpty(oRange.Cells[i, 1].Value2) ? oRange.Cells[i, 1].Value2.ToString() : "null");
                        requestProductCodeList.Add(!string.IsNullOrEmpty(oRange.Cells[i, 2].Value2) ? oRange.Cells[i, 2].Value2.ToString() : "null");
                        requestProductLocationList.Add(!string.IsNullOrEmpty(oRange.Cells[i, 3].Value2) ? oRange.Cells[i, 3].Value2.ToString() : "null");
                    }
                    double percent = Math.Round((Convert.ToDouble(i - 2) / Convert.ToDouble(rowCount - 2)) * 98);
                    int progress = Convert.ToInt32(percent) + 1;
                    bgwImportFromExcel.ReportProgress(progress);
                }

                bgwImportFromExcel.ReportProgress(99);

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Marshal.ReleaseComObject(oRange);
                Marshal.ReleaseComObject(oSheet);

                oBook.Close();
                Marshal.ReleaseComObject(oBook);

                oExcel.Quit();
                Marshal.ReleaseComObject(oExcel);

                bgwImportFromExcel.ReportProgress(100);
            }
            catch
            {
                bgwImportFromExcel.ReportProgress(-1);
            }
            finally { }
        }

        /* Sự kiện khi tiến trình nhập từ Excel hoàn thành làm việc */
        private void bgwImportFromExcel_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                lbxProductNameRequestList.Enabled = true;
                lbxProductNameDoneList.Enabled = true;

                btnClearProductNameRequestList.Enabled = true;
                txtAddProductNameRequest.Enabled = true;
                btnAddProductNameRequest.Enabled = true;
                btnClearProductNameDoneList.Enabled = true;
                btnImportRequestListFromExcel.Enabled = true;
                btnMarkProductAsDone.Enabled = true;
                btnClearProductNameRequest.Enabled = true;
                btnEditWithThisProduct.Enabled = true;
                btnProductNameRequestAgain.Enabled = true;
                btnClearProductNameDone.Enabled = true;

                tspImport.Visible = false;
                tspImport.Value = 0;
                tmrMonitorUI.Enabled = true;

                publicProductNameRequestDoneList();
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
