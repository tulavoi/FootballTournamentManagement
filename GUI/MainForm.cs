using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {
        Guna2GradientButton currentBtn;

        private Image imgHomeBtnBeforeHover = Properties.Resources.home;

        private Image imgPlayerBtnBeforeHover = Properties.Resources.soccer_player;

        private Image imgClubBtnBeforeHover = Properties.Resources.club__1_;

        private Image imgMatchBtnBeforeHover = Properties.Resources.football;

        private Image imgRefereeBtnBeforeHover = Properties.Resources.referee;

        private Image imgSettingBtnBeforeHover = Properties.Resources.setting;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ActiveButton(object sender)
        {
            if (sender != null)
            {
                currentBtn = (Guna2GradientButton)sender;
                currentBtn.FillColor = Color.FromArgb(160, 20, 110);
                currentBtn.FillColor2 = Color.FromArgb(50, 0, 60);
                currentBtn.ForeColor = Color.White;
                currentBtn.Image = currentBtn.HoverState.Image;
            }
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
        }
        

        private void btnOpenHomeForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Home";
            iconForm.Image = btnOpenHomeForm.HoverState.Image;
            Container(new HomeForm());
            btnOpenHomeForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenHomeForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenHomeForm.ForeColor = Color.White;
            btnOpenHomeForm.Image = btnOpenHomeForm.HoverState.Image;

            // Btn club 
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;
            btnOpenClubForm.Image = imgClubBtnBeforeHover;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;
            btnOpenMatchForm.Image = imgMatchBtnBeforeHover;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;
            btnOpenRefereeForm.Image = imgRefereeBtnBeforeHover;

            // Btn setting
            btnOpenSettingForm.FillColor = Color.White;
            btnOpenSettingForm.FillColor2 = Color.White;
            btnOpenSettingForm.ForeColor = Color.Black;
            btnOpenSettingForm.Image = imgSettingBtnBeforeHover;
        }


        internal void btnOpenClubForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Clubs";
            iconForm.Image = btnOpenClubForm.HoverState.Image;
            Container(new ClubForm());
            btnOpenClubForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenClubForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenClubForm.ForeColor = Color.White;
            btnOpenClubForm.Image = btnOpenClubForm.HoverState.Image;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;
            btnOpenHomeForm.Image = imgHomeBtnBeforeHover;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;
            btnOpenMatchForm.Image = imgMatchBtnBeforeHover;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;
            btnOpenRefereeForm.Image = imgRefereeBtnBeforeHover;

            // Btn setting
            btnOpenSettingForm.FillColor = Color.White;
            btnOpenSettingForm.FillColor2 = Color.White;
            btnOpenSettingForm.ForeColor = Color.Black;
            btnOpenSettingForm.Image = imgSettingBtnBeforeHover;
        }

        private void btnOpenMatchForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Matches";
            iconForm.Image = btnOpenMatchForm.HoverState.Image;
            //Container(new MatchForm());
            btnOpenMatchForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenMatchForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenMatchForm.ForeColor = Color.White;
            btnOpenMatchForm.Image = btnOpenMatchForm.HoverState.Image;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;
            btnOpenHomeForm.Image = imgHomeBtnBeforeHover;

            // Btn club
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;
            btnOpenClubForm.Image = imgClubBtnBeforeHover;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;
            btnOpenRefereeForm.Image = imgRefereeBtnBeforeHover;

            // Btn setting
            btnOpenSettingForm.FillColor = Color.White;
            btnOpenSettingForm.FillColor2 = Color.White;
            btnOpenSettingForm.ForeColor = Color.Black;
            btnOpenSettingForm.Image = imgSettingBtnBeforeHover;
        }

        private void btnOpenRefereeForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Referee";
            iconForm.Image = btnOpenRefereeForm.HoverState.Image;
            Container(new RefereeForm());
            btnOpenRefereeForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenRefereeForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenRefereeForm.ForeColor = Color.White;
            btnOpenRefereeForm.Image = btnOpenRefereeForm.HoverState.Image;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;
            btnOpenHomeForm.Image = imgHomeBtnBeforeHover;

            // Btn club
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;
            btnOpenClubForm.Image = imgClubBtnBeforeHover;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;
            btnOpenMatchForm.Image = imgMatchBtnBeforeHover;

            // Btn setting
            btnOpenSettingForm.FillColor = Color.White;
            btnOpenSettingForm.FillColor2 = Color.White;
            btnOpenSettingForm.ForeColor = Color.Black;
            btnOpenSettingForm.Image = imgSettingBtnBeforeHover;
        }

        private void btnOpenSettingForm_Click(object sender, EventArgs e)
        {
            labelValue.Text = "Setting";
            iconForm.Image = btnOpenSettingForm.HoverState.Image;
            //Container(new SettingForm());
            btnOpenSettingForm.FillColor = Color.FromArgb(160, 20, 110);
            btnOpenSettingForm.FillColor2 = Color.FromArgb(50, 0, 60);
            btnOpenSettingForm.ForeColor = Color.White;
            btnOpenSettingForm.Image = btnOpenSettingForm.HoverState.Image;

            // Btn home
            btnOpenHomeForm.FillColor = Color.White;
            btnOpenHomeForm.FillColor2 = Color.White;
            btnOpenHomeForm.ForeColor = Color.Black;
            btnOpenHomeForm.Image = imgHomeBtnBeforeHover;

            // Btn club
            btnOpenClubForm.FillColor = Color.White;
            btnOpenClubForm.FillColor2 = Color.White;
            btnOpenClubForm.ForeColor = Color.Black;
            btnOpenClubForm.Image = imgClubBtnBeforeHover;

            // Btn match
            btnOpenMatchForm.FillColor = Color.White;
            btnOpenMatchForm.FillColor2 = Color.White;
            btnOpenMatchForm.ForeColor = Color.Black;
            btnOpenMatchForm.Image = imgMatchBtnBeforeHover;

            // Btn referee
            btnOpenRefereeForm.FillColor = Color.White;
            btnOpenRefereeForm.FillColor2 = Color.White;
            btnOpenRefereeForm.ForeColor = Color.Black;
            btnOpenRefereeForm.Image = imgRefereeBtnBeforeHover;
        }
    }
}
