using DAO_CoffeeShop;
using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShop
{
    public partial class fTableManager : Form
    {
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public fTableManager(AccountDTO account)
        {
            InitializeComponent();
            LoginAccount = account;
            LoadTable();
            LoadCategory();
            LoadComboxTable(cbSwitchTable);
            timer.Start();
        }

        #region Methods
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            string account = " (" + LoginAccount.DisplayName + ")";
            accountToolStripMenuItem.Text += account;

        }

        void LoadCategory()
        {
            List<CategoryDTO> listCate = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCate;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id)
        {
            List<FoodDTO> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            if (listFood != null)
            {
                cbFood.DataSource = listFood;
                cbFood.DisplayMember = "Name";
            }
        }

        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<TableDTO> tableList = TableDAO.Instance.LoadTableList();
            foreach (TableDTO item in tableList)
            {
                Button btn = new Button() { Width = 75, Height = 75 };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = Color.Black;
                btn.FlatAppearance.BorderSize = 1;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Empty":
                        btn.BackColor = Color.LightGray;
                        break;
                    default:
                        btn.BackColor = Color.PaleVioletRed;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<MenuDTO> listBillInfo = new List<MenuDTO>();
            listBillInfo = MenuDAO.Instance.GetListMenuByTableId(id);
            double money = 0;
            foreach (MenuDTO item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                money += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txtMoney.Text = money.ToString("c", culture);

        }

        void LoadComboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }

        void ChangeWelcomeAccount(string name)
        {
            accountToolStripMenuItem.Text = "Account (" + name + ")";
        }

        void AddFoodEvent()
        {
            int idCate = (cbCategory.SelectedItem as CategoryDTO).Id;
            LoadFoodListByCategoryID(idCate);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as TableDTO).Id);
        }
        void UpdateFoodEvent()
        {
            int idCate = (cbCategory.SelectedItem as CategoryDTO).Id;
            LoadFoodListByCategoryID(idCate);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as TableDTO).Id);
        }
        void DeleteFoodEvent()
        {
            int idCate = (cbCategory.SelectedItem as CategoryDTO).Id;
            LoadFoodListByCategoryID(idCate);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as TableDTO).Id);
                LoadTable();
            }
        }
        #endregion


        #region Events
        private void btn_Click(object sender, EventArgs e)
        {
            
            int tableID = ((sender as Button).Tag as TableDTO).Id;
            txtTable.Text = ((sender as Button).Tag as TableDTO).Name;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void personalInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.ChangeInfo = ChangeWelcomeAccount;
            f.ShowDialog();

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loggedin = LoginAccount;
            f.AddFood = AddFoodEvent;
            f.UpdateFood = UpdateFoodEvent;
            f.DeleteFood = DeleteFoodEvent;
            f.ReLoadCategory = LoadCategory;
            f.LoadTable = LoadTable;
            f.ShowDialog();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;
            CategoryDTO selected = cb.SelectedItem as CategoryDTO;
            id = selected.Id;
            LoadFoodListByCategoryID(id);
        }

        private void btnMoreFood_Click(object sender, EventArgs e)
        {
            TableDTO table = lsvBill.Tag as TableDTO;
            if (table == null)
            {
                MessageBox.Show("Please select a table.");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.Id);
            if (cbFood.SelectedItem as FoodDTO == null)
            {
                return;
            }
            int idFood = (cbFood.SelectedItem as FoodDTO).IdFood;
            int count = (int)nmFoodCount.Value;
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.Id);
                BillInfoDAO.Instance.InsertBillInfno(BillDAO.Instance.GetMaxBillID(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfno(idBill, idFood, count);
            }

            ShowBill(table.Id);
            LoadTable();
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            TableDTO table = lsvBill.Tag as TableDTO;
            if (table == null)
            {
                MessageBox.Show("Please select a table.");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.Id);
            int discount = (int)nmDiscount.Value;
            string convertMoney = txtMoney.Text.Split(',')[0].Replace(".", ",");
            double money = Convert.ToDouble(convertMoney);
            double finalMoney = money - (money / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to pay this table: {0}\nMoney - (Money / 100) x Discount\n=> {1} - ({1} / 100) x {2} = {3}", table.Name, money, discount, finalMoney), "Notification!!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalMoney);
                    ShowBill(table.Id);
                    LoadTable();
                }
            }
            else
            {
                MessageBox.Show("Failed.");
            }
        }

        private void btnChangeTable_Click(object sender, EventArgs e)
        {
            TableDTO table = lsvBill.Tag as TableDTO;
            if (table == null)
            {
                MessageBox.Show("Please select a table.");
                return;
            }
            int idTable1 = (table).Id;
            int idTable2 = (cbSwitchTable.SelectedItem as TableDTO).Id;
            string name1 = (table).Name;
            string name2 = (cbSwitchTable.SelectedItem as TableDTO).Name;
            if (MessageBox.Show(string.Format("Are you sure you want to switch from {0} to {1}", name1, name2), "Notification!!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(idTable1, idTable2);

                LoadTable();
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.lblTimer.Text = "Time: " + dateTime.ToString();
        }
        #endregion


    }
}
