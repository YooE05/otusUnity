using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace ShootEmUp
{
    class LevelIntstaller : MonoInstaller
    {
        [SerializeField] private TestPlayer _testPlayer;

        public override void InstallBindings()
        {
            Container.Bind<TestPlayer>().FromInstance(_testPlayer).AsSingle();
            Container.Bind<SmthController>().AsSingle().NonLazy();

        }


    }
}