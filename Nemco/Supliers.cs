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
    public partial class Supliers : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Supliers()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;

        }



        private void button3_Click(object sender, EventArgs e)
        {
            addsup s = new addsup();
            this.Hide();
            s.ShowDialog();
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
            quersup qs = new quersup();
            this.Hide();
            qs.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setlsup ss = new setlsup();
            this.Hide();
            ss.ShowDialog();
            this.Close();
        }
    }
}
