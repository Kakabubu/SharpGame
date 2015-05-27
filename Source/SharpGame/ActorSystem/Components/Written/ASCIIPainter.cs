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
            switch (Actor.Name)
            {
                case "Racket":
                    {
                        Map = new char[5, 3]
                        {
                            {' ','▒',' '},
                            {' ','▒',' '},
                            {' ','▒',' '},
                            {' ','▒',' '},
                            {' ','▒',' '}
                        };
                        Color = new ConsoleColor[5, 3]
                        {
                            {ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Black},
                            {ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Black},
                            {ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Black},
                            {ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Black},
                            {ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Black}
                        };
                        break;
                    }
                case "Ball":
                    {
                        Map = new char[1, 1] { { '☻' } };
                        Color = new ConsoleColor[1, 1] { { ConsoleColor.Blue } };
                        break;
                    }
                case "Field":
                    {
                        Map = new char[1,Game.Resolution.x];
                       /* Color = new ConsoleColor[1, 1] { { ConsoleColor.Black } };
                        for (int X = 0; X < Game.Resolution.x; X++)
                            Map[X,0]=new GraphicsPrimitive(█,0,ConsoleColor.Black);*/
                            break;
                    }
                case "ScoreCounterLeft":
                    {
                        Map = new char[1,1]{{'0'}};
                        break;
                    }
                default:
                    {
                        Debug.Assert (false,
                        "Actor does not contains component which is drawn. Empty figure will be drawn");
                        Map = new char[1, 1] { { ' ' } };
                        Color = new ConsoleColor[1, 1] { { ConsoleColor.Black } };
                        break;
                    }
            } /*Think it shoud be somewhere in extended libruary*/
        }
        public void ActorToBuffer()
        {
            int y = (int)Actor.LocalPosition.y - (Map.GetLength(0) / 2);
            for (int i = 0; i < Map.GetLength(0); y++, i++)
            {
                int x = (int)Actor.LocalPosition.x - (Map.GetLength(1) / 2);
                for (int j = 0; j < Map.GetLength(1); x++, j++)
                {
                    if (IsInBounds(x, y))
                    {
                        Game.Graphics.PointChange(x, y, new GraphicsPrimitive(Map[i, j], (int)Actor.LocalPosition.z, Color[i, j]));
                    }
                }
            }      
        }
    }
}
