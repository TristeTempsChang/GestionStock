using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionStock
{
    public partial class Products : Form
    {
        bool apparead = boolMTBF.apparead;
        public static Products productData;
        public DataGridView dtv;
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public Products()
        {
            InitializeComponent();
            productData = this;
            dtv = dataGridView1;
        }

        private void Products_Load(object sender, EventArgs e)
        {
            dataGridReload();
            searchData("");
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }

        private void dataGridReload()
        {
            string MaterielSql = "select m.Nom, m.Noserie as 'N°série', m.Date_Install, m.Marque, m.MTBF, m.Type, c.Nom as 'Client' from MATERIEL m join Client c on m.ID_Client = c.ID_Client";
            SqlDataAdapter sqlDa = new SqlDataAdapter(MaterielSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.Fill(dttb);
            dataGridView1.DataSource = dttb;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form addProduct = new AddProduct();
            addProduct.Show();
        }

        public void searchData(string valueToSearch)
        {
            string query = "select m.Nom, m.Noserie as 'N°série', m.Date_Install, m.Marque, m.MTBF, m.Type, c.Nom as 'Client' from MATERIEL m join Client c on m.ID_Client = c.ID_Client WHERE m.Nom LIKE '%" + valueToSearch + "%' OR m.Noserie LIKE '%" + valueToSearch + "%' OR m.Date_Install LIKE '%" + valueToSearch + "%' OR m.Marque LIKE '%" + valueToSearch + "%' OR m.MTBF LIKE '%" + valueToSearch + "%' OR m.Type LIKE '%" + valueToSearch + "%' OR c.Nom LIKE '%" + valueToSearch + "%'";
            SqlCommand Command = new SqlCommand(query, bd);
            SqlDataAdapter adapter = new SqlDataAdapter(Command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGridView1.DataSource = Table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string valueToSearch = searchBar.Text.ToString();
            searchData(valueToSearch);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.showMessage("Êtes-vous sûr de vouloir supprimer le produit ?" + "\n" + "Attention, tout les clients et les interventions liés à ce produit " + "\n" + " seront aussi supprimés...", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                object id = dataGridView1.Rows[rowIndex].Cells[0].Value;
                dataGridView1.Rows.RemoveAt(rowIndex);
                string InterventionDeleteSql = "delete from Intervention where ID_MAT = (select ID_MAT from MATERIEL where Nom = @Nom)";
                bd.Open();
                SqlCommand cmdIntervention = new SqlCommand(InterventionDeleteSql, bd);
                cmdIntervention.Parameters.AddWithValue("@Nom", id);
                cmdIntervention.ExecuteNonQuery();
                string MatDeleteSql = "delete from MATERIEL where Nom = @Nom";
                SqlCommand cmdMat = new SqlCommand(MatDeleteSql, bd);
                cmdMat.Parameters.AddWithValue("@Nom", id);
                cmdMat.ExecuteNonQuery();

                bd.Close();
                this.Notif("Suppression effectué", NotifSuccess.enmType.Delete);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form ModifyProduct = new ModifyProduct();
            ModifyProduct.Show();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Vérifie si la colonne en cours de formatage est la colonne MTBF
            if (this.dataGridView1.Columns[e.ColumnIndex].Name.Equals("MTBF"))
            {
                // Récupère les valeurs de la colonne Date_Install et MTBF pour la ligne en cours de traitement
                DateTime dateInstall = Convert.ToDateTime(this.dataGridView1.Rows[e.RowIndex].Cells["Date_Install"].Value);
                int mtbf = Convert.ToInt32(e.Value);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    // Vérifie si la valeur de MTBF correspond à l'incrémentation de la date d'installation
                    if (mtbf == (DateTime.Now.Year - dateInstall.Year))
                    {
                        // Appliquer le format rouge à la cellule correspondante
                        dataGridView1.Rows[e.RowIndex].Cells[column.Name].Style.BackColor = Color.Red;
                        dataGridView1.Rows[e.RowIndex].Cells[column.Name].Style.ForeColor = Color.White;
                    }
                }
            }
            // Vérifiez si la cellule appartient à la colonne "Nom"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Nom")
            {
                // Vérifiez si la couleur de fond de la cellule est rouge
                if (e.CellStyle.BackColor == Color.Red)
                {
                    
                      string rowContent = dataGridView1.Rows[e.RowIndex].Cells["Nom"].Value.ToString();
                      // Affichez une MessageBox avec le contenu des cellules ayant un fond rouge
                        if (MyMessageBox.showMessage("Le MTBF de " + rowContent + " arrive à son terme" + "\n" + "Souhaitez-vous lancer une intervention immédiatement ?", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                           Form addIntervention = new AddIntervention();
                           addIntervention.Show();
                        }
                }
            }
        }
    }
}
