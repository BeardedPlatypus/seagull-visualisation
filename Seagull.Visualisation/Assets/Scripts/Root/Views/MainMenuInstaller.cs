using Seagull.Visualisation.Views.MainMenu.CreateProjects;
using Zenject;

namespace Seagull.Visualisation.Root.Views
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<NewProjectState, NewProjectState.Factory>();
        }
    }
}