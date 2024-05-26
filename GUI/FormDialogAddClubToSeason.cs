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

namespace GUI
{
    public partial class FormDialogAddClubToSeason : Form
    {
        ClubsBLL clubsBLL = new ClubsBLL();

        SeasonsBLL seasonBLL = new SeasonsBLL();

        SeasonClubsBLL ssClubsBLL = new SeasonClubsBLL();

        int seasonID;
        string seasonName;

        private string shortcutLogoPath = "Images\\Logos\\";

        public FormDialogAddClubToSeason(int seasonID, string seasonName)
        {
            InitializeComponent();
            this.seasonID = seasonID;
            this.seasonName = seasonName;
        }

        private void FormDialogAddClubToSeason_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            // Gán dữ liệu của table Seasons vào cboSeason
            BindSeasonCombobox();

            dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);

            LoadDataOfClubsToDatagridview();

            // Gán season name vào cboSeason sau đó lấy value member của display member (season name)
            cboSeason.Text = seasonName;
            seasonID = Convert.ToInt32(cboSeason.SelectedValue);
        }

        /// <summary>
        /// Gán tất cả dữ liệu của table Seasons vào cboSeason.
        /// </summary>
        private void BindSeasonCombobox()
        {
            List<Season> seasons = seasonBLL.LoadData();

            cboSeason.DataSource = seasons;
            cboSeason.ValueMember = "SeasonID";
            cboSeason.DisplayMember = "SeasonName";
        }

        private void LoadDataOfClubsToDatagridview()
        {
            List<Club> clubs = clubsBLL.LoadData();
            int i = 1;
            foreach (var club in clubs)
            {
                int rowIndex = dgvClubs.Rows.Add();

                // Kiểm tra nếu có đủ hàng trong DataGridView
                if (rowIndex != -1 && rowIndex < dgvClubs.Rows.Count)
                {
                    dgvClubs.Rows[rowIndex].Cells[0].Value = i++;
                    dgvClubs.Rows[rowIndex].Cells[1].Tag = club.ClubID;
                    dgvClubs.Rows[rowIndex].Cells[2].Value = Image.FromFile(shortcutLogoPath + club.Logo);
                    dgvClubs.Rows[rowIndex].Cells[3].Tag = club.Logo;
                    dgvClubs.Rows[rowIndex].Cells[4].Value = club.ClubName;
                }
            }
        }

        private void cboSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSeason.SelectedIndex != -1)
            {
                Season selectedSeason = (Season)cboSeason.SelectedItem;

                // Lấy SeasonID từ đối tượng Season được chọn
                seasonID = selectedSeason.SeasonID;

                LoadClubBySeasonID();
            }
        }

        /// <summary>
        /// Load dữ liệu vào dgvClubs dựa theo season id được chọn từ cboSeason.
        /// </summary>
        private void LoadClubBySeasonID()
        {
            List<Club> clubs = ssClubsBLL.LoadDataBySeasonID(seasonID);

            dgvClubInSeason.Rows.Clear();
            int i = 1;

            foreach (var club in clubs)
            {
                int rowIndex = dgvClubInSeason.Rows.Add();

                // Kiểm tra nếu có đủ hàng trong DataGridView
                if (rowIndex != -1 && rowIndex < dgvClubInSeason.Rows.Count)
                {
                    dgvClubInSeason.Rows[rowIndex].Cells[0].Value = i++;
                    dgvClubInSeason.Rows[rowIndex].Cells[1].Tag = club.ClubID;
                    dgvClubInSeason.Rows[rowIndex].Cells[2].Value = Image.FromFile(shortcutLogoPath + club.Logo);
                    dgvClubInSeason.Rows[rowIndex].Cells[3].Tag = club.Logo;
                    dgvClubInSeason.Rows[rowIndex].Cells[4].Value = club.ClubName;
                }
            }
            lblNumOfClubs.Text = dgvClubInSeason.Rows.Count.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            seasonID = Convert.ToInt32(cboSeason.SelectedValue);
            bool success = false;
            foreach (DataGridViewRow row in dgvClubs.SelectedRows)
            {
                int clubID = Convert.ToInt32(row.Cells[1].Tag);

                bool isAddedSeasonClub = ssClubsBLL.AddData(clubID, seasonID);

                if (isAddedSeasonClub)
                    success = true;
            }

            seasonName = cboSeason.Text;

            if (success)
            {
                MessageBox.Show($"Added clubs to season {seasonName} successfully!");
                LoadClubBySeasonID();
            }
            else
                MessageBox.Show($"Added clubs to season {seasonName} failed!");
        }
    }
}
