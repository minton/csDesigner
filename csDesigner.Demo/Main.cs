using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace csDesigner.Demo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var designer = new Designer();
            designer.RootComponent.Text = "My Test Root";
            designer.RootComponent.BackColor = Color.Magenta;
            designer.DesignerView.Text = "My Test View";
            designer.DesignerView.Dock = DockStyle.Fill;
            designer.DesignerView.Parent = this;
        }
    }
}
