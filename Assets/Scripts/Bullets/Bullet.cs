using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour,
        Listeners.IPauseListener,
        Listeners.IResumeListener
    {
        public event Action<Bullet> OnCollisionEntered;

        private int _damage;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Vector2 _crntVelocity;

        private void OnCollisionEnter2D(Collision2D collision)
        {
             OnCollisionEntered?.Invoke(this);

            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage( _damage);
            }
        }


        public void SetUpBullet(BulletArgs args)
        {
            SetPosition(args.position);
            SetColor(args.color);
            SetPhysicsLayer(args.physicsLayer);
            _damage = args.damage;
            SetVelocity(args.velocity);
        }

        private void SetVelocity(Vector2 velocity)
        {
             _rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
             gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
             transform.position = position;
        }

        private void SetColor(Color color)
        {
             _spriteRenderer.color = color;
        }

        public void OnResume()
        {
            _rigidbody2D.velocity = _crntVelocity;
        }

        public void OnPause()
        {
            _crntVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}