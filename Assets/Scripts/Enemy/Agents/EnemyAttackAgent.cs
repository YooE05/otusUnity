using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour,
        Listeners.IStartListener
    {
        [SerializeField] private float _attackDelay;

        private WeaponController _weaponController;
        private GameObject _target;
        private float _currentTime;

        internal void SetUpAgent(WeaponController enemyWeaponController, GameObject enemyTarget)
        {
            _weaponController = enemyWeaponController;
            _target = enemyTarget;
        }

        private void Fire()
        {
            _weaponController.ShootByTarget((Vector2)_target.transform.position);
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