using BeardedPlatypus.OrbitCamera.Core;
using Seagull.Visualisation.Components.Camera;
using Zenject;

namespace Seagull.Visualisation.Root.Components
{
    /// <summary>
    /// <see cref="CameraInstaller"/> provides the OrbitCamera components specifics
    /// to Seagull.Visualisation.
    /// </summary>
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CameraInputActions>()
                     .To<CameraInputActions>()
                     .AsSingle();
            Container.Bind<IBindings>()
                     .To<CameraBindings>()
                     .FromNewComponentOnNewGameObject()
                     .AsSingle();
        }
    }
}