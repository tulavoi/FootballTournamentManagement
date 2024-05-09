using BLL;
using DAL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogAddManager : Form
    {
        ManagersBLL managersBLL = new ManagersBLL();

        int clubID;

        Bitmap clubLogo;

        string tempImagePath = "";

        string defaultManagerImgFileName = "Images\\Managers\\photo-missing.png";

        public FormDialogAddManager(int clubID, Bitmap clubLogo)
        {
            InitializeComponent();
            this.clubID = clubID;
            this.clubLogo = clubLogo;
        }

        private void FormDialogAddManager_Load(object sender, EventArgs e)
        {
            pictureBoxClubLogo.Image = clubLogo;
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

        private Manager getManager()
        {

            Manager manager = new Manager();
            manager.ManagerName = txtManagerName.Text;
            manager.ClubID = clubID;
            manager.Country = txtCountry.Text;

            manager.DOB = getDOB();

            string managerFileName = Path.GetFileName(tempImagePath);
            manager.Image = managerFileName;

            return manager;
        }

        private DateTime? getDOB()
        {
            int day = Convert.ToInt32(txtDay.Text);
            int month = Convert.ToInt32(txtMonth.Text);
            int year = Convert.ToInt32(txtYear.Text);

            return new DateTime(year, month, day);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxValues())
                return;

            Manager manager = getManager();

            if (tempImagePath != "" && File.Exists(tempImagePath))
            {
                string destinationFolder = "Images\\Managers";
                string destinationFilePath = Path.Combine(destinationFolder, manager.Image);
                try
                {
                    //Sao chép tệp hình ảnh từ thư mục nguồn sang thư mục đích
                    //True để ghi đè nếu tệp đã tồn tại
                    File.Copy(tempImagePath, destinationFilePath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error occurred while copying image file: {ex.Message}");
                }
            }

            bool isAddManagerSuccess = managersBLL.AddData(manager);
            if (isAddManagerSuccess)
            {
                MessageBox.Show("Added player successfully!");
                ClearControls();
                pictureBoxManagerImg.Image = Image.FromFile(defaultManagerImgFileName);
            }
            else
                MessageBox.Show("Add player failed!");
        }

        


        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }


        private void dobTextboxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumericKeyPress(e);
        }


        private void txtName_txtCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeyPress(e);
        }


        // Phương thức chung để kiểm tra ký tự số
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
