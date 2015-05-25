using System;
using System.Diagnostics;
using System.Threading;

namespace SharpGame
{
    public class Game
    {
        public int TargetFPS { get; set; }
        public Resolution Resolution {get; private set;}
        public string Name { get; private set; }
        #region Subsystems
        public Physics Physics { get; private set; }
        public Graphics Graphics { get; private set; }
        public Resources Resources { get; private set; }
        #endregion

        private bool initialized;
        private bool runing;
        private bool exitQueued;

        public bool Initialize(string Nm)
        {
            Name = Nm;
            Resolution = new Resolution(40,30);
            Physics = new Physics(Resolution);
            Graphics = new Graphics(Resolution);
            Resources = new Resources();

            initialized = true;
            return true;
        }

        public void Run(Scene scene)
        {
            Debug.Assert(initialized);
            Debug.Assert(!runing);

            runing = true;
            Stopwatch time = new Stopwatch();
            time.Start();
            scene.Game = this;
            scene.Awake();
            scene.Start();

            while (!exitQueued)
            {
                float delta = time.ElapsedMilliseconds / 1000f;
                time.Restart();

                Graphics.BufferClear();
                scene.Update(delta);
                Physics.BufferClear();
                scene.Draw(delta);
                Graphics.DrawFrame();
                //Graphics.SwapBuffers();

                SleepToMatchFramerate(TargetFPS, time.ElapsedMilliseconds / 1000f);
            }

            time.Stop();
            scene.OnDestroy();
            runing = false;
            Shutdown();
        }

        public void EnqueueExit()
        {
            exitQueued = true;
        }

        private void Shutdown()
        {
            Graphics = null;

            initialized = false;
        }

        private void SleepToMatchFramerate(int targetFPS, float timeSoFar)
        {
            float targetFrameLength = 1f / targetFPS;
            float timeToSleep = targetFrameLength - timeSoFar;

            if (timeToSleep > 0)
                Thread.Sleep((int)(timeToSleep * 1000));
        }
    }
}
