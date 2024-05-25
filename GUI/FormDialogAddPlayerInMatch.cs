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

        PlayersBLL playersBLL = new PlayersBLL();

        private string shortcutPlayerImgPath = "Images\\Players\\";
        private string defaultPlayerImgPath = "Images\\Players\\photo-missing.png";

        public FormDialogAddPlayerInMatch(string selectedMatchID, Bitmap homeClubLogo, int homeClubIDInSelectedMatch)
        {
            InitializeComponent();
            clubLogo = homeClubLogo;
            matchID = selectedMatchID;
            clubID = homeClubIDInSelectedMatch;
        }

        private void FormDialogAddHomePlayerInMatch_Load(object sender, EventArgs e)
        {
            pictureBoxClubLogo.Image = clubLogo;

            LoadDataPlayer();
        }

        private void LoadDataPlayer()
        {
            List<Player> players = playersBLL.LoadDataByClubID(clubID);

            if (players != null)
            {
                int i = 0;
                foreach (var player in players)
                {
                    int rowIndex = dgvHomePlayers.Rows.Add();
                    if (rowIndex != -1 && rowIndex < dgvHomePlayers.Rows.Count)
                    {
                        dgvHomePlayers.Rows[rowIndex].Cells[0].Value = i++;
                        if (File.Exists(shortcutPlayerImgPath + player.Image))
                            dgvHomePlayers.Rows[rowIndex].Cells["Img"].Value = Image.FromFile(shortcutPlayerImgPath + player.Image);
                        else
                            dgvHomePlayers.Rows[rowIndex].Cells["Img"].Value = Image.FromFile(defaultPlayerImgPath);

                        dgvHomePlayers.Rows[rowIndex].Cells["PlayerID"].Tag = player.PlayerID;
                        dgvHomePlayers.Rows[rowIndex].Cells["PlayerName"].Value = player.PlayerName;
                        dgvHomePlayers.Rows[rowIndex].Cells["Position"].Value = player.Position;
                        dgvHomePlayers.Rows[rowIndex].Cells["Number"].Value = player.Number;
                    }
                }
            }
            
        }
    }
}
