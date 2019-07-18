using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Media;
/*Application:Snake Game/With 2 player option(  wsad )
 * Developer: Aaron Staight...
 * W.I.P:Just developing the second snake to detect gameover and boundaries correctly
 */



namespace Snake
{

    

    public partial class StartScreen : Form
    {
        private SoundPlayer _soundPlayer;

        Thread th;

        public StartScreen()
        {
            InitializeComponent();
            _soundPlayer = new SoundPlayer("C:/Users/aaron/Documents/ProgsC#/Snake2P/introSound.wav");
            _soundPlayer.Play();
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {
            
    }

        private void SnakeForm()
        {
            _soundPlayer.Stop();
            Application.Run(new Form1());
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                Settings.Speed = 6;
            }
            else if (radioButton2.Checked == true)
            {
                Settings.Speed = 9;
            }
            else if (radioButton3.Checked == true)
            {
                Settings.Speed = 13;
            }
            else
            { Settings.Speed = 6; }

            if (checkBox1.Checked == true)
            {
                Settings.Play2PMode = true;
            }
            else
                Settings.Play2PMode = false;

            //THEN...

            th = new Thread(SnakeForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Hide();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }

}
