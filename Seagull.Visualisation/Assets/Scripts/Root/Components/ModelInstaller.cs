using Seagull.Visualisation.Core.Application.Model;
using Seagull.Visualisation.Core.Persistence.Model.NetCDF;
using Zenject;

namespace Seagull.Visualisation.Root.Components
{
    /// <summary>
    /// <see cref="ModelInstaller"/> provides the dependencies related to the creation
    /// of models.
    /// </summary>
    public class ModelInstaller : MonoInstaller
    {
        // TODO: Do we want to keep this here
        // Alternatively we could move it to the creation of the ModelRepository
        private readonly EPSGCode _projectEPSG = new EPSGCode(3857);
        private ICoordinateSystemTransformationService _coordinateService;

        [Inject]
        private void Init(ICoordinateSystemTransformationService coordinateService)
        {
            _coordinateService = coordinateService;
        }
        
        public override void InstallBindings()
        {
            var factory = new ModelRepositoryFactory(new[]
            {
                new ModelRepositoryCreationStrategyNetCDF(_coordinateService, _projectEPSG)
            });

            Container.Bind<ModelRepositoryFactory>()
                     .FromInstance(factory)
                     .AsSingle();
        }
    }
}