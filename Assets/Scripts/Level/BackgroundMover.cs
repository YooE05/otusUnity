using UnityEngine;

namespace ShootEmUp
{
    public sealed class BackgroundMover :
        Listeners.IInitListener,
        Listeners.IFixUpdaterListener
    {
        private readonly BackgroundSettings _backParams;
        private Vector2 _position;

        public BackgroundMover(BackgroundSettings backParams)
        {
            _backParams = backParams;
        }

        public void OnInit()
        {
            _position = new Vector2(_backParams.BackTransform.position.x, _backParams.BackTransform.position.z);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            MoveBackground();
        }

        private void MoveBackground()
        {
            if (_backParams.BackTransform.position.y <= _backParams.EndPosY)
            {
                _backParams.BackTransform.position = new Vector3(
                    _position.x,
                    _backParams.StartPosY,
                    _position.y
               );
            }

            _backParams.BackTransform.position -= new Vector3(
                _position.x,
                _backParams.MovingSpeedY * Time.fixedDeltaTime,
                _position.y
           );
        }
    }
}