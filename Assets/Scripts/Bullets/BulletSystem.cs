using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour,
        Listeners.IFixUpdaterListener
    {
        [SerializeField]
        private LevelBounds _levelBounds;

        [Header("Pool")]
        [SerializeField]
        private BulletPool _bulletPool;
        private readonly List<Bullet> _cache = new();

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
                if (!_levelBounds.InBounds(bullet.transform.position))
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