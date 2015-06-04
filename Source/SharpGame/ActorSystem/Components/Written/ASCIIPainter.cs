using System;
using System.Diagnostics;

namespace SharpGame
{
    public class ASCIIPainter : ActorComponent
    {
        
        public char [,] Map;
        public ConsoleColor[,] Color;

        public override void Start()
        {
            Initialize();
        }
        public override void Update(float deltaTime)
        {
            ActorToBuffer();
        }
        
        public override void OnDestroy() {/*do i need to delete the reference to array here?*/}

        public bool IsInBounds (int x,int y)
        {
            return x >= 0 && y >= 0 && x < Game.Resolution.x && y < Game.Resolution.y;
        }
        public void Initialize()
        {
            ConsoleColor Blue, Red, White, Black, Green, Yellow, DarkRed;
            Blue = ConsoleColor.Blue;
            Red = ConsoleColor.Red;
            White = ConsoleColor.White;
            Black = ConsoleColor.Black;
            Green = ConsoleColor.Green;
            Yellow = ConsoleColor.Yellow;
            DarkRed = ConsoleColor.DarkRed;
            switch (Actor.Name)
            {
                #region Pong
                case "Racket":
                    {
                        Map = new char[5, 1]
                        {   {'▒'},
                            {'▒'},
                            {'▒'},
                            {'▒'},
                            {'▒'}};
                        Color = new ConsoleColor[5, 1]
                        {   {Blue},
                            {Blue},
                            {Blue},
                            {Blue},
                            {Blue}};
                        break;
                    }
                case "Ball":
                    {
                        Map = new char[1, 1] { { '☻' } };
                        Color = new ConsoleColor[1, 1] { { Blue } };
                        break;
                    }
                case "Field":
                    {
                       /* Map = new char[1,Game.Resolution.x];
                        Color = new ConsoleColor[1, Game.Resolution.x] { { ConsoleColor.Black } };
                        for (int X = 0; X < Game.Resolution.x; X++)
                        {
                            Map[X,0]='█';
                            Color[X,0]=Black;
                        }*/
                            break;
                    }
                case "ScoreCounterLeft":
                    {
                        Map = new char[1,1]{{'0'}};
                        break;
                    }
                #endregion
                #region Space Invaders
                case "Ship":
                    {
                        Map = new char[2,5]{{' ','▒','▒','▒',' '}
                                           ,{'▒','▒','▒','▒','▒'}};
                        Color = new ConsoleColor[2, 5] { {White,Blue,Black,Blue,White }, 
                                                         {Blue,Blue,Blue, Blue,Blue } };
                        break;
                    }
                case "Projectile":
                    {
                        Map = new char[1, 1] { { '│' } };
                        Color = new ConsoleColor [1,1] { { Red } };
                        break;
                    }
                case "Shattle1":
                    {
                        Map =           new char[1, 1] { { '▼' } };
                        Color = new ConsoleColor[1, 1] { { DarkRed } };
                        break;
                    }
                case "Shattle2":
                    {
                        Map =           new char[1, 1] { { '▼' } };
                        Color = new ConsoleColor[1, 1] { { Green } };
                        break;
                    }
                case "Shattle3":
                    {
                        Map = new char[1, 1] { { '▼' } };
                        Color = new ConsoleColor[1, 1] { { Yellow } };
                        break;
                    }
                case "Shattle4":
                    {
                        Map = new char[1, 1] { { '▼' } };
                        Color = new ConsoleColor[1, 1] { { Yellow } };
                        break;
                    }
                case "Shattle5":
                    {
                        Map = new char[1, 1] { { '▼' } };
                        Color = new ConsoleColor[1, 1] { { Yellow } };
                        break;
                    }

                #endregion
                default:
                    {
                        Debug.Assert (false,
                        "Actor does not contains component which is drawn. Empty figure will be drawn");
                        Map = new char[1, 1] { { ' ' } };
                        Color = new ConsoleColor[1, 1] { { Black } };
                        break;
                    }
            } /*should be serialized and deleted*/
        }
        public void ActorToBuffer()
        {
            int y = (int)Actor.WorldPosition.y - (Map.GetLength(0) / 2);
            for (int i = 0; i < Map.GetLength(0); y++, i++)
            {
                int x = (int)Actor.WorldPosition.x - (Map.GetLength(1) / 2);
                for (int j = 0; j < Map.GetLength(1); x++, j++)
                {
                    if (IsInBounds(x, y))
                    {
                        Game.Graphics.PointChange(x, y, new GraphicsPrimitive(Map[i, j], (int)Actor.WorldPosition.z, Color[i, j]));
                    }
                }
            }      
        }
    }
}
