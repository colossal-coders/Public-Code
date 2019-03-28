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
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        Thread th;


        public Form1()
        {
            InitializeComponent();

            

           //SET SETTINGS TO DEFAULT
            new Settings();

            HighScore.Text = Settings.HighScore.ToString();

            //SET GAME SPEED AND START TIMER
            GameTimer.Interval = 1000 / Settings.Speed;
            GameTimer.Tick += UpdateScreen;
            GameTimer.Start();
            //START NEW GAME
            StartGame();



        }
        
        private void StartForm()
        {
            Application.Run(new Start());
        }

       

        private void StartGame()
        {
            if (Start.DiffCount==1)
            {

            }
            label2.Visible = false;

            //SET SETTINGS TO DEFAULT

            new Settings();
            //CREATE NEW SNAKE
            Snake.Clear();
            Circle head = new Circle { X=10, Y = 5};
            
            Snake.Add(head);


            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

            




        }
        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                //Move head
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }


                    //Get maximum X and Y Pos
                    int maxXPos = PBcanvas.Size.Width / Settings.Width;
                    int maxYPos = PBcanvas.Size.Height / Settings.Height;

                    //Detect collission with game borders.
                    if (Snake[i].X < 0 || Snake[i].Y < 0
                        || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
                    {
                        Die();
                    }


                    //Detect collission with body
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X &&
                           Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    //Detect collision with food piece
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }

                }
                else
                {
                    //Move body
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
        private void Eat()
        {
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);

            //UPDATE SCORE
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();
            //if (Settings.HighScore<=Settings.Score)
            HighScore.Text = Settings.HighScore.ToString();
            if (Settings.Score >= Settings.HighScore)
            { Settings.HighScore = Settings.Score; }  //TRYING TO AMMEND SCORE// 
            
            GenerateFood();
        }
        private void Die()
        {
            label2.Visible = true;
            Settings.GameOver = true;
        }

        private void GenerateFood()
        {
            int maxXPos = PBcanvas.Size.Width / Settings.Width;
            int maxYPos = PBcanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();
            food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for Game Over
            if (Settings.GameOver)
            {
                //Check if Enter is pressed
                if (INPUTS.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (INPUTS.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (INPUTS.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (INPUTS.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (INPUTS.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;

                MovePlayer();
            }

            PBcanvas.Invalidate();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PBcanvas_Enter(object sender, EventArgs e)
        {

        }

        
           
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            INPUTS.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            INPUTS.ChangeState(e.KeyCode, false);
        }

        private void lblScore_Enter(object sender, EventArgs e)
        {

        }

        private void PBcanvas_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                //Set colour of snake

                //Draw snake
                for (int i = 0; i < Snake.Count; i++)
                {
                    Brush snakeColour;
                    if (i == 0)
                        snakeColour = Brushes.DeepPink;     //Draw head
                    else
                        snakeColour = Brushes.Green;    //Rest of body

                    //Draw snake
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));


                    //Draw Food
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Width,
                             food.Y * Settings.Height, Settings.Width, Settings.Height));

                }
            }
            else
            {
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                label2.Text = "GAMEOVER";
                label2.Visible = true;
            }
        }

        private void DiffEasy_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Speed = 6; //Diff Settings//
        }

        private void DiffMedium_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Speed = 11; //Diff Settings//
        }

        private void DiffHard_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Speed = 16; //Diff Settings//
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            th = new Thread(StartForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Hide();
        }

        private void HighScore_Click(object sender, EventArgs e)
        {

        }
    }   } 
