using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemco
{
    public partial class Billprint : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public static int bid { get; set; }
        public Billprint()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

            SqlConnection con = new SqlConnection("data source=.;initial catalog=nemco;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            con.Open();
            string command = "select * from Bills left join BillItems on BIlls.BillId=BillItems.BillId left join Items on Items.ItemId = BillItems.ItemId  left join Discounts on Bills.BillId = Discounts.BillId where Bills.BillId=" + bid.ToString() + "";
            SqlDataAdapter sd = new SqlDataAdapter(command, con);
            DataSet s = new DataSet();
            sd.Fill(s,"Table1");
            con.Close();
            Billprt brt = new Billprt();

            TextObject text = (TextObject)brt.ReportDefinition.Sections["Section3"].ReportObjects["Text1"];

            brt.SetDataSource(s.Tables["Table1"]);
            crystalReportViewer1.ReportSource = brt;
            crystalReportViewer1.Refresh();

        }

        private void crystalReportViewer1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void Billprint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
