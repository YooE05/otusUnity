/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public class EnemiesAttackController : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        [SerializeField]
        private float shotCountdown;

        [SerializeField]
        private BulletSystem bulletSystem;

        private Dictionary<WeaponComponent, AttackSpec> weapons = new();

        public void RegisterWeapon(WeaponComponent newEnemyWeapon)
        {
            this.weapons.TryAdd(newEnemyWeapon, new AttackSpec(shotCountdown));
            ResetWeapon(this.weapons[newEnemyWeapon]);
        }

        public void StopWeaponFire(WeaponComponent enemyWeapon)
        {
            if (this.weapons.ContainsKey(enemyWeapon))
            {
                this.weapons[enemyWeapon].isActive = false;
            }
        }


        private void ResetWeapon(AttackSpec spec)
        {
            spec.currentTime = this.shotCountdown;
            spec.isActive = true;
        }

        private void FixedUpdate()
        {

            foreach (var enemyWeapon in weapons)
            {

                if (this.target.GetComponent<HitPointsComponent>().IsHitPointsExists() && enemyWeapon.Value.isActive)
                {
                    enemyWeapon.Value.currentTime -= Time.fixedDeltaTime;
                    if (enemyWeapon.Value.currentTime <= 0)
                    {
                        this.Fire(enemyWeapon.Key);
                        enemyWeapon.Value.currentTime += this.shotCountdown;
                    }
                }

            }
        }

        private void Fire(WeaponComponent weapon)
        {
            var startPosition = weapon.Position;
            var vector = (Vector2)this.target.transform.position - startPosition;
            var direction = vector.normalized;

            bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                physicsLayer = (int)weapon.Config.physicsLayer,
                color = weapon.Config.color,
                damage = weapon.Config.damage,
                position = startPosition,
                velocity = direction * 2.0f
            });

        }

        private class AttackSpec
        {
            public AttackSpec(float countdown)
            {
                this.isActive = true;
                this.countdown = countdown;
            }
            public bool isActive;
            public float countdown;
            public float currentTime;
        }
    }
}

*/