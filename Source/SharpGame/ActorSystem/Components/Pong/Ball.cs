﻿using System;

namespace GameFramework
{
    class Ball : ActorComponent
    {
        private double speed;
        private double Facing;
        private double facing
        {
            get
            {
                return Facing;
            }
            set
            {
                Facing = (((value % 360) + 360) % 360);
            }
        }

        public override void Awake()
        {
            facing = 0;
            speed = 10;
        }
        public override void Start()
        {
            Parent.Name = this.GetType().Name;
            Centralize();    
        }
        public override void Update(float deltaTime)
        {
            MoveForward(deltaTime);
            PrintFacing(Game.Resolution.x / 2, Game.Resolution.y + 2);//just for debuging
        }
        public override void OnDestroy()
        { }


        public void MoveForward(float deltaTime)
        {
            Parent.Position = Parent.Position.Move(facing, speed*deltaTime);
        }
        public override void OnCollide(Actor Exciter)
        {
            switch (Exciter.Name)
            {
                case "Racket":
                    
                    if (Exciter.Position.x > Game.Resolution.x/2)
                    {
                        facing =facing+ 180 - (2 * ((Math.Asin((Parent.Position.y - Exciter.Position.y) / 4f)) / (Math.PI / 180F)) /*- facing % 90*/);
                    }
                    else facing = facing - 180 + (2 * ((Math.Asin((Parent.Position.y - Exciter.Position.y) / 4f)) / (Math.PI / 180F)) /*- facing % 90*/);
                    break;

                #region score
                case "ScoreCounterLeft":
                    Centralize();
                    break;
                case "ScoreCounterRight":
                    Centralize();
                    break;
                #endregion

                case "Field":
                    if (facing % 90 != 0)
                    {
                        if (((facing - facing % 90) / 90) % 2 == 0)
                            facing += (90 - facing % 90) * 2;
                        else facing -= (facing % 90) * 2;
                    }
                    else facing += 180;
                    break;

                default:   
                    break;

            }
        }
        public void Centralize ()
        {
            Parent.Position = new Vector3((Game.Resolution.x - 1) / 2+0.5f, (Game.Resolution.y - 1) / 2, 0);
            facing = 45;
        }
        public void PrintFacing(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("                            ");
            Console.SetCursorPosition(x, y);
            Console.WriteLine(facing);
        }
    }
}