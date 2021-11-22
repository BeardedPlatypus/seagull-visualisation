using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Views.MainMenu.Common;
using UniRx;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu
{
    /// <summary>
    /// <see cref="RecentProjectsServiceWrapper"/> provides a wrapper around the
    /// <see cref="IRecentProjectService"/>, forwarding any relevant messages to
    /// it.
    /// </summary>
    public sealed class RecentProjectsServiceWrapper : MonoBehaviour
    {
        private IRecentProjectService _service;
        
        [Inject]
        private void Init(IRecentProjectService recentProjectService)
        {
            _service = recentProjectService;
        }

        private void Start()
        {
            MessageBroker.Default.Receive<UpdateRecentProjectMessage>()
                                 .Subscribe(UpdateRecentProject)
                                 .AddTo(this);
        }

        private void UpdateRecentProject(UpdateRecentProjectMessage msg) =>
            _service.UpdateRecentProject(msg.RecentProject);
    }
}
