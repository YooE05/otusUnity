using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayerDeathObvserver : MonoBehaviour
    {
        [SerializeField]
        private HitPointsComponent playerHP;

        [SerializeField]
        private GameManager gameManager;

        private void OnEnable()
        {
            this.playerHP.OnHitpointsEmpty += FinishGame;
        }

        private void OnDisable()
        {
            this.playerHP.OnHitpointsEmpty -= FinishGame;
        }

        private void FinishGame(GameObject _)
        {
            this.gameManager.FinishGame();
        }
    }
}