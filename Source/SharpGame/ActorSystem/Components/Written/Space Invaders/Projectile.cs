﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    public class Projectile:ActorComponent
    {
        int speed;

        public override void Awake()
        {
            speed = 10;
        }

        public override void Start()
        {
            Actor.Name = this.GetType().Name;
        }

        public override void Update(float D)
        {
            if (Actor.LocalPosition.y > 0)
                MoveForward(D);
        }

        public override void OnCollide(Actor Exciter)
        { if (Exciter.Name!=this.GetType().Name)
            Actor.Scene.ForDestroy(Actor); }

        public override void OnDestroy()
        { }

        private void MoveForward(float D)
        {
            Actor.LocalPosition+=(Vector3.Up*speed*D);
        }

    }
}
