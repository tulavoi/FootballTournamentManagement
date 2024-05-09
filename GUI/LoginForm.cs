using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string serverName = txtServerName.Text;
            //string dbName = txtDBName.Text;
            //string connectionString = $"Data Source={serverName};Initial Catalog={dbName};Integrated Security=True;Trust Server Certificate=True";
            MainForm frm = new MainForm();
            frm.Show();
        }
    }
}
