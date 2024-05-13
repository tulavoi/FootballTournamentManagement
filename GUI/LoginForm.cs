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
        AccountsBLL accountsBLL = new AccountsBLL();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            HandleLogin();
        }

        // Nhấn enter để login nhưng bị lỗi 
        //private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)Keys.Enter)
        //    {
        //        e.Handled = true;

        //        HandleLogin();
        //    }
        //}

        private void HandleLogin()
        {
            string userName = txtUsername.Text;
            string password = txtPassword.Text;

            Account acc = new Account
            {
                Email = userName,
                Password = password
            };

            bool isLoginSuccess = accountsBLL.CheckLogin(acc);
            if (isLoginSuccess)
            {
                MainForm frm = new MainForm();

                //// Gán sự kiện FormClosing cho form hiện tại
                //this.FormClosing += (s, args) =>
                //{
                //    // Đảm bảo rằng ứng dụng sẽ không đóng nếu form đang đóng là MainForm
                //    if (frm != null && !frm.IsDisposed)
                //    {
                //        frm.Show();
                //    }
                //};

                // Ẩn form hiện tại
                Hide();

                frm.Show();
            }

            else
                MessageBox.Show("Login failed!");
        }
    }
}
