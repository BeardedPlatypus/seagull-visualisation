using System.Collections;
using System.Collections.Generic;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Core.Domain;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage.RecentProjects
{
    public class ContainerController : MonoBehaviour
    {
        private IRecentProjectsService _recentProjectsService;
        private ElementController.Factory _elementFactory;

        [Inject]
        public void Init(IRecentProjectsService recentProjectsService,
                         ElementController.Factory elementFactory)
        {
            _recentProjectsService = recentProjectsService;
            _elementFactory = elementFactory;
        }
        
        private void Start()
        {
            StartCoroutine(LoadRecentProjectsCoroutine());
        }

        private IEnumerator LoadRecentProjectsCoroutine()
        {
            yield return null;
            IEnumerable<RecentProject> recentProjects = _recentProjectsService.GetRecentProjects();
            
            foreach (var project in recentProjects)
            {
                ElementController elementController = _elementFactory.Create(project);
                elementController.transform.SetParent(transform);
                yield return null;
            }
        }
    }
}
