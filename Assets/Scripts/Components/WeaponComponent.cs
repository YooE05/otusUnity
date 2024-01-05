using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position
        {
            get { return _firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return _firePoint.rotation; }
        }

        [SerializeField]
        private Transform _firePoint;

        public BulletSystem _bulletSystem;
        public BulletConfig Config
        {
            get { return _bulletConfig; }
        }

        [SerializeField]
        private BulletConfig _bulletConfig;


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
                physicsLayer = (int)_bulletConfig.physicsLayer,
                color = _bulletConfig.color,
                damage = _bulletConfig.damage,
                position = _firePoint.transform.position,
                velocity = shoorDirection * Config.speed
            });
        }
    }
}