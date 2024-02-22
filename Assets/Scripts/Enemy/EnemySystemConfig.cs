using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
       fileName = "EnemySystemConfig",
       menuName = "Configs/New EnemySystemConfig"
   )]
    public class EnemySystemConfig : ScriptableObject
    {
        [field: SerializeField]
        public int PreloadInstanceCount { get; private set; }

        [field: SerializeField]
        public float InitSpawnDelay { get; private set; }

        [field: SerializeField]
        public float NewSpawnDelay { get; private set; }

        [field: SerializeField]
        public int SimultaneousEnemiesCount { get; private set; }
    }
}