﻿using Newtonsoft.Json;
using SharpGame.Internal;
using System;

namespace SharpGame
{
    public abstract class ActorComponent : IGameEntity
    {
        [JsonIgnore]
        public Actor Actor { get; set; }

        [JsonIgnore]
        public Game Game
        {
            get
            {
                Game game = Actor.Scene.Game;
                return game;
            }
        }

        public virtual void Awake() { }

        public virtual void Start() { }

        public virtual void Update(float deltaTime) { }

        public virtual void Draw(float deltaTime) { }

        public virtual void OnCollide(Actor Exciter) { }

        public virtual void OnDestroy() { }
    }
}
