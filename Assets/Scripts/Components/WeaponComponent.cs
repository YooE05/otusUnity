using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => _firePoint.position;

        public Quaternion Rotation => _firePoint.rotation;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private BulletSystem _bulletSystem;
        public BulletConfig Config
        {
            get { return _bulletConfig; }
        }

        [SerializeField]
        private BulletConfig _bulletConfig;

        public void SetBulletSystem(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        public void ShootByTarget(Vector2 targetPos)
        {
            var direction = (targetPos - Position).normalized;
            FlyBullet(direction);
        }

        public void ShootStraight()
        {
            var direction = Rotation * Vector3.up;
            FlyBullet(direction);
        }

        private void FlyBullet(Vector2 shoorDirection)
        {
            _bulletSystem.GetBullet().SetUpBullet(new BulletArgs
            {
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = _firePoint.transform.position,
                velocity = shoorDirection * Config.Speed
            });
        }
    }
}