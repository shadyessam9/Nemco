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
    public partial class emp : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public emp()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


            using (Model1 _entity = new Model1())
            {
                var emps = from e in _entity.Employees select new { الاسم = e.EmpNme };
                dataGridView1.DataSource = emps.ToList();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void emp_MouseDown(object sender, MouseEventArgs e)
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
                using (Model1 _entity = new Model1())
                {
                    var emp = new Employee() {  EmpNme = textBox1.Text };
                    _entity.Employees.Add(emp);
                    _entity.SaveChanges();
                    var emps = from em in _entity.Employees select new { الاسم = em.EmpNme };
                    dataGridView1.DataSource = emps.ToList();
                }


                textBox1.Clear();

            }
        }
    }
}
