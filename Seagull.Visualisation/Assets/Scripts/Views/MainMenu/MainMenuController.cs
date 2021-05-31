using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Core.Application;
using Seagull.Visualisation.Views.MainMenu.CreateProjects;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        private IDialogService _dialogService;
        private NewProjectState.Factory _newProjectStateFactory;
        private IProjectService _projectService;

        // TODO: introduce some underlying state for this?
        public GameObject recentProjectsMenu;
        public GameObject createNewProjectMenu;

        private SceneTransitionManager _sceneTransitionManager;

        [CanBeNull]
        private NewProjectState ProjectState { get; set; }
        
        private void RefreshCreateNewProjectView()
        {
            if (ProjectState == null) return;

            projectFilePath.text = ProjectState.ProjectPath?.ToString() ?? "";
            mapFilePath.text = ProjectState.MapFilePath?.ToString() ?? "";
        }
        
        [Inject]
        public void Init(IDialogService dialogService,
                         SceneTransitionManager sceneTransitionManager,
                         NewProjectState.Factory newProjectStateFactory,
                         IProjectService projectService)
        {
            _dialogService = dialogService;
            _sceneTransitionManager = sceneTransitionManager;
            _newProjectStateFactory = newProjectStateFactory;
            _projectService = projectService;
        }


        private void Start()
        {
            SetIsActiveMenu(recentProjectsMenu);
        }

        public void CreateNewProject_Click()
        {
            SetIsActiveMenu(createNewProjectMenu);
            ProjectState = _newProjectStateFactory.Create();
            RefreshCreateNewProjectView();
        }


        private void SetIsActiveMenu(GameObject menu)
        {
            recentProjectsMenu.SetActive(menu == recentProjectsMenu);
            createNewProjectMenu.SetActive(menu == createNewProjectMenu);
        }
        
        public void LoadProject_Click()
        {
            var configuration = new FileDialogConfiguration
            {
                ExtensionFilters = new[]
                {
                    ExtensionFilter.Predefined.SeagullProjectFiles,
                    ExtensionFilter.Predefined.AllFiles,
                }
            };
            
            var _ = _dialogService.OpenFileDialog(configuration);
        }

        public void SelectDemoProject_Click()
        {
            Debug.Log("Clicked 'Select demo project'");
        }
        
        // TODO: Create a separate controller per menu?
        // CreateNewProjectButtons
        public void CreateNewProjectMenu_Back_Click() =>
            SetIsActiveMenu(recentProjectsMenu);

        public TMPro.TMP_InputField projectFilePath;
        
        public void SelectProjectFile_Click()
        {
            if (ProjectState == null) return;
            
            var configuration = new FileDialogConfiguration
            { 
                Title = "Select seagull project location",
                FileDialogType = FileDialogType.Save,
                ExtensionFilters = new[] 
                {
                    ExtensionFilter.Predefined.SeagullProjectFiles, 
                    ExtensionFilter.Predefined.AllFiles,
                }
            };

            var result = _dialogService.OpenFileDialog(configuration).FirstOrDefault();
            ProjectState?.ConfigureProjectPath(result);
            RefreshCreateNewProjectView();
        }
        
        public TMPro.TMP_InputField mapFilePath;
        
        public void SelectMapFile_Click()
        {
            if (ProjectState == null) return;
            
            var configuration = new FileDialogConfiguration
            { 
                Title = "Select map file",
                ExtensionFilters = new[] 
                {
                    ExtensionFilter.Predefined.NetcdfFiles, 
                    ExtensionFilter.Predefined.AllFiles,
                }
            };

            var result = _dialogService.OpenFileDialog(configuration).FirstOrDefault();
            ProjectState.MapFilePath = result;
            RefreshCreateNewProjectView();
        }

        private ISceneTransitionDescription GetCreateProjectTransitionDescription()
        {
            IEnumerator PreLoad()
            {
                if (ProjectState == null)
                {
                    yield break;
                }

                yield return null;
                _projectService.CreateProject(ProjectState.ProjectPath);
            }

            IEnumerator PostLoad()
            {
                yield break;
            }

            return new SceneTransitionDescription("ProjectEditor", 
                                                  PreLoad(), 
                                                  PostLoad());
        }
        

        public void CreateProject_Click() =>
            _sceneTransitionManager.LoadScene(GetCreateProjectTransitionDescription());
    }
}
