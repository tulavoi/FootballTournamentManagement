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

                    dgvMatches.Rows[rowIndex].Cells["HomeID"].Tag = match.SeasonClub.ClubID; // = match.HomeID

                    if (File.Exists(shortcutLogoPath + match.SeasonClub.Club.Logo))
                        dgvMatches.Rows[rowIndex].Cells["HomeClubLogo"].Value = Image.FromFile(shortcutLogoPath + match.SeasonClub.Club.Logo);

                    dgvMatches.Rows[rowIndex].Cells["HomeClubName"].Value = match.SeasonClub.Club.ClubName;

                    dgvMatches.Rows[rowIndex].Cells["MatchID"].Value = "-";

                    dgvMatches.Rows[rowIndex].Cells["MatchID"].Tag = match.MatchID;

                    DateTime matchDate = (DateTime)match.MatchTime;

                    dgvMatches.Rows[rowIndex].Cells["MatchTime"].Value = matchDate.ToString("dd/MM/yyyy");

                    dgvMatches.Rows[rowIndex].Cells["AwayClubName"].Value = match.SeasonClub1.Club.ClubName;

                    if (File.Exists(shortcutLogoPath + match.SeasonClub1.Club.Logo))
                        dgvMatches.Rows[rowIndex].Cells["AwayClubLogo"].Value = Image.FromFile(shortcutLogoPath + match.SeasonClub1.Club.Logo);

                    dgvMatches.Rows[rowIndex].Cells["AwayID"].Tag = match.SeasonClub1.ClubID; // = match.AwayID

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

                if (dgvClubs.Rows.Count == 20)
                    btnOpenFormCreateMatches.Text = "Create Matches";

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
            lblClubNameInAwayPLayersTab.Text = selectedRow.Cells["AwayClubName"].Value.ToString();
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

                // Sau khi double click chọn 1 match trong datagridview thì chuyển sang tab control match detail
                tabControlMatchForm.SelectedIndex = 1;

                SetControlsToDefault();

                // Gán club logo và club name vào controls
                AssignClubLogoToPictureBoxes(selectedRow);

                AssignClubNameToLabels(selectedRow);

                // Load lại dữ liệu trong match detail tab
                RefreshMatchDetailTab();
            }
        }

        private void SetControlsToDefault()
        {
            lblHomeScore.Text = "0";
            lblAwayScore.Text = "0";
            lblMatchTime.Text = "N/A";
            lblMatchDay.Text = "N/A";
            lblMOTMNameAndID.Text = "N/A";
            lblHomeTactic.Text = "N/A";
            lblAwayTactic.Text = "N/A";
            lblRefereeName.Text = "Referee: N/A";
            pictureBoxMOTM.Image = null;
        }

        private void RefreshMatchDetailTab()
        {
            AssginDataOfMatchDetailToControlsInTabMatchDetail();

            AssignHomePlayersInMatchToDatagridview();
            AssignAwayPlayersInMatchToDatagridview();
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

        private void btnUpdateMatchDetail_Click(object sender, EventArgs e)
        {
            Bitmap homeClubLogo = (Bitmap)pictureBoxHomeClubLogo.Image;
            Bitmap awayClubLogo = (Bitmap)pictureBoxAwayClubLogo.Image;

            Match match = matchesBLL.GetDataByID(selectedMatchID);

            FormDialogUpdateMatchDetail frm = new FormDialogUpdateMatchDetail(homeClubLogo, awayClubLogo, selectedMatchID, match.MatchTime);
            frm.ShowDialog();

            // Load lại dữ liệu trong match detail tab
            RefreshMatchDetailTab();
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

                        dgvHomePlayers.Rows[rowIndex].Cells["HPID"].Tag = player.Player.PlayerID;
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

            if (matchDetail != null)
            {
                lblHomeTactic.Text = matchDetail.HomeTactical;
                lblAwayTactic.Text = matchDetail.AwayTactical;

                lblHomeScore.Text = matchDetail.HomeGoals.ToString();
                lblAwayScore.Text = matchDetail.AwayGoals.ToString();

                DateTime matchDate = (DateTime)matchDetail.Match.MatchTime;

                string matchDay = matchDate.ToString("dd/MM/yyyy");
                string matchTime = matchDate.ToString("HH:mm:ss");

                lblMatchTime.Text = matchTime;
                lblMatchDay.Text = matchDay;

                lblMOTMNameAndID.Text = matchDetail.PlayersInMatch.Player.PlayerName;

                lblRefereeName.Text = "";
                lblRefereeName.Text = "Referee: " + matchDetail.Referee.RefereeName;

                if (File.Exists(shortcutPlayerImgPath + matchDetail.PlayersInMatch.Player.Image))
                    pictureBoxMOTM.Image = Image.FromFile(shortcutPlayerImgPath + matchDetail.PlayersInMatch.Player.Image);
                else
                    pictureBoxMOTM.Image = Image.FromFile(shortcutPlayerImgPath + "photo-missing.png");
            }
        }


        private void dgvHomePlayers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, editColumn: dgvHomePlayers.Columns["btnEditHP"] as DataGridViewButtonColumn, deleteColumn: dgvHomePlayers.Columns["btnDeleteHP"] as DataGridViewButtonColumn);
        }

        private void btnOpenDialogAddHomePlayer_Click(object sender, EventArgs e)
        {
            Bitmap homeClubLogo = (Bitmap)pictureBoxClubLogoDetailInHomePlayerTab.Image;
            int isHomeTeam = 1;
            FormDialogAddPlayerInMatch frm = new FormDialogAddPlayerInMatch(selectedMatchID, homeClubLogo, homeClubIDInSelectedMatch, isHomeTeam);
            frm.ShowDialog();
            RefreshMatchDetailTab();
        }

        private void dgvHomePlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 12 && e.ColumnIndex != 13)
                return;

            // Button edit
            if (e.ColumnIndex == 12)
            {
                HandleEditHomePlayerInMatchButtonClick(e.RowIndex);
            }

            // Button delete
            if (e.ColumnIndex == 13)
            {
                HandleDeleteHomePlayerInMatchButtonClick(e.RowIndex);
            }
        }

        private void HandleDeleteHomePlayerInMatchButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            string playerName = dgvHomePlayers.Rows[rowIndex].Cells["HPName"].Value.ToString();

            DialogResult rs = MessageBox.Show($"Are you sure to delete \"{playerName}\" from match?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                int playerID = Convert.ToInt32(dgvHomePlayers.Rows[rowIndex].Cells["HPID"].Tag);
                bool isDeletePlayerSuccess = playersInMatchBLL.DeleteData(playerID, selectedMatchID);
                if (isDeletePlayerSuccess)
                {
                    MessageBox.Show($"Deleted player \"{playerName}\" successfully!");
                    RefreshMatchDetailTab();
                }
                else
                    MessageBox.Show($"Delete player \"{playerName}\" failed!");
            }
        }

        private void HandleEditHomePlayerInMatchButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            Bitmap clubLogo = (Bitmap)pictureBoxHomeClubLogo.Image;
            int playerID = Convert.ToInt32(dgvHomePlayers.Rows[rowIndex].Cells["HPID"].Tag);
            Bitmap playerImg = (Bitmap)dgvHomePlayers.Rows[rowIndex].Cells["HPImg"].Value;
            int isHomeTeam = 1;

            FormDialogEditPlayerInMatch frm = new FormDialogEditPlayerInMatch(clubLogo, selectedMatchID, playerID, playerImg, selectedMatchID, isHomeTeam);
            frm.ShowDialog();
            RefreshMatchDetailTab();
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
            int isHomeTeam = 0;
            FormDialogAddPlayerInMatch frm = new FormDialogAddPlayerInMatch(selectedMatchID, awayClubLogo, awayClubIDInSelectedMatch, isHomeTeam);
            frm.ShowDialog();
            RefreshMatchDetailTab();
        }

        private void dgvAwayPlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 12 && e.ColumnIndex != 13)
                return;

            // Button edit
            if (e.ColumnIndex == 12)
            {
                HandleEditAwayPlayerInMatchButtonClick(e.RowIndex);
            }

            // Button delete
            if (e.ColumnIndex == 13)
            {
                HandleDeleteAwayPlayerInMatchButtonClick(e.RowIndex);
            }
        }

        private void HandleDeleteAwayPlayerInMatchButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            string playerName = dgvAwayPlayers.Rows[rowIndex].Cells["APName"].Value.ToString();

            DialogResult rs = MessageBox.Show($"Are you sure to delete \"{playerName}\" from match?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                int playerID = Convert.ToInt32(dgvAwayPlayers.Rows[rowIndex].Cells["APID"].Tag);
                bool isDeletePlayerSuccess = playersInMatchBLL.DeleteData(playerID, selectedMatchID);
                if (isDeletePlayerSuccess)
                {
                    MessageBox.Show($"Deleted player \"{playerName}\" successfully!");
                    RefreshMatchDetailTab();
                }
                else
                    MessageBox.Show($"Delete player \"{playerName}\" failed!");
            }
        }

        private void HandleEditAwayPlayerInMatchButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            Bitmap clubLogo = (Bitmap)pictureBoxAwayClubLogo.Image;
            int playerID = Convert.ToInt32(dgvAwayPlayers.Rows[rowIndex].Cells["APID"].Tag);
            Bitmap playerImg = (Bitmap)dgvAwayPlayers.Rows[rowIndex].Cells["APImg"].Value;
            int isHomeTeam = 0;

            FormDialogEditPlayerInMatch frm = new FormDialogEditPlayerInMatch(clubLogo, selectedMatchID, playerID, playerImg, selectedMatchID, isHomeTeam);
            frm.ShowDialog();
            RefreshMatchDetailTab();
        }



        #endregion

        private void btnOpenReportMatch_Click(object sender, EventArgs e)
        {
            string roundID = cboRounds.SelectedValue.ToString();
            FormReportMatch frm = new FormReportMatch(seasonID, roundID);
            frm.ShowDialog();
        }

        private void btnOpenReportHomePlayer_Click(object sender, EventArgs e)
        {
            int isHome = 1;
            FormReportPlayersInMatch frm = new FormReportPlayersInMatch(selectedMatchID, isHome);
            frm.ShowDialog();
        }

        private void btnOpenReportAwayPlayer_Click(object sender, EventArgs e)
        {
            int isHome = 0;
            FormReportPlayersInMatch frm = new FormReportPlayersInMatch(selectedMatchID, isHome);
            frm.ShowDialog();
        }
    }
}
