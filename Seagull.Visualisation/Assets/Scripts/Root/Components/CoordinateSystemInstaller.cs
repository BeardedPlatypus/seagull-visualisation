using Seagull.Visualisation.Core.Application.Model;
using Seagull.Visualisation.Core.Infrastructure.Model;
using Zenject;

namespace Seagull.Visualisation.Root.Components
{
    /// <summary>
    /// <see cref="CoordinateSystemInstaller"/> provides the dependencies related to
    /// interacting with coordinate systems / EPSG codes.
    /// </summary>
    public class CoordinateSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoordinateSystemTransformationService>()
                     .To<CoordinateSystemTransformationService>()
                     .AsSingle();
        }
    }
}
