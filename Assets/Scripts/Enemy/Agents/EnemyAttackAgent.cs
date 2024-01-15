using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour,
        Listeners.IStartListener
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private float _attackDelay;

        private GameObject _target;
        private float _currentTime;


        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private void Fire()
        {
            _weaponComponent.ShootByTarget((Vector2)_target.transform.position);
        }

        public void DelayedAttack(float deltaTime)
        {
            _currentTime -= deltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _attackDelay;
            }
        }
        

        public void OnStart()
        {
            _currentTime = _attackDelay;
        }

    }
}