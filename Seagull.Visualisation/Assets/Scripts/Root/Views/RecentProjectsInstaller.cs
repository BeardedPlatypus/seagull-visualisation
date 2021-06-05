using Seagull.Visualisation.Core.Domain;
using UnityEngine;
using Zenject;
using RecentProjects = Seagull.Visualisation.Views.MainMenu.OpeningPage.RecentProjects;

namespace Seagull.Visualisation.Root.Views
{
    public class RecentProjectsInstaller : MonoInstaller
    {
        public GameObject elementPrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<RecentProject, RecentProjects.ElementController, RecentProjects.ElementController.Factory>()
                     .FromComponentInNewPrefab(elementPrefab)
                     .AsSingle();
        }
    }
}