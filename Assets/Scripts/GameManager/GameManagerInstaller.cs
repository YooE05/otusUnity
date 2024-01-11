using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    class GameManagerInstaller: MonoBehaviour
    {
        private GameManager _gameManager;
        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
            _gameManager.AddListeners(gameObject);
        }

    }
}
