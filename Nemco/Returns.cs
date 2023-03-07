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
    public partial class Returns : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
       
        public static int rid;
        public static int iid;
        public Returns()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon;


            button1.Enabled = false;

            dateTimePicker1.Format = DateTimePickerFormat.Short;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            string date = dateTimePicker1.Value.ToShortDateString();

            using (Model1 _entity = new Model1())
            {
                var bills = from b in _entity.Bills where b.DateTime==date  select b;
                comboBox3.DataSource = bills.ToList();
                comboBox3.DisplayMember = "Customer";
                comboBox3.ValueMember = "BillId";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            home h = new home();
            this.Hide();
            h.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.Enabled = false;

            int selectval;
            bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out selectval);


            Random rnd = new Random();

            string id = rnd.Next(10000000, 99999999).ToString();

            rid = int.Parse(DateTime.Now.ToString("dMy") + id.Substring(0, 3));


            using (Model1 _entity = new Model1())
            {   
                var ret = new Return() { ReturnId = rid, BillId = selectval };
                _entity.Returns.Add(ret);
                _entity.SaveChanges();
                var billitems = from bi in _entity.BillItems join itm in _entity.Items on bi.ItemId equals itm.ItemId where bi.BillId == selectval select new { الكود = bi.ItemId, المنتج = itm.ItemName, سعرالقطعه = itm.Cost, عدد = bi.ItemQuan, الاجمالي = bi.ItemTprice, المكسب = bi.ItemTprofit };
                dataGridView1.DataSource = billitems.ToList();
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            iid = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectval;
            bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out selectval);
            using (Model1 _entity = new Model1())
            {
                var retit = new ReturnItem() { ReturnId = rid, ItemId = iid };
                _entity.ReturnItems.Add(retit);
                _entity.SaveChanges();
                var cmd = ("Delete from BillItems where BillId = " + selectval + " and ItemId = " + iid);
                _entity.Database.ExecuteSqlCommand(cmd);
                var billitems = from bi in _entity.BillItems join itm in _entity.Items on bi.ItemId equals itm.ItemId where bi.BillId == selectval select new { الكود = bi.ItemId, المنتج = itm.ItemName, سعرالقطعه = itm.Cost, عدد = bi.ItemQuan, الاجمالي = bi.ItemTprice, المكسب = bi.ItemTprofit };
                dataGridView1.DataSource = billitems.ToList();
             
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            button1.Enabled = true;

            int selectval;
            bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out selectval);

            using (Model1 _entity = new Model1())
            {
                Bill bill = (from b in _entity.Bills where b.BillId == selectval select b).First();
                label7.Text = bill.BillId.ToString();
                label9.Text = bill.Total.ToString();
                label11.Text = bill.Profit.ToString();
                try
                {
                    Discount dis = (from d in _entity.Discounts where d.BillId == selectval select d).First();
                    label8.Text = dis.Discount1.ToString();
                }
                catch (Exception)
                {
                    label8.Text = "0";
                }
                

            }

        }

        private void Returns_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
