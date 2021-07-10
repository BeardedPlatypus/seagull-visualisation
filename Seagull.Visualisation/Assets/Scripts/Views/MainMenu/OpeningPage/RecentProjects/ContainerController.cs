using System.Collections.Generic;
using System.Linq;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Core.Domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage.RecentProjects
{
    public class ContainerController : MonoBehaviour
    {
        private IRecentProjectService _recentProjectService;
        private ElementController.Factory _elementFactory;

        [Inject]
        public void Init(IRecentProjectService recentProjectService,
                         ElementController.Factory elementFactory)
        {
            _recentProjectService = recentProjectService;
            _elementFactory = elementFactory;
        }
        
        private void Start()
        {
            var retrieveFunc = Observable.Start(RetrieveRecentProjects);
            Observable.WhenAll(retrieveFunc)
                      .ObserveOnMainThread()
                      .Subscribe(xs => CreateRecentProjects(xs[0]));
        }

        private IList<RecentProject> RetrieveRecentProjects() =>
            _recentProjectService.GetRecentProjects()
                                 .ToList();

        private void CreateRecentProjects(IList<RecentProject> recentProjects)
        {
            foreach (RecentProject recentProject in recentProjects)
            {
                var element = _elementFactory.Create(recentProject);
                element.transform.SetParent(transform);
            }
        }
    }
}
