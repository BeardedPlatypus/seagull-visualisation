using System.Globalization;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Core.Domain;
using Seagull.Visualisation.Views.MainMenu.Common;
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
        private SceneTransitionManager _sceneTransitionManager;
        private SceneTransitionFactory _sceneTransitionFactory;

        [Inject]
        public void Init(RecentProject recentProject,
                         SceneTransitionManager sceneTransitionManager,
                         SceneTransitionFactory sceneTransitionFactory)
        {
            _recentProject = recentProject;
            _sceneTransitionManager = sceneTransitionManager;
            _sceneTransitionFactory = sceneTransitionFactory;
        }

        private void Start()
        {
            projectNameLabel.text = _recentProject.Path.Basename;
            projectPathLabel.text = _recentProject.Path.ToString();
            lastOpenedLabel.text = _recentProject.LastOpened.ToString("G", CultureInfo.CurrentCulture);
        }

        public void OnClick()
        {
            // TODO fix this properly
            var path = PathLib.Paths.Create(_recentProject.Path.ToString());
            _sceneTransitionManager.LoadScene(_sceneTransitionFactory.GetLoadProjectTransition(path));
        }

        public sealed class Factory : PlaceholderFactory<RecentProject, ElementController> { }
    }
}
