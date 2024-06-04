using BLL;
using DAL;
using Microsoft.Reporting.WinForms;
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
    public partial class FormReportPlayersInMatch : Form
    {
        string matchID;
        int isHome;

        PlayersInMatchBLL playersInMatchBLL = new PlayersInMatchBLL();

        public FormReportPlayersInMatch(string selectedMatchID, int isHome)
        {
            InitializeComponent();
            matchID = selectedMatchID;
            this.isHome = isHome;
        }

        private void FormReportPlayersInMatch_Load(object sender, EventArgs e)
        {
            try
            {

                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.ReportPlayersInMatch.rdlc";
                ReportDataSource rpDataSource = new ReportDataSource();
                rpDataSource.Name = "DataSet1";
                List<PlayerInMatchDTO> players = playersInMatchBLL.LoadPlayerInMatchDTO(matchID, isHome);
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
