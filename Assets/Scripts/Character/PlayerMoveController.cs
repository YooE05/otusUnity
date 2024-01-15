using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public class PlayerMoveController :
        Listeners.IFixUpdaterListener,
        Listeners.IFinishListener
    {
        private BoundsChecker _boundsChecker;
        private MoveComponent _moveComponent;
        private InputManager _inputManager;

        public PlayerMoveController(BoundsChecker boundsChecker, MoveComponent moveComponent, InputManager inputManager)
        {
            _boundsChecker = boundsChecker;
            _moveComponent = moveComponent;
            _inputManager = inputManager;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            MoveCharacter(_moveComponent.GOTransform.position, _inputManager.MoveDirection, deltaTime);
        }

        public void MoveCharacter(Vector3 charPosition, float horizontalDirection, float timeDelta)
        {
            bool canMove = _boundsChecker.IsFreeByLeft(charPosition) && horizontalDirection < 0 || _boundsChecker.IsFreeByRight(charPosition) && horizontalDirection > 0;

            if (canMove)
            {
                _moveComponent.MoveByRigidbody(new Vector2(horizontalDirection, 0) * timeDelta);
            }

        }

        public void OnFinish()
        {
            _moveComponent.gameObject.SetActive(false);
        }

    }

}
