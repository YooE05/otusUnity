using System.Collections;
using UnityEngine;
using System;

namespace ShootEmUp
{
    class UpdatebleCountdown : Countdown, Listeners.IUpdateListener
    {
        public void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;

            UpdateTimer();
        }

    }

    class PrestartUpdatebleCountdown : Countdown, Listeners.IPrestartUpdateListener
    {
        public void OnPrestartUpdate(float deltaTime)
        {
            _timer += deltaTime;

            UpdateTimer();
        }

    }
}
