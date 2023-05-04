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
    public partial class AddProduct : Form
    {
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public AddProduct()
        {
            InitializeComponent();
        }

        public void Notif(string msg, NotifSuccess.enmType type)
        {
            NotifSuccess nfs = new NotifSuccess();
            nfs.showNotif(msg, type);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock= DockStyle.Fill;
            panelAddProduct.Controls.Clear();
            panelAddProduct.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void Btn_Click(object sender, EventArgs e )
        {
            foreach(var pnl in tableLayoutPanel1.Controls.OfType<Panel>())
                pnl.BackColor = Color.White;
                Button btn = (Button)sender;
                switch(btn.Name)
                {
                    case "general":
                        addUserControl(new General());
                        panelGeneral.BackColor = Color.OliveDrab;
                        break;
                    case "attribute":
                        addUserControl(new Attribute());
                        panelAttribute.BackColor = Color.OliveDrab;
                        break;
                    default:
                        break;
                }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nomMateriel = General.generalData.txt.Text;
            string noSerie = Attribute.attributeData.tbx.Text;
            DateTime dateInstall = Attribute.attributeData.dtp.Value;
            int mtbf = Convert.ToInt32(Attribute.attributeData.nud.Value);
            string marque = General.generalData.txtt.Text;
            string type = Attribute.attributeData.tbxxx.Text;
            string nomClient = Attribute.attributeData.cbb.Text;
            if (General.generalData.txt.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Nom du produit' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (General.generalData.txtt.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Marque' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Attribute.attributeData.tbx.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Numéro de série' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Attribute.attributeData.tbxxx.Text == "")
            {
                MyMessageBox.showMessage("Veuillez remplir le champ 'Type de matériel' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Convert.ToInt32(Attribute.attributeData.nud.Value) == 0)
            {
                MyMessageBox.showMessage("Veuillez sélectionner une valeur autre que 0 dans le champ"+"\n"+"'MTBF' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Attribute.attributeData.cbb.SelectedIndex == 0)
            {
                MyMessageBox.showMessage("Veuillez sélectionner un client dans le champ"+"\n"+" 'Client' avant de continuer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string query = "SELECT ID_Client FROM Client WHERE Nom = @Noom";
                string AddClientSql = "INSERT INTO MATERIEL (Nom, Noserie, Date_Install, MTBF, Marque, Type, ID_Client) VALUES (@Nom, @NoSerie, @DateInstall, @MTBF, @Marque, @Type, @IDClient)";
                bd.Open();
                int idClient = 0;
                SqlCommand command = new SqlCommand(query, bd);
                command.Parameters.AddWithValue("@Noom", nomClient);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idClient = reader.GetInt32(0);
                }
                reader.Close();
                SqlCommand cmd = new SqlCommand(AddClientSql, bd);
                cmd.Parameters.AddWithValue("@Nom", nomMateriel);
                cmd.Parameters.AddWithValue("@NoSerie", noSerie);
                cmd.Parameters.AddWithValue("@DateInstall", dateInstall);
                cmd.Parameters.AddWithValue("@MTBF", mtbf);
                cmd.Parameters.AddWithValue("@Marque", marque);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@IDClient", idClient);
                cmd.ExecuteNonQuery();
                bd.Close();
                string selectQuery = "select m.Nom, m.Noserie as 'N°série', m.Date_Install, m.Marque, m.MTBF, m.Type, c.Nom as 'Client' from MATERIEL m join Client c on m.ID_Client = c.ID_Client";
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, bd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                Products.productData.dtv.DataSource = table;
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
