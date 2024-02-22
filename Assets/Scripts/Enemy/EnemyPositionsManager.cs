using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositionsManager
    {
        private readonly EnemyPositions _enemyPositions;

        public EnemyPositionsManager(EnemyPositions enemyPositions)
        {
            _enemyPositions = enemyPositions;
        }

        public Transform GetRandomAttackPosition()
        {
            return RandomTransform(_enemyPositions.AttackPositions);
        }

        public Transform GetRandomSpawnPosition()
        {
            return RandomTransform(_enemyPositions.SpawnPositions);
        }

        private static Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}