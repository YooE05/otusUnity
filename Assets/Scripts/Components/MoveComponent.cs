using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        public Transform GOTransform { get=> _rb.transform; }

        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private float _speed = 5.0f;
        
        public void MoveByRigidbody(Vector2 vector)
        {
            var nextPosition =_rb.position + vector * _speed;
            _rb.MovePosition(nextPosition);
        }
    }
}