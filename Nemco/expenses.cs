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
    public partial class expenses : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        static int expid;
       
        public expenses()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


            Random rnd = new Random();

            string id = rnd.Next(10000000, 99999999).ToString();

            expid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

            label11.Text = expid.ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void expenses_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                double cost = double.Parse(textBox2.Text);
                using (Model1 _entity = new Model1())
                {
                    var exp = new Expens() { ID = expid, Expense = textBox1.Text , Cost=cost };
                    _entity.Expenses.Add(exp);
                    _entity.SaveChanges();
                }



                textBox1.Clear();
                textBox2.Clear();

                Random rnd = new Random();

                string id = rnd.Next(10000000, 99999999).ToString();

                expid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

                label11.Text = expid.ToString();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
