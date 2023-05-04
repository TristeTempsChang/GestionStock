using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionStock
{
    public partial class General : UserControl
    {
        public static General generalData;
        public TextBox txt;
        public TextBox txtt;
        public General()
        {
            InitializeComponent();
            generalData = this;
            txt = txtNom;
            txtt = txtMarque;
        }
    }
}
