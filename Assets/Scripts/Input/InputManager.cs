using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float moveDirection { get; private set; }

        public Action OnFireButtonPressed;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFireButtonPressed?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.moveDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.moveDirection = 1;
            }
            else
            {
                this.moveDirection = 0;
            }
        }

    }
}