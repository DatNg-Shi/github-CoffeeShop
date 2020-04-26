using DAO_CoffeeShop;
using DTO_CoffeeShop;
using System;
using System.Data;
using System.Windows.Forms;

namespace CoffeeShop
{
    public partial class fAdmin : Form
    {
        public AccountDTO loggedin;
        public fAdmin()
        {
            InitializeComponent();
            LoadDateTimePickerForBill();
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadCategoryBinding(cbFoodCategory);
            LoadListCategory();
            LoadListTable();
            LoadListAccount();
            LoadTypeBinding(cbAccountType);
        }


        #region Method
        void LoadTypeBinding(ComboBox cb)
        {
            cb.DataSource = AccountTypeDAO.Instance.GetListType();
            cb.DisplayMember = "Role";
        }

        void LoadListAccount()
        {
            DataTable dtAccount = AccountDAO.Instance.GetListAccount();
            txtUsername.DataBindings.Clear();
            txtFullName.DataBindings.Clear();

            txtUsername.DataBindings.Add("Text", dtAccount, "UserName", true, DataSourceUpdateMode.Never);
            txtFullName.DataBindings.Add("Text", dtAccount, "FullName", true, DataSourceUpdateMode.Never);

            dgvAccount.DataSource = dtAccount;
        }

        void LoadListTable()
        {
            dgvTable.DataSource = TableDAO.Instance.LoadTableList();
            txtTableID.DataBindings.Clear();
            txtTableName.DataBindings.Clear();

            txtTableID.DataBindings.Add("Text", dgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never);
            txtTableName.DataBindings.Add("Text", dgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never);
        }

        void LoadListTableDeleted()
        {
            dgvTable.DataSource = TableDAO.Instance.LoadTableListDeleted();
            txtTableID.DataBindings.Clear();
            txtTableName.DataBindings.Clear();

            txtTableID.DataBindings.Add("Text", dgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never);
            txtTableName.DataBindings.Add("Text", dgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never);
        }


        void LoadListCategory()
        {
            dgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
            txtCategoryID.DataBindings.Clear();
            txtCateName.DataBindings.Clear();

            txtCategoryID.DataBindings.Add("Text", dgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never);
            txtCateName.DataBindings.Add("Text", dgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never);

        }

        void LoadListFood()
        {
            DataTable dtFood = FoodDAO.Instance.GetListFood();
            FoodBinding(dtFood);

            dgvFood.DataSource = dtFood;
        }

        void FoodBinding(DataTable dtFood)
        {
            txtFoodID.DataBindings.Clear();
            txtFoodName.DataBindings.Clear();
            nmFoodPrice.DataBindings.Clear();

            txtFoodID.DataBindings.Add("Text", dtFood, "ID", true, DataSourceUpdateMode.Never);
            txtFoodName.DataBindings.Add("Text", dtFood, "Name", true, DataSourceUpdateMode.Never);
            nmFoodPrice.DataBindings.Add("value", dtFood, "Price", true, DataSourceUpdateMode.Never);
        }

        void LoadCategoryBinding(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadDateTimePickerForBill()
        {
            DateTime date = DateTime.Now;
            dtpkFormDate.Value = new DateTime(date.Year, date.Month, 1);
            dtpkToDate.Value = dtpkFormDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            DataTable dtBill = BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
            dgvBill.DataSource = dtBill;
        }

        void SearchFoodByName(string name)
        {
            //List<FoodDTO> listFood = new List<FoodDTO>();
            string check = "";
            if (rdActive.Checked)
                check = "1";
            if (rdInactive.Checked)
                check = "0";
            DataTable dtFood = FoodDAO.Instance.SearchFoodByName(name, check);
            FoodBinding(dtFood);
            dgvFood.DataSource = dtFood;
        }
        #endregion


        #region Events Bill
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
        }
        #endregion

        #region Events Food
        private void btnViewFood_Click(object sender, EventArgs e)
        {
            rdActive.Checked = false;
            rdInactive.Checked = false;
            btnRestore.Enabled = false;
            LoadListFood();
        }
        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            int foodId = Convert.ToInt32(txtFoodID.Text.Trim());
            FoodDTO categoryId = FoodDAO.Instance.GetCategoryIDByFoodID(foodId);
            if (categoryId != null)
            {
                CategoryDTO cate = CategoryDAO.Instance.GetCategoryByID(categoryId.IdCate);
                if (cate != null)
                {
                    int index = -1;
                    int i = 0;
                    foreach (CategoryDTO item in cbFoodCategory.Items)
                    {
                        if (item.Id == cate.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            bool result = false;
            string name = txtFoodName.Text.Trim();
            int idCate = (cbFoodCategory.SelectedItem as CategoryDTO).Id;
            float price = (float)nmFoodPrice.Value;
            bool existed = FoodDAO.Instance.IsExisedtFood(name);

            if (txtFoodName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            if (existed)
            {
                MessageBox.Show("This food: " + name + " is existed.");
                return;
            }

            result = FoodDAO.Instance.AddFood(name, idCate, price);
            if (result)
            {
                MessageBox.Show("Add Successfully!");
                LoadListFood();
                AddFood.Invoke();
            }
            else
            {
                MessageBox.Show("Add failed!");
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            bool result = false;
            string name = txtFoodName.Text.Trim();
            int idCate = (cbFoodCategory.SelectedItem as CategoryDTO).Id;
            float price = (float)nmFoodPrice.Value;
            int idFood = Convert.ToInt32(txtFoodID.Text.Trim());
            bool existed = FoodDAO.Instance.IsExisedtFood(name);

            if (txtFoodName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            if (MessageBox.Show(string.Format("Update this FoodID: {0}?", idFood), "Notification!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                result = FoodDAO.Instance.UpdateFood(idFood, name, idCate, price);
                if (result)
                {
                    MessageBox.Show("Update Successfully!");
                    LoadListFood();
                    UpdateFood.Invoke();
                }
                else
                {
                    MessageBox.Show("Update failed!");
                }
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            bool result = false;
            int idFood = Convert.ToInt32(txtFoodID.Text.Trim());
            if (MessageBox.Show(string.Format("Delete this FoodID: {0}?", idFood), "Notification!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                result = FoodDAO.Instance.DeleteFood(idFood);
                if (result)
                {
                    MessageBox.Show("Delete Successfully!");
                    LoadListFood();
                    DeleteFood.Invoke();
                    ReLoadCategory.Invoke();
                }
                else
                {
                    MessageBox.Show("Delete failed!");
                }
            }

        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string searchFood = txtSearchFood.Text.Trim();
            if (searchFood.Equals(""))
            {
                MessageBox.Show("Type the characters to search.");
                return;
            }
            SearchFoodByName(searchFood);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            bool result = false;
            int idFood = Convert.ToInt32(txtFoodID.Text.Trim());

            result = FoodDAO.Instance.RestoreFood(idFood);
            if (result)
            {
                MessageBox.Show("Restore Successfully!");
                LoadListFood();
                rdActive.Checked = true;
                DeleteFood.Invoke();
                ReLoadCategory.Invoke();
            }
            else
            {
                MessageBox.Show("Restore failed!");
            }

        }
        #endregion

        #region Event Category
        private void btnViewCate_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnAddCate_Click(object sender, EventArgs e)
        {
            bool result = false;
            string name = txtCateName.Text.Trim();
            int idCate = Convert.ToInt32(txtCategoryID.Text.Trim());

            bool existed = CategoryDAO.Instance.IsExistedCategory(name);

            if (txtCateName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            if (existed)
            {
                MessageBox.Show("This category: " + name + " is existed.");
                return;
            }

            result = CategoryDAO.Instance.AddCategory(name);
            if (result)
            {
                MessageBox.Show("Add Successfully!");
                LoadListCategory();
                LoadCategoryBinding(cbFoodCategory);
                LoadListFood();
                ReLoadCategory.Invoke();
            }
            else
            {
                MessageBox.Show("Add failed!");
            }
        }

        private void btnUpdateCate_Click(object sender, EventArgs e)
        {
            bool result = false;
            string name = txtCateName.Text.Trim();
            int idCate = Convert.ToInt32(txtCategoryID.Text.Trim());
            bool existed = CategoryDAO.Instance.IsExistedCategory(name);

            if (txtCateName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            if (MessageBox.Show(string.Format("Update this CategoryID: {0}?", idCate), "Notification!!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (existed)
                {
                    MessageBox.Show("This category: " + name + " is existed.");
                    return;
                }

                result = CategoryDAO.Instance.UpdateCategory(name, idCate);
                if (result)
                {
                    MessageBox.Show("Update Successfully!");
                    LoadListCategory();
                    LoadCategoryBinding(cbFoodCategory);
                    LoadListFood();
                    ReLoadCategory.Invoke();
                }
                else
                {
                    MessageBox.Show("Update failed!");
                }
            }
        }

        private void btnDeleteCate_Click(object sender, EventArgs e)
        {
            bool result = false;
            int idCate = Convert.ToInt32(txtCategoryID.Text.Trim());

            if (MessageBox.Show(string.Format("Delete this CategoryID: {0}?", idCate), "Notification!!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                result = CategoryDAO.Instance.DeleteCategory(idCate);
                if (result)
                {
                    MessageBox.Show("Delete Successfully!");
                    LoadListCategory();
                    LoadCategoryBinding(cbFoodCategory);
                    LoadListFood();
                    ReLoadCategory.Invoke();
                    LoadTable.Invoke();
                }
                else
                {
                    MessageBox.Show("Delete failed!");
                }
            }
        }
        #endregion

        #region Event Table

        private void txtTableID_TextChanged(object sender, EventArgs e)
        {
            int idTable = Convert.ToInt32(txtTableID.Text.Trim());
            TableDTO status = TableDAO.Instance.GetStatusByTableID(idTable);
            if (status != null)
            {
                for (int i = 0; i < cbTableStatus.Items.Count; i++)
                {
                    string text = cbTableStatus.GetItemText(cbTableStatus.Items[i]);
                    if (status.Status.Equals(text))
                    {
                        cbTableStatus.SelectedIndex = i;
                    }
                }

            }
        }

        private void btnViewTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            bool result = false;
            string name = txtTableName.Text.Trim();
            int idTable = Convert.ToInt32(txtTableID.Text.Trim());
            string status = cbTableStatus.SelectedItem.ToString();
            bool existed = TableDAO.Instance.IsExistedTable(name);

            if (txtTableName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            if (existed)
            {
                MessageBox.Show("This table: " + name + " is existed.");
                return;
            }

            result = TableDAO.Instance.AddTable(name);
            if (result)
            {
                MessageBox.Show("Add Successfully!");
                LoadListTable();
                LoadTable.Invoke();
            }
            else
            {
                MessageBox.Show("Add failed!");
            }
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            bool result = false;
            string name = txtTableName.Text.Trim();
            int idTable = Convert.ToInt32(txtTableID.Text.Trim());
            string status = cbTableStatus.SelectedItem.ToString();
            bool existed = TableDAO.Instance.IsExistedTable(name);

            if (txtTableName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            if (MessageBox.Show(string.Format("Update this TableID: {0}?", idTable), "Notification!!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {

                result = TableDAO.Instance.UpdateTable(name, status, idTable);
                if (result)
                {
                    MessageBox.Show("Update Successfully!");
                    LoadListTable();
                    LoadTable.Invoke();
                }
                else
                {
                    MessageBox.Show("Update failed!");
                }
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            bool result = false;
            int idTable = Convert.ToInt32(txtTableID.Text.Trim());
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(idTable);

            if (MessageBox.Show(string.Format("Delete this TableID: {0}?", idTable), "Notification!!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                BillInfoDAO.Instance.DeleteBillInfoByBillID(idBill);
                BillDAO.Instance.DeleteBillByTableID(idTable);
                result = TableDAO.Instance.DeleteTable(idTable);
                if (result)
                {
                    MessageBox.Show("Delete Successfully!");
                    LoadListTable();
                    LoadTable.Invoke();
                }
                else
                {
                    MessageBox.Show("Delete failed!");
                }
            }
        }

        private void btnRestoreTable_Click(object sender, EventArgs e)
        {
            int idTable = Convert.ToInt32(txtTableID.Text.Trim());
            bool result = false;

            result = TableDAO.Instance.RestoreTable(idTable);
            if (result)
            {
                MessageBox.Show("Restore Successfully!");
                LoadListTableDeleted();
                LoadTable.Invoke();
            }
            else
            {
                MessageBox.Show("Restore failed!");
            }
        }

        private void btnViewDeleted_Click(object sender, EventArgs e)
        {
            LoadListTableDeleted();
        }
        #endregion

        #region Event Account
        private void btnViewAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            AccountDTO account = AccountDAO.Instance.GetAccountByUsername(username);
            if (account != null)
            {
                AccountTypeDTO type = AccountTypeDAO.Instance.GetTypeByID(account.Type);
                if (type != null)
                {
                    int index = -1;
                    int i = 0;
                    foreach (AccountTypeDTO item in cbAccountType.Items)
                    {
                        if (item.Id == type.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbAccountType.SelectedIndex = index;
                }
            }
        }


        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            bool result = false;
            string username = txtUsername.Text.Trim();
            string displayname = txtFullName.Text.Trim();
            int type = cbAccountType.SelectedIndex;
            bool existed = AccountDAO.Instance.IsExisedtAccount(username);

            if (txtUsername.Text.Equals("") || txtFullName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            if (existed)
            {
                MessageBox.Show("This account: " + username + " is existed.");
                return;
            }

            result = AccountDAO.Instance.AddAccount(username, displayname, type);
            if (result)
            {
                MessageBox.Show("Add Successfully!");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Add failed!");
            }

        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            bool result = false;
            string username = txtUsername.Text.Trim();
            string displayname = txtFullName.Text.Trim();
            int type = cbAccountType.SelectedIndex;
            bool existed = AccountDAO.Instance.IsExisedtAccount(username);

            if (txtUsername.Text.Equals("") || txtFullName.Text.Equals(""))
            {
                MessageBox.Show("Empty input.");
                return;
            }

            result = AccountDAO.Instance.UpdateAccount(username, displayname, type);
            if (result)
            {
                MessageBox.Show("Update Successfully!");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Update failed!");
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            bool result = false;
            string username = txtUsername.Text.Trim();

            if (MessageBox.Show(string.Format("Delete this Account: {0}?", username), "Notification!!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (loggedin.Username.Equals(username))
                {
                    MessageBox.Show("Can not delete. You are currently logged in.!");
                    return;
                }
                result = AccountDAO.Instance.DeleteAccount(username);
                if (result)
                {
                    MessageBox.Show("Delete Successfully!");
                    LoadListAccount();
                }
                else
                {
                    MessageBox.Show("Delete failed!");
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MessageBoxManager.OK = "ADD";
            MessageBoxManager.Cancel = "UPDATE";
            MessageBoxManager.Register();
            DialogResult dialogResult = MessageBox.Show("Do you wanna ADD or UPDATE", "", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.OK)
            {
                txtUsername.ReadOnly = false;
                btnUpdateAccount.Enabled = false;
                btnAddAccount.Enabled = true;
                MessageBoxManager.Unregister();
            }
            else
            {
                txtUsername.ReadOnly = true;
                btnAddAccount.Enabled = false;
                btnUpdateAccount.Enabled = true;
                MessageBoxManager.Unregister();
            }
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            bool result = false;

            result = AccountDAO.Instance.ResetPassword(username);
            if (result)
            {
                MessageBox.Show("Reset Successfully!");
            }
            else
            {
                MessageBox.Show("Reset failed!");
            }
        }
        #endregion

        public Action AddFood;
        public Action UpdateFood;
        public Action DeleteFood;
        public Action ReLoadCategory;
        public Action LoadTable;

        private void rdActive_CheckedChanged(object sender, EventArgs e)
        {
            btnRestore.Enabled = false;
            DataTable dtFood = FoodDAO.Instance.GetListFoodActiveOrInactive(1);
            FoodBinding(dtFood);

            dgvFood.DataSource = dtFood;
        }

        private void rdInactive_CheckedChanged(object sender, EventArgs e)
        {
            btnRestore.Enabled = true;
            DataTable dtFood = FoodDAO.Instance.GetListFoodActiveOrInactive(0);
            FoodBinding(dtFood);

            dgvFood.DataSource = dtFood;
        }


    }
}
