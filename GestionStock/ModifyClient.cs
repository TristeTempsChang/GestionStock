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

namespace GestionStock
{
    public partial class ModifyClient : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public ModifyClient()
        {
            InitializeComponent();
            string numberClient = "SELECT * FROM Client WHERE ID_Client = @ID";
            string nameClient = "SELECT * FROM Client WHERE Nom = @Name";
            string adresseClient = "SELECT * FROM Client WHERE Adresse = @Adresse";
            string mailClient = "SELECT * FROM Client WHERE Mail = @Mail";
            string telClient = "SELECT * FROM Client WHERE Tel = @Tel";
            int rowIndex = Clients.clientData.dtv.CurrentCell.RowIndex;
            object id = Clients.clientData.dtv.Rows[rowIndex].Cells[0].Value;
            object name = Clients.clientData.dtv.Rows[rowIndex].Cells[1].Value;
            object adresse = Clients.clientData.dtv.Rows[rowIndex].Cells[2].Value;
            object mail = Clients.clientData.dtv.Rows[rowIndex].Cells[3].Value;
            object tel = Clients.clientData.dtv.Rows[rowIndex].Cells[4].Value;
            bd.Open();
            using (SqlCommand cmd = new SqlCommand(numberClient, bd))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        homeLabelNum.Text = dr.GetValue(0).ToString();
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(nameClient, bd))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        textBox1.Text = dr.GetValue(1).ToString();
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(adresseClient, bd))
            {
                cmd.Parameters.AddWithValue("@Adresse", adresse);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        textBox5.Text = dr.GetValue(2).ToString();
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(mailClient, bd))
            {
                cmd.Parameters.AddWithValue("@Mail", mail);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        textBox3.Text = dr.GetValue(3).ToString();
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(telClient, bd))
            {
                cmd.Parameters.AddWithValue("@Tel", tel);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        textBox2.Text = dr.GetValue(4).ToString();
                    }
                }
            }
            bd.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = Clients.clientData.dtv.CurrentCell.RowIndex;
            object id = Clients.clientData.dtv.Rows[rowIndex].Cells[0].Value;
            bd.Open();
            SqlCommand cmd = new SqlCommand("Update Client set Nom =@nom , Adresse=@adrss, Mail=@ml, Tel=@tl where ID_Client=@ID", bd);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@nom", textBox1.Text);
            cmd.Parameters.AddWithValue("@adrss", textBox5.Text);
            cmd.Parameters.AddWithValue("@ml", textBox3.Text);
            cmd.Parameters.AddWithValue("@tl", textBox2.Text);
            cmd.ExecuteNonQuery();
            bd.Close();
            string selectQuery = "SELECT * FROM Client";
            SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, bd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Clients.clientData.dtv.DataSource = table;
            this.Notif("Modifié avec succès", NotifSuccess.enmType.Info);
            this.Close();
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
