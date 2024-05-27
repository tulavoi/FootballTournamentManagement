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
    public partial class FormDialogAddPlayerInMatch : Form
    {
        Bitmap clubLogo;
        string matchID;
        int clubID;
        int isHomeTeam;

        PlayersBLL playersBLL = new PlayersBLL();
        PlayersInMatchBLL playersInMatchBLL = new PlayersInMatchBLL();

        private string shortcutPlayerImgPath = "Images\\Players\\";
        private string defaultPlayerImgPath = "Images\\Players\\photo-missing.png";

        public FormDialogAddPlayerInMatch(string selectedMatchID, Bitmap homeClubLogo, int homeClubIDInSelectedMatch, int isHomeTeam)
        {
            InitializeComponent();
            clubLogo = homeClubLogo;
            matchID = selectedMatchID;
            clubID = homeClubIDInSelectedMatch;
            this.isHomeTeam = isHomeTeam;
        }

        private void FormDialogAddHomePlayerInMatch_Load(object sender, EventArgs e)
        {
            pictureBoxClubLogo.Image = clubLogo;

            LoadDataPlayerInClub();

            LoadPlayersInMatch();
        }

        private void LoadDataPlayerInClub()
        {
            List<Player> players = playersBLL.LoadDataByClubID(clubID);

            if (players != null)
            {
                int i = 1;
                foreach (var player in players)
                {
                    int rowIndex = dgvPlayers.Rows.Add();
                    if (rowIndex != -1 && rowIndex < dgvPlayers.Rows.Count)
                    {
                        dgvPlayers.Rows[rowIndex].Cells[0].Value = i++;
                        if (File.Exists(shortcutPlayerImgPath + player.Image))
                            dgvPlayers.Rows[rowIndex].Cells["Img"].Value = Image.FromFile(shortcutPlayerImgPath + player.Image);
                        else
                            dgvPlayers.Rows[rowIndex].Cells["Img"].Value = Image.FromFile(defaultPlayerImgPath);

                        dgvPlayers.Rows[rowIndex].Cells["PlayerID"].Tag = player.PlayerID;
                        dgvPlayers.Rows[rowIndex].Cells["PlayerName"].Value = player.PlayerName;
                        dgvPlayers.Rows[rowIndex].Cells["Position"].Value = player.Position;
                        dgvPlayers.Rows[rowIndex].Cells["Number"].Value = player.Number;
                    }
                }
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvPlayers.SelectedRows.Count == 0)
            {
                MessageBox.Show($"You have not selected a player!");
                return;
            }
            bool success = false;
            List<PlayersInMatch> players = new List<PlayersInMatch>();
            foreach (DataGridViewRow row in dgvPlayers.SelectedRows)
            {
                int playerID = Convert.ToInt32(row.Cells["PlayerID"].Tag);
                PlayersInMatch player = new PlayersInMatch();

                player.MatchID = matchID;
                player.PlayerID = playerID;
                player.IsHomeTeam = isHomeTeam;
                player.Position = row.Cells["Position"].Value.ToString();
                player.Goal = 0;
                player.YellowCard = 0;
                player.RedCard = 0;
                player.OwnGoal = 0;
                player.IsCaptain = 0;

                players.Add(player);
            }
            bool isAddedPlayersInMatch = playersInMatchBLL.AddData(players);

            if (isAddedPlayersInMatch)
                success = true;

            if (success)
            {
                MessageBox.Show($"Added players to match successfully!");
                LoadPlayersInMatch();
                dgvPlayers.ClearSelection();
            }
            else
                MessageBox.Show($"Added players to match failed, player has been added already!");
        }

        private void LoadPlayersInMatch()
        {
            List<PlayersInMatch> players = playersInMatchBLL.LoadPlayerInMatch(matchID, isHomeTeam);
            lblNumOfPlayersInMatch.Text = players.Count.ToString();
            if (players.Count > 0)
            {
                int i = 1;
                foreach (var player in players)
                {
                    int rowIndex = dgvPlayersInMatch.Rows.Add();
                    if (rowIndex != -1 && rowIndex < dgvPlayersInMatch.Rows.Count)
                    {
                        dgvPlayersInMatch.Rows[rowIndex].Cells["STTPIM"].Value = i++;

                        if (File.Exists(shortcutPlayerImgPath + player.Player.Image))
                            dgvPlayersInMatch.Rows[rowIndex].Cells["ImgPIM"].Value = Image.FromFile(shortcutPlayerImgPath + player.Player.Image);
                        else
                            dgvPlayersInMatch.Rows[rowIndex].Cells["ImgPIM"].Value = Image.FromFile(defaultPlayerImgPath);

                        dgvPlayersInMatch.Rows[rowIndex].Cells["PIMID"].Tag = player.PlayerID;
                        dgvPlayersInMatch.Rows[rowIndex].Cells["PIMName"].Value = player.Player.PlayerName;
                        dgvPlayersInMatch.Rows[rowIndex].Cells["PIMPosition"].Value = player.Position;
                        dgvPlayersInMatch.Rows[rowIndex].Cells["PIMNumber"].Value = player.Player.Number;
                    }
                }
            }
        }
    }
}
