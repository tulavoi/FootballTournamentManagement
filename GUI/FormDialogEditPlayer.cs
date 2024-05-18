using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogEditPlayer : Form
    {
        PlayersBLL playersBLL = new PlayersBLL();
        ClubsBLL clubsBLL = new ClubsBLL();

        string shortcutPlayerImgFilePath = "Images\\Players\\";

        int playerID;

        int clubID;

        Bitmap clubLogo;

        string playerImgFileName;

        // Biến tạm thời để lưu đường dẫn của ảnh đã chọn
        private string tempImagePath;

        string defaultPlayerImgFilePath = "Images\\Players\\photo-missing.png";

        public FormDialogEditPlayer(int playerID, Bitmap clubLogo, int clubID, string playerImgFileName)
        {
            InitializeComponent();
            this.playerID = playerID;
            this.clubLogo = clubLogo;
            this.clubID = clubID;
            this.playerImgFileName = playerImgFileName;
        }

        private void FormDialogEditPlayer_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            // Gán club logo vào picturebox
            pictureBoxClubLogo.Image = clubLogo;

            Player player = playersBLL.LoadDataByPlayerID(playerID);

            // Gán player vào controls
            AssignDataToTextBoxes(player);

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

        /// <summary>
        /// Gán dữ liệu player từ database vào controls
        /// </summary>
        /// <param name="player"></param>
        private void AssignDataToTextBoxes(Player player)
        {
            txtPlayerName.Text = player.PlayerName != null ? player.PlayerName : "";
            
            if (File.Exists(shortcutPlayerImgFilePath + player.Image) && player.Image != "")
                pictureBoxPlayerImg.Image = Image.FromFile(shortcutPlayerImgFilePath + player.Image);

            txtNumber.Text = player.Number != null ? player.Number.ToString() : "";

            txtCountry.Text = player.Country != null ? player.Country : "";

            if (player.DOB != null)
            {
                DateTime dob = (DateTime)player.DOB;
                txtDay.Text = dob.Day.ToString();
                txtMonth.Text = dob.Month.ToString();
                txtYear.Text = dob.Year.ToString();
            }

            // Chuyển height từ m qua cm
            txtHeight.Text = player.Height != null ? (player.Height * 100).ToString() : "";
            txtWeight.Text = player.Weight != null ? player.Weight.ToString(): "";

            txtSalary.Text = player.Salary != null ? player.Salary.ToString() : "";

            cboPosition.Text = player.Position != null ? player.Position : "";

            cboFoot.Text = player.Foot != null ? player.Foot : "";
        }

        /// <summary>
        /// Phương thức chung để kiểm tra ký tự số và phím Backspace
        /// </summary>
        /// <param name="e"></param>
        private void CheckNumericKeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự được nhập không phải là số hoặc không phải phím điều khiển (như phím Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Ngăn người dùng nhập ký tự đó vào TextBox
                e.Handled = true;
            }
        }

        /// <summary>
        /// Phương thức chung để kiểm tra ký tự chữ
        /// </summary>
        /// <param name="e"></param>
        private void CheckWordKeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự người dùng nhập vào có phải là chữ, khoảng trắng hoặc ký tự điều khiển không
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                // Nếu không phải là chữ, khoảng trắng và không phải là ký tự điều khiển (như phím Backspace), thì hủy sự kiện KeyPress
                e.Handled = true;
            }
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

            Club club = clubsBLL.LoadDataByID(clubID);
            cboClubs.Text = club.ClubName;
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

            if (!ValidateSalary())
                return false;

            return true;
        }

        private bool ValidateSalary()
        {
            if (txtSalary.Text.Length > 9)
            {
                MessageBox.Show("Invalid salary!");
                return false;
            }
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


        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumericKeyPress(e);
        }

        private void txtCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeyPress(e);
        }

        private void txtPlayerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeyPress(e);
        }


        private void btnBrowseClubLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Are you sure to edit?",
                                          "Confirm",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                EditPlayer();
            }
        }

        private void EditPlayer()
        {
            if (!ValidateTextBoxValues())
                return;

            Player player = getPlayer();

            if (tempImagePath != "" && File.Exists(tempImagePath))
            {
                string destinationFilePath = Path.Combine(shortcutPlayerImgFilePath, player.Image);
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

            bool isEditPLayerSuccess = playersBLL.EditData(player);
            if (isEditPLayerSuccess)
            {
                MessageBox.Show("Edited player successfully!");
                Close();
            }
            else
                MessageBox.Show("Edit player failed!");
        }


        private Player getPlayer()
        {
            Player player = new Player();
            player.PlayerID = playerID;
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
            // catch loi
            player.Salary = txtSalary.Text != "" ? int.Parse(txtSalary.Text) : 0;

            player.DOB = getDOB();
            player.Position = cboPosition.Text;
            player.Foot = cboFoot.Text;
            player.ClubID = (int)cboClubs.SelectedValue;

            if (string.IsNullOrEmpty(tempImagePath))
                player.Image = playerImgFileName;
            else
                player.Image = Path.GetFileName(tempImagePath);

            return player;
        }


        private DateTime? getDOB()
        {
            int day = int.Parse(txtDay.Text);
            int month = int.Parse(txtMonth.Text);
            int year = int.Parse(txtYear.Text);

            return new DateTime(year, month, day);
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

    }
}
