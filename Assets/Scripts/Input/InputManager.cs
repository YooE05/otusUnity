using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour,
        Listeners.IUpdateListener
    {
        public float _moveDirection { get; private set; }

        public Action OnFireButtonPressed;

        public void OnUpdate(float deltaTime)
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
}