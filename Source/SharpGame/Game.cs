﻿using System;
using System.Diagnostics;
using System.Threading;

namespace GameFramework
{
    public class Game
    {
        public int TargetFPS { get; set; }

        public Graphics Graphics { get; private set; }

        private bool initialized;
        private bool runing;
        private bool exitQueued;

        public bool Initialize()
        {
            Graphics = new Graphics();

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
            scene.Parent = this;
            scene.Awake();
            scene.Start();

            while (!exitQueued)
            {
                float delta = time.ElapsedMilliseconds / 1000f;
                time.Restart();

                Graphics.ClearBuffer();
                scene.Update(delta);
                Graphics.DrawOnScreen();
                Graphics.SwapBuffers();

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
