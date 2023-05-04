using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionStock
{
    public partial class Forgot : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        private Point offset;
        bool mouseDown;
        public Forgot()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwdRegexPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+=[\\]{}|\\\\:;\"'<>,.?/])(?!.*(.)\\1{2}).{8,}$";

            if (textBox1.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Nom d'utilisateur' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!Regex.IsMatch(textBox2.Text, pwdRegexPattern))
            {
                MyMessageBox.showMessage("Veuillez entrer un mot de passe valide" + "\n" + "- Doit contenir au moins un nombre et une lettre majuscule" + "\n" + "- Doit possèder au moins 8 caractères minimum" + "\n" + "- Doit contenir au moins un caractère spécial", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBox3.Text == "")
            {
                MyMessageBox.showMessage("Veuillez confirmer votre mot de passe avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBox3.Text != textBox2.Text)
            {
                MyMessageBox.showMessage("Le mot de passe n'est pas le même que celui rentré plus haut !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    bd.Open();
                    string hashedPassword = HashPassword(textBox2.Text);
                    SqlCommand cmd = new SqlCommand("SELECT PwdHash FROM Utilisateur WHERE Login = @username", bd);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    string currentHash = (string)cmd.ExecuteScalar();
                    if (currentHash != null && currentHash.Equals(hashedPassword))
                    {
                        MyMessageBox.showMessage("Le nouveau mot de passe est identique" + "\n" + "ou très similaire à l'ancien mot de passe !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (hashedPassword.StartsWith(currentHash))
                    {
                        MyMessageBox.showMessage("Le nouveau mot de passe contient l'ancien mot de passe !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE Utilisateur SET PwdHash = @password WHERE Login = @username", bd);
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.ExecuteNonQuery();
                        bd.Close();
                        this.Notif("Modifié avec succès", NotifSuccess.enmType.Info);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    this.Notif("Erreur lors de la modification", NotifSuccess.enmType.Error);
                }
                finally
                {
                    bd.Close();
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
