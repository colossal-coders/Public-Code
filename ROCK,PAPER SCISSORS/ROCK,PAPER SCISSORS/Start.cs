using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ROCK_PAPER_SCISSORS
{
    public partial class Start : Form
    {
        Thread th; //THREAD POINT//

        public static byte DiffCount;
        

        public Start()
        {
            InitializeComponent();

            if (INPUTS.KeyPressed(Keys.Enter))
            {

                
            }
        }

        private void Start_Load(object sender, EventArgs e)
        {
            new Settings();

        }
        private void StartForm1(object obj)
        {
            Application.Run(new Form1()); 
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DiffLabel1.Text = ("Difficulty:Easy");
            Settings.Speed = 6; //Diff Settings//
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DiffLabel1.Text = ("Difficulty:Medium");
            Settings.Speed = 11; //Diff Settings//
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            DiffLabel1.Text = ("Difficulty:Hard");
            Settings.Speed = 16; //Diff Settings//
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                DiffCount = 1;
                Settings.Speed = 6;
            } else if (radioButton2.Checked==true)
            {
                DiffCount = 2;
                Settings.Speed = 11;
            } else if(radioButton3.Checked==true)
            {
                DiffCount = 3;
                Settings.Speed = 16;
            }

                th = new Thread(StartForm1);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();
        }
    }
}
