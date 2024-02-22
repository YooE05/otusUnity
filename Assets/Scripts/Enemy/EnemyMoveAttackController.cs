using UnityEngine;

namespace ShootEmUp
{
    internal sealed class EnemyMoveAttackController : 
        Listeners.IFixUpdaterListener
    {
        private readonly EnemyAttackAgent _attackAgent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly GameObject _enemyGO;

        public EnemyMoveAttackController(GameObject enemyGO, EnemyMoveAgent moveAgent, EnemyAttackAgent attackAgent)
        {
            _enemyGO = enemyGO;
            _moveAgent = moveAgent;
            _attackAgent = attackAgent;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_moveAgent.IsReached && _enemyGO.activeInHierarchy)
            {
                _attackAgent.DelayedAttack(deltaTime);
            }
        }
    }
}
