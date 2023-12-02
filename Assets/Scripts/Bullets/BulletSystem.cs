using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {


        [SerializeField]
        private LevelBounds levelBounds;


        [SerializeField]
        private Transform container;
        [SerializeField]
        private Bullet prefab;


        [Header("Pool")]
        [SerializeField]
        private int preloadCount = 50;

        private Pool<Bullet> pool;

        private readonly List<Bullet> cache = new();

        private void Awake()
        {
            this.pool = new Pool<Bullet>(Preload, GetAction, ReturnAction, preloadCount);
        }

        private void FixedUpdate()
        {
            CheckBoardersReaching();
        }
       
        private void CheckBoardersReaching()
        {
            this.cache.Clear();
            this.cache.AddRange(pool.GetActiveItms());

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var bullet = this.cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        private void OnBulletCollision(Bullet bullet)
        {
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            this.pool.Return(bullet);
        }

        internal Bullet GetBullet()
        {
            var bullet = this.pool.Get();
            bullet.OnCollisionEntered += this.OnBulletCollision;
            return bullet;
        }

        private Bullet Preload() => Instantiate(this.prefab, this.container);

        private void GetAction(Bullet bullet) => bullet.gameObject.SetActive(true);

        private void ReturnAction(Bullet bullet) => bullet.gameObject.SetActive(false);

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