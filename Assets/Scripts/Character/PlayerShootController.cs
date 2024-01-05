using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class PlayerShootController : MonoBehaviour, 
        Listeners.IStartListener,
        Listeners.IFinishListener
    {
        [SerializeField]
        private WeaponComponent _playerWeapon;

        [SerializeField]
        private InputManager _inputManager;

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