using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    class Piece:ActorComponent
    {
        private char Design;
        private ConsoleColor pieceColor;
        private ConsoleKey Left, Right, Turn, Drop, Down;
        private int speed;

        public override void Awake()
        {
            Design = 'O';
            speed = 3;
            SetStandartControls();
        }
        public override void Start()
        {
            Actor.Name = this.GetType().Name;
            RandomPieceSet();placeOnStart();
        }
        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(Left) && Actor.WorldPosition.x > 0)
                MoveLeft(deltaTime * speed);
            if (Input.IsKeyDown(Right) && Actor.WorldPosition.y < Game.Resolution.y - 1)
                MoveRight(deltaTime * speed);
            if (Input.IsKeyDown(Turn))
                TurnPiece(speed*deltaTime);

            MoveDown((speed/2)*deltaTime);  
        }
        public override void OnCollide(Actor Exciter)
        {
            switch (Exciter.Name)
            {
                case "Stack":
                    /*this piece is copied to stack and:   */
                    RandomPieceSet();
                    placeOnStart();
                    break;
                case "GameInterface":
                    /*like rigidbody*/
                    break;
                default:
                    break;
            }
        }
        public void RandomPieceSet()
        {
            Random random=new Random();
            switch (random.Next (1, 7))
            {
                case 1:
                    Design = 'I';
                    pieceColor = ConsoleColor.Red;
                    break;
                case 2:
                    Design = 'L';
                    pieceColor = ConsoleColor.DarkCyan;
                    break;
                case 3:
                    Design = 'J';
                    pieceColor = ConsoleColor.Cyan;
                    break;
                case 4:
                    Design = 'S';
                    pieceColor = ConsoleColor.Magenta;
                    break;
                case 5:
                    Design = 'Z';
                    pieceColor = ConsoleColor.DarkMagenta;
                    break;
                case 6:
                    Design = 'T';
                    pieceColor = ConsoleColor.Yellow;
                    break;    
                case 7:
                    Design = 'O';
                    pieceColor = ConsoleColor.DarkRed;
                    break;
                default: break;
            }
        }
        public void placeOnStart ()
        {
            Actor.WorldPosition = new Vector3(6, 0, 0);
        }
        public void MoveLeft(float L)
        {
            Actor.WorldPosition = Actor.WorldPosition.Move(0, -L, 0);
        }
        public void MoveRight(float L)
        {
            Actor.WorldPosition = Actor.WorldPosition.Move(0, L, 0);
        }
        public void MoveDown(float L)
        {
            Actor.WorldPosition = Actor.WorldPosition.Move(L, 0, 0);
        }
        public void TurnPiece(float L)
        {

        }
        public void SetStandartControls()
        {
            Right = ConsoleKey.RightArrow;
            Left = ConsoleKey.LeftArrow;
            Down = ConsoleKey.DownArrow;
            Turn = ConsoleKey.UpArrow;
            Drop = ConsoleKey.Spacebar;
        }
    }
}
