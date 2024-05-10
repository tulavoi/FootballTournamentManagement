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
    public partial class FormDialogEditReferee : Form
    {
        RefereesBLL refereesBLL = new RefereesBLL();
        int refereeID;
        public FormDialogEditReferee(int refereeID)
        {
            InitializeComponent();
            this.refereeID = refereeID;
        }

        private void FormDialogEditReferee_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            Referee referee = refereesBLL.LoadDataByRefereeID(refereeID);

            // Gán values referee vào controls
            AssignDataToTextBoxes(referee);
        }

        private void AssignDataToTextBoxes(Referee referee)
        {
            if (referee == null)
                return;

            txtRefereeName.Text = referee.RefereeName;

            if (referee.DOB != null)
            {
                DateTime dob = (DateTime)referee.DOB;
                txtDay.Text = dob.Day.ToString();
                txtMonth.Text = dob.Month.ToString();
                txtYear.Text = dob.Year.ToString();
            }

            txtCountry.Text = referee.Country;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxValues())
                return;

            Referee referee = getReferee();

            bool isEditRefereeSuccess = refereesBLL.EditData(referee);
            if (isEditRefereeSuccess)
            {
                MessageBox.Show("Edited referee successfully!");
                Close();
            }
            else
                MessageBox.Show("Edit referee failed!");
        }

        private Referee getReferee()
        {
            Referee referee = new Referee();

            referee.RefereeID = refereeID;
            referee.RefereeName = txtRefereeName.Text;

            referee.DOB = getDOB();

            referee.Country = txtCountry.Text;

            return referee;
        }

        private DateTime? getDOB()
        {
            int day = int.Parse(txtDay.Text);
            int month = int.Parse(txtMonth.Text);
            int year = int.Parse(txtYear.Text);

            return new DateTime(year, month, day);
        }

        #region Validate textboxes value
        private bool ValidateTextBoxValues()
        {
            if (!ValidateName())
                return false;

            if (!ValidateCountry())
                return false;

            if (!ValidateDOB())
                return false;

            return true;
        }


        private bool ValidateCountry()
        {
            if (txtCountry.Text == "")
            {
                MessageBox.Show("Please enter a manager country");
                return false;
            }

            if (txtCountry.Text.Length > 255)
            {
                MessageBox.Show("Country must be less than 255 characters!");
                return false;
            }
            return true;
        }

        private bool ValidateDOB()
        {
            int day = txtDay.Text != "" ? int.Parse(txtDay.Text) : 0;
            int month = txtMonth.Text != "" ? int.Parse(txtMonth.Text) : 0;
            int year = txtYear.Text != "" ? int.Parse(txtYear.Text) : 0;

            if (day < 1 || day > 31)
            {
                MessageBox.Show("Invalid day!");
                return false;
            }
            if (month < 1 || month > 12)
            {
                MessageBox.Show("Invalid month!");
                return false;
            }
            if (year < 1900 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Invalid year!");
                return false;
            }
            try
            {
                DateTime dob = new DateTime(year, month, day);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Invalid DOB!");
                return false;
            }
        }

        private bool ValidateName()
        {
            if (txtRefereeName.Text == "")
            {
                MessageBox.Show("Please enter a name!");
                return false;
            }

            if (txtRefereeName.Text.Length > 255)
            {
                MessageBox.Show("Name must be less than 255 characters!");
                return false;
            }
            return true;
        }
        #endregion
    }
}
