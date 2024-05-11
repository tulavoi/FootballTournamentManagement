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

        private string shortcutLogoPath = "Images\\Logos\\";

        public FormDialogCreateMatches()
        {
            InitializeComponent();
        }

        private void FormDialogCreateMatches_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);

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
    }
}
