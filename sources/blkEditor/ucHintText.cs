using System.Drawing;
using System.Windows.Forms;

namespace blkEditor
{
public partial class ucHintText : UserControl
{
    /***************************************************************
        Hàm get instance của user control
    ***************************************************************/
    private static ucHintText _instance;
    public static ucHintText Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ucHintText();
            }
            return _instance;
        }
    }

    /***************************************************************
        Hàm set nội dung hiển thị
    ***************************************************************/
    public string setTemplateHint
    {
        set
        {
            txtHintText.Text = value;
        }
    }

    /***************************************************************
        Hàm set font hiển thị
    ***************************************************************/
    public Font setTemplateFont
    {
        set
        {
            txtHintText.Font = value;
        }
    }

    /***************************************************************
        Hàm get độ rộng hiển thị
    ***************************************************************/
    public int getTemplateWidth
    {
        get
        {
            int txtWidth = txtHintText.Width;
            return txtWidth;
        }
    }

    public ucHintText()
    {
        InitializeComponent();
        txtHintText.ScrollBars = RichTextBoxScrollBars.None;
    }
}
}
