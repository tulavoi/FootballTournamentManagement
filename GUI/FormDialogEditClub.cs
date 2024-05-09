using BLL;
using DAL;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogEditClub : Form
    {
        ClubsBLL clubsBLL = new ClubsBLL();

        private int clubID;
        private string clubLogo;

        // Biến tạm thời để lưu đường dẫn của ảnh đã chọn
        private string tempImagePath = "";

        public FormDialogEditClub(int clubID, string clubLogo)
        {
            InitializeComponent();
            this.clubID = clubID;
            this.clubLogo = clubLogo;
        }

        private void FormDialogEditClub_Load(object sender, EventArgs e)
        {
            Club club = clubsBLL.LoadDataByID(clubID);
            txtClubName.Text = club.ClubName;
            pictureBoxClubLogo.Image = Image.FromFile($"Images\\Logos\\{club.Logo}");
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

                    pictureBoxClubLogo.Image = new Bitmap(tempImagePath);
                    Console.WriteLine(tempImagePath);
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
                EditClub();
            }
        }

        private void EditClub()
        {
            Club club = new Club();
            club.ClubID = clubID;
            club.ClubName = txtClubName.Text;

            // Kiểm tra xem có cần sao chép hình ảnh hay không
            if (tempImagePath == "")
                club.Logo = clubLogo;
            else
                club.Logo = CopyImageToDestinationFolder();

            bool isEditClubSuccess = clubsBLL.EditData(club);
            if (isEditClubSuccess)
            {
                MessageBox.Show("Edited club successfully!");
                Close();
            }
            else
                MessageBox.Show("Edit club failed!");
        }

        private string CopyImageToDestinationFolder()
        {
            string logoFileName = Path.GetFileName(tempImagePath);
            //Tạo path thư mục đích
            string destinationFolder = "Images\\Logos";

            try
            {
                // Tạo đường dẫn đầy đủ đến tệp hình ảnh trong thư mục đích
                string destinationFilePath = Path.Combine(destinationFolder, logoFileName);

                if (!File.Exists(destinationFilePath))
                {
                    // Sao chép tệp hình ảnh từ thư mục nguồn sang thư mục đích
                    // Thêm true để ghi đè nếu tệp đã tồn tại
                    var sourceFile = new FileInfo(tempImagePath);
                    sourceFile.CopyTo(destinationFilePath, true);
                }
                return logoFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while copying image file: {ex.Message}");
                return null;
            }
        }
    }
}
