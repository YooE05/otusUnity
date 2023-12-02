using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField]
        private EnemiesManager enemiesManager;

        [SerializeField]
        private float initSpawnDelay;

        [SerializeField]
        private float newSpawnDelay;

        [SerializeField]
        private int initEnemiesCnt;

        private void OnEnable()
        {
            enemiesManager.OnEnemyDied += SpawnNewEnemy;
        }
        private void OnDisable()
        {
            enemiesManager.OnEnemyDied -= SpawnNewEnemy;
        }
   
        private void Start()
        {
            StartCoroutine(InitEnemySpawn());
        }

        private IEnumerator InitEnemySpawn()
        {
            int i = initEnemiesCnt;
            while (i > 0)
            {
                yield return StartCoroutine(SpawnByDelay(initSpawnDelay));
                i--;
            }
        }

        private void SpawnNewEnemy()
        {
            StartCoroutine(SpawnByDelay(newSpawnDelay));
        }

        private IEnumerator SpawnByDelay(float delay)
        {
                yield return new WaitForSeconds(delay);
                enemiesManager.InitNewEnemy();
        }


    }
}