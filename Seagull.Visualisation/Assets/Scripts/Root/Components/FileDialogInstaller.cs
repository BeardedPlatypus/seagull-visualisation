using Seagull.Visualisation.Components.FileDialogs;
using Zenject;

namespace Seagull.Visualisation.Root.Components
{
    public class FileDialogInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFileDialogService>()
                     .To<FileDialogService>()
                     .AsSingle();

            Container.Bind<IDialogService>()
                     .To<DialogService>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}