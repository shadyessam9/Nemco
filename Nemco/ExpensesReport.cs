using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemco
{
    public partial class ExpensesReport : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public ExpensesReport()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string date1 = dateTimePicker1.Value.ToShortDateString();
            string date2 = dateTimePicker2.Value.ToShortDateString();


            Epxensesrpt ert = new Epxensesrpt();

         


            string total;

            using (Model1 _entity = new Model1())
            {
                var tot = from exp in _entity.Expenses where exp.DateTime.CompareTo(date1) >= 0 && exp.DateTime.CompareTo(date2) <= 0 select exp;
                total = tot.AsEnumerable().Sum(tt => tt.Cost).ToString();
               
            }

            TextObject d1 = (TextObject)ert.ReportDefinition.Sections["Section1"].ReportObjects["date1"];
            d1.Text = date1;
            TextObject d2 = (TextObject)ert.ReportDefinition.Sections["Section1"].ReportObjects["date2"];
            d2.Text = date2;
            TextObject t = (TextObject)ert.ReportDefinition.Sections["Section1"].ReportObjects["t"];
            t.Text = total;

            SqlConnection con = new SqlConnection("data source=.;initial catalog=nemco;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            con.Open();
            string command = "select * from Expenses  where DateTime between  '" + date1 + "'  and  '" + date2 + "' ";
            SqlDataAdapter sd = new SqlDataAdapter(command, con);
            DataSet s = new DataSet();
            sd.Fill(s, "Table1");
            con.Close();
            ert.SetDataSource(s.Tables["Table1"]);
            crystalReportViewer1.ReportSource = ert;
            crystalReportViewer1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void ExpensesReport_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
    }
