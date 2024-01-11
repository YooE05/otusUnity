using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool: MonoBehaviour,
        Listeners.IInitListener
    {

        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private Transform _container;
        [SerializeField]
        private Bullet _prefab;


        [Header("Pool")]
        [SerializeField]
        private int _preloadCount = 50;

        private Pool<Bullet> _pool;

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
            var newBullet = Instantiate(_prefab, _container);

            _gameManager.AddListeners(newBullet.gameObject);

            return newBullet;
        }

        private void GetAction(Bullet bullet) => bullet.gameObject.SetActive(true);

        private void ReturnAction(Bullet bullet) => bullet.gameObject.SetActive(false);
    }
}