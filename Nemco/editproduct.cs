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
    public partial class editproduct : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        public static int iid;
        public editproduct()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

            using (Model1 _entity = new Model1())
            {


                var depts = from d in _entity.Departments orderby d.DeptName select d;
                comboBox2.DataSource = depts.ToList();
                comboBox2.DisplayMember = "DeptName";
                comboBox2.ValueMember = "DeptId";



            }




        }

        private void button7_Click(object sender, EventArgs e)
        {
            warehousing w = new warehousing();
            this.Hide();
            w.ShowDialog();
            this.Close();
        }

        private void editproduct_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectval;
            bool parseOK = Int32.TryParse(comboBox2.SelectedValue.ToString(), out selectval);

            using (Model1 _entity = new Model1())
            {
                var items = from item in _entity.Items join wh in _entity.Warehouses on item.ItemId equals wh.ItemId where item.DeptId == selectval orderby item.ItemName select item;
                comboBox3.DataSource = items.ToList();
                comboBox3.DisplayMember = "ItemName";
                comboBox3.ValueMember = "ItemId";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out iid);
            using (Model1 _entity = new Model1())
            {
                Item item = (from i in _entity.Items join wh in _entity.Warehouses on i.ItemId equals wh.ItemId where i.ItemId == iid  select i).First();
                textBox1.Text = item.ItemName;
                textBox2.Text = item.Cost.ToString();
                textBox3.Text = item.Profit.ToString();
                label12.Text = item.ItemId.ToString();
                Warehouse quan = (from wh in _entity.Warehouses where wh.ItemId == iid select wh).First();
                label7.Text = quan.Quan.ToString();


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "") 
            {
                MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                using (Model1 _entity = new Model1())
                {
                    Item item = (from i in _entity.Items join wh in _entity.Warehouses on i.ItemId equals wh.ItemId where i.ItemId == iid select i).First();
                    item.ItemName = textBox1.Text;
                    item.Cost = double.Parse(textBox2.Text);
                    item.Profit = double.Parse(textBox3.Text); 
                    _entity.SaveChanges();
                    warehousing w = new warehousing();
                    this.Hide();
                    w.ShowDialog();
                    this.Close();
                }
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
