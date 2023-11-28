using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float startPositionY;

        private float endPositionY;

        private float movingSpeedY;

        private Vector2 position;

        private Transform backTransform;

        [SerializeField]
        private BackgorundParams backParams;

        private void Awake()
        {
            SetUpBackMove();
        }

        private void SetUpBackMove()
        {
            this.startPositionY = this.backParams.startPosY;
            this.endPositionY = this.backParams.endPosY;
            this.movingSpeedY = this.backParams.movingSpeedY;
            this.backTransform = this.transform;
            this.position = new Vector2(this.backTransform.position.x, this.backTransform.position.z);
        }

        private void FixedUpdate()
        {
            MoveBackground();
        }

        private void MoveBackground()
        {
            if (this.backTransform.position.y <= this.endPositionY)
            {
                this.backTransform.position = new Vector3(
                    this.position.x,
                    this.startPositionY,
                    this.position.y
                );
            }

            this.backTransform.position -= new Vector3(
                this.position.x,
                this.movingSpeedY * Time.fixedDeltaTime,
                this.position.y
            );
        }

        [Serializable]
        public sealed class BackgorundParams
        {
            [SerializeField]
            public float startPosY;

            [SerializeField]
            public float endPosY;

            [SerializeField]
            public float movingSpeedY;
        }
    }
}