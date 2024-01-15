using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletPool : 
        Listeners.IInitListener
    {
        private ObjectsSpawner _spawner;
        private GameManager _gameManager;
        private Transform _container;
        private Bullet _prefab;

        private int _preloadCount;
        private Pool<Bullet> _pool;

        public BulletPool(ObjectsSpawner spawner, GameManager gameManager, Transform container, Bullet prefab, int preloadCount)
        {
            _spawner = spawner;
            _gameManager = gameManager;
            _container = container;
            _prefab = prefab;
            _preloadCount = preloadCount;
        }


        public void OnInit()
        {
            _pool = new Pool<Bullet>(Preload, GetAction, ReturnAction, _preloadCount);
        }

        internal IEnumerable<Bullet> GetActiveBullet()
        {
            return _pool.GetActiveItms();

        }

        public void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= RemoveBullet;
            _pool.Return(bullet);
        }

        private Bullet Preload() => SetUpListeners();

        internal Bullet Get()
        {
            var bullet = _pool.Get();
            bullet.OnCollisionEntered += RemoveBullet;
            return bullet;
        }

        private Bullet SetUpListeners()
        {
            var newBullet = _spawner.InstantianeObject(_prefab.gameObject, _container);

            _gameManager.AddListeners(newBullet);

            return newBullet.GetComponent<Bullet>();
        }

        private void GetAction(Bullet bullet) => bullet.gameObject.SetActive(true);

        private void ReturnAction(Bullet bullet) => bullet.gameObject.SetActive(false);
    }
}