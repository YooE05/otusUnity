using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "StartGameConfig",
        menuName = "Configs/New StartGameConfig"
    )]
    public sealed class StartGameConfig : ScriptableObject
    {
        [field: SerializeField]
        public bool IsDelayedStart { get; private set; }

        [field: SerializeField]
        public int CountdownTime { get; private set; }

    }
}
