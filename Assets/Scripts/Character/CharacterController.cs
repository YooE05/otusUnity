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
        private HitPointsComponent characterHP;

        private void Awake()
        {
            this.characterHP = this.character.GetComponent<HitPointsComponent>();
            this.characterWeapon = this.character.GetComponent<WeaponComponent>();

            this.playerMovement = new PlayerMovement(this.character.GetComponent<MoveComponent>(), FindObjectOfType<LevelBounds>());
        }

        private void OnEnable()
        {
            this.characterHP.OnHpIsEmpty += this.OnCharacterDeath;
            this.inputManager.OnFireButtonPressed += Shoot;
        }

        private void OnDisable()
        {
            this.characterHP.OnHpIsEmpty -= this.OnCharacterDeath;
            this.inputManager.OnFireButtonPressed -= Shoot;
        }

        private void OnCharacterDeath(GameObject _) => OnPlayerDied?.Invoke();

        private void FixedUpdate()
        {
            playerMovement.MoveCharacter(character.transform.position, inputManager.horizontalDirection, Time.fixedDeltaTime);
        }

        private void Shoot()
        {
            this.bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                physicsLayer = (int)characterWeapon.Config.physicsLayer,
                color = characterWeapon.Config.color,
                damage = characterWeapon.Config.damage,
                position = characterWeapon.Position,
                velocity = characterWeapon.Rotation * Vector3.up * characterWeapon.Config.speed
            });
        }
    }
}