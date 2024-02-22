using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletView : MonoBehaviour
    {
        public event Action<Collision2D> OnCollisionEntered;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public int Damage { get; private set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(collision);
        }

        public Vector2 GetCurrentVelocity()
        {
            return _rigidbody2D.velocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetDamage(int damage)
        {
            Damage = damage;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}