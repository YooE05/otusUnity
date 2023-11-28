using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private float speed = 5.0f;
        
        public void MoveByRigidbody(Vector2 vector)
        {
            var nextPosition = this.rb.position + vector * this.speed;
            this.rb.MovePosition(nextPosition);
        }
    }
}