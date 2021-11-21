using System;
using System.Collections;
using System.Linq;
using PathLib;
using Seagull.Visualisation.Components.Camera.Messages;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Core.Domain;

namespace Seagull.Visualisation.Views.MainMenu.Common
{
    public sealed class ViewTransitionFactory
    {
        private const string ProjectEditorSceneName = "ProjectEditor";

        private readonly IRecentProjectService _recentProjectService;
        private readonly IProjectService _projectService;

        public ViewTransitionFactory(IRecentProjectService recentProjectService, 
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
            
            // TODO: move this to a message.
            IEnumerator PostLoad()
            {
                var recentProject = new RecentProject(projectManifestPath, DateTime.Now);
                _recentProjectService.UpdateRecentProject(recentProject);
                yield break;
            }

            var loadMessages = Enumerable.Empty<object>();
            var postLoadMessages = new[] {new SetIsActiveMessage(true)};
            
            return new ViewTransitionDescription(loadMessages, postLoadMessages);
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

            var loadMessages = Enumerable.Empty<object>();
            var postLoadMessages = new[] {new SetIsActiveMessage(true)};
            
            return new ViewTransitionDescription(loadMessages, postLoadMessages);
        }
    }
}
