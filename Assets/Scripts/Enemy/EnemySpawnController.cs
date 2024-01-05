using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawnController : MonoBehaviour,
        Listeners.IUpdateListener,
        Listeners.IInitListener,
        Listeners.IStartListener,
        Listeners.IFinishListener,
        Listeners.IPauseListener,
        Listeners.IResumeListener
    {
        [SerializeField]
        private EnemiesManager _enemiesManager;

        [SerializeField]
        private float _initSpawnDelay;

        [SerializeField]
        private float _newSpawnDelay;

        [SerializeField]
        private int _initEnemiesCnt;

        public bool _CanUpdate { get => _canUpdate; set => _canUpdate = value; }
        private bool _canUpdate;
        private float _timer;

        public void OnUpdate(float deltaTime)
        {
            if (_canUpdate)
            {
                _timer += deltaTime;
            }
        }

        public void OnInit()
        {
            _canUpdate = false;
        }
        public void OnStart()
        {
            _canUpdate = true;

            _timer = 0f;
            _enemiesManager.OnEnemyDied += SpawnNewEnemy;
            StartCoroutine(InitEnemySpawn());
        }

        public void OnFinish()
        {
            _enemiesManager.OnEnemyDied -= SpawnNewEnemy;
        }

        public void OnPause()
        {
            _canUpdate = false;
        }

        public void OnResume()
        {
            _canUpdate = true;
        }



        private IEnumerator InitEnemySpawn()
        {
            int i = _initEnemiesCnt;
            while (i > 0)
            {
                float needTime = _timer + _initSpawnDelay;
                StartCoroutine(SpawnByDelay(_initSpawnDelay));

                while (true)
                {
                    if (_timer >= needTime)
                    { break; }
                    yield return null;
                }

                i--;
            }
        }

        private void SpawnNewEnemy()
        {
            StartCoroutine(SpawnByDelay(_newSpawnDelay));
        }

        private IEnumerator SpawnByDelay(float delay)
        {
            float needTime = _timer + delay;

            while (true)
            {
                if (_timer >= needTime)
                {
                    _enemiesManager.InitNewEnemy();
                    break;
                }

                yield return null;
            }

            yield return null;
        }
    }
}