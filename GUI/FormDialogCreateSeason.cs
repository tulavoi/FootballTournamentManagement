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
    public partial class FormDialogCreateSeason : Form
    {
        SeasonsBLL seasonsBLL = new SeasonsBLL();
        public FormDialogCreateSeason()
        {
            InitializeComponent();
        }

        private void FormDialogCreateSeason_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxValues())
                return;

            Season season = getSeason();

            bool isAddSeasonSuccess = seasonsBLL.AddData(season);
            if (isAddSeasonSuccess)
            {
                MessageBox.Show("Added season successfully!");
                ClearControls();
            }
            else
                MessageBox.Show("Add season failed!");
        }

        private void ClearControls()
        {
            txtStartDay.Clear();
            txtStartMonth.Clear();
            txtStartYear.Clear();

            txtEndDay.Clear();
            txtEndMonth.Clear();
            txtEndYear.Clear();

            txtStartDay.Focus();
        }

        private Season getSeason()
        {
            Season season = new Season();

            season.StartDate = getDate(txtStartDay.Text, txtStartMonth.Text, txtStartYear.Text);
            season.EndDate = getDate(txtEndDay.Text, txtEndMonth.Text, txtEndYear.Text);

            string startYear = txtStartYear.Text.Substring(2);
            string endYear = txtEndYear.Text.Substring(2);

            season.SeasonName = startYear + "/" + endYear;

            return season;
        }

        private DateTime? getDate(string dayStr, string monthStr, string yearStr)
        {
            int day = int.Parse(dayStr);
            int month = int.Parse(monthStr);
            int year = int.Parse(yearStr);

            return new DateTime(year, month, day);
        }

        #region Validate textboxes value
        private bool ValidateTextBoxValues()
        {
            if (!ValidateStartDate())
                return false;

            if (!ValidateEndDate())
                return false;

            return true;
        }

        private bool ValidateEndDate()
        {
            int startYear = txtStartYear.Text != "" ? int.Parse(txtStartYear.Text) : 0;

            int day = txtEndDay.Text != "" ? int.Parse(txtEndDay.Text) : 0;
            int month = txtEndMonth.Text != "" ? int.Parse(txtEndMonth.Text) : 0;
            int year = txtEndYear.Text != "" ? int.Parse(txtEndYear.Text) : 0;

            if (day < 1 || day > 31)
            {
                MessageBox.Show("Invalid end day!");
                return false;
            }
            if (month < 1 || month > 12)
            {
                MessageBox.Show("Invalid end month!");
                return false;
            }
            if (year < startYear || year > startYear + 2 || year == startYear)
            {
                MessageBox.Show("Invalid end year!");
                return false;
            }
            try
            {
                DateTime dob = new DateTime(year, month, day);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Invalid end date!");
                return false;
            }
        }

        private bool ValidateStartDate()
        {
            int day = txtStartDay.Text != "" ? int.Parse(txtStartDay.Text) : 0;
            int month = txtStartMonth.Text != "" ? int.Parse(txtStartMonth.Text) : 0;
            int year = txtStartYear.Text != "" ? int.Parse(txtStartYear.Text) : 0;

            if (day < 1 || day > 31)
            {
                MessageBox.Show("Invalid start day!");
                return false;
            }
            if (month < 1 || month > 12)
            {
                MessageBox.Show("Invalid start month!");
                return false;
            }
            if (year < 1900 || year > 2040)
            {
                MessageBox.Show("Invalid start year!");
                return false;
            }
            try
            {
                DateTime dob = new DateTime(year, month, day);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Invalid start date!");
                return false;
            }
        }

        #endregion
    }
}
