using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class PlayerShootController :
        Listeners.IStartListener,
        Listeners.IFinishListener
    {
        private WeaponComponent _playerWeapon;
        private InputManager _inputManager;

        public PlayerShootController(WeaponComponent playerWeapon, InputManager inputManager)
        {
            _playerWeapon = playerWeapon;
            _inputManager = inputManager;
        }

        public void OnStart()
        {
           _inputManager.OnFireButtonPressed += Shoot;
        }
        public void OnFinish()
        {
            _inputManager.OnFireButtonPressed -= Shoot;
        }
       
        private void Shoot()
        {
            _playerWeapon.ShootStraight();
        }



    }
}