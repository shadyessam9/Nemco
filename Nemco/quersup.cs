using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemco
{
    public partial class quersup : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public quersup()
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

        private void button1_Click(object sender, EventArgs e)
        {
            int selectval;
            bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out selectval);

            using (Model1 _entity = new Model1())
            {
                Supplier sup = (from s in _entity.Suppliers where s.SupplierId == selectval select s).First();
                label8.Text = sup.SupplierId.ToString();
                label10.Text = sup.Balance.ToString();
                double? bal = sup.Balance;
            }
        }

        private void quersup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Supliers s = new Supliers();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }
    }
}
