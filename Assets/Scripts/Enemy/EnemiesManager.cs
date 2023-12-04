using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemiesManager : MonoBehaviour
    {
        public Action OnEnemyDied;

        [SerializeField]
        private EnemyPositions enemiePosHandler;

        [SerializeField]
        private EnemyPool enemyPool;

        [SerializeField]
        private BulletSystem bulletSystem;

        [SerializeField]
        private GameObject enemyTarget;

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty -= this.OnDestroyed;
            enemyPool.HideEnemy(enemy);

            OnEnemyDied?.Invoke();
        }

        public void InitNewEnemy()
        {
            var enemy = enemyPool.SpawnEnemy();

            var spawnPosition = this.enemiePosHandler.GetRandSpawnPos();
            enemy.transform.position = spawnPosition.position;
            var attackPosition = this.enemiePosHandler.GetRandAtkPos().position;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(enemyTarget);
            enemy.GetComponent<WeaponComponent>().bulletSystem = bulletSystem;
            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty += this.OnDestroyed;
        }
    }
}