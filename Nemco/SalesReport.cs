using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Nemco
{
    public partial class SalesReport : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public SalesReport()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

        }

        private void button2_Click(object sender, EventArgs e)
        {


            string date1 = dateTimePicker1.Value.ToShortDateString();
            string date2 = dateTimePicker2.Value.ToShortDateString();

            string itemscount;
            string total;
            string prft;
            string dis;


            using (Model1 _entity = new Model1())
            {
                var itemssold = from bi in _entity.BillItems join b in _entity.Bills on bi.BillId equals b.BillId where b.DateTime.CompareTo(date1) >= 0 && b.DateTime.CompareTo(date2) <= 0 select bi;
                itemscount = itemssold.AsEnumerable().Sum(q => q.ItemQuan).ToString();
                var sales = from sls in _entity.Sales where sls.DateTime.CompareTo(date1) >= 0 && sls.DateTime.CompareTo(date2) <= 0 select sls;
                total = sales.AsEnumerable().Sum(sls => sls.Total).ToString();
                var profit = from prf in _entity.Profits where prf.DateTime.CompareTo(date1) >= 0 && prf.DateTime.CompareTo(date2) <= 0 select prf;
                prft = profit.AsEnumerable().Sum(prf => prf.Profit1).ToString();
                var discounts = from dsc in _entity.Discounts join b in _entity.Bills on dsc.BillId equals b.BillId where b.DateTime.CompareTo(date1) >= 0 && b.DateTime.CompareTo(date2) <= 0 select dsc;
                dis = discounts.AsEnumerable().Sum(dsc => dsc.Discount1).ToString();
            }



               


            Salesrpt srt = new Salesrpt();

            TextObject d1 = (TextObject)srt.ReportDefinition.Sections["Section1"].ReportObjects["date1"];
            d1.Text = date1;
            TextObject d2 = (TextObject)srt.ReportDefinition.Sections["Section1"].ReportObjects["date2"];
            d2.Text = date2;
            TextObject n = (TextObject)srt.ReportDefinition.Sections["Section1"].ReportObjects["n"];
            n.Text = itemscount;
            TextObject t = (TextObject)srt.ReportDefinition.Sections["Section1"].ReportObjects["t"];
            t.Text = total;
            TextObject p = (TextObject)srt.ReportDefinition.Sections["Section1"].ReportObjects["p"];
            p.Text = prft;
            TextObject d = (TextObject)srt.ReportDefinition.Sections["Section1"].ReportObjects["d"];
            d.Text = dis;


            SqlConnection con = new SqlConnection("data source=.;initial catalog=nemco;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            con.Open();
            string command = "select * from Bills left join BillItems on BIlls.BillId=BillItems.BillId left join Items on Items.ItemId = BillItems.ItemId  left join Discounts on Bills.BillId = Discounts.BillId where Bills.DateTime between  '"+date1+"'  and  '"+date2+"' ";
            SqlDataAdapter sd = new SqlDataAdapter(command, con);
            DataSet s = new DataSet();
            sd.Fill(s, "Table1");
            con.Close();
            srt.SetDataSource(s.Tables["Table1"]);
            crystalReportViewer1.ReportSource = srt;
            crystalReportViewer1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void SalesReport_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
