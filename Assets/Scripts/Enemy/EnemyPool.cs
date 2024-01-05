using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField]
        private GameManagerBuilder _gameManagerBuilder;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private int _preloadCount;

        private Pool<GameObject> _pool;

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
            var newEnemy = Instantiate(_prefab, _container);

            _gameManagerBuilder.AddListeners(newEnemy);

            return newEnemy;
        }
        private void GetAction(GameObject enemy) => enemy.SetActive(true);

        private void ReturnAction(GameObject enemy) => enemy.SetActive(false);

    }
}