using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour,
        Listeners.IUpdateListener,
        Listeners.IInitListener,
        Listeners.IStartListener,
        Listeners.IFinishListener,
        Listeners.IPauseListener,
        Listeners.IResumeListener
    {
        public float _moveDirection { get; private set; }

        public Action OnFireButtonPressed;
        public bool _CanUpdate { get => _canUpdate; set => _canUpdate = value; }
        private bool _canUpdate;

        public void OnUpdate(float deltaTime)
        {
            if (_canUpdate)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnFireButtonPressed?.Invoke();
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    _moveDirection = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    _moveDirection = 1;
                }
                else
                {
                    _moveDirection = 0;
                }
            }

        }

        public void OnInit()
        {
            _canUpdate = false;
        }
        public void OnStart()
        {
            _canUpdate = true;
        }

        public void OnFinish()
        {
            _canUpdate = false;
        }

        public void OnPause()
        {
            _canUpdate = false;
        }

        public void OnResume()
        {
            _canUpdate = true;
        }

    }
}