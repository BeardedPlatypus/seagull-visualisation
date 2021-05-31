using System;
using System.Globalization;
using UnityEngine;
using Zenject;
using Seagull.Visualisation.Core.Domain;

namespace Seagull.Visualisation.Views.MainMenu
{
    public class RecentProjectController : MonoBehaviour
    {
        public TMPro.TMP_Text projectNameLabel;
        public TMPro.TMP_Text projectPathLabel;
        public TMPro.TMP_Text lastOpenedLabel;

        private RecentProject _recentProject;

        [Inject]
        public void Init(RecentProject recentProject)
        {
            _recentProject = recentProject;
        }

        private void Start()
        {
            projectNameLabel.text = _recentProject.Path.Basename;
            projectPathLabel.text = _recentProject.Path.ToString();
            lastOpenedLabel.text = _recentProject.LastOpened.ToString("G", CultureInfo.CurrentCulture);
        }
        
        public void OnClick() {}
    }
}
