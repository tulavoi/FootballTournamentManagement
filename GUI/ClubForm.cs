using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class ClubForm : Form
    {
        SeasonsBLL seasonBLL = new SeasonsBLL();

        SeasonClubsBLL ssClubsBLL = new SeasonClubsBLL();

        ClubsBLL clubsBLL = new ClubsBLL();

        PlayersBLL playersBLL = new PlayersBLL();

        ManagersBLL managersBLL = new ManagersBLL();

        StadiumsBLL stadiumsBLL = new StadiumsBLL();

        // Biến tạm thời để lưu đường dẫn của ảnh đã chọn
        private string tempImagePath = "";

        // Biến để lưu club id đã chọn
        private int selectedClubID = 0;

        private string shortcutLogoPath = "Images\\Logos\\";
        private string shortcutPlayerImgPath = "Images\\Players\\";
        string shortcutManagerImgPath = "Images\\Managers\\";
        string shortcutStadiumImgPath = "Images\\Stadiums\\";

        public ClubForm()
        {
            InitializeComponent();
        }


        private void ClubForm_Load(object sender, EventArgs e)
        {
            // Gán dữ liệu của table Seasons vào cboSeason
            BindSeasonCombobox();

            // Chỉnh lại font size header text của datagridviews
            dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);
            dgvPlayers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);
            dgvManagers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);
        }


        #region Tab Control: Club List
        /// <summary>
        /// Load dữ liệu vào dgvClubs dựa theo season id được chọn từ cboSeason.
        /// </summary>
        private void LoadClubBySeasonID()
        {
            int seasonID = Convert.ToInt32(cboSeason.SelectedValue);
            List<Club> clubs = ssClubsBLL.LoadDataBySeasonID(seasonID);
            LoadDataOfClubsToDataGridView(clubs);
        }


        /// <summary>
        /// Hàm này được sử dụng để load dữ liệu của table Clubs.
        /// Gọi hàm LoadData() từ lớp BLL để truy vấn danh sách các câu lạc bộ từ cơ sở dữ liệu.
        /// Sau đó, nó gọi hàm LoadDataOfClubsToDataGridView() để hiển thị dữ liệu trong dgvClubs.
        /// </summary>
        private void LoadDataAllClubs()
        {
            List<Club> clubs = clubsBLL.LoadData();
            LoadDataOfClubsToDataGridView(clubs);
        }


        private void tabControlClubForm_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (selectedClubID == 0)
                tabControlClubForm.SelectedIndex = 0;
        }


        /// <summary>
        /// Load dữ liệu của table Clubs vào dgvClubs.
        /// </summary>
        /// <param name="clubs"></param>
        private void LoadDataOfClubsToDataGridView(List<Club> clubs)
        {
            foreach (var club in clubs)
            {
                int rowIndex = dgvClubs.Rows.Add();

                // Kiểm tra nếu có đủ hàng trong DataGridView
                if (rowIndex != -1 && rowIndex < dgvClubs.Rows.Count)
                {
                    dgvClubs.Rows[rowIndex].Cells[0].Tag = club.ClubID;
                    dgvClubs.Rows[rowIndex].Cells[1].Value = Image.FromFile(shortcutLogoPath + club.Logo);
                    dgvClubs.Rows[rowIndex].Cells[2].Tag = club.Logo;
                    dgvClubs.Rows[rowIndex].Cells[3].Value = club.ClubName;
                }
            }
        }


        /// <summary>
        /// Gán tất cả dữ liệu của table Seasons vào cboSeason.
        /// </summary>
        private void BindSeasonCombobox()
        {
            List<Season> seasons = seasonBLL.LoadData();

            // Thêm lựa chọn All vào đầu danh sách cboSeason 
            Season season = new Season();
            season.SeasonName = "ALL";
            seasons.Insert(0, season);
            cboSeason.DataSource = seasons;
            cboSeason.ValueMember = "SeasonID";
            cboSeason.DisplayMember = "SeasonName";
        }


        /// <summary>
        /// Clear các controls của phần add club
        /// </summary>
        private void ClearAddClubControls()
        {
            txtClubName.Clear();
            pictureBoxClubLogo.Image = null;
            txtClubName.Focus();
        }


        /// <summary>
        /// Hàm này được gọi khi thay đổi index trong cboSeason.
        /// Xóa all row in dgvClubs
        /// Nếu lựa chọn là "ALL" hoặc không có lựa chọn, gọi hàm LoadDataAllClubs().
        /// Ngược lại, gọi hàm LoadClubBySeasonID().
        /// Cập nhật số lượng club hiển thị.
        /// </summary>
        private void cboSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvClubs.Rows.Clear();
            if (cboSeason.SelectedIndex == 0 || cboSeason.SelectedIndex == -1)
            {
                LoadDataAllClubs();
                lblNumOfClubs.Text = dgvClubs.Rows.Count.ToString();
            }
            else
            {
                LoadClubBySeasonID();
                lblNumOfClubs.Text = dgvClubs.Rows.Count.ToString();
            }
        }


        /// <summary>
        /// Hàm được gọi khi một ô trong dgvClubs được nhấp đúp.
        /// Chuyển sang tabClub của tabControlClubForm.
        /// Nếu chỉ mục hàng là hợp lệ, lấy thông tin của câu lạc bộ được chọn và hiển thị trong tab chỉnh sửa.
        /// </summary>
        private void dgvClubs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 4 && e.ColumnIndex != 5)
            {
                tabControlClubForm.SelectedIndex = 1;

                if (e.RowIndex >= 0 && e.RowIndex < dgvClubs.Rows.Count)
                {
                    DataGridViewRow selectedRow = dgvClubs.Rows[e.RowIndex];

                    selectedClubID = Convert.ToInt32(selectedRow.Cells[0].Tag);

                    AssignClubLogoToPictureBoxes(selectedRow);

                    AssignClubNameToLabels(selectedRow);

                    // Sau khi nhấn chọn 1 club trong datagridview thì chuyển sang tab control club detail
                    tabControlClubForm.SelectedIndex = 1;

                    LoadDataOfPlayersToDataGridView(selectedClubID);
                    LoadDataOfManagersToDataGridView(selectedClubID);
                    LoadDataOfStaidumToControlsInTabStadium(selectedClubID);
                }
            }
        }

        private void AssignClubNameToLabels(DataGridViewRow selectedRow)
        {
            lblClubNameInPLayersTab.Text = selectedRow.Cells[3].Value.ToString();
            lblClubNameInManagersTab.Text = selectedRow.Cells[3].Value.ToString();
            lblClubNameInStadiumTab.Text = selectedRow.Cells[3].Value.ToString();
        }

        private void AssignClubLogoToPictureBoxes(DataGridViewRow selectedRow)
        {
            pictureBoxClubLogoDetailInPlayersTab.Image = (Bitmap)selectedRow.Cells[1].Value;
            pictureBoxClubLogoDetailInManagersTab.Image = (Bitmap)selectedRow.Cells[1].Value;
            pictureBoxClubLogoDetailInStadiumTab.Image = (Bitmap)selectedRow.Cells[1].Value;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            tabControlClubForm.SelectedIndex = 0;
        }


        /// <summary>
        /// Xử lý sự kiện CellFormatting của DataGridView để định dạng nền cho các ô chứa nút chỉnh sửa và xóa.
        /// </summary>
        /// /// <param name="sender">Đối tượng gửi sự kiện.</param>
        /// <param name="e">Đối số của sự kiện CellFormatting.</param>
        /// <param name="editColumn">Cột chứa nút chỉnh sửa.</param>
        /// <param name="deleteColumn">Cột chứa nút xóa.</param>
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e, DataGridViewButtonColumn editColumn, DataGridViewButtonColumn deleteColumn)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv != null && (e.ColumnIndex == editColumn.Index || e.ColumnIndex == deleteColumn.Index))
            {
                object cellValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (cellValue != null && cellValue.ToString() == "Edit")
                {
                    DataGridViewButtonCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Style.BackColor = Color.FromArgb(70, 20, 80);
                }
                else if (cellValue != null && cellValue.ToString() == "Delete")
                {
                    DataGridViewButtonCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Style.BackColor = Color.FromArgb(180, 40, 130);
                }
            }
        }


        private void dgvClubs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, editColumn: dgvClubs.Columns[4] as DataGridViewButtonColumn, deleteColumn: dgvClubs.Columns[5] as DataGridViewButtonColumn);
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

                    pictureBoxClubLogo.Image = new Bitmap(tempImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Image loading failed: {ex.Message}");
                }
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAddClubControls();
        }


        private void dgvClubs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            // Button column edit
            if (e.ColumnIndex == 4)
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvClubs.Rows.Count)
                    return;

                int clubID = Convert.ToInt32(dgvClubs.Rows[e.RowIndex].Cells[0].Tag);
                string clubLogo = dgvClubs.Rows[e.RowIndex].Cells[2].Tag.ToString();
                FormDialogEditClub frm = new FormDialogEditClub(clubID, clubLogo);
                frm.ShowDialog();
                cboSeason_SelectedIndexChanged(sender, e);
            }

            // Button column delete
            if (e.ColumnIndex == 5)
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvClubs.Rows.Count)
                    return;

                DialogResult rs = MessageBox.Show("Are you sure to delete?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int clubID = Convert.ToInt32(dgvClubs.Rows[e.RowIndex].Cells[0].Tag);

                    bool isDeleteClubSuccess = clubsBLL.DeleteData(clubID);
                    if (isDeleteClubSuccess)
                    {
                        MessageBox.Show("Deleted club successfully!");
                        cboSeason.SelectedIndex = 0;
                        cboSeason_SelectedIndexChanged(sender, e);
                        ClearAddClubControls();
                    }
                    else
                        MessageBox.Show("Delete club failed, this club is in a season!");
                }
            }
        }


        private void btnSaveClub_Click(object sender, EventArgs e)
        {
            Club club = new Club();
            club.ClubName = txtClubName.Text;
            string logoFileName = Path.GetFileName(tempImagePath);
            club.Logo = logoFileName;

            string destinationFolder = "Images\\Logos";

            try
            {
                string destinationFilePath = Path.Combine(destinationFolder, logoFileName);

                if (!File.Exists(destinationFilePath))
                {
                    // Sao chép tệp hình ảnh từ thư mục nguồn sang thư mục đích
                    // True để ghi đè nếu tệp đã tồn tại
                    File.Copy(tempImagePath, destinationFilePath, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while copying image file: {ex.Message}");
            }

            bool isAddClubSuccess = clubsBLL.AddData(club);
            if (isAddClubSuccess)
            {
                MessageBox.Show("Added club successfully!");
                cboSeason.SelectedIndex = 0;
                cboSeason_SelectedIndexChanged(sender, e);
                ClearAddClubControls();
            }
            else
                MessageBox.Show("Add club failed!");
        }
        #endregion


        #region Tab Control: Club Detail, PLayers
        /// <summary>
        /// Load dữ liệu của table Players vào dgvPLayers.
        /// </summary>
        /// <param name="clubs"></param>
        private void LoadDataOfPlayersToDataGridView(int clubID)
        {
            List<Player> players = playersBLL.LoadDataByClubID(clubID);

            // Gán số lượng player vào lblCountPlayer
            lblCountPlayer.Text = players.Count.ToString();

            DisplayPlayersToDatagridview(players);
        }


        private void btnOpenDialogAddPlayer_Click(object sender, EventArgs e)
        {
            Bitmap clubLogo = (Bitmap)pictureBoxClubLogoDetailInPlayersTab.Image;
            FormDialogAddPlayer frm = new FormDialogAddPlayer(selectedClubID, clubLogo);
            frm.ShowDialog();

            // Sau khi đóng form dialog add player
            LoadDataOfPlayersToDataGridView(selectedClubID);
        }


        private void dgvPlayers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, editColumn: dgvPlayers.Columns[9] as DataGridViewButtonColumn, deleteColumn: dgvPlayers.Columns[10] as DataGridViewButtonColumn);
        }


        private void dgvPlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 9 && e.ColumnIndex != 10)
                return;

            // Button column edit
            if (e.ColumnIndex == 9)
            {
                HandleEditPlayerButtonClick(e.RowIndex);
            }

            // Button column delete
            if (e.ColumnIndex == 10)
            {
                HandleDeletePlayerButtonClick(e.RowIndex);
            }
        }


        private void HandleDeletePlayerButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            string playerName = dgvPlayers.Rows[rowIndex].Cells[4].Value.ToString();

            DialogResult rs = MessageBox.Show($"Are you sure to delete \"{playerName}\"?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                int playerID = Convert.ToInt32(dgvPlayers.Rows[rowIndex].Cells[1].Tag);
                bool isDeletePlayerSuccess = playersBLL.DeleteData(playerID);
                if (isDeletePlayerSuccess)
                {
                    MessageBox.Show($"Deleted player \"{playerName}\" successfully!");
                    LoadDataOfPlayersToDataGridView(selectedClubID);
                }
                else
                    MessageBox.Show($"Delete player \"{playerName}\" failed!");
            }
        }


        private void HandleEditPlayerButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            // Lấy player id
            int playerID = Convert.ToInt32(dgvPlayers.Rows[rowIndex].Cells[1].Tag);

            // Lấy club logo từ picturebox
            Bitmap clubLogo = (Bitmap)pictureBoxClubLogoDetailInPlayersTab.Image;

            // Lấy tên file của hình ảnh
            string playerImgFileName = dgvPlayers.Rows[rowIndex].Cells[3].Tag != null ? dgvPlayers.Rows[rowIndex].Cells[3].Tag.ToString() : "";

            FormDialogEditPlayer frm = new FormDialogEditPlayer(playerID, clubLogo, selectedClubID, playerImgFileName);
            frm.ShowDialog();

            // Sau khi đóng form dialog edit player
            LoadDataOfPlayersToDataGridView(selectedClubID);
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Are you sure to delete all players?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                bool isDeleteClubSuccess = playersBLL.DeleteAllData(selectedClubID);
                if (isDeleteClubSuccess)
                {
                    MessageBox.Show("Deleted all players successfully!");
                    LoadDataOfPlayersToDataGridView(selectedClubID);
                }
                else
                    MessageBox.Show("Delete all players failed!");
            }
        }

        // Tìm kiếm player
        private void txtSearchPlayer_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchPlayer.Text.ToLower();
            List<Player> players = playersBLL.SearchPlayer(keyword, selectedClubID);

            DisplayPlayersToDatagridview(players);
        }

        private void DisplayPlayersToDatagridview(List<Player> players)
        {
            dgvPlayers.Rows.Clear();

            int i = 1;
            foreach (var player in players)
            {
                int rowIndex = dgvPlayers.Rows.Add();
                if (rowIndex != -1 && rowIndex < dgvPlayers.Rows.Count)
                {
                    dgvPlayers.Rows[rowIndex].Cells[0].Value = i++;
                    dgvPlayers.Rows[rowIndex].Cells[1].Tag = player.PlayerID;

                    // Kiểm tra nếu cầu thủ không có hình ảnh thì gán cho cầu thủ hình ảnh mặc định
                    string imageFileName = shortcutPlayerImgPath + player.Image;
                    if (File.Exists(imageFileName))
                        dgvPlayers.Rows[rowIndex].Cells[2].Value = Image.FromFile(shortcutPlayerImgPath + player.Image);
                    else
                        dgvPlayers.Rows[rowIndex].Cells[2].Value = Image.FromFile(shortcutPlayerImgPath + "photo-missing.png");

                    DateTime dob = Convert.ToDateTime(player.DOB);

                    dgvPlayers.Rows[rowIndex].Cells[3].Tag = player.Image;
                    dgvPlayers.Rows[rowIndex].Cells[4].Value = player.PlayerName;
                    dgvPlayers.Rows[rowIndex].Cells[5].Value = dob.ToString("dd/MM/yyyy");
                    dgvPlayers.Rows[rowIndex].Cells[6].Value = player.Number;
                    dgvPlayers.Rows[rowIndex].Cells[7].Value = player.Country;
                    dgvPlayers.Rows[rowIndex].Cells[8].Value = player.Position;
                }
            }
        }
        #endregion


        #region Tab Control: Club Detail, Managers
        private void tabControlClubDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlClubDetail.SelectedIndex == 1)
            {
                LoadDataOfManagersToDataGridView(selectedClubID);
            }
        }

        /// <summary>
        /// Load dữ liệu của table Managers vào dgvManagerls.
        /// </summary>
        private void LoadDataOfManagersToDataGridView(int selectedClubID)
        {
            dgvManagers.Rows.Clear();

            List<Manager> managers = managersBLL.LoadManagerByClubID(selectedClubID);

            lblCountManager.Text = managers.Count.ToString();

            foreach (var manager in managers)
            {
                int rowIndex = dgvManagers.Rows.Add();
                if (rowIndex != -1 && rowIndex < dgvManagers.Rows.Count)
                {
                    dgvManagers.Rows[rowIndex].Cells[0].Tag = manager.ManagerID;
                    // Kiểm tra nếu cầu thủ không có hình ảnh thì gán cho cầu thủ hình ảnh mặc định
                    string imageFileName = shortcutManagerImgPath + manager.Image;
                    if (File.Exists(imageFileName))
                        dgvManagers.Rows[rowIndex].Cells[1].Value = Image.FromFile(imageFileName);
                    else
                        dgvManagers.Rows[rowIndex].Cells[1].Value = Image.FromFile(shortcutManagerImgPath + "photo-missing.png");

                    dgvManagers.Rows[rowIndex].Cells[3].Tag = manager.Image;
                    dgvManagers.Rows[rowIndex].Cells[3].Value = manager.ManagerName;
                    dgvManagers.Rows[rowIndex].Cells[4].Value = manager.Country;

                    DateTime dob = Convert.ToDateTime(manager.DOB);

                    dgvManagers.Rows[rowIndex].Cells[5].Value = dob.ToString("dd/MM/yyyy");
                }
            }
        }


        private void btnOpenDialogAddManager_Click(object sender, EventArgs e)
        {
            Bitmap clubLogo = (Bitmap)pictureBoxClubLogoDetailInPlayersTab.Image;
            FormDialogAddManager frm = new FormDialogAddManager(selectedClubID, clubLogo);
            frm.ShowDialog();

            // Sau khi đóng form dialog add player
            LoadDataOfManagersToDataGridView(selectedClubID);
        }

        private void dgvManagers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, editColumn: dgvManagers.Columns[6] as DataGridViewButtonColumn, deleteColumn: dgvManagers.Columns[7] as DataGridViewButtonColumn);
        }


        private void dgvManagers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 6 là button column edit, 7 là button column delete
            if (e.ColumnIndex != 6 && e.ColumnIndex != 7)
                return;

            // Button column edit
            if (e.ColumnIndex == 6)
            {
                HandleEditManagerButtonClick(e.RowIndex);
            }

            // Button column delete
            if (e.ColumnIndex == 7)
            {
                HandleDeleteManagerButtonClick(e.RowIndex);
            }

        }

        private void HandleDeleteManagerButtonClick(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvManagers.Rows.Count)
                return;

            string managerName = dgvManagers.Rows[rowIndex].Cells[3].Value.ToString();
            DialogResult rs = MessageBox.Show($"Are you sure to delete \"{managerName}\"?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                int managerID = Convert.ToInt32(dgvManagers.Rows[rowIndex].Cells[0].Tag);
                Console.WriteLine(managerID);
                bool isDeleteManagerSuccess = managersBLL.DeleteData(managerID);
                if (isDeleteManagerSuccess)
                {
                    MessageBox.Show($"Deleted manager \"{managerName}\" successfully!");
                    LoadDataOfManagersToDataGridView(selectedClubID);
                }
                else
                    MessageBox.Show($"Delete manager \"{managerName}\" failed!");
            }
        }

        private void HandleEditManagerButtonClick(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvManagers.Rows.Count)
                return;

            // Lấy player id
            int managerID = Convert.ToInt32(dgvManagers.Rows[rowIndex].Cells[0].Tag);

            // Lấy club logo từ picturebox
            Bitmap clubLogo = (Bitmap)pictureBoxClubLogoDetailInManagersTab.Image;

            // Lấy tên file của hình ảnh, nếu không có hình ảnh thì gán = ""
            string managerImgFileName = dgvManagers.Rows[rowIndex].Cells[3].Tag != null ? dgvManagers.Rows[rowIndex].Cells[3].Tag.ToString() : "";

            FormDialogEditManager frm = new FormDialogEditManager(managerID, clubLogo, selectedClubID, managerImgFileName);
            frm.ShowDialog();

            // Sau khi đóng form dialog edit manager
            LoadDataOfManagersToDataGridView(selectedClubID);
        }
        #endregion


        #region Tab Control: Club Detail, Stadium
        // Tạo biến stadiumID để lưu giá trị id của stadium
        string stadiumID;
        string stadiumImgFileName;
        private void LoadDataOfStaidumToControlsInTabStadium(int selectedClubID)
        {
            Stadium stadium = stadiumsBLL.LoadDataByClubID(selectedClubID);

            if (stadium != null)
            {
                btnOpenDialogAddStadium.Enabled = false;

                if (File.Exists(shortcutStadiumImgPath + stadium.Image))
                    pictureBoxStadium.Image = Image.FromFile(shortcutStadiumImgPath + stadium.Image);

                lblStadiumName.Text = stadium.StadiumName;

                lblSize.Text = stadium.Size.ToString();
                lblCapacity.Text = stadium.Capacity.ToString();
                lblLocation.Text = stadium.Location.ToString();

                DateTime builtTime = Convert.ToDateTime(stadium.BuiltTime);
                lblBuiltYear.Text = builtTime.Year.ToString();

                stadiumImgFileName = stadium.Image;
                stadiumID = stadium.StadiumID;
            }
            if (stadium == null)
            {
                // Kích hoạt nút thêm stadium khi 1 club không có stadium
                btnOpenDialogAddStadium.Enabled = true;

                ClearControlsInStadiumTab();
            }
        }

        private void ClearControlsInStadiumTab()
        {
            pictureBoxStadium.Image = null;
            lblStadiumName.Text = "N/A";
            lblSize.Text = "N/A";
            lblCapacity.Text = "N/A";
            lblLocation.Text = "N/A";
            lblBuiltYear.Text = "N/A";
        }

        private void btnAddStadium_Click(object sender, EventArgs e)
        {
            Bitmap clubLogo = (Bitmap)pictureBoxClubLogoDetailInPlayersTab.Image;
            FormDialogAddStadium frm = new FormDialogAddStadium(selectedClubID, clubLogo);
            frm.ShowDialog();
            LoadDataOfStaidumToControlsInTabStadium(selectedClubID);
        }

        private void btnDeleteStadium_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show($"Are you sure to delete stadium \"{lblStadiumName.Text}\"?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                bool isDeleteStadiumSuccess = stadiumsBLL.DeleteData(stadiumID);
                if (isDeleteStadiumSuccess)
                {
                    MessageBox.Show("Deleted stadium successfully!");
                    LoadDataOfStaidumToControlsInTabStadium(selectedClubID);
                }
                else
                    MessageBox.Show("Delete stadium failed!");
            }
        }

        private void btnOpenDialogEditStadium_Click(object sender, EventArgs e)
        {
            Bitmap clubLogo = (Bitmap)pictureBoxClubLogoDetailInStadiumTab.Image;
            FormDialogEditStadium frm = new FormDialogEditStadium(stadiumID, clubLogo, selectedClubID, stadiumImgFileName);
            frm.ShowDialog();

            LoadDataOfStaidumToControlsInTabStadium(selectedClubID);
        }



        #endregion

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
