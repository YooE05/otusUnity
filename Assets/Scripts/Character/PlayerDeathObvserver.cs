using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayerDeathObvserver : MonoBehaviour,
        Listeners.IStartListener,
        Listeners.IFinishListener
    {
        [SerializeField]
        private HitPointsComponent _playerHP;

        [SerializeField]
        private GameManager _gameManager;
       
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