using BLL;
using DAL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogAddPlayer : Form
    {
        PlayersBLL playersBLL = new PlayersBLL();

        int clubID;

        Bitmap clubLogo;

        string tempImagePath = "";

        string defaultPlayerImgFilePath = "Images\\Players\\photo-missing.png";

        public FormDialogAddPlayer(int clubID, Bitmap clubLogo)
        {
            InitializeComponent();
            this.clubID = clubID;
            this.clubLogo = clubLogo;
        }

        private void ClearControls()
        {
            txtPlayerName.Clear();
            pictureBoxPlayerImg.Image = Image.FromFile(defaultPlayerImgFilePath);
            txtNumber.Clear();
            txtCountry.Clear();
            txtDay.Clear();
            txtMonth.Clear();
            txtYear.Clear();
            txtHeight.Clear();
            txtWeight.Clear();
            txtSalary.Clear();
            cboPosition.SelectedIndex = -1;
            cboFoot.SelectedIndex = -1;
            txtPlayerName.Focus();
        }


        #region Validate textboxes value
        private bool ValidateTextBoxValues()
        {
            if (!ValidateName())
                return false;

            if (!ValidateNumber())
                return false;

            if (!ValidateCountry())
                return false;

            if (!ValidateDOB())
                return false;

            if (!ValidateHeight())
                return false;

            if (!ValidateWeight())
                return false;

            return true;
        }

        private bool ValidateHeight()
        {
            if (txtHeight.Text != "")
            {
                int height = int.Parse(txtHeight.Text);
                if (height > 300)
                {

                    MessageBox.Show("Invalid height!");
                    return false;
                }
            }
            return true;
        }

        private bool ValidateWeight()
        {
            if (txtWeight.Text != "")
            {
                int weight = int.Parse(txtWeight.Text);
                if (weight > 300)
                {

                    MessageBox.Show("Invalid weight!");
                    return false;
                }
            }
            return true;
        }

        private bool ValidateNumber()
        {
            if (txtNumber.Text == "")
            {
                MessageBox.Show("Please enter a shirt number!");
                return false;
            }
            if (txtNumber.Text.Length > 3)
            {
                MessageBox.Show("Shirt number must be less than 4 characters!");
                return false;
            }
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
            if (txtPlayerName.Text == "")
            {
                MessageBox.Show("Please enter a name!");
                return false;
            }

            if (txtPlayerName.Text.Length > 255)
            {
                MessageBox.Show("Name must be less than 255 characters!");
                return false;
            }
            return true;
        }
        #endregion


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

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumericKeyPress(e);
        }

        private void txtPlayerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeyPress(e);
        }

        private void btnBrowseClubLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Lọc cho các loại tệp hình ảnh được chấp nhận
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    tempImagePath = openFileDialog.FileName;

                    pictureBoxPlayerImg.Image = new Bitmap(tempImagePath);
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Image loading failed: {ex.Message}");
                }
            }
        }


        private Player getPlayer()
        {
            Player player = new Player();
            player.PlayerName = txtPlayerName.Text;
            player.ClubID = clubID;
            player.Number = txtNumber.Text != "" ? Convert.ToInt32(txtNumber.Text) : 0;
            player.Country = txtCountry.Text;

            // Chuyển chiều cao từ cm sang m
            if (txtHeight.Text != "")
            {
                float heightInCm = float.Parse(txtHeight.Text);
                player.Height = heightInCm / 100;
            }
            else
                player.Height = 0;

            player.Weight = txtWeight.Text != "" ? Convert.ToInt32(txtWeight.Text) : 0;
            player.Salary = txtSalary.Text != "" ? Convert.ToInt32(txtSalary.Text) : 0;

            player.DOB = getDOB();

            player.Position = cboPosition.Text;
            player.Foot = cboFoot.Text;

            string playerImgFileName = Path.GetFileName(tempImagePath);
            player.Image = playerImgFileName;

            return player;
        }

        private DateTime? getDOB()
        {
            int day = int.Parse(txtDay.Text);
            int month = int.Parse(txtMonth.Text);
            int year = int.Parse(txtYear.Text);

            return new DateTime(year, month, day);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxValues())
                return;

            Player player = getPlayer();

            if (tempImagePath != "" && File.Exists(tempImagePath))
            {
                string destinationFolder = "Images\\Players";
                string destinationFilePath = Path.Combine(destinationFolder, player.Image);
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

            bool isAddPLayerSuccess = playersBLL.AddData(player);
            if (isAddPLayerSuccess)
            {
                MessageBox.Show("Added player successfully!");
                ClearControls();
            }
            else
                MessageBox.Show("Add player failed!");
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

    }
}
