using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grdData
{
    public partial class FrmMsg : Form
    {
        //  Property used to share data
        public static string action { get; internal set; }

        public FrmMsg(ref string txtMsg)
        {
            InitializeComponent();

            //  Put messages in place
            lblCell.Text = lblCell.Text + "\"" + txtMsg + "\";" + action;
            lblColumn.Text = lblColumn.Text + "\"" + txtMsg + "\";" + action;
        }

        private void FrmMsg_Load(object sender, EventArgs e) {}
    }
}
