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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (Login(username, password))
            {
                AccountDTO account = AccountDAO.Instance.GetAccountByUsername(username);
                fTableManager f = new fTableManager(account);
                this.Hide();
                f.ShowDialog();
                txtUsername.Text = "";
                txtPassword.Text = "";
                this.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password!!!");
            }
        }

        bool Login(string username, string password)
        {
            bool result = AccountDAO.Instance.CheckLogin(username, password);
            return result;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the program?", "Warning!!!", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
