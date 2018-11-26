using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsersCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int startuser = Convert.ToInt32(numericUpDown2.Value);
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                string currpass = Program.generate();
                Program.CreateLocalWindowsAccount($"{textBox2.Text + startuser}", currpass, $"{textBox2.Text + startuser}", $"{textBox2.Text + startuser}", true, true);
                textBox1.AppendText($"{textBox2.Text + startuser} {currpass}{System.Environment.NewLine}");
                startuser++;
                
            }
        }
    }
}
