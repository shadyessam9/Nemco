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
    public partial class Reports : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Reports()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


        }

     
        private void button3_Click(object sender, EventArgs e)
        {
            SalesReport srt = new SalesReport();
            this.Hide();
            srt.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void Supliers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           SuppliersReport sr = new SuppliersReport();
            this.Hide();
            sr.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExpensesReport er = new ExpensesReport();
            this.Hide();
            er.ShowDialog();
            this.Close();
        }
    }
}
