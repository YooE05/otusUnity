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

        public BulletConfig Config
        {
            get { return this.bulletConfig; }
        }

        [SerializeField]
        private BulletConfig bulletConfig;
        private Bullet crntBullet;


        internal void SetCrntBullet(Bullet bullet)
        {
            this.crntBullet = bullet;
        }
        public void Shoot(Vector2 direction)
        {
                crntBullet.SetUpBullet(new BulletArgs
                {
                    physicsLayer = (int)bulletConfig.physicsLayer,
                    color = bulletConfig.color,
                    damage = bulletConfig.damage,
                    position = firePoint.transform.position,
                    velocity = direction
                });
                crntBullet = null;
            

        }
    }
}