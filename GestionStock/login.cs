using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace GestionStock
{
    public partial class login : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        private Point offset;
        bool mouseDown;
        public login()
        {
            InitializeComponent();
            if (Properties.Settings.Default.SauvegarderSession)
            {
                textBox1.Text = Properties.Settings.Default.NomUtilisateur;
                textBox2.Text = Properties.Settings.Default.MotDePasse;
                checkBox1.Checked = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_Load(object sender, EventArgs e)
        {
          
        }

        public static void ouvrirnouveauform()
        {
            Application.Run(new Form1());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Nom d'utilisateur' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Mot de passe' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    bd.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Utilisateur WHERE Login = @username", bd);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                        reader.Read();
                        string hashedPassword = reader.GetString(reader.GetOrdinal("PwdHash"));

                        if (VerifyPassword(textBox2.Text, hashedPassword))
                        {
                            if (checkBox1.Checked)
                            {
                                Properties.Settings.Default.NomUtilisateur = textBox1.Text;
                                Properties.Settings.Default.MotDePasse = textBox2.Text;
                                Properties.Settings.Default.SauvegarderSession = true;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.SauvegarderSession = false;
                                Properties.Settings.Default.Save();
                            }
                            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(ouvrirnouveauform));
                            monthread.Start();
                            this.Close();
                        }
                        else
                        {
                            MyMessageBox.showMessage("Le nom d'utilisateur ou le mot de passe ou les deux sont incorrect...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MyMessageBox.showMessage("Le nom d'utilisateur ou le mot de passe ou les deux sont incorrect...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                    bd.Close();
                }
                catch (Exception ex)
                {
                    this.Notif("Erreur lors de la connexion", NotifSuccess.enmType.Error);
                    bd.Close();
                }
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                string newHashedPassword = builder.ToString();
                return newHashedPassword.Equals(hashedPassword);
            }
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form register = new Register();
            register.Show();
        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void login_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void login_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.NomUtilisateur = textBox1.Text;
                Properties.Settings.Default.MotDePasse = textBox2.Text;
                Properties.Settings.Default.SauvegarderSession = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.NomUtilisateur = "";
                Properties.Settings.Default.MotDePasse = "";
                Properties.Settings.Default.SauvegarderSession = false;
                Properties.Settings.Default.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form forgot = new Forgot();
            forgot.Show();
        }
    }
}
