using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float horizontalDirection { get; private set; }

        public Action OnFireButtonPressed;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFireButtonPressed?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.horizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.horizontalDirection = 1;
            }
            else
            {
                this.horizontalDirection = 0;
            }
        }

    }
}