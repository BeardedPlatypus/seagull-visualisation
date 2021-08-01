using Seagull.Visualisation.Components.Project;
using Seagull.Visualisation.Core.Application.Model;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Development
{
    /// <summary>
    /// <see cref="DevProjectDescriptionInstaller"/> provides a test description in
    /// development scenes.
    /// </summary>
    public class DevProjectDescriptionInstaller : MonoInstaller
    {
        private ModelRepositoryFactory _modelRepositoryFactory;

        [Inject]
        private void Init(ModelRepositoryFactory modelRepositoryFactory)
        {
            _modelRepositoryFactory = modelRepositoryFactory;
        }

        public override void InstallBindings()
        {
            if (!_modelRepositoryFactory.CanCreateFor(DevelopmentConfig.TestModelPath))
            {
                Debug.Log($"Cannot construct a IModelRepository from {DevelopmentConfig.TestModelPath}");
                return;
            }

            var modelRepository = 
                _modelRepositoryFactory.Create(DevelopmentConfig.TestModelPath);
            var projectDescription = new ProjectDescription(
                DevelopmentConfig.TestModelPath, 
                modelRepository);
            
            Container.Bind<ProjectDescription>().FromInstance(projectDescription);
        }
    }
}