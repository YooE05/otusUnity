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
        private EnemyPositions _enemiePosHandler;

        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletSystem _bulletSystem;

        [SerializeField]
        private GameObject _enemyTarget;

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty -= this.OnDestroyed;
            _enemyPool.HideEnemy(enemy);

            OnEnemyDied?.Invoke();
        }

        public void InitNewEnemy()
        {
            var enemy = _enemyPool.SpawnEnemy();

            var spawnPosition = _enemiePosHandler.GetRandSpawnPos();
            enemy.transform.position = spawnPosition.position;
            var attackPosition = _enemiePosHandler.GetRandAtkPos().position;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_enemyTarget);
            enemy.GetComponent<WeaponComponent>()._bulletSystem = _bulletSystem;
            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty += this.OnDestroyed;
        }
    }
}