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
    public partial class warehousing : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public warehousing()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


            using (Model1 _entity = new Model1())
            {
                var whitems = from wh in _entity.Warehouses select wh;
                label16.Text = whitems.AsEnumerable().Sum(wh => wh.Quan).ToString();
                label17.Text = whitems.AsEnumerable().Sum(wh => wh.ItemWorth).ToString();
                label18.Text = whitems.AsEnumerable().Sum(wh => wh.ItemProfit).ToString();
                var lowinv = from wh in _entity.Warehouses where 0 < wh.Quan && wh.Quan < 10 select wh;
                label21.Text = lowinv.AsEnumerable().Count().ToString();
                var noinv = from wh in _entity.Warehouses where wh.Quan <= 0 select wh;
                label20.Text = noinv.AsEnumerable().Count().ToString();
                var items = from i in _entity.Items join wh in _entity.Warehouses on i.ItemId equals wh.ItemId select new { الكود = i.ItemId, المنتج = i.ItemName, سعرالقطعه = i.Cost, مكسبxالقطعه = i.Profit, عددالمخزون = wh.Quan, الاجماليxالمخزون = wh.ItemWorth, المكسبxالمخزون = wh.ItemProfit };
                dataGridView1.DataSource=items.ToList();
            }
            }

        private void warehousing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addproduct ap = new addproduct();
            this.Hide();
            ap.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            editproduct ep = new editproduct();
            this.Hide();
            ep.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            editinv ei = new editinv();
            this.Hide();
            ei.ShowDialog();
            this.Close();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            noinvlist ni = new noinvlist();
            ni.ShowDialog();
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            lowinvlist li = new lowinvlist();
            li.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Depts d = new Depts();
            this.Hide();
            d.ShowDialog();
            this.Close();
        }
    }
}
