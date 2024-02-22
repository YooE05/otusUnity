using System.Collections.Generic;

namespace ShootEmUp
{
    public class EnemySpawnController :
        Listeners.IStartListener,
        Listeners.IFinishListener
    {

        private readonly GameManager _gameManager;
        private readonly EnemiesManager _enemiesManager;
        private readonly EnemySystemConfig _config;

        private UpdatebleCountdown _initSpawnCountdown = new UpdatebleCountdown();
        private List<UpdatebleCountdown> _playSpawnCountdowns = new();

        public EnemySpawnController(GameManager gameManager, EnemiesManager enemiesManager, EnemySystemConfig config)
        {
            _gameManager = gameManager;
            _enemiesManager = enemiesManager;
            _config = config;
        }

        public void OnStart()
        {
            _initSpawnCountdown.OnValueChanged += InitEnemy;
            _initSpawnCountdown.OnCountdownEnded += RemoveCountdownListener;

            _enemiesManager.OnEnemyDied += SpawnNewEnemy;

            _gameManager.AddListener(_initSpawnCountdown);

            _initSpawnCountdown.StartTimer(_config.SimultaneousEnemiesCount, _config.InitSpawnDelay);
        }

        public void OnFinish()
        {
            _enemiesManager.OnEnemyDied -= SpawnNewEnemy;
        }

        private void RemoveCountdownListener(Countdown countdown)
        {
            _initSpawnCountdown.OnValueChanged -= InitEnemy;
            _initSpawnCountdown.OnCountdownEnded -= RemoveCountdownListener;
        }

        private void SpawnNewEnemy()
        {
            var spawnCountdown = new UpdatebleCountdown();
            spawnCountdown.OnCountdownEnded += InitEnemy;

            _gameManager.AddListener(spawnCountdown);
            _playSpawnCountdowns.Add(spawnCountdown);

            spawnCountdown.StartTimer(1, _config.NewSpawnDelay);
        }
       
        private void InitEnemy(int _)
        {
            _enemiesManager.InitNewEnemy();
        }
        
        private void InitEnemy(Countdown countdown)
        {
            countdown.OnCountdownEnded -= InitEnemy;
            _playSpawnCountdowns.Remove((UpdatebleCountdown)countdown);
            _gameManager.RemoveListener((UpdatebleCountdown)countdown);
            _enemiesManager.InitNewEnemy();
        }
    }
}