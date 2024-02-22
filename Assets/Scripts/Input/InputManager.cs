using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class InputManager :
        Listeners.IUpdateListener
    {
        public Action OnFireButtonPressed;
        public float MoveDirection { get; private set; }

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFireButtonPressed?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveDirection = 1;
            }
            else
            {
                MoveDirection = 0;
            }
        }
    }
}