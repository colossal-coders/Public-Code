using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROCK_PAPER_SCISSORS
{

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };


    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static int HighScore { get; set; }
        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }

        public Settings()
        {
            Width = 16;
            Height = 16;

            if (Start.DiffCount==1)
            { Speed = 8; } else if ( Start.DiffCount==2)
            { Speed = 12; } else if (Start.DiffCount == 3)
            { Speed = 16; } //Speed Settings//
            
            Score = 0;
            Points = 100;
            HighScore = 3400; //SET by Aaron Staight@/
            GameOver = false;
            direction = Direction.Right;
            
        }
    }
}
