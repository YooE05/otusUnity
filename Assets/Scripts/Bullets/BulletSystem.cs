using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem :
        Listeners.IFixUpdaterListener
    {
        private readonly BoundsChecker _boundsChecker;
        private readonly BulletPool _bulletPool;
        private readonly List<BulletBehaviourController> _cache = new();

        public BulletSystem(BoundsChecker boundsChecker, BulletPool bulletPool)
        {
            _boundsChecker = boundsChecker;
            _bulletPool = bulletPool;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            CheckBoardersReaching();
        }

        private void CheckBoardersReaching()
        {
            _cache.Clear();
            _cache.AddRange(_bulletPool.GetAllActiveBullets());

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!_boundsChecker.InBounds(bullet.GetBulletPosition()))
                {
                    _bulletPool.RemoveBullet(bullet);
                }
            }
        }

        internal BulletBehaviourController GetBullet()
        {
            return _bulletPool.Get();
        }
    }
    public struct BulletArgs
    {
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public int physicsLayer;
        public int damage;
    }
}