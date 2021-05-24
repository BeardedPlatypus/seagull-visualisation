using Seagull.Visualisation.UserInterface.FileDialogs;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFileDialogService>()
                     .To<StandAloneFileBrowserFileDialogService>()
                     .AsSingle();

            Container.Bind<IDialogService>()
                     .To<DialogService>()
                     .AsSingle()
                     .NonLazy();
        }
    }

    public class Greeter
    {
        public Greeter(string message)
        {
            Debug.Log(message);
        }
    }
}