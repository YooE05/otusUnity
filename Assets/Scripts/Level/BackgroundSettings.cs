using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BackgroundSettings : MonoBehaviour

    {
        [field: SerializeField]
        public float StartPosY { get; private set; }

        [field: SerializeField]
        public float EndPosY { get; private set; }

        [field: SerializeField]
        public float MovingSpeedY { get; private set; }

        [field: SerializeField]
        public Transform BackTransform { get; private set; }

    }
}