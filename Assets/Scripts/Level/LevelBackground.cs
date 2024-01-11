using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour,
        Listeners.IInitListener,
        Listeners.IFixUpdaterListener

    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private Vector2 _position;

        private Transform _backTransform;

        [SerializeField]
        private BackgorundParams _backParams;


        public void OnInit()
        {
            SetUpBackMove();
        }
       
        
        private void SetUpBackMove()
        {
            _startPositionY = _backParams.startPosY;
            _endPositionY = _backParams.endPosY;
            _movingSpeedY = _backParams.movingSpeedY;
            _backTransform = transform;
            _position = new Vector2(_backTransform.position.x, _backTransform.position.z);
        }

        public void OnFixedUpdate(float deltaTime)
        {
           MoveBackground(); 
        }

        private void MoveBackground()
        {
            if (_backTransform.position.y <= _endPositionY)
            {
                _backTransform.position = new Vector3(
                    _position.x,
                    _startPositionY,
                    _position.y
               );
            }

            _backTransform.position -= new Vector3(
                _position.x,
                _movingSpeedY * Time.fixedDeltaTime,
                _position.y
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