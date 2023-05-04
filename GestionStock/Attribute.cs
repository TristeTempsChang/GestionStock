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
    public partial class Attribute : UserControl
    {
        public static Attribute attributeData;
        public ComboBox cbb;
        public TextBox tbx;
        public NumericUpDown nud;
        public TextBox tbxxx;
        public DateTimePicker dtp;
        SqlConnection bd = new SqlConnection(@"Data Source=.\SQLEXPRESS;database=projetPPE;Integrated Security=True");
        public Attribute()
        {
            InitializeComponent();
            attributeData = this;
            cbb = comboBox2;
            tbx = textBox1;
            nud = numericUpDown1;
            tbxxx= textBox3;
            dtp = dateTimePicker1;
        }

        private void Attribute_Load(object sender, EventArgs e)
        {
            string MaterielSql = "select ID_Client, Nom from Client";
            SqlDataAdapter sqlDa = new SqlDataAdapter(MaterielSql, bd);
            DataTable dttb = new DataTable();
            sqlDa.Fill(dttb);
            DataRow itemrow = dttb.NewRow();
            itemrow[1] = "Sélectionnez un client";
            dttb.Rows.InsertAt(itemrow, 0);
            comboBox2.DataSource = dttb;
            comboBox2.DisplayMember = "Nom";
            comboBox2.ValueMember = "ID_Client";
        }
    }
}
