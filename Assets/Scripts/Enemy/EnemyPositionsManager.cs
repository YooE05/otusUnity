using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositionsManager
    {
        private EnemyPositions _enemyPositions;

        public EnemyPositionsManager(EnemyPositions enemyPositions)
        {
            _enemyPositions = enemyPositions;
        }

        public Transform GetRandAtkPos()
        {
            return RandomTransform(_enemyPositions.AttackPositions);
        }

        public Transform GetRandSpawnPos()
        {
            return RandomTransform(_enemyPositions.SpawnPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}