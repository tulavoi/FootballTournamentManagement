using BLL;
using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormDialogEditPlayerInMatch : Form
    {
        Bitmap clubLogo, playerImg;
        int playerID, isHomeTeam;
        string selectedMatchID, matchID;

        PlayersInMatchBLL playersInMatchBLL = new PlayersInMatchBLL();


        public FormDialogEditPlayerInMatch(Bitmap clubLogo, string selectedMatchID, int playerID, Bitmap playerImg, string selectedMatchID1, int isHomeTeam)
        {
            InitializeComponent();
            this.clubLogo = clubLogo;
            this.playerImg = playerImg;
            this.selectedMatchID = selectedMatchID;
            this.playerID = playerID;
            matchID = selectedMatchID;
            this.isHomeTeam = isHomeTeam;
        }

        private void FormDialogEditPlayerInMatch_Load(object sender, System.EventArgs e)
        {
            AssignDataOfPlayerInMatchToControls();
        }

        private void AssignDataOfPlayerInMatchToControls()
        {
            PlayersInMatch player = playersInMatchBLL.Get1PlayerInMatch(matchID, playerID);
            pictureBoxClubLogo.Image = clubLogo;
            pictureBoxPlayerImg.Image = playerImg;
            txtPlayerName.Text = player.Player.PlayerName;
            cboPosition.Text = player.Position;
            txtGoals.Text = player.Goal.ToString();
            txtOwnGoals.Text = player.OwnGoal.ToString();
            txtYellowCards.Text = player.YellowCard.ToString();
            txtRedCards.Text = player.RedCard.ToString();
            if (player.IsCaptain == 1)
                chkCaptain.Checked = true;
        }

        // Phương thức chung để kiểm tra ký tự số và phím Backspace
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            PlayersInMatch playerInMatch = GetPlayerInMatch();

            bool isEditPlayerInMatchSuccess = playersInMatchBLL.EditData(playerInMatch);
            if (isEditPlayerInMatchSuccess)
            {
                MessageBox.Show($"Edited \"{playerInMatch.Player.PlayerName}\" in match successfully!");
                Close();
            }
            else
            {
                MessageBox.Show($"Edit \"{playerInMatch.Player.PlayerName}\" in match failed!");
            }
        }

        private PlayersInMatch GetPlayerInMatch()
        {
            PlayersInMatch player = new PlayersInMatch();

            player.PlayerID = playerID;
            player.MatchID = matchID;
            player.Position = cboPosition.Text;
            player.Goal = Convert.ToInt32(txtGoals.Text);
            player.OwnGoal = Convert.ToInt32(txtOwnGoals.Text);
            player.YellowCard = Convert.ToInt32(txtYellowCards.Text);
            player.RedCard = Convert.ToInt32(txtRedCards.Text);
            player.IsCaptain = chkCaptain.Checked == true ? 1 : 0; 
            player.Player = new Player
            {
                PlayerID = playerID,
                PlayerName = txtPlayerName.Text
            };

            return player;
        }
    }
}
