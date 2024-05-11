using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {
        Guna2GradientButton currentBtn;

        public MainForm()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Gắn form con vào panel của main form 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private bool Container(object form)
        {
            try
            {
                if (panelContainer.Controls.Count > 0) panelContainer.Controls.Clear();

                Form frm = (Form)form;
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                Control parent = panelContainer.Parent;
                while (parent != null)
                {
                    if(parent is MainForm mainForm)
                    {
                        mainForm.panelContainer.Controls.Add(frm);
                        mainForm.panelContainer.Tag = frm;
                        frm.Show();
                        return frm.Dock == DockStyle.Fill;
                    }
                }

                //panelContainer.Controls.Add(frm);
                //panelContainer.Tag = frm;
                //frm.Show();
                //return frm.Dock == DockStyle.Fill;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            //string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            //MessageBox.Show(connectionString);
        }
        

        private void btnOpenHomeForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Home";
            Container(new HomeForm());
            btnOpenHomeForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenHomeForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenHomeForm.ForeColor = Color.White;

            // Btn club 
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;

            // Btn setting
            btnOpenSeasonForm.FillColor = Color.White;
            btnOpenSeasonForm.FillColor2 = Color.White;
            btnOpenSeasonForm.ForeColor = Color.Black;
        }


        internal void btnOpenClubForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Clubs";
            Container(new ClubForm());
            btnOpenClubForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenClubForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenClubForm.ForeColor = Color.White;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;

            // Btn setting
            btnOpenSeasonForm.FillColor = Color.White;
            btnOpenSeasonForm.FillColor2 = Color.White;
            btnOpenSeasonForm.ForeColor = Color.Black;
        }

        private void btnOpenMatchForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Matches";
            Container(new MatchForm());
            btnOpenMatchForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenMatchForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenMatchForm.ForeColor = Color.White;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;

            // Btn club
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;

            // Btn setting
            btnOpenSeasonForm.FillColor = Color.White;
            btnOpenSeasonForm.FillColor2 = Color.White;
            btnOpenSeasonForm.ForeColor = Color.Black;
        }

        private void btnOpenRefereeForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Referee";
            Container(new RefereeForm());
            btnOpenRefereeForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenRefereeForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenRefereeForm.ForeColor = Color.White;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;

            // Btn club
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;

            // Btn setting
            btnOpenSeasonForm.FillColor = Color.White;
            btnOpenSeasonForm.FillColor2 = Color.White;
            btnOpenSeasonForm.ForeColor = Color.Black;
        }

        private void btnOpenSettingForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Season";
            //Container(new SeasonForm());
            btnOpenSeasonForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenSeasonForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenSeasonForm.ForeColor = Color.White;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;

            // Btn club
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;
        }
    }
}
