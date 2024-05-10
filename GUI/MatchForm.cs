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
    public partial class MatchForm : Form
    {
        SeasonBLL seasonBLL = new SeasonBLL();

        SeasonClubsBLL ssClubsBLL = new SeasonClubsBLL();

        RoundsBLL roundsBLL = new RoundsBLL();

        ClubsBLL clubsBLL = new ClubsBLL();

        public MatchForm()
        {
            InitializeComponent();
        }

        private void MatchForm_Load(object sender, EventArgs e)
        {
            // Gán dữ liệu của table Seasons vào cboSeason
            BindSeasonCombobox();            
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

            cboSeason.SelectedIndex = cboSeason.Items.Count - 1;
        }

        private void cboSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSeason.SelectedIndex != -1)
            {
                Season selectedSeason = (Season)cboSeason.SelectedItem;

                // Lấy SeasonID từ đối tượng Season được chọn
                int seasonID = selectedSeason.SeasonID;

                // Gọi hàm để hiển thị danh sách các Round dựa vào SeasonID
                BindRoundCombobox(seasonID);
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
    }
}
