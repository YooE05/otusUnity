using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour,
        Listeners.IFixUpdaterListener,
        Listeners.IInitListener,
        Listeners.IStartListener,
        Listeners.IPauseListener,
        Listeners.IResumeListener
    {
        public bool IsReached
        {
            get { return _isReached; }
        }

        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;
        public bool _CanUpdate { get => _canUpdate; set => _canUpdate = value; }
        private bool _canUpdate;
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }


        public void OnFixedUpdate(float deltaTime)
        {
            if (_canUpdate)
            {
                if (_isReached)
                {
                    return;
                }

                var vector = _destination - (Vector2)transform.position;
                if (vector.magnitude <= 0.25f)
                {
                    _isReached = true;
                    return;
                }

                var direction = vector.normalized * deltaTime;
                _moveComponent.MoveByRigidbody(direction);
            }
        }
        public void OnInit()
        {
            _canUpdate = false;
            _isReached = true;
        }
        public void OnStart()
        {
            _canUpdate = true;
        }
        public void OnPause()
        {
            _canUpdate = false;
        }
        public void OnResume()
        {
            _canUpdate = true;
        }
    }
}