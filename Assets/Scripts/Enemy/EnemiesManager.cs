using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemiesManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPositions enemiePosHandler;

        [SerializeField]
        private EnemyPool enemyPool;

        [SerializeField]
        private EnemiesMoveController moveController;

        [SerializeField]
        private EnemiesAttackController attackController;


        private IEnumerator Start()
        {
            int i = enemyPool.maxActiveCount;
            while (i > 0)
            {
                yield return new WaitForSeconds(1f);

                InitNewEnemy();
                i--;
            }
        }

        private void OnEnable()
        {
            moveController.OnEnemyReachPosition += InitEnemyWeapon;
        }

        private void OnDisable()
        {
            moveController.OnEnemyReachPosition -= InitEnemyWeapon;
        }

        private void InitEnemyWeapon(GameObject enemy)
        {
            attackController.RegisterWeapon(enemy.GetComponent<WeaponComponent>());
        }

        private void OnDestroyed(GameObject enemy)
        {

            enemy.GetComponent<HitPointsComponent>().OnHpIsEmpty -= this.OnDestroyed;

            attackController.StopWeaponFire(enemy.GetComponent<WeaponComponent>());

            enemyPool.HideEnemy(enemy);
            InitNewEnemy();

        }

        private void InitNewEnemy()
        {
            var enemy = enemyPool.SpawnEnemy();
            if (enemy)
            {

                var spawnPosition = this.enemiePosHandler.GetRandSpawnPos();
                enemy.transform.position = spawnPosition.position;

                var attackPosition = this.enemiePosHandler.GetRandAtkPos().position;
                moveController.TrackEnemy(enemy.GetComponent<MoveComponent>(), attackPosition);


                enemy.GetComponent<HitPointsComponent>().OnHpIsEmpty += this.OnDestroyed;

            }
        }

    }
}