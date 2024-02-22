using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : 
        Listeners.IInitListener
    {
        private readonly GameManager _gameManager;
        private readonly Transform _container;
        private readonly GameObject _prefab;
        private readonly int _preloadCount;

        private Pool<GameObject> _pool;

        public EnemyPool(GameManager gameManager, Transform container, GameObject prefab, EnemySystemConfig config)
        {
            _gameManager = gameManager;
            _container = container;
            _prefab = prefab;

            _preloadCount = config.PreloadInstanceCount;
        }

        public void OnInit()
        {
            _pool = new Pool<GameObject>(Preload, GetAction, ReturnAction, _preloadCount);
        }

        public GameObject SpawnEnemy()
        {
            return _pool.Get();
        }

        public void HideEnemy(GameObject enemy)
        {
            _pool.Return(enemy);
        }

        public List<GameObject> GetActiveEnemies()
        {
            return _pool.GetActiveItms();
        }

        private GameObject Preload() => SetUpListeners();
        
        private GameObject SetUpListeners()
        {
            var newEnemy = Object.Instantiate(_prefab, _container);

            _gameManager.AddListeners(newEnemy);

            return newEnemy;
        }
        
        private void GetAction(GameObject enemy) => enemy.SetActive(true);
        
        private void ReturnAction(GameObject enemy) => enemy.SetActive(false);
    }
}