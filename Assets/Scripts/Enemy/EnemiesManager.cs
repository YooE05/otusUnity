using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemiesManager
    {
        public Action OnEnemyDied;

        private EnemyPositionsManager _enemyPosManager;
        private EnemyPool _enemyPool;
        private BulletSystem _bulletSystem;
        private GameObject _enemyTarget;

        public EnemiesManager(EnemyPositionsManager enemiePosManager, BulletSystem bulletSystem, EnemyPool enemyPool, GameObject enemyTarget)
        {
            _enemyPosManager = enemiePosManager;
            _bulletSystem = bulletSystem;
            _enemyPool = enemyPool;
            _enemyTarget = enemyTarget;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty -= this.OnDestroyed;
            _enemyPool.HideEnemy(enemy);

            OnEnemyDied?.Invoke();
        }

        public void InitNewEnemy()
        {
            var enemy = _enemyPool.SpawnEnemy();

            var spawnPosition = _enemyPosManager.GetRandSpawnPos();
            enemy.transform.position = spawnPosition.position;
            var attackPosition = _enemyPosManager.GetRandAtkPos().position;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_enemyTarget);
            enemy.GetComponent<WeaponComponent>().Construct(_bulletSystem);
            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty += this.OnDestroyed;
        }
    }
}