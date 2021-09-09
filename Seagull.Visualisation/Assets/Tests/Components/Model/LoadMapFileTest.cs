using System.Linq;
using NUnit.Framework;
using PathLib;
using Seagull.Visualisation.Core.Application.Model;
using Seagull.Visualisation.Core.Domain.Model;
using Seagull.Visualisation.Core.Infrastructure.Model;
using Seagull.Visualisation.Core.Persistence.Model.NetCDF;

namespace Seagull.Visualisation.Tests.Components.Model
{
    public class LoadMapFileTest
    {
        [Test]
        public void LoadMapFile_RetrievesCorrectValues()
        {
            var transformationService = new CoordinateSystemTransformationService();
            var netcdfStrategy = new ModelRepositoryCreationStrategyNetCDF(transformationService, 
                                                                           new EPSGCode(3857));

            var factory = new ModelRepositoryFactory(new[] {netcdfStrategy});
            IPath modelPath = Paths.Create(TestContext.CurrentContext.TestDirectory)
                                   .Parent()
                                   .Parent()
                                   .Join("Assets", "Tests", "Components", "test-data", "map.nc");
            
            Assert.That(factory.CanCreateFor(modelPath));

            var modelRepository = factory.Create(modelPath);

            Assert.That(modelRepository.RetrieveMeshes1D(), Is.Empty);
            Assert.That(modelRepository.RetrieveMeshes2D(), Has.Length.EqualTo(1));

            IMesh2D mesh2d = modelRepository.RetrieveMeshes2D().First();
            
            Assert.That(mesh2d.RetrieveName(), Is.EqualTo("mesh2d"));
            Assert.That(mesh2d.RetrieveVertices(), Has.Length.EqualTo(36));
            Assert.That(mesh2d.RetrieveEdges(), Has.Length.EqualTo(60));
            Assert.That(mesh2d.RetrieveFaces(), Has.Length.EqualTo(25));
        }
    }
}