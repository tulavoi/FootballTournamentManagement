using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace GUI
{
    public partial class FormDialogCreateMatches : Form
    {
        ClubsBLL clubsBLL = new ClubsBLL();

        RoundsBLL roundsBLL = new RoundsBLL();

        MatchesBLL matchesBLL = new MatchesBLL();

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
            string roundID = cboRound.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(roundID))
            {
                bool isCreatedMatches = matchesBLL.AddData(roundID, seasonID);
                if (isCreatedMatches)
                    MessageBox.Show("Created matches successfully!");

                else
                    MessageBox.Show("Create matches failed!");
            }

        }
    }
}
