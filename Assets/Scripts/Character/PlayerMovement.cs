using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public class PlayerMovement
    {
        private LevelBounds bounds;

        private MoveComponent moveComponent;

        public PlayerMovement(MoveComponent moveComponent, LevelBounds bounds)
        {
            this.bounds = bounds;
            this.moveComponent = moveComponent;
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