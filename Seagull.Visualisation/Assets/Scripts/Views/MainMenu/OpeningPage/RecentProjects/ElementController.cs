using System.Globalization;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Core.Domain;
using Seagull.Visualisation.Views.MainMenu.Common;
using UniRx;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage.RecentProjects
{
    public class ElementController : MonoBehaviour
    {
        public TMPro.TMP_Text projectNameLabel;
        public TMPro.TMP_Text projectPathLabel;
        public TMPro.TMP_Text lastOpenedLabel;

        private RecentProject _recentProject;
        private ViewTransitionFactory _viewTransitionFactory;

        [Inject]
        public void Init(RecentProject recentProject,
                         ViewTransitionFactory viewTransitionFactory)
        {
            _recentProject = recentProject;
            _viewTransitionFactory = viewTransitionFactory;
        }

        private void Start()
        {
            projectNameLabel.text = _recentProject.Path.Basename;
            projectPathLabel.text = _recentProject.Path.ToString();
            lastOpenedLabel.text = _recentProject.LastOpened.ToString("G", CultureInfo.CurrentCulture);
        }

        public void OnClick()
        {
            var path = PathLib.Paths.Create(_recentProject.Path.ToString());
            var transitionDescription = _viewTransitionFactory.GetLoadProjectTransition(path);
            var msg = new ChangeViewMessage(transitionDescription);
            MessageBroker.Default.Publish(msg);
        }

        public sealed class Factory : PlaceholderFactory<RecentProject, ElementController> { }
    }
}
