using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Nemco
{


    public partial class billaddress : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public static int bid;
      
        public billaddress()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

            Random rnd = new Random();

            string id = rnd.Next(10000000, 99999999).ToString();

            bid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

            label7.Text = bid.ToString();


            using (Model1 _entity = new Model1())
            {
                var emps = from emp in _entity.Employees select emp;
                comboBox1.DataSource = emps.ToList();
                comboBox1.DisplayMember = "EmpNme";
                comboBox1.ValueMember = "EmpNme";
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (Model1 _entity = new Model1())
            {
                var bill = new Bill() { BillId = bid  , agent=comboBox1.SelectedValue.ToString() , Customer=textBox1.Text };
                _entity.Bills.Add(bill);
                _entity.SaveChanges();
            }

            billing.bid = bid;
            billing b = new billing();
            this.Hide();
            b.ShowDialog();
            this.Close();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void billaddress_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
