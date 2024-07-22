using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace thread1lek
{
    public partial class Form1 : Form
    {

        Thread[] Thread = new Thread[10];
        public Form1()
        {
            InitializeComponent();
        }

        void a()
        {
            for (int i = 0; i <10000; i++)
            {
                this.Invoke((MethodInvoker)delegate () { richTextBox1.AppendText(i + "\n"); });
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Thread[0] = new Thread(a);
            Thread[0].Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Thread[0].Abort();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Thread[0].Suspend();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Thread[0].Resume();
        }
    }
}
