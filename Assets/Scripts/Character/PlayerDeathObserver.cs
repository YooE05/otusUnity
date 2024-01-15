using UnityEngine;

namespace ShootEmUp
{
    public class PlayerDeathObserver :
        Listeners.IStartListener,
        Listeners.IFinishListener
    {
        private HitPointsComponent _playerHP;
        private GameManager _gameManager;

        public PlayerDeathObserver(GameManager gameManager, HitPointsComponent playerHP)
        {
            _gameManager = gameManager;
            _playerHP = playerHP;
        }

        public void OnStart()
        {
            _playerHP.OnHitpointsEmpty += FinishGame;
        }
        public void OnFinish()
        {
           _playerHP.OnHitpointsEmpty -= FinishGame;
        }
        
        private void FinishGame(GameObject _)
        {
            _gameManager.OnFinish();
        }


    }
}