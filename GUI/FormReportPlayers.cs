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
using BLL;
using DAL;
using Microsoft.Reporting.WinForms;

namespace GUI
{
    public partial class FormReportPlayers : Form
    {
        Bitmap clubLogo;
        int clubID;

        PlayersBLL playersBLL = new PlayersBLL();
        ClubsBLL clubsBLL = new ClubsBLL();

        public FormReportPlayers(int selectedClubID, Bitmap clubLogo)
        {
            InitializeComponent();
            clubID = selectedClubID;
            this.clubLogo = clubLogo;
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            try
            {
                Club club = clubsBLL.LoadDataByID(clubID);
                lblClubName.Text += " " + club.ClubName;
                
                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.ReportPlayers.rdlc";
                ReportDataSource rpDataSource = new ReportDataSource();
                rpDataSource.Name = "DataSet1";
                List<Player> players = playersBLL.LoadDataByClubID(clubID);
                rpDataSource.Value = players;
                reportViewer1.LocalReport.DataSources.Add(rpDataSource);

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
