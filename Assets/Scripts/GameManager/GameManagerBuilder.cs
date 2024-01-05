using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    class GameManagerBuilder: MonoBehaviour
    {
        public GameManager _gameManager;
        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
            AddListeners(this.gameObject);

        }

        public void AddListeners(GameObject root)
        {
            var listeners = root.GetComponentsInChildren<Listeners.IGameListener>();

            for (int i = 0; i < listeners.Length; i++)
            {
                _gameManager.AddListener(listeners[i]);
            }
        }

    }
}
