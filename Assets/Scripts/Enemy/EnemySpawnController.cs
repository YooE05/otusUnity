using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawnController : MonoBehaviour,
        Listeners.IStartListener,
        Listeners.IFinishListener
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private EnemiesManager _enemiesManager;

        [SerializeField]
        private float _initSpawnDelay;

        [SerializeField]
        private float _newSpawnDelay;

        [SerializeField]
        private int _initEnemiesCnt;

        private UpdatebleCountdown _initSpawnCountdown = new UpdatebleCountdown();
        private List<UpdatebleCountdown> _playSpawnCountdowns = new();


        public void OnStart()
        {
            _initSpawnCountdown.OnValueChanged += InitEnemy;
            _initSpawnCountdown.OnCountdownEnded += RemoveCountdownListener;

            _enemiesManager.OnEnemyDied += SpawnNewEnemy;

            _gameManager.AddListener(_initSpawnCountdown);

            _initSpawnCountdown.StartTimer(_initEnemiesCnt, _initSpawnDelay);
        }

        public void OnFinish()
        {
            _enemiesManager.OnEnemyDied -= SpawnNewEnemy;
        }

        private void RemoveCountdownListener(Countdown countdown)
        {
            _initSpawnCountdown.OnValueChanged -= InitEnemy;
            _initSpawnCountdown.OnCountdownEnded -= RemoveCountdownListener;
            _initSpawnCountdown.Dispose();
        }

        private void SpawnNewEnemy()
        {
            var spawnCountdown = new UpdatebleCountdown();
            spawnCountdown.OnCountdownEnded += InitEnemyAndDespose;

            _gameManager.AddListener(spawnCountdown);
            _playSpawnCountdowns.Add(spawnCountdown);

            spawnCountdown.StartTimer(1, _newSpawnDelay);
        }
        private void InitEnemy(int _)
        {
            _enemiesManager.InitNewEnemy();
        }
        private void InitEnemyAndDespose(Countdown countdown)
        {
            countdown.OnCountdownEnded -= InitEnemyAndDespose;
            _playSpawnCountdowns.Remove((UpdatebleCountdown)countdown);
            _gameManager.RemoveListener((UpdatebleCountdown)countdown);
            countdown.Dispose();
            _enemiesManager.InitNewEnemy();
        }

    }
}