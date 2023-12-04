using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position
        {
            get { return this.firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return this.firePoint.rotation; }
        }

        [SerializeField]
        private Transform firePoint;

        public BulletSystem bulletSystem;
        public BulletConfig Config
        {
            get { return this.bulletConfig; }
        }

        [SerializeField]
        private BulletConfig bulletConfig;


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
            bulletSystem.GetBullet().SetUpBullet(new BulletArgs
            {
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = firePoint.transform.position,
                velocity = shoorDirection * Config.speed
            });
        }
    }
}