using BLL;
using DAL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogEditStadium : Form
    {
        StadiumsBLL stadiumsBLL = new StadiumsBLL();
        ClubsBLL clubsBLL = new ClubsBLL();

        string shortcutStadiumImgFilePath = "Images\\Stadiums\\";

        string stadiumID;

        int clubID;

        Bitmap clubLogo;

        string stadiumImgFileName;

        // Biến tạm thời để lưu đường dẫn của ảnh đã chọn
        private string tempImagePath;

        public FormDialogEditStadium(string stadiumID, Bitmap clubLogo, int clubID, string stadiumImgFileName)
        {
            InitializeComponent();
            this.stadiumID = stadiumID;
            this.clubLogo = clubLogo;
            this.clubID = clubID;
            this.stadiumImgFileName = stadiumImgFileName;
        }

        private void FormDialogEditStadium_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            // Gán club logo vào picturebox
            pictureBoxClubLogo.Image = clubLogo;

            Stadium stadium = stadiumsBLL.LoadDataByClubID(clubID);

            // Gán player vào controls
            AssignDataToTextBoxes(stadium);

            BindCLubsCombobox();
        }

        private void ClearControls()
        {
            txtStadiumName.Clear();
            txtWidth.Clear();
            txtLength.Clear();
            txtCapacity.Clear();
            txtYear.Clear();
            txtLocation.Clear();
            pictureBoxStadium.Image = null;
            txtStadiumName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void BindCLubsCombobox()
        {
            cboClubs.DataSource = clubsBLL.LoadData();

            cboClubs.ValueMember = "ClubID";
            cboClubs.DisplayMember = "ClubName";

            // Hiển thị club name của đội đc chọn
            Club club = clubsBLL.LoadDataByID(clubID);
            cboClubs.Text = club.ClubName;
        }

        private void AssignDataToTextBoxes(Stadium stadium)
        {
            txtStadiumName.Text = stadium.StadiumName;

            if (stadium.Size != null)
            {
                string[] parts = stadium.Size.Split(' ');
                string length = parts[0].Trim('m');
                string width = parts[2].Trim('m');
                txtLength.Text = length;
                txtWidth.Text = width;
            }

            txtCapacity.Text = stadium.Capacity != null ? stadium.Capacity.ToString() : "";

            txtLocation.Text = stadium.Location;

            DateTime builtTime = Convert.ToDateTime(stadium.BuiltTime);
            txtYear.Text = builtTime.Year.ToString();

            Club club = clubsBLL.LoadDataByID(clubID);
            cboClubs.Text = club.ClubName;

            pictureBoxStadium.Image = Image.FromFile(shortcutStadiumImgFilePath + stadium.Image);
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

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumericKeyPress(e);
        }

        private void btnBrowseStadiumImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Lọc cho các loại tệp hình ảnh được chấp nhận
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    tempImagePath = openFileDialog.FileName;

                    pictureBoxStadium.Image = new Bitmap(tempImagePath);
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
                EditStadium();
            }
           
        }

        private void EditStadium()
        {
            if (!ValidateTextBoxValues())
                return;

            Stadium stadium = getStadium();

            if (tempImagePath != "" && File.Exists(tempImagePath))
            {
                string destinationFilePath = Path.Combine(shortcutStadiumImgFilePath, stadium.Image);
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

            bool isEditStadiumSuccess = stadiumsBLL.EditData(stadium);
            if (isEditStadiumSuccess)
            {
                MessageBox.Show("Edited stadium successfully!");
                Close();
            }
            else
                MessageBox.Show("Edit stadium failed!");
        }

        private Stadium getStadium()
        {
            Stadium stadium = new Stadium();

            stadium.StadiumID = stadiumID;
            stadium.StadiumName = txtStadiumName.Text;

            string size = txtWidth.Text + "m" + " x " + txtLength.Text + "m";
            stadium.Size = size;

            stadium.Capacity = int.Parse(txtCapacity.Text);

            int year = int.Parse(txtYear.Text);
            stadium.BuiltTime = new DateTime(year, 1, 1);

            stadium.Location = txtLocation.Text;

            stadium.ClubID = (int)cboClubs.SelectedValue;

            if (string.IsNullOrEmpty(tempImagePath))
                stadium.Image = stadiumImgFileName;
            else
                stadium.Image = Path.GetFileName(tempImagePath);

            return stadium;
        }

        #region Validate textboxes value
        private bool ValidateTextBoxValues()
        {
            if (!ValidateName())
                return false;

            if (!ValidateCapacity())
                return false;

            if (!ValidateBuiltYear())
                return false;

            return true;
        }

        private bool ValidateCapacity()
        {
            if (string.IsNullOrEmpty(txtCapacity.Text))
            {
                MessageBox.Show("Please enter capacity!");
                return false;
            }
            return true;
        }

        private bool ValidateBuiltYear()
        {
            int year = txtYear.Text != "" ? int.Parse(txtYear.Text) : 0;

            if (year < 1000 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Invalid year!");
                return false;
            }
            return true;
        }

        private bool ValidateName()
        {
            if (txtStadiumName.Text == "")
            {
                MessageBox.Show("Please enter a name!");
                return false;
            }

            if (txtStadiumName.Text.Length > 255)
            {
                MessageBox.Show("Name must be less than 255 characters!");
                return false;
            }
            return true;
        }
        #endregion
    }
}
