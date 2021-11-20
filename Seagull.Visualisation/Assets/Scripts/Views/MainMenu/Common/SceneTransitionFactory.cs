using System;
using System.Collections;
using PathLib;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Core.Domain;

namespace Seagull.Visualisation.Views.MainMenu.Common
{
    public sealed class SceneTransitionFactory
    {
        private const string ProjectEditorSceneName = "ProjectEditor";

        private readonly IRecentProjectService _recentProjectService;
        private readonly IProjectService _projectService;

        public SceneTransitionFactory(IRecentProjectService recentProjectService, 
                                      IProjectService projectService)
        {
            _recentProjectService = recentProjectService;
            _projectService = projectService;
        }

        public IViewTransitionDescription GetLoadProjectTransition(IPath projectManifestPath)
        {
            IEnumerator PreLoad()
            {
                yield break;
            }

            IEnumerator PostLoad()
            {
                var recentProject = new RecentProject(projectManifestPath, DateTime.Now);
                _recentProjectService.UpdateRecentProject(recentProject);
                yield break;
            }

            return new ViewTransitionDescription(ProjectEditorSceneName, PreLoad(), PostLoad());
        }

        public IViewTransitionDescription GetCreateProjectTransition(NewProjectPage.State state)
        {
            IEnumerator PreLoad()
            {
                yield return null;
                _projectService.CreateProject(state.ProjectPath);
            }

            IEnumerator PostLoad()
            {
                var recentProject = new RecentProject(state.ProjectPath, DateTime.Now);
                _recentProjectService.UpdateRecentProject(recentProject);

                yield break;
            }

            return new ViewTransitionDescription(ProjectEditorSceneName, PreLoad(), PostLoad());
        }
    }
}
