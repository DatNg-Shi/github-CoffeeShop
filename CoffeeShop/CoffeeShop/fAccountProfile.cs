using DAO_CoffeeShop;
using DTO_CoffeeShop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShop
{
    public partial class fAccountProfile : Form
    {
        public static string displayname = "";
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; LoadAccount(loginAccount); }
        }
        public fAccountProfile(AccountDTO account)
        {
            InitializeComponent();
            LoginAccount = account;
        }

        void LoadAccount(AccountDTO account)
        {
            txtUsername.Text = LoginAccount.Username;
            txtDisplayName.Text = LoginAccount.DisplayName;
        }

        void UpdateAccountInfo()
        {
            lblMessage.Visible = true;
            string username = txtUsername.Text;
            string displayname = txtDisplayName.Text;
            string password = txtPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPass.Text;

            //if (displayname.Equals("") || password.Equals("") || newPassword.Equals("") || confirmPassword.Equals(""))
            //{
            //    MessageBox.Show("Empty input!");
            //    return;
            //}

            if (!newPassword.Equals(confirmPassword))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Confirm wrong password, please type again !!!";
            }
            else if (AccountDAO.Instance.UpdateProfile(username, displayname, password, newPassword))
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Update Account Successfully!";
                ChangeInfo.Invoke(displayname);
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please enter the correct password!";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        public Action<string> ChangeInfo;
    }

}
