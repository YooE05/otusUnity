using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour,
        Listeners.IFixUpdaterListener,
        Listeners.IInitListener,
        Listeners.IStartListener,
        Listeners.IFinishListener,
        Listeners.IPauseListener,
        Listeners.IResumeListener
    {

        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private float _countdown;

        private GameObject _target;
        private float _currentTime;

        public bool _CanUpdate { get => _canUpdate; set => _canUpdate = value; }
        private bool _canUpdate;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private void Fire()
        {
            weaponComponent.ShootByTarget((Vector2)_target.transform.position);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_canUpdate&& gameObject.activeSelf)
            {
                if (!_moveAgent.IsReached)
                {
                    return;
                }

                if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
                {
                    return;
                }


                _currentTime -= deltaTime;
                if (_currentTime <= 0)
                {
                    Fire();
                    _currentTime += _countdown;
                }
            }
        }

        public void OnInit()
        {
            _canUpdate = false;
        }
        public void OnStart()
        {
            _currentTime = _countdown;
            _canUpdate = true;
        }

        public void OnFinish()
        {
            _canUpdate = false;
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