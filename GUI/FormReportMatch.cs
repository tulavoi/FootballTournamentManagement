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
    public partial class FormReportMatch : Form
    {
        int seasonID;
        string roundID;

        MatchesBLL matchesBLL = new MatchesBLL();

        public FormReportMatch(int seasonID, string roundID)
        {
            InitializeComponent();
            this.seasonID = seasonID;
            this.roundID = roundID;
        }

        private void FormReportClub_Load(object sender, EventArgs e)
        {

            try
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.ReportMatches.rdlc";
                ReportDataSource rpDataSource = new ReportDataSource();
                rpDataSource.Name = "DataSet1";
                List<Match> matches = matchesBLL.GetDataByRoundID(roundID, seasonID);
                rpDataSource.Value = matches;
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
