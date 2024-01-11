using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour,
        Listeners.IStartListener
    {
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private float _countdown;

        private GameObject _target;
        private float _currentTime;


        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private void Fire()
        {
            weaponComponent.ShootByTarget((Vector2)_target.transform.position);
        }

        public void DelayedAttack(float deltaTime)
        {
            _currentTime -= deltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }
        

        public void OnStart()
        {
            _currentTime = _countdown;
        }

    }
}