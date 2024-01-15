using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [field: SerializeField]
        public Transform[] SpawnPositions { get; private set; }

        [field: SerializeField]
        public Transform[] AttackPositions { get; private set; }
    }
}