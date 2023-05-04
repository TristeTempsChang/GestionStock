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
    public partial class Demarrage : Form
    {
        int counter = 0;
        int mimic = 0;
        string txts;
        public Demarrage()
        {
            InitializeComponent();
        }

        private void Demarrage_Load(object sender, EventArgs e)
        {
            timer1.Start();
            txts = label2.Text;
            mimic = txts.Length;
            label2.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;
            
            if (panel2.Width >= 297)
            {
                timer1.Stop();
                System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(ouvrirnouveauform));
                monthread.Start();
                this.Close();
            }
        }

        public static void ouvrirnouveauform()
        {
            Application.Run(new login());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter > mimic )
            {
                counter = 0;
                label2.Text = "";
            }
            else
            {
                label2.Text = txts.Substring(0, counter);
            }
        }
    }
}
