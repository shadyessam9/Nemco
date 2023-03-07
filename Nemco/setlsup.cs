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
    public partial class setlsup : Form
    {

        string today = DateTime.Now.ToString("dd/MM/yyyy");

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public setlsup()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


            textBox1.Text = "0";
            textBox2.Text = "0";

            using (Model1 _entity = new Model1())
            {
                var supls = from supl in _entity.Suppliers orderby supl.SupplierName select supl;
                comboBox3.DataSource = supls.ToList();
                comboBox3.DisplayMember = "SupplierName";
                comboBox3.ValueMember = "SupplierId";

            }

        }

        private void setlsup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(textBox1.Text == "" || textBox2.Text == "" )
                {
                    MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int selectval;
                    bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out selectval);
                    using (Model1 _entity = new Model1())
                    {
                        var trc = new Transaction() { SupplierId = selectval, Credit = double.Parse(textBox1.Text), Debit = double.Parse(textBox2.Text) , DateTime = today };
                        _entity.Transactions.Add(trc);
                        _entity.SaveChanges();
                    }
                }
                
            }

            textBox1.Text = "0";
            textBox2.Text = "0";
            comboBox3.SelectedItem = null;


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Supliers s = new Supliers();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
