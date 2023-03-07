using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Nemco
{

    public partial class addproduct : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        static int iid;
        static int depid;
        static int supid;
       


        public addproduct()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

            Random rnd = new Random();

            string id = rnd.Next(10000000, 99999999).ToString();

            iid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

            label7.Text = iid.ToString();


            using (Model1 _entity = new Model1())
            {

                var depts = from d in _entity.Departments orderby d.DeptName select d ;
                comboBox2.DataSource = depts.ToList();
                comboBox2.DisplayMember = "DeptName";
                comboBox2.ValueMember = "DeptId";

            }

           

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool parseOK1 = Int32.TryParse(this.comboBox2.SelectedValue.ToString(), out depid);
        }


        private void button7_Click(object sender, EventArgs e)
        {
            warehousing w = new warehousing();
            this.Hide();
            w.ShowDialog();
            this.Close();
        }

        private void addproduct_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            if(textBox1.Text==""|| textBox2.Text=="" || textBox3.Text=="" ||  comboBox2.SelectedItem == null)
            {
                MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (Model1 _entity = new Model1())
                {
                    var item = new Item() { ItemId = iid, ItemName = textBox1.Text, Cost = double.Parse(textBox2.Text), Profit = double.Parse(textBox3.Text), DeptId = depid };
                    _entity.Items.Add(item);
                    var whitem = new Warehouse() { ItemId = iid, Quan=0 };
                    _entity.Warehouses.Add(whitem);
                    _entity.SaveChanges();
                }



                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                Random rnd = new Random();

                string id = rnd.Next(10000000, 99999999).ToString();

                iid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

                label7.Text = iid.ToString();
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


