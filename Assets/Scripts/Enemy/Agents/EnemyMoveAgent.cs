using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour,
        Listeners.IFixUpdaterListener,
        Listeners.IInitListener
    {
        public bool IsReached
        {
            get { return _isReached; }
        }

        private const float _moveThewshold = 0.25f;
        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;

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

            var vector = _destination - (Vector2)transform.position;
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