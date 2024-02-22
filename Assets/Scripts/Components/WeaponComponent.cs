using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [field: SerializeField]
        public Transform FirePoint { get; private set; }

        [field: SerializeField]
        public BulletConfig BulletConfig { get; private set; }
    }
}