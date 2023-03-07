using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemco
{
      
    public partial class billing : Form
    {

        string today = DateTime.Now.ToString("dd/MM/yyyy");

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public static int bid { get; set; }


        public static int rowid;


        public billing()
        {


        InitializeComponent();

            this.Icon = Properties.Resources.icon;


            label7.Text =  bid.ToString() ;

            using (Model1 _entity = new Model1())
            {




                var billitems = from bi in _entity.BillItems join itm in _entity.Items on  bi.ItemId equals itm.ItemId   where bi.BillId == bid  select new { الكود = bi.ItemId, المنتج = itm.ItemName,سعرالقطعه=itm.Cost, عدد = bi.ItemQuan, الاجمالي = bi.ItemTprice, المكسب = bi.ItemTprofit };
                dataGridView1.DataSource = billitems.ToList();

                var total = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select  b.Total).SingleOrDefault();
                label5.Text = total.ToString();
                var profit = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select b.Profit).SingleOrDefault();
                label11.Text = profit.ToString();



                var depts = from d in _entity.Departments orderby d.DeptName select d;
                comboBox2.DataSource = depts.ToList();
                comboBox2.DisplayMember = "DeptName";
                comboBox2.ValueMember = "DeptId";


            }





        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectval;
            bool parseOK = Int32.TryParse(comboBox2.SelectedValue.ToString(),out selectval);

            using (Model1 _entity = new Model1())
            {
                var items = from item in _entity.Items join wh in _entity.Warehouses on item.ItemId equals wh.ItemId  where item.DeptId == selectval &&  wh.Quan > 10 orderby item.ItemName select item;
                comboBox3.DataSource = items.ToList();
                comboBox3.DisplayMember = "ItemName";
                comboBox3.ValueMember = "ItemId";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox3.Text == "") {
                MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                int itmid;
                bool parseOK = Int32.TryParse(comboBox3.SelectedValue.ToString(), out itmid);
                int quan = int.Parse(textBox3.Text);
                using (Model1 _entity = new Model1())
                {
                    var item = new BillItem() { BillId = bid, ItemId = itmid, ItemQuan = quan };
                    _entity.BillItems.Add(item);
                    _entity.SaveChanges();
                    var billitems = from bi in _entity.BillItems join itm in _entity.Items on bi.ItemId equals itm.ItemId where bi.BillId == bid select new { الكود = bi.ItemId, المنتج = itm.ItemName, سعرالقطعه = itm.Cost, عدد = bi.ItemQuan, الاجمالي = bi.ItemTprice, المكسب = bi.ItemTprofit };
                    dataGridView1.DataSource = billitems.ToList();


                    var total = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select b.Total).SingleOrDefault();
                    label5.Text = total.ToString();
                    var profit = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select b.Profit).SingleOrDefault();
                    label11.Text = profit.ToString();

                }

            }

        }

        private void billing_MouseDown(object sender, MouseEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox2.Text == "")
            {
                MessageBox.Show("يرجي ادخال كل البيانات ", "بعض البيانات ناقصه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (Model1 _entity = new Model1())
                {
                    var discount = new Discount() { BillId = bid, Discount1 = int.Parse(textBox2.Text) };
                    _entity.Discounts.Add(discount);
                    _entity.SaveChanges();
                    var total = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select b.Total).SingleOrDefault();
                    label5.Text = total.ToString();
                    var profit = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select b.Profit).SingleOrDefault();
                    label11.Text = profit.ToString();
                }
            }  

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowid = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Model1 _entity = new Model1())
            {

                var cmd = ("Delete from BillItems where BillId = " + bid + " and ItemId = " + rowid);
                _entity.Database.ExecuteSqlCommand(cmd);
                var billitems = from bi in _entity.BillItems join itm in _entity.Items on bi.ItemId equals itm.ItemId where bi.BillId == bid select new { الكود = bi.ItemId, المنتج = itm.ItemName, سعرالقطعه = itm.Cost, عدد = bi.ItemQuan, الاجمالي = bi.ItemTprice, المكسب = bi.ItemTprofit };
                dataGridView1.DataSource = billitems.ToList();
                var total = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select b.Total).SingleOrDefault();
                label5.Text = total.ToString();
                var profit = (from b in _entity.Bills where b.DateTime == today && b.BillId == bid select b.Profit).SingleOrDefault();
                label11.Text = profit.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Billprint.bid = bid;
            Billprint bp = new Billprint();
            this.Hide();
            bp.ShowDialog();
            this.Close();
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
