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
    public partial class AddIntervention : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public AddIntervention()
        {
            InitializeComponent();
        }

        private void AddIntervention_Load_1(object sender, EventArgs e)
        {
            string MaterielSql = "select ID_MAT, Nom from MATERIEL";
            SqlDataAdapter sqlDa = new SqlDataAdapter(MaterielSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.Fill(dttb);
            DataRow itemrow = dttb.NewRow();
            itemrow[1] = "Sélectionnez un Matériel";
            dttb.Rows.InsertAt(itemrow, 0);
            comboBox2.DataSource = dttb;
            comboBox2.DisplayMember = "Nom";
            comboBox2.ValueMember = "ID_MAT";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Commentaire' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox2.SelectedIndex == 0)
            {
                MyMessageBox.showMessage("Veuillez sélectionner un matériel dans le champ" + "\n" + " 'Matériel' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBox1.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Technicien' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DateTime dateInstall = dateTimePicker1.Value;
                string query = "SELECT ID_MAT FROM MATERIEL WHERE Nom = @Noom";
                string AddInterSql = "INSERT INTO Intervention (Date_Inter, Commentaire, Technicien, ID_MAT, Validate) VALUES (@dateInter, @Com, @Tech, @IdMat, 0)";
                bd.Open();
                int idmat = 0;
                SqlCommand command = new SqlCommand(query, bd);
                command.Parameters.AddWithValue("@Noom", comboBox2.Text);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idmat = reader.GetInt32(0);
                }
                reader.Close();
                SqlCommand cmd = new SqlCommand(AddInterSql, bd);
                cmd.Parameters.AddWithValue("@dateInter", dateInstall);
                cmd.Parameters.AddWithValue("@Com", textBox3.Text);
                cmd.Parameters.AddWithValue("@Tech", textBox1.Text);
                cmd.Parameters.AddWithValue("@IdMat", idmat);
                cmd.ExecuteNonQuery();
                bd.Close();
                Form interventionForm = Application.OpenForms["Intervention"];
                if (interventionForm != null)
                {
                    DataGridView dataGridView = interventionForm.Controls.Find("dtv", true).FirstOrDefault() as DataGridView;
                    if (dataGridView != null)
                    {
                        string interventionSql = "select m.ID_Inter as 'Intervention n°', m.Date_Inter, m.Commentaire, m.Technicien, c.Nom as 'Matériel', m.Validate from Intervention m join MATERIEL c on m.ID_MAT = c.ID_MAT WHERE Validate = 0 ";
                        SqlDataAdapter adapter = new SqlDataAdapter(interventionSql, bd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView.DataSource = table;
                    }
                    else
                    {
                        string interventionSql2 = "select m.ID_Inter as 'Intervention n°', m.Date_Inter, m.Commentaire, m.Technicien, c.Nom as 'Matériel', m.Validate from Intervention m join MATERIEL c on m.ID_MAT = c.ID_MAT WHERE Validate = 0 ";
                        SqlDataAdapter adapter2 = new SqlDataAdapter(interventionSql2, bd);
                        DataTable table2 = new DataTable();
                        adapter2.Fill(table2);
                        Intervention.InterData.dtv.DataSource = table2;
                    }
                }
                this.Notif("Intervention programée !", NotifSuccess.enmType.Add);
                this.Close();
            }
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
