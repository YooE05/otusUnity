using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public class PlayerMoveController: MonoBehaviour
    {
        [SerializeField]
        private LevelBounds bounds;
        [SerializeField]
        private MoveComponent moveComponent;
        [SerializeField]
        private InputManager inputManager;


        private void FixedUpdate()
        {
           MoveCharacter(this.moveComponent.GOTransform.position, this.inputManager.moveDirection, Time.fixedDeltaTime);
        }

        public void MoveCharacter(Vector3 charPosition, float horizontalDirection, float timeDelta)
        {
            bool canMove = this.bounds.IsFreeByLeft(charPosition) && horizontalDirection < 0 || this.bounds.IsFreeByRight(charPosition) && horizontalDirection > 0;

            if (canMove)
            {
                this.moveComponent.MoveByRigidbody(new Vector2(horizontalDirection, 0) * timeDelta);
            }

        }

    }
}