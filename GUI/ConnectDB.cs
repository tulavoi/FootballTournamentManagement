using DAL.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
namespace GUI
{
    public partial class ConnectDB : Form
    {
        
        public ConnectDB()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string nameSever = txtServerName.Text;
            ChangeConnectionString.setConnectionString($"Data Source={nameSever};Initial Catalog=DBProject.Net;Integrated Security=True");
            
            this.Hide();
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
            MessageBox.Show("hi");
            MessageBox.Show(ChangeConnectionString.getConnectionString());
            this.Close();
        }
    }
}
