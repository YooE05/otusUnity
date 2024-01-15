using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [field: SerializeField]
        public Transform LeftBorder { get; private set; }

        [field: SerializeField]
        public Transform RightBorder { get; private set; }

        [field: SerializeField]
        public Transform DownBorder { get; private set; }

        [field: SerializeField]
        public Transform TopBorder { get; private set; }
    }
}