using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GUI
{
    public partial class FormDialogCreateMatches : Form
    {
        ClubsBLL clubsBLL = new ClubsBLL();

        RoundsBLL roundsBLL = new RoundsBLL();

        MatchesBLL matchesBLL = new MatchesBLL();

        SeasonsBLL seasonsBLL = new SeasonsBLL();

        private string shortcutLogoPath = "Images\\Logos\\";

        // Tạo 2 biến để gán seasonID và seasonName từ form cha vào 
        int seasonID;
        string seasonName;

        public FormDialogCreateMatches(int seasonID, string seasonName)
        {
            InitializeComponent();
            this.seasonID = seasonID;
            this.seasonName = seasonName;
        }

        private void FormDialogCreateMatches_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            lblCreateMatches.Text += " " + seasonName;

            dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);

            BindRoundCombobox(seasonID);

            LoadDataOfClubs();
        }

        private void AssignStartEndDateOfSeasonToDateTimePicker(DAL.Match lastMatchInPreviousMatchweek)
        {
            Season season = seasonsBLL.LoadDataByID(seasonID);
            if (lastMatchInPreviousMatchweek == null)
            {
                dtpStartDate.Value = (DateTime)season.StartDate;
                dtpEndDate.Value = (DateTime)season.StartDate;
            }
            else
            {
                dtpStartDate.MinDate = (DateTime)season.StartDate;
                dtpStartDate.MaxDate = (DateTime)season.EndDate;
                dtpStartDate.Value = (DateTime)lastMatchInPreviousMatchweek.MatchTime;

                dtpEndDate.MinDate = (DateTime)season.StartDate;
                dtpEndDate.MaxDate = (DateTime)season.EndDate;
                dtpEndDate.Value = (DateTime)lastMatchInPreviousMatchweek.MatchTime;
            }
            
        }

        private void LoadDataOfClubs()
        {
            List<Club> clubs = clubsBLL.LoadData();

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
            cboRound.DataSource = rounds;
            cboRound.ValueMember = "RoundID";
            cboRound.DisplayMember = "RoundName";
        }

        private void btnOpenDialogCreateSeason_Click(object sender, EventArgs e)
        {
            string selectedRoundID = cboRound.SelectedValue.ToString();

            DAL.Match lastMatchInPreviousMatchweek = GetLastMatchInPreviousMatchweek();

            // Lấy ra vòng đấu hiện tại
            string roundName = cboRound.Text;

            if (roundName != "Matchweek 1")
            {
                Round previousRound = roundsBLL.GetPreviousRound(seasonID, roundName);

                string beforeRoundName = previousRound.RoundName;

                string currentRoundName = cboRound.Text;

                if (lastMatchInPreviousMatchweek == null)
                {
                    MessageBox.Show($"\"{beforeRoundName}\" does not have any matches yet.");
                    return;
                }

                if (lastMatchInPreviousMatchweek.MatchTime >= dtpStartDate.Value)
                {
                    MessageBox.Show($"The start date of \"{currentRoundName}\" must be later than the last match of \"{beforeRoundName}\"");
                    return;
                }
            }

            if (dtpStartDate.Value >= dtpEndDate.Value)
            {
                MessageBox.Show($"The end time must be later the start time");
                return;
            }

            if (!string.IsNullOrEmpty(selectedRoundID))
            {
                bool isCreatedMatches = matchesBLL.AddData(selectedRoundID, seasonID, dtpStartDate.Value, dtpEndDate.Value);
                if (isCreatedMatches)
                    MessageBox.Show("Created matches successfully!");

                else
                    MessageBox.Show("Create matches failed!");
            }
        }

        /// <summary>
        /// Lấy ra trận đấu cuối cùng của vòng đấu trước
        /// </summary>
        /// <returns></returns>
        private DAL.Match GetLastMatchInPreviousMatchweek()
        {
            if (cboRound.SelectedIndex != -1)
            {
                Round selectedRound = (Round)cboRound.SelectedItem;

                string currentRoundName = selectedRound.RoundName;

                DAL.Match previousMatch = roundsBLL.GetLastMatchInPreviousMatchweekByRoundNameAndSeasonID(seasonID, currentRoundName);

                return previousMatch;
            }
            return null;
        }

        private void cboRound_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.Match lastMatchInPreviousMatchweek = GetLastMatchInPreviousMatchweek();
            AssignStartEndDateOfSeasonToDateTimePicker(lastMatchInPreviousMatchweek);
        }
    }
}
