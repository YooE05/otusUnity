using UnityEngine;
using System;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        public Action OnPlayerDied;


        [SerializeField]
        private GameObject character;
        [SerializeField]
        private BulletSystem bulletSystem;
        [SerializeField]
        private InputManager inputManager;


        private PlayerMovement playerMovement;
        private WeaponComponent characterWeapon;

        private void Awake()
        {
            this.characterWeapon = this.character.GetComponent<WeaponComponent>();

            this.playerMovement = new PlayerMovement(this.character.GetComponent<MoveComponent>(), FindObjectOfType<LevelBounds>());
        }
        private void OnEnable()
        {
            this.inputManager.OnFireButtonPressed += Shoot;
        }

        private void OnDisable()
        {
            this.inputManager.OnFireButtonPressed -= Shoot;
        }

        private void FixedUpdate()
        {
            playerMovement.MoveCharacter(character.transform.position, inputManager.horizontalDirection, Time.fixedDeltaTime);
        }

        private void Shoot()
        {
            characterWeapon.SetCrntBullet(this.bulletSystem.GetBullet());
            characterWeapon.Shoot(characterWeapon.Rotation * Vector3.up * characterWeapon.Config.speed);
        }
    }
}