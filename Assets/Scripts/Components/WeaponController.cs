using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponController
    {
        private readonly WeaponComponent _weaponComponent;
        private readonly BulletSystem _bulletSystem;

        public Vector2 Position => _weaponComponent.FirePoint.position;
        public Quaternion Rotation => _weaponComponent.FirePoint.rotation;

        public WeaponController(WeaponComponent weaponComponent, BulletSystem bulletSystem)
        {
            _weaponComponent = weaponComponent;
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
                physicsLayer = (int)_weaponComponent.BulletConfig.PhysicsLayer,
                color = _weaponComponent.BulletConfig.Color,
                damage = _weaponComponent.BulletConfig.Damage,
                position = Position,
                velocity = shoorDirection * _weaponComponent.BulletConfig.Speed
            });
        }
    }
}