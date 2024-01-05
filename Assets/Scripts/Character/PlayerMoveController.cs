using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public class PlayerMoveController : MonoBehaviour,
        Listeners.IFixUpdaterListener,
        Listeners.IInitListener,
        Listeners.IStartListener,
        Listeners.IFinishListener,
        Listeners.IPauseListener,
        Listeners.IResumeListener
    {
        [SerializeField]
        private LevelBounds _bounds;
        [SerializeField]
        private MoveComponent _moveComponent;
        [SerializeField]
        private InputManager _inputManager;
        public bool _CanUpdate { get => _canUpdate; set => _canUpdate = value; }
        private bool _canUpdate;

        public void OnFixedUpdate(float deltaTime)
        {
            if (_canUpdate)
            {
                MoveCharacter(_moveComponent.GOTransform.position, _inputManager._moveDirection, deltaTime);
            }

        }

        public void MoveCharacter(Vector3 charPosition, float horizontalDirection, float timeDelta)
        {
            bool canMove = this._bounds.IsFreeByLeft(charPosition) && horizontalDirection < 0 || this._bounds.IsFreeByRight(charPosition) && horizontalDirection > 0;

            if (canMove)
            {
                this._moveComponent.MoveByRigidbody(new Vector2(horizontalDirection, 0) * timeDelta);
            }

        }

        public void OnInit()
        {
            _canUpdate = false;
        }
        public void OnStart()
        {

            _canUpdate = true;
        }

        public void OnFinish()
        {
            _canUpdate = false;
            _moveComponent.gameObject.SetActive(false);
        }

        public void OnPause()
        {
            _canUpdate = false;
        }

        public void OnResume()
        {
            _canUpdate = true;
        }
    }

}
