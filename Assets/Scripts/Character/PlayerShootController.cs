using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class PlayerShootController : MonoBehaviour
    {
        [SerializeField]
        private WeaponComponent playerWeapon;
        [SerializeField]
        private BulletSystem bulletSystem;
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
            this.playerWeapon.SetCrntBullet(this.bulletSystem.GetBullet());
            this.playerWeapon.Shoot(playerWeapon.Rotation * Vector3.up * playerWeapon.Config.speed);
        }
    }
}