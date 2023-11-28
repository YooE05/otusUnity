using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private CharacterController characterController;

        private void OnEnable()
        {
            this.characterController.OnPlayerDied += FinishGame;
        }

        private void OnDisable()
        {
            this.characterController.OnPlayerDied -= FinishGame;
        }

        private void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}