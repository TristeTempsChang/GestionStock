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
using System.Data.SqlClient;
using System.Globalization;

namespace GestionStock
{
    public partial class ModifyProduct : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public ModifyProduct()
        {
            InitializeComponent();
            string numberMat = "SELECT * FROM MATERIEL WHERE Nom = @Nom";
            string noserie = "SELECT * FROM MATERIEL WHERE Noserie = @Noserie";
            string dateInstall = "SELECT * FROM MATERIEL WHERE Date_Install = @DateInstall";
            string mtbf = "SELECT * FROM MATERIEL WHERE MTBF = @Mtbf";
            string marque = "SELECT * FROM MATERIEL WHERE Marque = @Marque";
            string type = "SELECT * FROM MATERIEL WHERE Type = @Type";
            int rowIndex = Products.productData.dtv.CurrentCell.RowIndex;
            object id = Products.productData.dtv.Rows[rowIndex].Cells[0].Value;
            object Noserie = Products.productData.dtv.Rows[rowIndex].Cells[1].Value;
            object DateInstall = Products.productData.dtv.Rows[rowIndex].Cells[2].Value;
            object MTBF = Products.productData.dtv.Rows[rowIndex].Cells[4].Value;
            object Marque = Products.productData.dtv.Rows[rowIndex].Cells[3].Value;
            object Type = Products.productData.dtv.Rows[rowIndex].Cells[5].Value;
            bd.Open();
            using (SqlCommand cmd = new SqlCommand(numberMat, bd))
            {
                cmd.Parameters.AddWithValue("@Nom", id);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        label1.Text = dr.GetValue(0).ToString();
                        txtNom.Text = Convert.ToString(dr.GetValue(1));
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(noserie, bd))
            {
                cmd.Parameters.AddWithValue("@Noserie", Noserie);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        textBox1.Text = dr.GetValue(2).ToString();
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(dateInstall, bd))
            {
                cmd.Parameters.AddWithValue("@DateInstall", DateInstall);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DateTime date = dr.GetDateTime(3);
                        dateTimePicker1.Value = date; // affecter la valeur convertie au dateTimePicker
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(mtbf, bd))
            {
                cmd.Parameters.AddWithValue("@Mtbf", MTBF);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        decimal mtbfValue = Convert.ToDecimal(dr.GetValue(4));
                        mtbfValue = Math.Min(mtbfValue, numericUpDown1.Maximum);
                        numericUpDown1.Value = mtbfValue;
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(marque, bd))
            {
                cmd.Parameters.AddWithValue("@Marque", Marque);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        txtMarque.Text = dr.GetValue(5).ToString();
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(type, bd))
            {
                cmd.Parameters.AddWithValue("@Type", Type);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        textBox3.Text = dr.GetValue(6).ToString();
                    }
                }
            }
            bd.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           int rowIndex = Products.productData.dtv.CurrentCell.RowIndex;
            object id = Products.productData.dtv.Rows[rowIndex].Cells[0].Value;
            bd.Open();
            SqlCommand cmd = new SqlCommand("Update MATERIEL set Nom =@nom , Noserie=@noserie, Date_Install=@dateInstall, MTBF=@mtbf, Marque = @marque, Type = @type where Nom=@ID", bd);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@nom", txtNom.Text);
            cmd.Parameters.AddWithValue("@noserie", textBox1.Text);
            cmd.Parameters.AddWithValue("@dateInstall", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@mtbf", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@marque", txtMarque.Text);
            cmd.Parameters.AddWithValue("@type", textBox3.Text);
            cmd.ExecuteNonQuery();
            bd.Close();
            string selectQuery = "select m.Nom, m.Noserie, m.Date_Install, m.Marque, m.MTBF, m.Type, c.Nom as 'Client' from MATERIEL m join Client c on m.ID_Client = c.ID_Client";
            SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, bd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Products.productData.dtv.DataSource = table;
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
