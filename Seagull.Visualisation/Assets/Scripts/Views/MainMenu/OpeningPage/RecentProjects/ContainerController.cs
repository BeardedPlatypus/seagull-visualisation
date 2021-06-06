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
            StartCoroutine(LoadRecentProjectsCoroutine());
        }

        private IEnumerator LoadRecentProjectsCoroutine()
        {
            yield return null;
            IEnumerable<RecentProject> recentProjects = _recentProjectService.GetRecentProjects();
            
            foreach (var project in recentProjects)
            {
                ElementController elementController = _elementFactory.Create(project);
                elementController.transform.SetParent(transform);
                yield return null;
            }
        }
    }
}
