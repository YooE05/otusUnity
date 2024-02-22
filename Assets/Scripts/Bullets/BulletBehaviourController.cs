using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletBehaviourController :
        Listeners.IPauseListener,
        Listeners.IResumeListener
    {
        public event Action<BulletBehaviourController> OnCollisionEntered;

        private readonly BulletView _bulletView;
        private Vector2 _currentVelocity;

        public BulletBehaviourController(BulletView bulletView)
        {
            _bulletView = bulletView;
            _bulletView.OnCollisionEntered += InvokeCollisionListeners;
        }

        public void EnableView()
        {
            _bulletView.gameObject.SetActive(true);
        }

        public void DisableView()
        {
            _bulletView.gameObject.SetActive(false);
        }

        public void SetUpBullet(BulletArgs args)
        {
            _bulletView.SetPosition(args.position);
            _bulletView.SetColor(args.color);
            _bulletView.SetPhysicsLayer(args.physicsLayer);
            _bulletView.SetDamage(args.damage);
            _bulletView.SetVelocity(args.velocity);
        }

        internal Vector3 GetBulletPosition()
        {
            return _bulletView.transform.position;
        }

        public void OnResume()
        {
            _bulletView.SetVelocity(_currentVelocity);
        }

        public void OnPause()
        {
            _currentVelocity = _bulletView.GetCurrentVelocity();
            _bulletView.SetVelocity(Vector2.zero);
        }

        private void InvokeCollisionListeners(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(_bulletView.Damage);
            }

            OnCollisionEntered?.Invoke(this);
        }

        internal void AddCollisionListener(Action<BulletBehaviourController> removeBullet)
        {
            OnCollisionEntered += removeBullet;
        }

        internal void RemoveCollisionListener(Action<BulletBehaviourController> removeBullet)
        {
            OnCollisionEntered -= removeBullet;
        }
    }
}
