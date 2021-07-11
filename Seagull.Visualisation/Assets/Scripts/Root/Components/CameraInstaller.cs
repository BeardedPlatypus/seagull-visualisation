using Seagull.Visualisation.Components.Camera;
using Zenject;

namespace Seagull.Visualisation.Root.Components
{
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MouseControls>()
                     .To<MouseControls>()
                     .AsSingle();
            Container.Bind<EditorCameraInputBindings>()
                     .FromNewComponentOnNewGameObject()
                     .AsSingle();
        }
    }
}