using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionStock
{
    public partial class Form1 : Form
    {
        private Button currentButton;
        private Form activeForm;
        bool mouseDown;
        private Point offset;
        public Form1()
        {
            InitializeComponent();
            btnCloseChildForm.Visible = false;
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null) {
                if (currentButton != (Button)btnSender) {
                    DisableButton();
                    Color color = Color.DarkOliveGreen;
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    btnCloseChildForm.Visible = true;
                }
            }
        }

        private void DisableButton()
        {
            foreach(Control previousbtn in panel1.Controls)
            {
                if(previousbtn.GetType() == typeof(Button)) {
                    previousbtn.BackColor = Color.YellowGreen;
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            HomeLabel.Text = childForm.Text;
        }

        private void Reset()
        {
            DisableButton();
            HomeLabel.Text = "HOME";
            currentButton = null;
            btnCloseChildForm.Visible = false;
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Dashboard(), sender);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Products(), sender);
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Clients(), sender);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                Reset();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(deconnexion));
            monthread.Start();
            this.Close();
        }

        public static void deconnexion()
        {
            Application.Run(new login());
        }

        private void btnIntervention_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Intervention(), sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.showMessage("Souhaitez-vous réellement quitter l'application ?", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void btnResize_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnDesize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos  = PointToScreen(e.Location);
                Location =  new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
