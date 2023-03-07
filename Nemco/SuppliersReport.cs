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
    public partial class SuppliersReport : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public SuppliersReport()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


            using (Model1 _entity = new Model1())
            {
                var supls = from supl in _entity.Suppliers orderby supl.SupplierName select supl;
                comboBox3.DataSource = supls.ToList();
                comboBox3.DisplayMember = "SupplierName";
                comboBox3.ValueMember = "SupplierId";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Suppliersrpt sr = new Suppliersrpt();

            int selectval;
            bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out selectval);

            string date1 = dateTimePicker1.Value.ToShortDateString();
            string date2 = dateTimePicker2.Value.ToShortDateString();


            TextObject d1 = (TextObject)sr.ReportDefinition.Sections["Section1"].ReportObjects["d1"];
            d1.Text = date1;
            TextObject d2 = (TextObject)sr.ReportDefinition.Sections["Section1"].ReportObjects["d2"];
            d2.Text = date2;


            

            SqlConnection con = new SqlConnection("data source=.;initial catalog=nemco;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            con.Open();
            string command = "select * from Suppliers left join Transactions on Suppliers.SupplierId = Transactions.SupplierId where Suppliers.SupplierId = '"+selectval+"' and  Transactions.DateTime between '"+date1+"' and '"+date2+"'";
            SqlDataAdapter sd = new SqlDataAdapter(command, con);
            DataSet s = new DataSet();
            sd.Fill(s, "Table1");
            con.Close();
            sr.SetDataSource(s.Tables["Table1"]);
            crystalReportViewer1.ReportSource = sr;
            crystalReportViewer1.Refresh();



        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void SuppliersReport_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
