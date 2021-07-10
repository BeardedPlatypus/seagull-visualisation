using Zenject;

using Common = Seagull.Visualisation.Views.MainMenu.Common;
using NewProjectPage = Seagull.Visualisation.Views.MainMenu.NewProjectPage;
using OpeningPage = Seagull.Visualisation.Views.MainMenu.OpeningPage;

namespace Seagull.Visualisation.Root.Views
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<NewProjectPage.State, NewProjectPage.State.Factory>();
            Container.Bind<Common.SceneTransitionFactory>().AsSingle();

            Container.Bind<OpeningPage.Controller>()
                     .To<OpeningPage.Controller>()
                     .AsSingle();
            Container.Bind<NewProjectPage.Controller>()
                     .To<NewProjectPage.Controller>()
                     .AsSingle();
        }
    }
}