using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MC.Selenium.DSL.GUI
{
    public partial class frm : Form
    {
        public frm()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            MC.Selenium.DSL.Runner.Model.Runner r = new Runner.Model.Runner(new ConsoleTestEventObserver());

     
            r.Execute(txtInput.Text);
        }

    }
}
