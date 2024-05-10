using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;

namespace GUI
{
    public partial class LoginForm : Form
    {
        string serverName;
        string dbName;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            txtServerName.Text = "LAPTOP-5I4BGSNV\\HOANGVU";
            txtDBName.Text = "DBProject.Net";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtServerName.Text == "")
            {
                MessageBox.Show("Please enter a server name!");
                return;
            }

            if (txtDBName.Text == "")
            {
                MessageBox.Show("Please enter a database name!");
                return;
            }

            serverName = txtServerName.Text;
            dbName = txtDBName.Text;

            MainForm frm = new MainForm();
            frm.Show();
        }
    }
}
