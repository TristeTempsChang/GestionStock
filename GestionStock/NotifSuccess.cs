using GestionStock.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionStock
{
    public partial class NotifSuccess : Form
    {
        public NotifSuccess()
        {
            InitializeComponent();
        }

        public enum enmAction
        {
            wait,
            start,
            close
        }

        public enum enmType
        {
            Add,
            Delete,
            Info,
            Error,
            Valid
        }

        private NotifSuccess.enmAction action;

        private int x, y;

        private void cancel_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = enmAction.close;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(this.action)
            {
                case enmAction.wait:
                    timer1.Interval = 5000;
                    action = enmAction.close;
                    break;
                case enmAction.start:
                    timer1.Interval = 1;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left --;
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = enmAction.wait;
                        }
                    }
                    break;
                case enmAction.close:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;

                    this.Left -= 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Close();
                    }
                    break;
            }
        }

        public void showNotif(string msg, enmType type)
        {
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 0; i < 10; i++)
            {
                fname = "Success" + i.ToString();
                NotifSuccess frm = (NotifSuccess)Application.OpenForms[fname];

                if (frm == null)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - -10;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 10;
                    this.Location = new Point(this.x, this.y);
                    break;
                }
            }

            switch (type)
            {
                case enmType.Add:
                    this.pictureBox1.Image = Resources.icons8_check_circle_64px;
                    this.BackColor = Color.ForestGreen;
                    break;
                case enmType.Delete:
                    this.pictureBox1.Image = Resources.delete;
                    this.BackColor = Color.DarkRed;
                    this.pictureBox1.BackColor = Color.DarkRed;
                    this.cancel.BackColor = Color.DarkRed;
                    this.cancel.FlatAppearance.MouseDownBackColor = Color.DarkRed;
                    this.cancel.FlatAppearance.MouseOverBackColor = Color.DarkRed;
                    break;
                case enmType.Info:
                    this.pictureBox1.Image = Resources.update;
                    this.BackColor = Color.Orange;
                    this.pictureBox1.BackColor = Color.Orange;
                    this.cancel.BackColor = Color.Orange;
                    this.cancel.FlatAppearance.MouseDownBackColor = Color.Orange;
                    this.cancel.FlatAppearance.MouseOverBackColor = Color.Orange;
                    break;
                case enmType.Error:
                    this.pictureBox1.Image = Resources.error;
                    this.BackColor = Color.Violet;
                    this.pictureBox1.BackColor = Color.Violet;
                    this.cancel.BackColor = Color.Violet;
                    this.cancel.FlatAppearance.MouseDownBackColor = Color.Violet;
                    this.cancel.FlatAppearance.MouseOverBackColor = Color.Violet;
                    break;
                case enmType.Valid:
                    this.pictureBox1.Image = Resources.icons8_check_circle_64px;
                    this.BackColor = Color.SteelBlue;
                    this.pictureBox1.BackColor = Color.SteelBlue;
                    this.cancel.BackColor = Color.SteelBlue;
                    this.cancel.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
                    this.cancel.FlatAppearance.MouseOverBackColor = Color.SteelBlue;
                    break;
            }

            this.SuccessText.Text = msg;

            this.Show();
            this.action = enmAction.start;
            this.timer1.Interval = 1;
            timer1.Start();

            // ajuster la position de la fenêtre en fonction de la taille de l'écran
            int margin = 15;
            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - margin + 10;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height - margin - -50;
            this.Location = new Point(x, y);
        }
    }
}
