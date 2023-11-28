using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {

        //oldCode
        /*internal static void DealDamage(Bullet bullet, GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bullet.isPlayerFrom == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.damage);
            }
        }*/
    }
}