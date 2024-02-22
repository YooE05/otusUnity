using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMoveController :
        Listeners.IFixUpdaterListener,
        Listeners.IFinishListener
    {
        private readonly BoundsChecker _boundsChecker;
        private readonly MoveComponent _moveComponent;
        private readonly InputManager _inputManager;

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

        public void OnFinish()
        {
            _moveComponent.gameObject.SetActive(false);
        }

        private void MoveCharacter(Vector3 charPosition, float horizontalDirection, float timeDelta)
        {
            bool canMove = _boundsChecker.IsFreeByLeft(charPosition) && horizontalDirection < 0 || _boundsChecker.IsFreeByRight(charPosition) && horizontalDirection > 0;

            if (canMove)
            {
                _moveComponent.MoveByRigidbody(new Vector2(horizontalDirection, 0) * timeDelta);
            }
        }
    }
}
