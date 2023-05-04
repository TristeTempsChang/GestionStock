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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GestionStock
{
    public partial class AddClient : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public AddClient()
        {
            InitializeComponent();
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string phoneRegexPattern = @"^(0|\+33)[1-9]([-. ]?[0-9]{2}){4}$";
            string emailregexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (textBox1.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Nom du client' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!Regex.IsMatch(textBox2.Text, emailregexPattern))
            {
                MyMessageBox.showMessage("Veuillez entrer une adresse e-mail valide", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!Regex.IsMatch(textBox3.Text, phoneRegexPattern))
            {
                MyMessageBox.showMessage("Veuillez entrer un numéro de téléphone valide", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBox5.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Adresse client' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string AddClientSql = "insert into Client values (@Nom, @Adresse, @Mail, @Tel)";
                bd.Open();
                SqlCommand cmd = new SqlCommand(AddClientSql, bd);
                cmd.Parameters.AddWithValue("@Nom", textBox1.Text);
                cmd.Parameters.AddWithValue("@Adresse", textBox5.Text);
                cmd.Parameters.AddWithValue("@Mail", textBox2.Text);
                cmd.Parameters.AddWithValue("@Tel", textBox3.Text);
                cmd.ExecuteNonQuery();
                bd.Close();
                string selectQuery = "SELECT * FROM Client";
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, bd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                Clients.clientData.dtv.DataSource = table;
                this.Notif("Ajouté avec succès", NotifSuccess.enmType.Add);
                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
