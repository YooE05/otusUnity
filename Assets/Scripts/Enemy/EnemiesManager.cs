using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemiesManager
    {
        public Action OnEnemyDied;

        private readonly EnemyPositionsManager _enemyPosManager;
        private readonly EnemyPool _enemyPool;
        private readonly BulletSystem _bulletSystem;
        private readonly GameObject _enemyTarget;
        private readonly GameManager _gameManager;

        public EnemiesManager(GameManager gameManager, EnemyPositionsManager enemyPosManager, BulletSystem bulletSystem, EnemyPool enemyPool, GameObject enemyTarget)
        {
            _gameManager = gameManager;
            _enemyPosManager = enemyPosManager;
            _bulletSystem = bulletSystem;
            _enemyPool = enemyPool;
            _enemyTarget = enemyTarget;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty -= OnDestroyed;
            _enemyPool.HideEnemy(enemy);

            OnEnemyDied?.Invoke();
        }

        public void InitNewEnemy()
        {
            var enemy = _enemyPool.SpawnEnemy();
            var spawnPosition = _enemyPosManager.GetRandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemyPosManager.GetRandomAttackPosition().position;
            var enemyMoveAgent = new EnemyMoveAgent(enemy.GetComponent<MoveComponent>());
            enemyMoveAgent.SetDestination(attackPosition);
            _gameManager.AddListener(enemyMoveAgent);

            var enemyWeaponController = new WeaponController(enemy.GetComponent<WeaponComponent>(), _bulletSystem);//нужно ли их как-то отслеживать или собирать, чтбы потом удалить при удалении врага
            enemy.GetComponent<EnemyAttackAgent>().SetUpAgent(enemyWeaponController, _enemyTarget);

            var enemyAttackController = new EnemyMoveAttackController(enemy, enemyMoveAgent, enemy.GetComponent<EnemyAttackAgent>());
            _gameManager.AddListener(enemyAttackController);

            enemy.GetComponent<HitPointsComponent>().OnHitpointsEmpty += OnDestroyed;
        }
    }
}