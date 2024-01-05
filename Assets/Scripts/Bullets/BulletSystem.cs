using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour,
        Listeners.IInitListener,
        Listeners.IFixUpdaterListener,
        Listeners.IPauseListener,
        Listeners.IResumeListener,
        Listeners.IStartListener
    {
        public bool _CanUpdate { get => _canUpdate; set => _canUpdate = value; }
        private bool _canUpdate;

        [SerializeField]
        private GameManagerBuilder _gameManagerBuilder;

        [SerializeField]
        private LevelBounds _levelBounds;

        [SerializeField]
        private Transform _container;
        [SerializeField]
        private Bullet _prefab;


        [Header("Pool")]
        [SerializeField]
        private int _preloadCount = 50;

        private Pool<Bullet> _pool;

        private readonly List<Bullet> _cache = new();

        public void OnInit()
        {
            _pool = new Pool<Bullet>(Preload, GetAction, ReturnAction, _preloadCount);
            _canUpdate = false;
        }
        public void OnStart()
        {
            _canUpdate = true;
        }

        public void OnPause()
        {
            _canUpdate = false;
        }

        public void OnResume()
        {
            _canUpdate = true;
        }


        public void OnFixedUpdate(float deltaTime)
        {
            if (_canUpdate)
            {
                CheckBoardersReaching();
            }

        }

        private void CheckBoardersReaching()
        {
            _cache.Clear();
            _cache.AddRange(_pool.GetActiveItms());

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
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
            bullet.OnCollisionEntered -= OnBulletCollision;
            _pool.Return(bullet);
        }

        internal Bullet GetBullet()
        {
            var bullet = _pool.Get();
            bullet.OnCollisionEntered += OnBulletCollision;
            return bullet;
        }

        private Bullet Preload() => SetUpListeners();

        private Bullet SetUpListeners()
        {
            var newBullet = Instantiate(_prefab, _container);

            _gameManagerBuilder.AddListeners(newBullet.gameObject);

            return newBullet;
        }

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