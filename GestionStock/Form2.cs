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
    public partial class Dashboard : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public Dashboard()
        {
            InitializeComponent();
            /*Partie affichage des nombres dans les cases*/

            bd.Open();
            string cmdSql = "SELECT COUNT(*) FROM Client";
            string cmdSql2 = "SELECT COUNT(*) FROM Utilisateur";
            string cmdSql3 = "SELECT COUNT(*) FROM MATERIEL";
            using (SqlCommand cmd = new SqlCommand(cmdSql, bd))
            {
                
                int count = (int)cmd.ExecuteScalar();
                
                if (count != 0)
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            label7.Text = dr.GetValue(0).ToString();
                        }
                    }

                    using (SqlCommand cmd2 = new SqlCommand(cmdSql2, bd))
                    {
                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                label5.Text = dr2.GetValue(0).ToString();
                            }
                        }
                    }
                    using (SqlCommand cmd3 = new SqlCommand(cmdSql3, bd))
                    {
                        using (SqlDataReader dr3 = cmd3.ExecuteReader())
                        {
                            while (dr3.Read())
                            {
                                label6.Text = dr3.GetValue(0).ToString();
                            }
                        }
                    }
                }
                else
                {
                    label7.Text = "0";
                    label5.Text = "0";
                    label6.Text = "0";
                }
            }
            bd.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            /*Partie sql pour les charts*/
            bd.Open();
            string validateSql = "SELECT CASE WHEN (SELECT count(*) FROM Intervention where Validate = 1) = 0 THEN 0 ELSE COUNT(*)*100/(SELECT count(*) FROM Intervention where Validate = 1)  END as result FROM Intervention";
            string nonValidateSql = "SELECT  CASE  WHEN (SELECT count(*) FROM Intervention where Validate = 0) = 0 THEN 0 ELSE COUNT(*)*100/(SELECT count(*) FROM Intervention where Validate = 0) END as result FROM Intervention";
            SqlCommand validateCmd = new SqlCommand(validateSql, bd);
            SqlCommand nonValidateCmd = new SqlCommand(nonValidateSql, bd);
            int count = (int)validateCmd.ExecuteScalar();
          
            int count2 = (int)nonValidateCmd.ExecuteScalar();

            if (count != 0 && count2 != 0)
            {
                SqlDataReader dr = validateCmd.ExecuteReader();
                dr.Read();
                chart2.Series["Series1"].Points.AddXY("Intervention validées", dr.GetInt32(0).ToString());
                dr.Close();
                dr = nonValidateCmd.ExecuteReader();
                dr.Read();
                chart2.Series["Series1"].Points.AddXY("Intervention non validées", dr.GetInt32(0).ToString());
                dr.Close();
            }
            else
            {
                chart2.Series["Series1"].Points.AddXY("Intervention validées", 0);
                chart2.Series["Series1"].Points.AddXY("Intervention non validées", 0);
            }
            string query = "SELECT Client.Nom, COUNT(Intervention.ID_Inter) AS NbInterventions FROM Intervention " +
                           "INNER JOIN Materiel ON Intervention.ID_MAT = Materiel.ID_MAT " +
                           "INNER JOIN Client ON Materiel.ID_Client = Client.ID_Client " +
                           "GROUP BY Client.Nom";
            using (SqlCommand cmd = new SqlCommand(query, bd))
            {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nomClient = reader["Nom"].ToString();
                            int nbInterventions = Convert.ToInt32(reader["NbInterventions"]);

                            chart1.Series["Clients"].Points.AddXY(nomClient, nbInterventions);
                        }
                        reader.Close();
                    }
            }


            ///////////////////////////////////////////////////////////////////////////////

            /*Partie du datagrid avec toutes les interventions validées*/

            string interventionSql = "select m.ID_Inter as 'Intervention n°', m.Date_Inter, m.Commentaire, m.Technicien, c.Nom as 'Matériel', m.Validate from Intervention m join MATERIEL c on m.ID_MAT = c.ID_MAT WHERE Validate = 1 ";
            SqlDataAdapter sqlDa = new SqlDataAdapter(interventionSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.Fill(dttb);
            dataGridView1.DataSource = dttb;
        }
    }
}
