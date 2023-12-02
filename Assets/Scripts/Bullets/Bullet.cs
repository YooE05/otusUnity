using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;

        private int damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this);

            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(this.damage);
            }
        }


        public void SetUpBullet(BulletArgs args)
        {
            SetPosition(args.position);
            SetColor(args.color);
            SetPhysicsLayer(args.physicsLayer);
            this.damage = args.damage;
            SetVelocity(args.velocity);
        }

        private void SetVelocity(Vector2 velocity)
        {
            this.rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        private void SetColor(Color color)
        {
            this.spriteRenderer.color = color;
        }
    }
}