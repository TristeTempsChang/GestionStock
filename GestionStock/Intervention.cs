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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GestionStock
{
    public partial class Intervention : Form
    {
        public static Intervention InterData;
        public DataGridView dtv;
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public Intervention()
        {
            InitializeComponent();
            InterData = this;
            dtv = dataGridView1;
        }

        public void datafill()
        {
            string interventionSql = "select m.ID_Inter as 'Intervention n°', m.Date_Inter, m.Commentaire, m.Technicien, c.Nom as 'Matériel', m.Validate from Intervention m join MATERIEL c on m.ID_MAT = c.ID_MAT WHERE Validate = 0 ";
            SqlDataAdapter sqlDa = new SqlDataAdapter(interventionSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.Fill(dttb);
            dataGridView1.DataSource = dttb;
        }

        private void Intervention_Load(object sender, EventArgs e)
        {
            datafill();

            //////////////// Client //////////////// 
            string ClientSql = "select ID_Client, Nom from Client";
            SqlDataAdapter sqlDa = new SqlDataAdapter(ClientSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.Fill(dttb);
            DataRow itemrow = dttb.NewRow();
            itemrow[1] = "Client";
            dttb.Rows.InsertAt(itemrow, 0);
            comboBoxClient.DataSource = dttb;
            comboBoxClient.DisplayMember = "Nom";
            comboBoxClient.ValueMember = "ID_Client";
            ////////////////////////////////////////////

            //////////////// Materiel //////////////// 
            string MaterielSql = "select ID_MAT, Nom from MATERIEL";
            SqlDataAdapter sqlDaa = new SqlDataAdapter(MaterielSql, bd);
            DataTable dttbb = new DataTable();
            sqlDaa.Fill(dttbb);
            DataRow itemroww = dttbb.NewRow();
            itemroww[1] = "Matériel";
            dttbb.Rows.InsertAt(itemroww, 0);
            comboBoxMat.DataSource = dttbb;
            comboBoxMat.DisplayMember = "Nom";
            comboBoxMat.ValueMember = "ID_MAT";
            ////////////////////////////////////////////

            //////////////// Type //////////////// 
            string TypeSql = "select ID_Mat, Type from MATERIEL";
            SqlDataAdapter sqlDaaaa = new SqlDataAdapter(TypeSql, bd);
            DataTable dttbbb = new DataTable();
            sqlDaaaa.Fill(dttbbb);
            DataRow itemrooww = dttbbb.NewRow();
            itemrooww[1] = "Type de matériel";
            dttbbb.Rows.InsertAt(itemrooww, 0);
            comboBoxType.DataSource = dttbbb;
            comboBoxType.DisplayMember = "Type";
            comboBoxType.ValueMember = "ID_MAT";
            ////////////////////////////////////////////
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form addInter = new AddIntervention();
            addInter.Show();
        }

        public void RemplirDataGridView()
        {
            string interventionSql = "select m.ID_Inter as 'Intervention n°', m.Date_Inter, m.Commentaire, m.Technicien, c.Nom as 'Matériel', m.Validate from Intervention m join MATERIEL c on m.ID_MAT = c.ID_MAT WHERE Validate = 0 ";
            SqlDataAdapter adapter = new SqlDataAdapter(interventionSql, bd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.showMessage("Souhaitez-vous valider l'intervention ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                object id = dataGridView1.Rows[rowIndex].Cells[0].Value;
                bd.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Intervention SET Validate = 1 WHERE ID_Inter=@ID AND Validate = 0", bd);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
                SqlCommand updateDate = new SqlCommand("UPDATE MATERIEL SET Date_Install = GETDATE() WHERE ID_MAT = (SELECT ID_MAT FROM Intervention WHERE ID_Inter = @ID)", bd);
                updateDate.Parameters.AddWithValue("@ID", id);
                updateDate.ExecuteNonQuery();
                bd.Close();
                RemplirDataGridView();
                this.Notif("Intervention validée !", NotifSuccess.enmType.Valid);
            }
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.showMessage("Êtes-vous sûr de vouloir supprimer cette intervention ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                object id = dataGridView1.Rows[rowIndex].Cells[0].Value;
                dataGridView1.Rows.RemoveAt(rowIndex);
                string ClientDeleteSql = "delete from Intervention where ID_inter = @ID";
                bd.Open();
                SqlCommand cmd = new SqlCommand(ClientDeleteSql, bd);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
                bd.Close();
                this.Notif("Suppression effectué", NotifSuccess.enmType.Delete);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            datafill();
            comboBoxMat.SelectedIndex = 0;
            comboBoxClient.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            // Construction de la requête SQL pour filtrer les données
            string interventionSql = "select m.ID_Inter as 'Intervention n°', m.Date_Inter, m.Commentaire, m.Technicien, c.Nom as 'Matériel', m.Validate from Intervention m join MATERIEL c on m.ID_MAT = c.ID_MAT WHERE Validate = 0 ";
            if (comboBoxClient.SelectedIndex > 0)
            {
                interventionSql += "AND c.ID_Client = " + comboBoxClient.SelectedValue.ToString();
            }
            if (comboBoxMat.SelectedIndex > 0)
            {
                interventionSql += "AND m.ID_MAT = " + comboBoxMat.SelectedValue.ToString();
            }
            if (comboBoxType.SelectedIndex > 0)
            {
                interventionSql += "AND c.Type = '" + comboBoxType.SelectedValue.ToString() + "'";
            }

            // Exécution de la requête SQL et affichage des données dans le DataGridView
            SqlDataAdapter sqlDa = new SqlDataAdapter(interventionSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.Fill(dttb);
            dataGridView1.DataSource = dttb;
        }

        private void comboBoxMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            InterventionDetail detail = new InterventionDetail();
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            detail.label3.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            detail.textBox4.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            detail.textBox3.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            detail.textBox2.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            detail.textBox1.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
            detail.ShowDialog();
        }

        private void comboBoxClient_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}
