using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace threadlab5
{
    public partial class Jacket : Form
    {
        public Jacket()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Glasses g = new Glasses();
            g.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Helmen h = new Helmen();
            h.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gloves g = new Gloves();
            g.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
