using System.Linq;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Views.MainMenu.Common;
using Seagull.Visualisation.Views.MainMenu.PageState;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage
{
    public class Controller : PageController
    {
        private Bindings _bindings;
        private SceneTransitionManager _sceneTransitionManager;
        private PageState.Controller _pageStateController;
        private SceneTransitionFactory _sceneTransitionFactory;
        
        private IDialogService _dialogService;

        [Inject]
        public void Init(Bindings bindings,
                         SceneTransitionManager sceneTransitionManager,
                         PageState.Controller pageStateController,
                         SceneTransitionFactory sceneTransitionFactory,
                         IDialogService dialogService)
        {
            _bindings = bindings;
            _sceneTransitionManager = sceneTransitionManager;
            _dialogService = dialogService;
            _pageStateController = pageStateController;
            _sceneTransitionFactory = sceneTransitionFactory;
        }

        private void Start()
        {
            _bindings.createNewProjectButton.onClick.AddListener(OnCreateNewProjectButtonClick);
            _bindings.loadProjectButton.onClick.AddListener(OnLoadProjectButtonClick);
            _bindings.selectDemoProject.onClick.AddListener(OnSelectDemoProjectButtonClick);
        }

        private void OnCreateNewProjectButtonClick() =>
            _pageStateController.Activate(PageState.Page.NewProjectPage);

        private void OnLoadProjectButtonClick()
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
        private void OnSelectDemoProjectButtonClick() { }
        
        public override void Activate()
        {
            // No-op
        }

        public override void Deactivate()
        {
            // No-op
        }
    }
}