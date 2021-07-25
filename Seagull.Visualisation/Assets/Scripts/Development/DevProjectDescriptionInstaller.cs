using Seagull.Visualisation.Components.Project;
using Zenject;

namespace Seagull.Visualisation.Development
{
    /// <summary>
    /// <see cref="DevProjectDescriptionInstaller"/> provides a test description in
    /// development scenes.
    /// </summary>
    public class DevProjectDescriptionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var projectDescription = new ProjectDescription(DevelopmentConfig.TestModelPath);
            Container.Bind<ProjectDescription>().FromInstance(projectDescription);
        }
    }
}