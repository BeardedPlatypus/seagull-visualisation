using System;
using System.Collections;
using System.Linq;
using PathLib;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Core.Domain;
using Seagull.Visualisation.Views.MainMenu.PageState;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage
{
    public class Controller : PageController
    {
        private Bindings _bindings;
        private SceneTransitionManager _sceneTransitionManager;
        private PageState.Controller _pageStateController;
        
        private IDialogService _dialogService;
        private IRecentProjectService _recentProjectService;

        [Inject]
        public void Init(Bindings bindings,
                         SceneTransitionManager sceneTransitionManager,
                         PageState.Controller pageStateController,
                         IDialogService dialogService,
                         IRecentProjectService recentProjectService)
        {
            _bindings = bindings;
            _sceneTransitionManager = sceneTransitionManager;
            _dialogService = dialogService;
            _pageStateController = pageStateController;
            _recentProjectService = recentProjectService;
        }

        private void Start()
        {
            _bindings.createNewProjectButton.onClick.AddListener(OnCreateNewProjectButtonClick);
            _bindings.loadProjectButton.onClick.AddListener(OnLoadProjectButtonClick);
            _bindings.selectDemoProject.onClick.AddListener(OnSelectDemoProjectButtonClick);
        }

        private void OnCreateNewProjectButtonClick() =>
            _pageStateController.Activate(PageState.Page.NewProjectPage);

        private ISceneTransitionDescription GetLoadProjectTransitionDescription(IPath path)
        {
            IEnumerator PreLoad()
            {
                yield break;
            }

            IEnumerator PostLoad()
            {
                var recentProject = new RecentProject(path, DateTime.Now);
                _recentProjectService.UpdateRecentProject(recentProject);
                yield break;
            }

            return new SceneTransitionDescription("ProjectEditor", 
                                                  PreLoad(), 
                                                  PostLoad());
        }
        
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
                _sceneTransitionManager.LoadScene(GetLoadProjectTransitionDescription(path));
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