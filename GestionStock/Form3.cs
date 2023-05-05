using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionStock
{
    public partial class Clients : Form
    {
        public static Clients clientData;
        public DataGridView dtv;
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public Clients()
        {
            InitializeComponent();
            clientData = this;
            dtv = dataGridView1;
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }

        public void datafill()
        {
            string ClientSql = "GetClients";
            SqlDataAdapter sqlDa = new SqlDataAdapter(ClientSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.Fill(dttb);
            dataGridView1.DataSource = dttb;
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            datafill();
            searchData("");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form addClient = new AddClient();
            addClient.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.showMessage("Êtes-vous sûr de vouloir supprimer le client ?" + "\n" + "Attention, tout les produits et les interventions liés à ce client " + "\n" + " seront aussi supprimés...", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                object id = dataGridView1.Rows[rowIndex].Cells[0].Value;
                dataGridView1.Rows.RemoveAt(rowIndex);
                string InterventionDeleteSql = "delete from Intervention where ID_MAT IN (select ID_MAT from MATERIEL where ID_Client = @ID)";
                bd.Open();
                SqlCommand cmdIntervention = new SqlCommand(InterventionDeleteSql, bd);
                cmdIntervention.Parameters.AddWithValue("@ID", id);
                cmdIntervention.ExecuteNonQuery();
                string MATERIELDeleteSql = "delete from MATERIEL where ID_Client = @ID";
                SqlCommand cmdMATERIEL = new SqlCommand(MATERIELDeleteSql, bd);
                cmdMATERIEL.Parameters.AddWithValue("@ID", id);
                cmdMATERIEL.ExecuteNonQuery();
                string ClientDeleteSql = "delete from Client where ID_Client = @ID";
                SqlCommand cmdClient = new SqlCommand(ClientDeleteSql, bd);
                cmdClient.Parameters.AddWithValue("@ID", id);
                cmdClient.ExecuteNonQuery();
                bd.Close();
                this.Notif("Suppression effectué", NotifSuccess.enmType.Delete);
            }
        }

        public void searchData(string valueToSearch)
        {
            string query = "SELECT * FROM Client WHERE Nom LIKE '%" + valueToSearch + "%' OR Adresse LIKE '%" + valueToSearch + "%' OR Mail LIKE '%" + valueToSearch + "%' OR Tel LIKE '%" + valueToSearch + "%'";
            SqlCommand Command = new SqlCommand(query, bd);
            SqlDataAdapter adapter = new SqlDataAdapter(Command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGridView1.DataSource = Table;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string valueToSearch = searchBar.Text.ToString();
            searchData(valueToSearch);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form modifyClient = new ModifyClient();
            modifyClient.Show();
        }
    }
}
