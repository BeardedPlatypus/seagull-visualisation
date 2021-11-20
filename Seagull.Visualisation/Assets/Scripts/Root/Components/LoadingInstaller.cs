using Seagull.Visualisation.Components.Loading;
using Zenject;

namespace Seagull.Visualisation.Root.Components
{
    /// <summary>
    /// <see cref="LoadingInstaller"/> defines the dependencies related to Loading.
    /// </summary>
    public sealed class LoadingInstaller : MonoInstaller
    {
        /// <summary>
        /// The <see cref="viewTransitionManager"/> of the scene.
        /// </summary>
        public ViewTransitionManager viewTransitionManager;
        
        public override void InstallBindings()
        {
            Container.Bind<ViewTransitionManager>().FromInstance(viewTransitionManager);
        }
    }
}