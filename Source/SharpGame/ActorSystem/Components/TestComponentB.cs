using System;

namespace SharpGame
{
    public class TestComponentB : ActorComponent
    {
        public Vector3 Direction { get; set; }
        public float Speed { get; set; }

        public override void Update(float deltaTime)
        {
            Actor.LocalPosition += Direction * Speed * deltaTime;
        }
    }
}
