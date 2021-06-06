using Zenject;

using Common = Seagull.Visualisation.Views.MainMenu.Common;
using NewProjectPage = Seagull.Visualisation.Views.MainMenu.NewProjectPage;
using OpeningPage = Seagull.Visualisation.Views.MainMenu.OpeningPage;
using PageState = Seagull.Visualisation.Views.MainMenu.PageState;

namespace Seagull.Visualisation.Root.Views
{
    public class MainMenuInstaller : MonoInstaller
    {
        public NewProjectPage.Bindings newProjectBindings;
        public OpeningPage.Bindings openingPageBindings;

        public PageState.Controller pageStateController;
        
        public override void InstallBindings()
        {
            Container.BindFactory<NewProjectPage.State, NewProjectPage.State.Factory>();
            Container.Bind<NewProjectPage.Bindings>().FromInstance(newProjectBindings);
            Container.Bind<OpeningPage.Bindings>().FromInstance(openingPageBindings);
            Container.Bind<PageState.Controller>().FromInstance(pageStateController);
            Container.Bind<Common.SceneTransitionFactory>().AsSingle();
        }
    }
}