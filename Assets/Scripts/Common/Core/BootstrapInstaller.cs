using System.Collections;
using System.Collections.Generic;
using Zenject;


namespace ShootEmUp
{
    public sealed class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
    {

        public override void InstallBindings()
        {
            Container.Bind<ILogger>().To<Logger>().AsSingle().NonLazy();
        }


    }
}