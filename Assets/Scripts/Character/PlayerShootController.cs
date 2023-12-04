using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class PlayerShootController : MonoBehaviour
    {
        [SerializeField]
        private WeaponComponent playerWeapon;

        [SerializeField]
        private InputManager inputManager;

        private void OnEnable()
        {
            this.inputManager.OnFireButtonPressed += Shoot;
        }

        private void OnDisable()
        {
            this.inputManager.OnFireButtonPressed -= Shoot;
        }
        private void Shoot()
        {
            this.playerWeapon.ShootStraight();
        }
    }
}