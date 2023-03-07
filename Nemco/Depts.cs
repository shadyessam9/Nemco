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
    public partial class Depts : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        static int depid;
       
        public Depts()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

            Random rnd = new Random();

            string id = rnd.Next(10000000, 99999999).ToString();

            depid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

            label1.Text = depid.ToString();

            using (Model1 _entity = new Model1())
            {
                var depts = from d in _entity.Departments orderby d.DeptName select new { قسم = d.DeptName } ;
                dataGridView1.DataSource = depts.ToList();
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
                using (Model1 _entity = new Model1())
                {
                    var dep = new Department() { DeptId = depid , DeptName = textBox1.Text };
                    _entity.Departments.Add(dep);
                    _entity.SaveChanges();
                    var depts = from d in _entity.Departments select new { قسم = d.DeptName };
                    dataGridView1.DataSource = depts.ToList();
                }


                textBox1.Clear();

                Random rnd = new Random();

                string id = rnd.Next(10000000, 99999999).ToString();

                depid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));

                label1.Text = depid.ToString();


            }
        }

        private void Depts_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            warehousing w = new warehousing();
            this.Hide();
            w.ShowDialog();
            this.Close();
        }
    }
}
