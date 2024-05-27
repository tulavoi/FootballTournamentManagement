using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogUpdateMatchDetail : Form
    {
        RefereesBLL refereesBLL = new RefereesBLL();
        PlayersInMatchBLL playersInMatchBLL = new PlayersInMatchBLL();
        MatchDetailBLL matchDetailBLL = new MatchDetailBLL();

        Bitmap homeClubLogo;
        Bitmap awayClubLogo;
        string matchID;
        DateTime matchTime;

        private string shortcutPlayerImgPath = "Images\\Players\\";
        private string defaultPlayerImgPath = "Images\\Players\\photo-missing.png";


        public FormDialogUpdateMatchDetail(Bitmap homeClubLogo, Bitmap awayClubLogo, string selectedMatchID, DateTime? matchTime)
        {
            InitializeComponent();
            this.homeClubLogo = homeClubLogo;
            this.awayClubLogo = awayClubLogo;
            matchID = selectedMatchID;
            this.matchTime = (DateTime)matchTime;
        }

        private void FormDialogUpdateMatchDetail_Load(object sender, EventArgs e)
        {
            pictureBoxHomeClubLogo.Image = homeClubLogo;
            pictureBoxAwayClubLogo.Image = awayClubLogo;

            BindRefereeToCombobox();
            LoadDataOfPlayersInMatch();

            LoadDataOfMatchDetailToControls();
        }

        private void LoadDataOfMatchDetailToControls()
        {
            MatchDetail matchDetail = matchDetailBLL.GetMatchDetail(matchID);

            dtpMatchTime.Value = matchTime;

            if (matchDetail != null)
            {
                SetFormationAndTactic(matchDetail.HomeTactical, cboHomeFormations, cboHomeTactics);
                SetFormationAndTactic(matchDetail.AwayTactical, cboAwayFormations, cboAwayTactics);

                txtHomeGoals.Text = matchDetail.HomeGoals.ToString();
                txtAwayGoals.Text = matchDetail.AwayGoals.ToString();

                SelectRowByTag(dgvPLayersInMatch, matchDetail.MotmID);

                cboReferees.Text = matchDetail.Referee.RefereeName;
            }
        }

        // Chọn ra dòng có tag player id = motmID
        private void SelectRowByTag(DataGridView dgv, int motmID)
        {
            dgv.ClearSelection();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["PlayerID"].Tag != null && (int)row.Cells["PlayerID"].Tag == motmID)
                {
                    row.Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void SetFormationAndTactic(string tacticalFormation, ComboBox cboFormations, ComboBox cboTactics)
        {
            // Lấy chỉ số của đội hình dựa trên chuỗi chiến thuật
            int formationIndex = GetFormationIndex(tacticalFormation);

            // Kiểm tra nếu tìm thấy chỉ số của đội hình
            if (formationIndex != -1)
            {
                // Thiết lập chỉ số đã chọn cho ComboBox đội hình
                cboFormations.SelectedIndex = formationIndex;

                // Thiết lập các chiến thuật tương ứng với đội hình đã chọn
                SetTactics(cboFormations, cboTactics);

                // Thiết lập văn bản của ComboBox chiến thuật bằng chiến thuật đầu vào
                cboTactics.Text = tacticalFormation;
            }
        }


        // Lấy ra tatic rồi kiểm tra xem nó là đội hình bao nhiêu defender trong cboFormations
        private int GetFormationIndex(string tacticalFormation)
        {
            if (tacticalFormation.StartsWith("3-"))
            {
                return 0; // 3 defenders
            }
            if (tacticalFormation.StartsWith("4-"))
            {
                return 1; // 4 defenders
            }
            if (tacticalFormation.StartsWith("5-"))
            {
                return 2; // 5 defenders
            }
            return -1; // Default or unknown formation
        }

        // Lấy ra danh sách players in match sau đó gán vào datagridview
        private void LoadDataOfPlayersInMatch()
        {
            List<PlayersInMatch> players = playersInMatchBLL.LoadAllPlayerInMatch(matchID);

            int i = 1;
            foreach (PlayersInMatch player in players)
            {
                int rowIndex = dgvPLayersInMatch.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgvPLayersInMatch.Rows.Count)
                {
                    dgvPLayersInMatch.Rows[rowIndex].Cells["STT"].Value = i++;

                    if (File.Exists(shortcutPlayerImgPath + player.Player.Image))
                        dgvPLayersInMatch.Rows[rowIndex].Cells["Img"].Value = Image.FromFile(shortcutPlayerImgPath + player.Player.Image);
                    else
                        dgvPLayersInMatch.Rows[rowIndex].Cells["Img"].Value = Image.FromFile(defaultPlayerImgPath);

                    dgvPLayersInMatch.Rows[rowIndex].Cells["PlayerID"].Tag = player.Player.PlayerID;
                    dgvPLayersInMatch.Rows[rowIndex].Cells["PlayerName"].Value = player.Player.PlayerName;
                    dgvPLayersInMatch.Rows[rowIndex].Cells["Number"].Value = player.Player.Number;
                    dgvPLayersInMatch.Rows[rowIndex].Cells["Position"].Value = player.Player.Position;
                }
            }
        }

        private void BindRefereeToCombobox()
        {
            List<Referee> referees = refereesBLL.LoadData();
            cboReferees.DataSource = referees;
            cboReferees.DisplayMember = "RefereeName";
            cboReferees.ValueMember = "RefereeID";
        }

        private void SetTactics(ComboBox cboFormations, ComboBox cboTactics)
        {
            cboTactics.Items.Clear();

            switch (cboFormations.SelectedIndex)
            {
                case 0:
                    cboTactics.Items.AddRange(new object[]
                    {
                        "3-1-2-1-3",
                        "3-2-3-2",
                        "3-1-4-2",
                        "3-4-1-2",
                        "3-4-3"
                    });
                    break;
                case 1:
                    cboTactics.Items.AddRange(new object[]
                    {
                        "4-1-2-1-2",
                        "4-4-2",
                        "4-3-3",
                        "4-2-3-1",
                        "4-1-4-1",
                        "4-5-1",
                        "4-4-1-1",
                        "4-2-2-2",
                        "4-3-2-1",
                        "4-1-3-2",
                        "4-1-2-3",
                        "4-2-1-3",
                        "4-2-4"
                    });
                    break;
                case 2:
                    cboTactics.Items.AddRange(new object[]
                    {
                        "5-1-2-1-1",
                        "5-2-1-2",
                        "5-2-3",
                        "5-3-2",
                        "5-4-1"
                    });
                    break;
                default:
                    break;
            }
        }

        private void cboFormation_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTactics(cboHomeFormations, cboHomeTactics);
        }

        private void cboAwayFormations_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTactics(cboAwayFormations, cboAwayTactics);
        }

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Tạo matchDetail kiểm tra có tồn tại trong db hay không, nếu tồn tại thì sửa match detail, nếu chưa tồn tại thì thêm mới
            MatchDetail matchDetail = matchDetailBLL.GetMatchDetail(matchID);

            Match match = new Match
            {
                MatchID = matchID,
                MatchTime = dtpMatchTime.Value
            };

            if (matchDetail != null)
            {
                MatchDetail matchDetailToEdit = GetMatchDetail();

                bool isEditMatchDetailSuccess = matchDetailBLL.EditData(matchDetailToEdit, match);

                if (isEditMatchDetailSuccess)
                {
                    MessageBox.Show("Updated match detail successfully!");
                    Close();
                }
                else
                    MessageBox.Show("Update match detail failed!");
            }

            else
            {
                MatchDetail matchDetailToAdd = GetMatchDetail();

                bool isEditMatchDetailSuccess = matchDetailBLL.EditData(matchDetailToAdd, match);

                if (isEditMatchDetailSuccess)
                {
                    MessageBox.Show("Updated match detail successfully!");
                    Close();
                }
                else
                    MessageBox.Show("Update match detail failed!");
            }

            //bool isEditMatchDetailSuccess;
            //if (matchDetailBLL.GetMatchDetail(matchID) != null) {
            //    isEditMatchDetailSuccess = matchDetailBLL.EditData(matchDetail, match);
            //}
            //else
            //{
            //    isEditMatchDetailSuccess = matchDetailBLL.EditData(matchDetail, match);
            //}
            //if (isEditMatchDetailSuccess)
            //{
            //    MessageBox.Show("Updated match detail successfully!");
            //    Close();
            //}
            //else
            //    MessageBox.Show("Update match detail failed!");
        }

        private MatchDetail GetMatchDetail()
        {
            MatchDetail matchDetail = new MatchDetail();
            int motmID = GetMotmID();
            matchDetail.MotmID = motmID;
            matchDetail.HomeGoals = Convert.ToInt32(txtHomeGoals.Text);
            matchDetail.AwayGoals = Convert.ToInt32(txtAwayGoals.Text);

            matchDetail.HomeTactical = cboHomeTactics.Text;
            matchDetail.AwayTactical = cboAwayTactics.Text;

            matchDetail.RefereeID = Convert.ToInt32(cboReferees.SelectedValue);

            matchDetail.Match = new Match { MatchID = matchID };
            
            return matchDetail;
        }

        private int GetMotmID()
        {
            if (dgvPLayersInMatch.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn đầu tiên
                DataGridViewRow selectedRow = dgvPLayersInMatch.SelectedRows[0];
                int selectedMotmID = Convert.ToInt32(selectedRow.Cells["PlayerID"].Tag);
                return selectedMotmID;
            }
            return 0;
        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }
    }
}
