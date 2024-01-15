using Zenject;
using UnityEngine;
using System;

namespace ShootEmUp
{
    class LevelIntstaller : MonoInstaller
    {
        [SerializeField] private ObjectsSpawner _spawner;
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private BackgroundSettings _backgroundSettings;
        [SerializeField] private StartGameConfig _startGameConfig;

        [Header("Bullets")]
        [SerializeField] private Transform _bulletPoolRoot;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletsPreloadCount;

        [Header("Player")]
        [SerializeField] private HitPointsComponent _playerHP;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private WeaponComponent _weaponComponent;

        [Header("Enemy")]
        [SerializeField] private Transform _enemyPoolRoot;
        [SerializeField] private GameObject _enemyPrefab;

        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private GameObject _enemyTarget;
        [SerializeField] private EnemySystemConfig _enemySystemConfig;

        [Header("UI")]
        [SerializeField] private StartFinishUIView _startFinishView;
        [SerializeField] private PauseResumeUIView _pauseResumeView;


        public override void InstallBindings()
        {
            Container.Bind<BoundsChecker>().AsSingle().WithArguments(_levelBounds);
            Container.BindInterfacesAndSelfTo<BackgroundMover>().AsSingle().WithArguments(_backgroundSettings).NonLazy();
            Container.Bind<ObjectsSpawner>().FromInstance(_spawner).AsSingle();

            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().WithArguments(gameObject);


            BulletsSystemBind();

            PLayerSystemBind();
            EnemySystemBind();

            UI();
        }

        private void BulletsSystemBind()
        {
            Container.BindInterfacesAndSelfTo<BulletPool>().AsSingle().WithArguments(_bulletPoolRoot, _bulletPrefab, _bulletsPreloadCount);
            Container.BindInterfacesAndSelfTo<BulletSystem>().AsSingle().NonLazy();
        }
        private void PLayerSystemBind()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle().WithArguments(_moveComponent).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShootController>().AsSingle().WithArguments(_weaponComponent).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerDeathObserver>().AsSingle().WithArguments(_playerHP).NonLazy();
        }
        private void EnemySystemBind()
        {
            Container.Bind<EnemySystemConfig>().FromInstance(_enemySystemConfig).AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyPool>().AsSingle().WithArguments(_enemyPoolRoot, _enemyPrefab);
            Container.Bind<EnemyPositionsManager>().AsSingle().WithArguments(_enemyPositions);
            Container.Bind<EnemiesManager>().AsSingle().WithArguments(_enemyTarget).NonLazy();
            Container.BindInterfacesAndSelfTo<EnemySpawnController>().AsSingle().NonLazy();
        }
        private void UI()
        {
            Container.BindInterfacesAndSelfTo<StartFinishUIController>().AsSingle().WithArguments(_startFinishView, _startGameConfig).NonLazy();
            Container.BindInterfacesAndSelfTo<PauseResumeUIController>().AsSingle().WithArguments(_pauseResumeView).NonLazy();
        }
    }
}