using System;
using System.Collections;
using PathLib;
using Seagull.Visualisation.Components.Camera.Messages;
using Seagull.Visualisation.Components.Common;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Components.Loading.Messages;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Core.Domain;

namespace Seagull.Visualisation.Views.MainMenu.Common
{
    public sealed class ViewTransitionFactory
    {
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
            IPublishableMessage[] loadMessages =
            {
            };
            
            IPublishableMessage[] postLoadMessages =
            {
                new UpdateRecentProjectMessage(new RecentProject(projectManifestPath, DateTime.Now)),
                new SetIsActiveMessage(true),
                new MainMenuToggleMessage(false),
                new EditorUserInterfaceToggleMessage(true),
            };
            
            return new ViewTransitionDescription(loadMessages, postLoadMessages);
        }

        public IViewTransitionDescription GetCreateProjectTransition(NewProjectPage.State state)
        {
            // TODO: move this to a message.
            IEnumerator PreLoad()
            {
                yield return null;
                _projectService.CreateProject(state.ProjectPath);
            }

            IPublishableMessage[] loadMessages =
            {
            };
            
            IPublishableMessage[] postLoadMessages =
            {
                new UpdateRecentProjectMessage(new RecentProject(state.ProjectPath, DateTime.Now)),
                new SetIsActiveMessage(true),
                new MainMenuToggleMessage(false),
                new EditorUserInterfaceToggleMessage(true),
            };
            
            return new ViewTransitionDescription(loadMessages, postLoadMessages);
        }
    }
}
