using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField]
        private Transform container;

        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private int preloadCount;

        public int maxActiveCount;

        private Pool<GameObject> pool;

        private void Awake()
        {
            pool = new Pool<GameObject>(Preload, GetAction, ReturnAction, preloadCount);
        }


        public GameObject SpawnEnemy()
        {
            return pool.Get();
        }

        public void HideEnemy(GameObject enemy)
        {
            pool.Return(enemy);
        }

        public List<GameObject> GetActiveEnemies()
        {
            return pool.GetActiveItms();
        }



        private GameObject Preload() => Instantiate(this.prefab, this.container);

        private void GetAction(GameObject enemy) => enemy.SetActive(true);

        private void ReturnAction(GameObject enemy) => enemy.SetActive(false);


        private struct EnemyOutData
        {
            public EnemyOutData(GameObject enemyGO, Vector2 attackPosition)
            {
                this.enemyGO = enemyGO;
                this.attackPosition = attackPosition;
            }

            public GameObject enemyGO;
            public Vector2 attackPosition;
        }
    }
}