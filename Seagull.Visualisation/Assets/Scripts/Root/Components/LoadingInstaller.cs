using Seagull.Visualisation.Components.Loading;
using Zenject;

namespace Seagull.Visualisation.Root.Components
{
    public class LoadingInstaller : MonoInstaller
    {
        public SceneTransitionManager sceneTransitionManager;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneTransitionManager>().FromInstance(sceneTransitionManager);
        }
    }
}