using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool :
        Listeners.IInitListener
    {
        private readonly GameManager _gameManager;
        private readonly Transform _container;
        private readonly BulletView _prefab;
        private readonly int _preloadCount;

        private Pool<BulletBehaviourController> _pool;

        public BulletPool(GameManager gameManager, Transform container, BulletView prefab, int preloadCount)
        {
            _gameManager = gameManager;
            _container = container;
            _prefab = prefab;
            _preloadCount = preloadCount;
        }

        public void OnInit()
        {
            _pool = new Pool<BulletBehaviourController>(Preload, GetAction, ReturnAction, _preloadCount);
        }

        private BulletBehaviourController Preload() => SetUpListeners();

        internal IEnumerable<BulletBehaviourController> GetAllActiveBullets()
        {
            return _pool.GetActiveItms();
        }

        internal BulletBehaviourController Get()
        {
            var bullet = _pool.Get();
            bullet.AddCollisionListener(RemoveBullet);
            return bullet;
        }

        private void GetAction(BulletBehaviourController bullet) => bullet.EnableView();

        public void RemoveBullet(BulletBehaviourController bullet)
        {
            bullet.RemoveCollisionListener(RemoveBullet);
            _pool.Return(bullet);
        }

        private void ReturnAction(BulletBehaviourController bullet) => bullet.DisableView();

        private BulletBehaviourController SetUpListeners()
        {
            var newBulletView = Object.Instantiate(_prefab.gameObject, _container);
            var newBulletBehavior = new BulletBehaviourController(newBulletView.GetComponent<BulletView>());

            _gameManager.AddListener(newBulletBehavior);
            _gameManager.AddListeners(newBulletView);

            return newBulletBehavior;
        }
    }
}