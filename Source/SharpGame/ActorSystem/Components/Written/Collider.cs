using System;
using System.Collections.Generic;

namespace SharpGame
{
    public class Collider:ActorComponent
    {

        private List<Vector3> ColliderPoints = new List<Vector3>(0);
        public override void Awake()
        { } 
        public override void Start()
        {
            Initialize();
            Place();
        }

        public override void Update(float deltaTime)
        {
            Place();
        }

        public override void OnDestroy()
        { }

        private void Place()
        {
            foreach (Vector3 Deviation in ColliderPoints)
                Game.Physics.Place(Actor.WorldPosition+Deviation, Actor);
        }

        public void AddPoint(Vector3 point)
        {
            this.ColliderPoints.Add(point);
        }

        private void Initialize()
        {
            switch (Actor.Name)
            {
                case "Racket":
                    for (int i = -3; i < 4; i++)
                    {                         
                        AddPoint(new Vector3(0, i, 0));
                    } 
                    break;
                case "Ball":
                    {
                        ColliderPoints.Add(new Vector3(-1, 0, 0)); 
                        ColliderPoints.Add(new Vector3(0, 0, 0)); 
                        ColliderPoints.Add(new Vector3(1, 0, 0));
                        ColliderPoints.Add(new Vector3(0, 1, 0));
                        ColliderPoints.Add(new Vector3(0, -1, 0));
                    }
                    break;

                case "GameInterface":
                    for (int x = 1; x < Game.Resolution.x-1; x++)
                    {
                        AddPoint(new Vector3(x, 1, 0));
                        AddPoint(new Vector3(x, Game.Resolution.y - 1, 0));
                    } 
                    break;

                case "ScoreCounterLeft":
                    for (int y = 0; y < Game.Resolution.y; y++)
                    {
                        AddPoint(new Vector3(0, y, 0));
                    }
                    break;
                case "ScoreCounterRight":
                    for (int y = 0; y < Game.Resolution.y; y++)
                    {
                        AddPoint(new Vector3(Game.Resolution.x - 1, y, 0));
                    }
                    break;
                default: break;
            }
        }

    }
}
