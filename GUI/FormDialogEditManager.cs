using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogEditManager : Form
    {
        ManagersBLL managersBLL = new ManagersBLL();
        ClubsBLL clubsBLL = new ClubsBLL();

        string shortcutManagerImgFilePath = "Images\\Managers\\";

        int managerID;

        int clubID;

        Bitmap clubLogo;

        string managerImgFileName;

        private string tempImagePath;

        public FormDialogEditManager(int managerID, Bitmap clubLogo, int clubID, string managerImgFileName)
        {
            InitializeComponent();
            this.managerID = managerID;
            this.clubLogo = clubLogo;
            this.clubID = clubID;
            this.managerImgFileName = managerImgFileName;
        }

        private void ClearControls()
        {
            txtManagerName.Clear();
            pictureBoxManagerImg.Image = null;
            txtCountry.Clear();

            txtDay.Clear();
            txtMonth.Clear();
            txtYear.Clear();
            txtManagerName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void btnBrowseManagerImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Lọc cho các loại tệp hình ảnh được chấp nhận
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    tempImagePath = openFileDialog.FileName;

                    pictureBoxManagerImg.Image = new Bitmap(tempImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Image loading failed: {ex.Message}");
                }
            }
        }

        private void FormDialogEditManager_Load(object sender, EventArgs e)
        {
            pictureBoxClubLogo.Image = clubLogo;

            Manager manager = managersBLL.LoadDataByManagerID(managerID);

            // Gán values manager vào controls
            AssignDataToTextBoxes(manager);

            BindCLubsCombobox();
        }

        /// <summary>
        /// Gán dữ liệu table clubs vào cboClubs
        /// </summary>
        private void BindCLubsCombobox()
        {
            cboClubs.DataSource = clubsBLL.LoadData();

            cboClubs.ValueMember = "ClubID";
            cboClubs.DisplayMember = "ClubName";

            // Hiển thị club name của đội đc chọn
            Club club = clubsBLL.LoadDataByID(clubID);
            cboClubs.Text = club.ClubName;
        }

        private void AssignDataToTextBoxes(Manager manager)
        {
            if (File.Exists(shortcutManagerImgFilePath + manager.Image) && manager.Image != "")
                pictureBoxManagerImg.Image = Image.FromFile(shortcutManagerImgFilePath + manager.Image);

            txtManagerName.Text = manager.ManagerName != null ? manager.ManagerName : "";

            if (manager.DOB != null)
            {
                DateTime dob = (DateTime)manager.DOB;
                txtDay.Text = dob.Day.ToString();
                txtMonth.Text = dob.Month.ToString();
                txtYear.Text = dob.Year.ToString();
            }

            txtCountry.Text = manager.Country != null ? manager.Country : "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Are you sure to edit?",
                                          "Confirm",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                EditManager();
            }
        }

        private void EditManager()
        {
            throw new NotImplementedException();
        }

        private void EditClub()
        {
            if (!ValidateTextBoxValues())
                return;

            Manager manager = getManager();

            if (tempImagePath != "" && File.Exists(tempImagePath))
            {
                string destinationFilePath = Path.Combine(shortcutManagerImgFilePath, manager.Image);
                try
                {
                    // Sao chép tệp hình ảnh từ thư mục nguồn sang thư mục đích
                    // True để ghi đè nếu tệp đã tồn tại
                    File.Copy(tempImagePath, destinationFilePath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error occurred while copying image file: {ex.Message}");
                }
            }

            bool isEditManagerSuccess = managersBLL.EditData(manager);
            if (isEditManagerSuccess)
            {
                MessageBox.Show("Edited manager successfully!");
                Close();
            }
            else
                MessageBox.Show("Edit manager failed!");
        }

        #region Validate textboxes value
        private bool ValidateTextBoxValues()
        {
            if (!ValidateName())
                return false;

            if (!ValidateDOB())
                return false;

            if (!ValidateCountry())
                return false;

            return true;
        }

        private bool ValidateCountry()
        {
            if (txtCountry.Text == "")
            {
                MessageBox.Show("Please enter a manager country");
                return false;
            }

            if (txtCountry.Text.Length > 255)
            {
                MessageBox.Show("Country must be less than 255 characters!");
                return false;
            }
            return true;
        }

        private bool ValidateDOB()
        {
            int day = txtDay.Text != "" ? int.Parse(txtDay.Text) : 0;
            int month = txtMonth.Text != "" ? int.Parse(txtMonth.Text) : 0;
            int year = txtYear.Text != "" ? int.Parse(txtYear.Text) : 0;

            if (day < 1 || day > 31)
            {
                MessageBox.Show("Invalid day!");
                return false;
            }
            if (month < 1 || month > 12)
            {
                MessageBox.Show("Invalid month!");
                return false;
            }
            if (year < 1900 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Invalid year!");
                return false;
            }
            try
            {
                DateTime dob = new DateTime(year, month, day);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Invalid DOB!");
                return false;
            }
        }


        private bool ValidateName()
        {
            if (txtManagerName.Text == "")
            {
                MessageBox.Show("Please enter a name!");
                return false;
            }

            if (txtManagerName.Text.Length > 255)
            {
                MessageBox.Show("Name must be less than 255 characters!");
                return false;
            }
            return true;
        }
        #endregion

        private Manager getManager()
        {
            Manager manager = new Manager();
            manager.ManagerID = managerID;
            manager.ClubID = (int)cboClubs.SelectedValue;
            manager.ManagerName = txtManagerName.Text;
            manager.Country = txtCountry.Text;

            manager.DOB = getDOB();

            if (string.IsNullOrEmpty(tempImagePath))
                manager.Image = managerImgFileName;
            else
                manager.Image = Path.GetFileName(tempImagePath);

            return manager;
        }

        private DateTime? getDOB()
        {
            int day = Convert.ToInt32(txtDay.Text);
            int month = Convert.ToInt32(txtMonth.Text);
            int year = Convert.ToInt32(txtYear.Text);

            return new DateTime(year, month, day);
        }

        private void word_keyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeyPress(e);
        }

        private void numeric_keyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumericKeyPress(e);
        }

        // Phương thức chung để kiểm tra ký tự số và phím Backspace
        private void CheckNumericKeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự được nhập không phải là số hoặc không phải phím điều khiển (như phím Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Ngăn người dùng nhập ký tự đó vào TextBox
                e.Handled = true;
            }
        }

        // Phương thức chung để kiểm tra ký tự chữ
        private void CheckWordKeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự người dùng nhập vào có phải là chữ, khoảng trắng hoặc ký tự điều khiển không
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                // Nếu không phải là chữ, khoảng trắng và không phải là ký tự điều khiển (như phím Backspace), thì hủy sự kiện KeyPress
                e.Handled = true;
            }
        }
    }
}
