using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent :
        Listeners.IFixUpdaterListener,
        Listeners.IInitListener
    {
        private readonly MoveComponent _moveComponent;
        private const float _moveThewshold = 0.25f;
        private Vector2 _destination;
        private bool _isReached;
        public bool IsReached
        {
            get => _isReached; 
        }

        public EnemyMoveAgent(MoveComponent moveComponent)
        {
            _moveComponent = moveComponent;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)_moveComponent.transform.position;
            if (vector.sqrMagnitude <= _moveThewshold * _moveThewshold)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * deltaTime;
            _moveComponent.MoveByRigidbody(direction);
        }

        public void OnInit()
        {
            _isReached = true;
        }
    }
}