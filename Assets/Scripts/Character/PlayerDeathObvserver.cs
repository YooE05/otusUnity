using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayerDeathObvserver : MonoBehaviour
    {
        [SerializeField]
        private HitPointsComponent characterHP;

        [SerializeField]
        private GameManager gameManager;

        private void OnEnable()
        {
            this.characterHP.OnHpIsEmpty += FinishGame;
        }

        private void OnDisable()
        {
            this.characterHP.OnHpIsEmpty -= FinishGame;
        }

        private void FinishGame(GameObject _)
        {
            gameManager.FinishGame();
        }
    }
}