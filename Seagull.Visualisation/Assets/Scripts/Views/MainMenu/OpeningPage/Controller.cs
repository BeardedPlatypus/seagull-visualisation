using System.Linq;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Views.MainMenu.Common;
using UniRx;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage
{
    public sealed class Controller : IPageController
    {
        private readonly SceneTransitionManager _sceneTransitionManager;
        private readonly SceneTransitionFactory _sceneTransitionFactory;
        private readonly IDialogService _dialogService;

        public Controller(SceneTransitionManager sceneTransitionManager,
                          SceneTransitionFactory sceneTransitionFactory,
                          IDialogService dialogService)
        {
            _sceneTransitionManager = sceneTransitionManager;
            _dialogService = dialogService;
            _sceneTransitionFactory = sceneTransitionFactory;
        }

        // TODO this functions could be split up in separate streams of selecting a path and handling it
        public void OnLoadProject()
        {
            var configuration = new FileDialogConfiguration
            {
                ExtensionFilters = new[]
                {
                    ExtensionFilter.Predefined.SeagullProjectFiles,
                    ExtensionFilter.Predefined.AllFiles,
                }
            };
            
            var path = _dialogService.OpenFileDialog(configuration).FirstOrDefault();

            if (path != null)
            {
                _sceneTransitionManager.LoadScene(_sceneTransitionFactory.GetLoadProjectTransition(path));
            }
        }
        
        public void OnSelectDemoProject() { }

        public ReactiveProperty<bool> IsActive { get; } = new ReactiveProperty<bool>(true);
    }
}