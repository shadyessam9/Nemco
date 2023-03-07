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
    public partial class addsup : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        static int supid;
      
        public addsup()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

            Random rnd = new Random();

            string id = rnd.Next(10000000, 99999999).ToString();

            supid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

            label11.Text = supid.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (Model1 _entity = new Model1())
                {
                    var sup = new Supplier() { SupplierId = supid, SupplierName = textBox1.Text};
                    _entity.Suppliers.Add(sup);
                    _entity.SaveChanges();
                }



                textBox1.Clear();

                Random rnd = new Random();

                string id = rnd.Next(10000000, 99999999).ToString();

                supid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

                label11.Text = supid.ToString();
            }
        }

        private void addsup_MouseDown(object sender, MouseEventArgs e)
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
