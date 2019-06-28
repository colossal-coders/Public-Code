using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
/*Application:Snake Game/With 2 player option(  wsad )
 * Developer: Aaron Staight...
 * W.I.P: Just developing the second snake to detect gameover and boundaries correctly
 */

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private List<Circle> Snake2P = new List<Circle>();//
        private Circle food = new Circle();
        static bool Eat2P;
        static bool Died2P;
        private SoundPlayer _soundPlayer;

        public Form1()   
            /*You left off after finishing seperate controls, now need to*******
                                                         ******implement boundary and gameover correctly on 2P game mode */
        {
            InitializeComponent();

            //Set settings to default
            new Settings();

            //Initalsie my sound files
            //_soundPlayer = new SoundPlayer("introSound.wav");

            //Set game speed and start timer
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //Start New game
            if (Settings.Play2PMode == true)
            { StartGame2P();
                
            }
            else
                StartGame();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;

            //Set settings to default
            new Settings();

            //Create new player object
            Snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);


            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

        }

        private void StartGame2P()
        {
            lblGameOver.Visible = false;

            //Set settings to default
            new Settings();

            //Create new player object
            Snake.Clear();
            Snake2P.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            Circle head2P = new Circle { X = 20, Y = 10 };
            Snake.Add(head);
            Snake2P.Add(head2P);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

        }


        private void GenerateFood()//Place random food object
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
            
        }


        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for Game Over
            if (Settings.GameOver)
            {
                //Check if Enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    if (Settings.Play2PMode == false)
                        StartGame();
                    else
                        StartGame2P();
                }
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;



                if (Settings.Play2PMode == true)
                {
                    if (Input.KeyPressed(Keys.D) && Settings.direction2P != Direction2P.Left)
                        Settings.direction2P = Direction2P.Right;
                    else if (Input.KeyPressed(Keys.A) && Settings.direction2P != Direction2P.Right)
                        Settings.direction2P = Direction2P.Left;
                    else if (Input.KeyPressed(Keys.W) && Settings.direction2P != Direction2P.Down)
                        Settings.direction2P = Direction2P.Up;
                    else if (Input.KeyPressed(Keys.S) && Settings.direction2P != Direction2P.Up)
                        Settings.direction2P = Direction2P.Down;
                    MovePlayer2P();
                }
                else
                    MovePlayer();
            }

            pbCanvas.Invalidate();

        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
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
                            snakeColour = Brushes.Black;     //Draw head
                        else
                            snakeColour = Brushes.Green;    //Rest of body

                        //Draw snake
                        canvas.FillEllipse(snakeColour,
                            new Rectangle(Snake[i].X * Settings.Width,
                                          Snake[i].Y * Settings.Height,
                                          Settings.Width, Settings.Height));
                        if (Settings.Play2PMode == true) 
                        {
                            for (int t = 0; t < Snake2P.Count; t++)
                            {
                                Brush snake2PColour;
                                if (t == 0)
                                    snake2PColour = Brushes.Red;     //Draw head
                                else
                                    snake2PColour = Brushes.Orange;    //Rest of body

                                canvas.FillEllipse(snake2PColour,
                            new Rectangle(Snake2P[t].X * Settings.Width,
                                          Snake2P[t].Y * Settings.Height,
                                          Settings.Width, Settings.Height));
                            }

                        }


                        //Draw Food
                        canvas.FillEllipse(Brushes.Purple,
                            new Rectangle(food.X * Settings.Width,
                                 food.Y * Settings.Height, Settings.Width, Settings.Height));

                    }
                }
                else
                {
                if (!Died2P == true)
                {
                    string gameOver = "Game over \nPlayer one died!\nYour final score is: " + Snake.Count + "\nPress Enter to try again";
                    lblGameOver.Text = gameOver;
                    lblGameOver.Visible = true;
                }
                else
                {
                    string gameOver = "Game over \nPlayer 1 died\nYour final score is: " + Snake2P.Count + "\nPress Enter to try again";
                    lblGameOver.Text = gameOver;
                    lblGameOver.Visible = true;
                }

                }
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

                    //The last thing i remember doing is trying to add movement to player 2//

                   


                    //Get maximum X and Y Pos
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;

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
        private void MovePlayer2P()
        {
            for (int t = Snake2P.Count - 1; t >= 0; t--)
            {
                if (t == 0)
                {

                    switch (Settings.direction2P)
                    {
                        case Direction2P.Right:
                            Snake2P[t].X++;
                            break;
                        case Direction2P.Left:
                            Snake2P[t].X--;
                            break;
                        case Direction2P.Up:
                            Snake2P[t].Y--;
                            break;
                        case Direction2P.Down:
                            Snake2P[t].Y++;
                            break;

                    }

                    //Get maximum X and Y Pos
                    int maxXPos2P = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos2P = pbCanvas.Size.Height / Settings.Height;

                    //Detect collission with game borders.
                    if (Snake2P[t].X < 0 || Snake2P[t].Y < 0
                        || Snake2P[t].X >= maxXPos2P || Snake2P[t].Y >= maxYPos2P)
                    {
                        Die2P();
                    }

                    //Body collision
                    for (int j = 1; j < Snake2P.Count; j++)
                    {
                        if (Snake2P[t].X == Snake2P[j].X &&
                           Snake2P[t].Y == Snake2P[j].Y)
                        {
                            Die2P();
                        }
                    }
                    //Detect collision with food piece
                    if (Snake2P[0].X == food.X && Snake2P[0].Y == food.Y)
                    {
                        Eat2P = true;
                        Eat();
                    }
                }
                else
                {
                    //Move body
                    Snake2P[t].X = Snake2P[t - 1].X;
                    Snake2P[t].Y = Snake2P[t - 1].Y;
                }
            }
            for (int i = Snake.Count - 1; i >= 0; i--)
            {

                



                    //Move head
                    if (i  == 0 )
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

                        //The last thing i remember doing is trying to add movement to player 2//

                        


                    

                        //Get maximum X and Y Pos
                        int maxXPos = pbCanvas.Size.Width / Settings.Width;
                        int maxYPos = pbCanvas.Size.Height / Settings.Height;

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
                            Eat2P = false;
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Eat()
        {
            //Add circle to body
            if(Eat2P==false)
            { 
            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            //Update Score
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
            }
            //Add circle to body
            if (Eat2P == true)
            {
                Circle circle2P = new Circle
                {
                    X = Snake2P[Snake2P.Count - 1].X,
                    Y = Snake2P[Snake2P.Count - 1].Y
                };
                Snake2P.Add(circle2P);

                //Update Score
                Settings.Score += Settings.Points;
                lblScore.Text = Settings.Score.ToString();
                Eat2P = false;
                GenerateFood();
            }
        }

        private void Die()
        {
            Settings.GameOver = true;
        }
        private void Die2P()
        {
            Died2P = true;
            Settings.GameOver = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}        

   