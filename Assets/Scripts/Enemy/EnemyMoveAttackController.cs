using System;
using UnityEngine;

namespace ShootEmUp
{
    class EnemyMoveAttackController : MonoBehaviour,
        Listeners.IFixUpdaterListener
    {
        [SerializeField] private EnemyAttackAgent _attackAgent;
        [SerializeField] private EnemyMoveAgent _moveAgent;

        public void OnFixedUpdate(float deltaTime)
        {
            if (_moveAgent.IsReached && gameObject.activeInHierarchy)
            {
                _attackAgent.DelayedAttack(deltaTime);
            }
        }
    }
}
