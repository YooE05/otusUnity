using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public class PlayerMoveController : MonoBehaviour,
        Listeners.IFixUpdaterListener,
        Listeners.IFinishListener

    {
        [SerializeField]
        private LevelBounds _bounds;
        [SerializeField]
        private MoveComponent _moveComponent;
        [SerializeField]
        private InputManager _inputManager;

        public void OnFixedUpdate(float deltaTime)
        {
            MoveCharacter(_moveComponent.GOTransform.position, _inputManager._moveDirection, deltaTime);
        }

        public void MoveCharacter(Vector3 charPosition, float horizontalDirection, float timeDelta)
        {
            bool canMove =  _bounds.IsFreeByLeft(charPosition) && horizontalDirection < 0 || _bounds.IsFreeByRight(charPosition) && horizontalDirection > 0;

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
