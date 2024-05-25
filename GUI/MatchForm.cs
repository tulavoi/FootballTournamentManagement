using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class MatchForm : Form
    {
        SeasonsBLL seasonBLL = new SeasonsBLL();

        SeasonClubsBLL ssClubsBLL = new SeasonClubsBLL();

        RoundsBLL roundsBLL = new RoundsBLL();

        ClubsBLL clubsBLL = new ClubsBLL();

        MatchesBLL matchesBLL = new MatchesBLL();

        MatchDetailBLL matchDetailBLL = new MatchDetailBLL();

        PlayersInMatchBLL playersInMatchBLL = new PlayersInMatchBLL();

        private string shortcutLogoPath = "Images\\Logos\\";

        private string shortcutPlayerImgPath = "Images\\Players\\";

        int seasonID;

        string selectedMatchID;

        int homeClubIDInSelectedMatch;
        int awayClubIDInSelectedMatch;

        public MatchForm()
        {
            InitializeComponent();
        }

        private void MatchForm_Load(object sender, EventArgs e)
        {
            // Gán dữ liệu của table Seasons vào cboSeason
            BindSeasonCombobox();

            // Chỉnh lại font size header text của datagridviews
            dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);
            dgvMatches.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);

            // Căn chỉnh header text của dgvMatches ở chính giữa
            CustomDgvMatches();
        }

        private void CustomDgvMatches()
        {
            dgvMatches.Columns["HomeClubName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatches.Columns["HomeClubName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvMatches.Columns["AwayClubName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMatches.Columns["MatchID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatches.Columns["MatchID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// Gán tất cả dữ liệu của table Seasons vào cboSeason.
        /// </summary>
        private void BindSeasonCombobox()
        {
            try
            {
                List<Season> seasons = seasonBLL.LoadData();
                cboSeason.DataSource = seasons;
                cboSeason.ValueMember = "SeasonID";
                cboSeason.DisplayMember = "SeasonName";
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show($"Invalid cast exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void cboSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvClubs.Rows.Clear();
            if (cboSeason.SelectedIndex != -1)
            {
                Season selectedSeason = (Season)cboSeason.SelectedItem;

                // Lấy SeasonID từ đối tượng Season được chọn
                seasonID = selectedSeason.SeasonID;

                // Gọi hàm để hiển thị danh sách các Round dựa vào SeasonID
                BindRoundCombobox(seasonID);

                LoadClubToDgvClubsBySeasonID();
            }

            // Kiểm tra nếu như có đủ 20 club trong 1 season thì sẽ hiển thị nút tạo trận đấu,
            // Nếu không đủ 20 club sẽ hiển thị nút thêm đội bóng
            if (dgvClubs.Rows.Count < 20)
                btnOpenFormCreateMatches.Text = "Add Club";
            else
                btnOpenFormCreateMatches.Text = "Create Matches";
        }

        private void cboRound_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMatches.Rows.Clear();
            if (cboRounds.SelectedIndex != -1)
            {
                Round selectedRound = (Round)cboRounds.SelectedItem;

                // Lấy RoundID từ đối tượng Season được chọn
                string roundID = selectedRound.RoundID;

                LoadMatchesToDgvMatches(roundID);
            }
        }

        private void LoadMatchesToDgvMatches(string roundID)
        {
            List<Match> matches = matchesBLL.GetDataByRoundID(roundID, seasonID);
            int i = 1;
            foreach (Match match in matches)
            {
                int rowIndex = dgvMatches.Rows.Add();

                // Kiểm tra nếu có đủ hàng trong DataGridView
                if (rowIndex != -1 && rowIndex < dgvMatches.Rows.Count)
                {
                    dgvMatches.Rows[rowIndex].Cells["Number"].Value = i++;
                    dgvMatches.Rows[rowIndex].Cells["HomeID"].Tag = match.Club.ClubID; // = match.HomeID
                    dgvMatches.Rows[rowIndex].Cells["HomeClubLogo"].Value = Image.FromFile(shortcutLogoPath + match.Club.Logo);
                    dgvMatches.Rows[rowIndex].Cells["HomeClubName"].Value = match.Club.ClubName;

                    dgvMatches.Rows[rowIndex].Cells["MatchID"].Value = "-";
                    dgvMatches.Rows[rowIndex].Cells["MatchID"].Tag = match.MatchID;

                    DateTime matchDate = (DateTime)match.MatchTime;

                    dgvMatches.Rows[rowIndex].Cells["MatchTime"].Value = matchDate.ToString("dd/MM/yyyy");

                    dgvMatches.Rows[rowIndex].Cells["AwayClubName"].Value = match.Club1.ClubName;
                    dgvMatches.Rows[rowIndex].Cells["AwayClubLogo"].Value = Image.FromFile(shortcutLogoPath + match.Club1.Logo);
                    dgvMatches.Rows[rowIndex].Cells["AwayID"].Tag = match.Club1.ClubID; // = match.AwayID
                    
                }
            }
        }

        /// <summary>
        /// Load dữ liệu vào dgvClubs dựa theo season id được chọn từ cboSeason.
        /// </summary>
        private void LoadClubToDgvClubsBySeasonID()
        {
            List<Club> clubs = ssClubsBLL.LoadDataBySeasonID(seasonID);
            lblNumOfClubs.Text = clubs.Count.ToString();
            LoadDataOfClubsToDataGridView(clubs);
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
        /// Gán tất cả dữ liệu của table Rounds vào cboRound dựa vào seasonID.
        /// </summary>
        private void BindRoundCombobox(int seasonID)
        {
            List<Round> rounds = roundsBLL.LoadDataBySeasonID(seasonID);

            // Thêm lựa chọn All vào đầu danh sách cboSeason 
            cboRounds.DataSource = rounds;
            cboRounds.ValueMember = "RoundID";
            cboRounds.DisplayMember = "RoundName";
        }

        private void btnOpenFormCreateMatches_Click(object sender, EventArgs e)
        {
            if (dgvClubs.Rows.Count == 20)
            {
                string seasonName = cboSeason.Text;
                FormDialogCreateMatches frm = new FormDialogCreateMatches(seasonID, seasonName);
                frm.ShowDialog();

                // Gọi lại hàm này để gọi LoadMatchesToDgvMatches()
                // Cũng có thể dùng hàm LoadMatchesToDgvMatches() thay vì cboRound_SelectedIndexChanged()
                cboRound_SelectedIndexChanged(sender, e);
            }
            if (dgvClubs.Rows.Count < 20)
            {
                string seasonName = cboSeason.Text;
                FormDialogAddClubToSeason frm = new FormDialogAddClubToSeason(seasonID, seasonName);
                frm.ShowDialog();

                LoadClubToDgvClubsBySeasonID();
            }
        }

        private void dgvClubs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, deleteColumn: dgvClubs.Columns[4] as DataGridViewButtonColumn);
        }

        /// <summary>
        /// Xử lý sự kiện CellFormatting của DataGridView để định dạng nền cho các ô chứa nút chỉnh sửa và xóa.
        /// </summary>
        /// /// <param name="sender">Đối tượng gửi sự kiện.</param>
        /// <param name="e">Đối số của sự kiện CellFormatting.</param>
        /// <param name="editColumn">Cột chứa nút chỉnh sửa.</param>
        /// <param name="deleteColumn">Cột chứa nút xóa.</param>
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e, DataGridViewButtonColumn deleteColumn)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv != null && (e.ColumnIndex == deleteColumn.Index))
            {
                object cellValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (cellValue != null && cellValue.ToString() == "Delete")
                {
                    DataGridViewButtonCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Style.BackColor = Color.FromArgb(180, 40, 130);
                }
            }
        }

        private void dgvClubs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            // Button column delete
            if (e.ColumnIndex == 4)
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvClubs.Rows.Count)
                    return;

                string clubName = dgvClubs.Rows[e.RowIndex].Cells[3].Value.ToString();
                string seasonName = cboSeason.Text;

                DialogResult rs = MessageBox.Show($"Are you sure to delete \"{clubName}\" from season {seasonName}?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int clubID = Convert.ToInt32(dgvClubs.Rows[e.RowIndex].Cells[0].Tag);

                    bool isDeleteClubSuccess = ssClubsBLL.DeleteDataByClubID(clubID, seasonID);
                    if (isDeleteClubSuccess)
                    {
                        MessageBox.Show("Deleted club successfully!");
                        cboSeason_SelectedIndexChanged(sender, e);
                    }
                    else
                        MessageBox.Show("Delete club failed, this club is in a season!");
                }
            }
        }

        private void btnOpenDialogCreateSeason_Click(object sender, EventArgs e)
        {
            FormDialogCreateSeason frm = new FormDialogCreateSeason();
            frm.ShowDialog();

            BindSeasonCombobox();
        }

        private void AssignClubNameToLabels(DataGridViewRow selectedRow)
        {
            // Tab match detail
            lblHomeClubName.Text = selectedRow.Cells["HomeClubName"].Value.ToString();
            lblAwayClubName.Text = selectedRow.Cells["AwayClubName"].Value.ToString();

            // Tab home players
            lblClubNameInHomePLayersTab.Text = selectedRow.Cells["HomeClubName"].Value.ToString();
            lblClubNameInAwayPLayersTab.Text = selectedRow.Cells["AwayClubLogo"].Value.ToString();
        }

        private void AssignClubLogoToPictureBoxes(DataGridViewRow selectedRow)
        {
            // Tab match detail
            pictureBoxHomeClubLogo.Image = (Bitmap)selectedRow.Cells["HomeClubLogo"].Value;
            pictureBoxAwayClubLogo.Image = (Bitmap)selectedRow.Cells["AwayClubLogo"].Value;

            // Tab home players
            pictureBoxClubLogoDetailInHomePlayerTab.Image = (Bitmap)selectedRow.Cells["HomeClubLogo"].Value;
            pictureBoxClubLogoDetailInAwayPlayerTab.Image = (Bitmap)selectedRow.Cells["AwayClubLogo"].Value;
        }

        private void tabControlMatchForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMatchID))
                tabControlMatchForm.SelectedIndex = 0;
        }

        private void dgvMatches_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMatches.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvMatches.Rows[e.RowIndex];

                selectedMatchID = selectedRow.Cells["MatchID"].Tag.ToString();
                homeClubIDInSelectedMatch = Convert.ToInt32(selectedRow.Cells["HomeID"].Tag);
                awayClubIDInSelectedMatch = Convert.ToInt32(selectedRow.Cells["AwayID"].Tag);

                AssignClubLogoToPictureBoxes(selectedRow);

                AssignClubNameToLabels(selectedRow);

                // Sau khi double click chọn 1 match trong datagridview thì chuyển sang tab control match detail
                tabControlMatchForm.SelectedIndex = 1;

                AssginDataOfMatchDetailToControlsInTabMatchDetail();

                AssignHomePlayersInMatchToDatagridview();
                AssignAwayPlayersInMatchToDatagridview();

            }
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

        #region Home players tab
        private void AssignHomePlayersInMatchToDatagridview()
        {
            dgvHomePlayers.Rows.Clear();

            List<PlayersInMatch> playersInMatch = playersInMatchBLL.LoadPlayerInMatch(selectedMatchID, 1);
            lblCountHomePlayer.Text = playersInMatch.Count.ToString();

            int i = 1;
            if (playersInMatch != null)
            {
                foreach (var player in playersInMatch)
                {
                    int rowIndex = dgvHomePlayers.Rows.Add();
                    if (rowIndex != -1 && rowIndex < dgvHomePlayers.Rows.Count)
                    {
                        dgvHomePlayers.Rows[rowIndex].Cells[0].Value = i++;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPImgFileName"].Tag = player.Player.Image;

                        dgvHomePlayers.Rows[rowIndex].Cells["HPImgFileName"].Tag = player.Player.Image;

                        if (File.Exists(shortcutPlayerImgPath + player.Player.Image))
                            dgvHomePlayers.Rows[rowIndex].Cells["HPImg"].Value = Image.FromFile(shortcutPlayerImgPath + player.Player.Image);
                        else
                            dgvHomePlayers.Rows[rowIndex].Cells["HPImg"].Value = Image.FromFile(shortcutPlayerImgPath + "photo-missing.png");

                        dgvHomePlayers.Rows[rowIndex].Cells["HPID"].Tag = player.PlayerID;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPName"].Value = player.Player.PlayerName;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPNumber"].Value = player.Player.Number;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPPosition"].Value = player.Position;

                        if (player.IsCaptain == 1)
                            dgvHomePlayers.Rows[rowIndex].Cells["Captain"].Value = "Captain";

                        dgvHomePlayers.Rows[rowIndex].Cells["HPPosition"].Value = player.Position;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPGoals"].Value = player.Goal;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPYellowCard"].Value = player.YellowCard;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPRedCard"].Value = player.RedCard;
                        dgvHomePlayers.Rows[rowIndex].Cells["HPOwnGoal"].Value = player.OwnGoal;
                    }
                }
            }
            PlayersInMatch playerInMatch = new PlayersInMatch();
        }


        private void AssginDataOfMatchDetailToControlsInTabMatchDetail()
        {
            MatchDetail matchDetail = matchDetailBLL.LoadDataByMatchID(selectedMatchID);

            lblHomeTactic.Text = matchDetail.HomeTactical;
            lblAwayTactic.Text = matchDetail.AwayTactical;

            lblHomeScore.Text = matchDetail.HomeGoals.ToString();
            lblAwayScore.Text = matchDetail.AwayGoals.ToString();

            DateTime matchDate = (DateTime)matchDetail.Match.MatchTime;

            string matchDay = matchDate.ToString("dd/MM/yyyy");
            string matchTime = matchDate.ToString("HH:mm:ss");

            lblMatchTime.Text = matchTime;
            lblMatchDay.Text = matchDay;

            lblMOTMNameAndID.Text = matchDetail.Player.PlayerName;
            if (File.Exists(shortcutPlayerImgPath + matchDetail.Player.Image))
                pictureBoxMOTM.Image = Image.FromFile(shortcutPlayerImgPath + matchDetail.Player.Image);
            else
                pictureBoxMOTM.Image = Image.FromFile(shortcutPlayerImgPath + "photo-missing.png");
        }


        private void dgvHomePlayers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, editColumn: dgvHomePlayers.Columns["btnEditHP"] as DataGridViewButtonColumn, deleteColumn: dgvHomePlayers.Columns["btnDeleteHP"] as DataGridViewButtonColumn);
        }

        private void btnOpenDialogAddHomePlayer_Click(object sender, EventArgs e)
        {
            Bitmap homeClubLogo = (Bitmap)pictureBoxClubLogoDetailInHomePlayerTab.Image;

            FormDialogAddPlayerInMatch frm = new FormDialogAddPlayerInMatch(selectedMatchID, homeClubLogo, homeClubIDInSelectedMatch);
            frm.ShowDialog();
        }
        #endregion


        #region Away players tab
        private void AssignAwayPlayersInMatchToDatagridview()
        {
            dgvAwayPlayers.Rows.Clear();

            List<PlayersInMatch> playersInMatch = playersInMatchBLL.LoadPlayerInMatch(selectedMatchID, 0);
            lblCountAwayPlayer.Text = playersInMatch.Count.ToString();

            int i = 1;
            if (playersInMatch != null)
            {
                foreach (var player in playersInMatch)
                {
                    int rowIndex = dgvAwayPlayers.Rows.Add();
                    if (rowIndex != -1 && rowIndex < dgvAwayPlayers.Rows.Count)
                    {
                        dgvAwayPlayers.Rows[rowIndex].Cells[0].Value = i++;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APImgFileName"].Tag = player.Player.Image;

                        dgvAwayPlayers.Rows[rowIndex].Cells["APImgFileName"].Tag = player.Player.Image;

                        if (File.Exists(shortcutPlayerImgPath + player.Player.Image))
                            dgvAwayPlayers.Rows[rowIndex].Cells["APImg"].Value = Image.FromFile(shortcutPlayerImgPath + player.Player.Image);
                        else
                            dgvAwayPlayers.Rows[rowIndex].Cells["APImg"].Value = Image.FromFile(shortcutPlayerImgPath + "photo-missing.png");

                        dgvAwayPlayers.Rows[rowIndex].Cells["APID"].Tag = player.PlayerID;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APName"].Value = player.Player.PlayerName;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APNumber"].Value = player.Player.Number;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APPosition"].Value = player.Position;

                        if (player.IsCaptain == 1)
                            dgvAwayPlayers.Rows[rowIndex].Cells["APCaptain"].Value = "Captain";

                        dgvAwayPlayers.Rows[rowIndex].Cells["APPosition"].Value = player.Position;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APGoals"].Value = player.Goal;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APYellowCard"].Value = player.YellowCard;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APRedCard"].Value = player.RedCard;
                        dgvAwayPlayers.Rows[rowIndex].Cells["APOwnGoal"].Value = player.OwnGoal;
                    }
                }
            }
            PlayersInMatch playerInMatch = new PlayersInMatch();
        }

        private void dgvAwayPlayers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, editColumn: dgvAwayPlayers.Columns["btnEditAP"] as DataGridViewButtonColumn, deleteColumn: dgvAwayPlayers.Columns["btnDeleteAP"] as DataGridViewButtonColumn);
        }


        private void btnOpenDialogAddAwayPlayer_Click(object sender, EventArgs e)
        {
            Bitmap awayClubLogo = (Bitmap)pictureBoxClubLogoDetailInAwayPlayerTab.Image;

            FormDialogAddPlayerInMatch frm = new FormDialogAddPlayerInMatch(selectedMatchID, awayClubLogo, awayClubIDInSelectedMatch);
            frm.ShowDialog();
        }
        #endregion


    }
}
