using PathLib;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Core.Persistence.AppDataRepository;
using Seagull.Visualisation.Core.Persistence.Projects;
using Seagull.Visualisation.Core.Persistence.RecentProjects;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Root
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAppDataRepository>()
                     .To<AppDataRepositoryJson>()
                     .AsSingle()
                     .WithArguments(Paths.Create(Application.persistentDataPath));
            
            Container.Bind<IRecentProjectService>()
                     .To<RecentProjectServiceJson>()
                     .AsSingle();
            
            Container.Bind<IProjectService>()
                     .To<ProjectServiceJson>()
                     .AsSingle();
        }
    }
}