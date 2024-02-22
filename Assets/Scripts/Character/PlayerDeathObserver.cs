using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerDeathObserver :
        Listeners.IStartListener,
        Listeners.IFinishListener
    {
        private readonly HitPointsComponent _playerHitPoints;
        private readonly GameManager _gameManager;

        public PlayerDeathObserver(GameManager gameManager, HitPointsComponent playerHP)
        {
            _gameManager = gameManager;
            _playerHitPoints = playerHP;
        }

        public void OnStart()
        {
            _playerHitPoints.OnHitpointsEmpty += FinishGame;
        }

        public void OnFinish()
        {
           _playerHitPoints.OnHitpointsEmpty -= FinishGame;
        }

        private void FinishGame(GameObject _)
        {
            _gameManager.OnFinish();
        }
    }
}