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
    public partial class RefereeForm : Form
    {
        RefereesBLL refereesBLL = new RefereesBLL();
        public RefereeForm()
        {
            InitializeComponent();
        }

        private void RefereeForm_Load(object sender, EventArgs e)
        {
            LoadData();

            dgvReferees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI SemiBold", 10);
        }

        private void LoadData()
        {
            dgvReferees.Rows.Clear();

            List<Referee> referees = refereesBLL.LoadData();

            lblCountReferee.Text = referees.Count.ToString();

            int i = 1;
            foreach (var referee in referees)
            {
                int rowIndex = dgvReferees.Rows.Add();
                dgvReferees.Rows[rowIndex].Cells[0].Tag = referee.RefereeID;
                dgvReferees.Rows[rowIndex].Cells[1].Value = referee.RefereeName;

                DateTime? dob = referee.DOB;
                string dobString = dob.HasValue ? dob.Value.ToString("dd/MM/yyyy") : "";

                dgvReferees.Rows[rowIndex].Cells[2].Value = dobString;
                dgvReferees.Rows[rowIndex].Cells[3].Value = referee.Country;
            }
        }

        private void dgvReferees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgv_CellFormatting(sender, e, editColumn: dgvReferees.Columns[4] as DataGridViewButtonColumn, deleteColumn: dgvReferees.Columns[5] as DataGridViewButtonColumn);
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e, DataGridViewButtonColumn editColumn, DataGridViewButtonColumn deleteColumn)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv != null && (e.ColumnIndex == editColumn.Index || e.ColumnIndex == deleteColumn.Index))
            {
                object cellValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (cellValue != null && cellValue.ToString() == "Edit")
                {
                    DataGridViewButtonCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Style.BackColor = Color.FromArgb(70, 20, 80);
                }
                else if (cellValue != null && cellValue.ToString() == "Delete")
                {
                    DataGridViewButtonCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Style.BackColor = Color.FromArgb(180, 40, 130);
                }
            }
        }

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumericKeyPress(e);
        }

        private void word_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeyPress(e);
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

        // Phương thức chung để kiểm tra ký tự chữ
        private void CheckWordKeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự người dùng nhập vào có phải là chữ, khoảng trắng hoặc ký tự điều khiển không
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                // Nếu không phải là chữ, khoảng trắng và không phải là ký tự điều khiển (như phím Backspace), thì hủy sự kiện KeyPress
                e.Handled = true;
            }
        }

        private void btnSaveClub_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxValues())
                return;

            Referee referee = getReferee();

            bool isAddRefereeSuccess = refereesBLL.AddData(referee);
            if (isAddRefereeSuccess)
            {
                MessageBox.Show("Added referee successfully!");
                ClearControls();
                LoadData();
            }
            else
                MessageBox.Show("Add referee failed!");
        }

        private void ClearControls()
        {
            txtRefereeName.Clear();
            txtDay.Clear();
            txtMonth.Clear();
            txtYear.Clear();
            txtCountry.Clear();
            txtRefereeName.Focus();
        }

        private Referee getReferee()
        {
            Referee referee = new Referee();

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

        private void dgvReferees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 4 && e.ColumnIndex != 5)
                return;

            // Button column edit
            if (e.ColumnIndex == 4)
            {
                HandleEditPlayerButtonClick(e.RowIndex);
            }

            // Button column delete
            if (e.ColumnIndex == 5)
            {
                HandleDeletePlayerButtonClick(e.RowIndex);
            }
        }

        private void HandleEditPlayerButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            string refereeName = dgvReferees.Rows[rowIndex].Cells[1].Value.ToString();

            int refereeID = (int)dgvReferees.Rows[rowIndex].Cells[0].Tag;
            FormDialogEditReferee frm = new FormDialogEditReferee(refereeID);
            frm.ShowDialog();

            LoadData();
        }

        private void HandleDeletePlayerButtonClick(int rowIndex)
        {
            if (rowIndex < 0)
                return;

            string refereeName = dgvReferees.Rows[rowIndex].Cells[1].Value.ToString();

            DialogResult rs = MessageBox.Show($"Are you sure to delete \"{refereeName}\"?",
                                                "Information",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                int refereeID = int.Parse(dgvReferees.Rows[rowIndex].Cells[0].Tag.ToString());

                bool isDeleteRefereeSuccess = refereesBLL.DeleteData(refereeID);
                if (isDeleteRefereeSuccess)
                {
                    MessageBox.Show($"Deleted player \"{refereeName}\" successfully!");
                    LoadData();
                }
                else
                    MessageBox.Show($"Delete player \"{refereeName}\" failed!");
            }
        }

        private void panelAddClub_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
