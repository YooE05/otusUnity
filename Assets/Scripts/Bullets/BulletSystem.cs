using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSystem : 
        Listeners.IFixUpdaterListener
    {
        private BoundsChecker _boundsChecker;
        private BulletPool _bulletPool;

        private readonly List<Bullet> _cache = new();

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
            _cache.AddRange(_bulletPool.GetActiveBullet());

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!_boundsChecker.InBounds(bullet.transform.position))
                {
                    _bulletPool.RemoveBullet(bullet);
                }
            }
        }

        internal Bullet GetBullet()
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