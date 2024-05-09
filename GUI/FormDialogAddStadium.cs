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
    public partial class FormDialogAddStadium : Form
    {
        StadiumsBLL stadiumsBLL = new StadiumsBLL();

        int clubID;

        Bitmap clubLogo;

        string tempImagePath = "";

        public FormDialogAddStadium(int clubID, Bitmap clubLogo)
        {
            InitializeComponent();
            this.clubID = clubID;
            this.clubLogo = clubLogo;
        }

        private void FormDialogAddStadium_Load(object sender, EventArgs e)
        {
            pictureBoxClubLogo.Image = clubLogo;
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
            if (!ValidateTextBoxValues())
                return;

            Stadium stadium = getStadium();

            // Nếu hình ảnh vừa thêm vào stadium chưa có trong project thì thêm vào project
            if (tempImagePath != "" && File.Exists(tempImagePath))
            {
                string destinationFolder = "Images\\Stadiums\\";
                string destinationFilePath = Path.Combine(destinationFolder, stadium.Image);
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

            bool isAddStadiumSuccess = stadiumsBLL.AddData(stadium);
            if (isAddStadiumSuccess)
            {
                MessageBox.Show("Added stadium successfully!");
                ClearControls();
            }
            else
                MessageBox.Show("Add stadium failed!");
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

        private Stadium getStadium()
        {
            Stadium stadium = new Stadium();

            // Tạo stadium Id
            string stadiumID = "Stadium " + clubID;
            stadium.StadiumID = stadiumID;

            stadium.StadiumName = txtStadiumName.Text;

            stadium.ClubID = clubID;

            string size = txtWidth.Text + "m" + " x " + txtLength.Text + "m";
            stadium.Size = size;

            stadium.Capacity = int.Parse(txtCapacity.Text);

            int year = int.Parse(txtYear.Text); 
            stadium.BuiltTime = new DateTime(year, 1, 1);

            stadium.Location = txtLocation.Text;

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }
}
