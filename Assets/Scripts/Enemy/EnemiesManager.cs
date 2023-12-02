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
            enemy.GetComponent<HitPointsComponent>().OnHpIsEmpty -= this.OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.SetUpEnemyBullet;
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
            enemy.GetComponent<EnemyAttackAgent>().OnFire += this.SetUpEnemyBullet;
            enemy.GetComponent<HitPointsComponent>().OnHpIsEmpty += this.OnDestroyed;
        }



        private void SetUpEnemyBullet(WeaponComponent weaponComponent)
        {
            weaponComponent.SetCrntBullet(bulletSystem.GetBullet());

        }


    }
}