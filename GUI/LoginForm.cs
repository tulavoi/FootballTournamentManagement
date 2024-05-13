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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using System.IO;

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

            LoadLoginInfo();
        }

        private void LoadLoginInfo()
        {
            if (File.Exists("configFilePath"))
            {
                // Đọc thông tin đăng nhập từ file cấu hình
                XDocument doc = XDocument.Load("configFilePath");
                XElement root = doc.Root;
                XElement usernameElement = root.Element("Username");
                XElement passwordElement = root.Element("Password");

                if (usernameElement != null && passwordElement != null)
                {
                    txtUsername.Text = usernameElement.Value;
                    txtPassword.Text = passwordElement.Value;
                    toggleSwitchRemember.Checked = true;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            HandleLogin();
        }

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

                // Ẩn form hiện tại
                Hide();

                frm.Show();
            }

            else
                MessageBox.Show("Login failed!");
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnLogin_Click(sender, e);
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnLogin_Click(sender, e);
            }
        }

        private void toggleSwitchRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleSwitchRemember.Checked)
            {
                SaveLoginInfo(txtUsername.Text, txtPassword.Text);
            }
            if (!toggleSwitchRemember.Checked)
            {
                ClearLoginInfo();
            }
        }

        private void ClearLoginInfo()
        {
            // Xóa thông tin đăng nhập từ file cấu hình
            if (File.Exists("configFilePath"))
                File.Delete("configFilePath");
        }

        private void SaveLoginInfo(string username, string password)
        {
            // Tạo XML document và lưu thông tin đăng nhập vào file cấu hình
            XDocument doc = new XDocument(
                new XElement("LoginInfo",
                    new XElement("Username", username),
                    new XElement("Password", password)
                )
            );
            doc.Save("configFilePath");
        }
    }
}
