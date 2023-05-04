using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock
{
    public static class MyMessageBox
    {
        public static System.Windows.Forms.DialogResult showMessage(string message, string caption, System.Windows.Forms.MessageBoxButtons button, System.Windows.Forms.MessageBoxIcon icon) 
        {
            System.Windows.Forms.DialogResult dlgResult = System.Windows.Forms.DialogResult.None;
            switch (button)
            {
                case System.Windows.Forms.MessageBoxButtons.OK:
                    using(messageBox msgOk = new messageBox())
                    {
                        msgOk.Text = caption;
                        msgOk.Message = message;
                        switch (icon)
                        {
                            case System.Windows.Forms.MessageBoxIcon.Information:
                                msgOk.MessageIcon = GestionStock.Properties.Resources.icons8_cancel_48px;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Question:
                                msgOk.MessageIcon = GestionStock.Properties.Resources.icons8_info_48px;
                                break;
                        }
                        dlgResult = msgOk.ShowDialog();
                    }
                    break;
                case System.Windows.Forms.MessageBoxButtons.YesNo:
                    using (messageBoxYesNo msgYesNo = new messageBoxYesNo())
                    {
                        msgYesNo.Text = caption;
                        msgYesNo.Message = message;
                        switch (icon)
                        {
                            case System.Windows.Forms.MessageBoxIcon.Information:
                                msgYesNo.MessageIcon = GestionStock.Properties.Resources.icons8_cancel_48px;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Question:
                                msgYesNo.MessageIcon = GestionStock.Properties.Resources.icons8_info_48px;
                                break;
                        }
                        dlgResult = msgYesNo.ShowDialog();
                    }
                    break;
                case System.Windows.Forms.MessageBoxButtons.YesNoCancel:
                    using (messageBoxYesNoMTBF msgMTBF = new messageBoxYesNoMTBF())
                    {
                        msgMTBF.Text = caption;
                        msgMTBF.Message = message;
                        switch (icon)
                        {
                            case System.Windows.Forms.MessageBoxIcon.Information:
                                msgMTBF.MessageIcon = GestionStock.Properties.Resources.icons8_cancel_48px;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Question:
                                msgMTBF.MessageIcon = GestionStock.Properties.Resources.icons8_info_48px;
                                break;
                        }
                        dlgResult = msgMTBF.ShowDialog();
                    }
                    break;
            }
            return dlgResult;
        }
    }
}
