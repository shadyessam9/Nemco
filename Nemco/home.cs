using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;


namespace Nemco
{
    public partial class home : Form
    {

        string today = DateTime.Now.ToString("dd/MM/yyyy");
        
        

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();



        public home()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


            using (Model1 _entity = new Model1())
            {
                 var bills = from b in _entity.Bills where b.DateTime == today select new { الكود = b.BillId, الاجمالي = b.Total, المكسب = b.Profit, التاريخ = b.DateTime, عميل = b.Customer, موظف = b.agent };
                 dataGridView1.DataSource = bills.ToList();
                 label16.Text = bills.Count().ToString();
                 var itemssold = from bi in _entity.BillItems join b in _entity.Bills on bi.BillId equals b.BillId where b.DateTime == today select  bi;
                 label17.Text = itemssold.AsEnumerable().Sum(q => q.ItemQuan).ToString();
                 var sales = from s in _entity.Sales  where  s.DateTime == today select s;
                 label18.Text = sales.AsEnumerable().Sum(s => s.Total).ToString();
                 var profit = from p in _entity.Profits where p.DateTime == today select p;
                 label19.Text = profit.AsEnumerable().Sum(p => p.Profit1).ToString();
                 var whitems = from wh in _entity.Warehouses  select wh;
                 label23.Text = whitems.AsEnumerable().Sum(wh => wh.Quan).ToString();
                 var lowinv = from wh in _entity.Warehouses where 0 < wh.Quan && wh.Quan < 10 select wh;
                 label21.Text = lowinv.AsEnumerable().Count().ToString();
                 var noinv = from wh in _entity.Warehouses where wh.Quan <= 0 select wh;
                 label20.Text = noinv.AsEnumerable().Count().ToString();

            } 



        }



        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            billaddress ba = new billaddress();    
            this.Hide();    
            ba.ShowDialog();   
            this.Close();    
        }

        private void button5_Click(object sender, EventArgs e)
        {
            warehousing w = new warehousing();
            this.Hide();
            w.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            lowinvlist li = new lowinvlist();
            li.ShowDialog();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            noinvlist ni = new noinvlist();
            ni.ShowDialog();
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            warehousing w = new warehousing();
            this.Hide();
            w.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Returns r = new Returns();
            this.Hide();
            r.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            emp ep = new emp();
            this.Hide();
            ep.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Supliers s = new Supliers();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            expenses exp = new expenses();
            this.Hide();
            exp.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reports r = new Reports();
            this.Hide();
            r.ShowDialog();
            this.Close();
        }
    }
}
