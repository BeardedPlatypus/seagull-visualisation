using JetBrains.Annotations;
using PathLib;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Components.UserInterface;
using Seagull.Visualisation.Views.MainMenu.Common;
using Seagull.Visualisation.Views.MainMenu.PageState;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.NewProjectPage
{
    /// <summary>
    /// <see cref="Controller"/> is responsible for
    /// managing the 
    /// </summary>
    public class Controller : PageController
    {
        private Bindings _bindings;
        private State.Factory _stateFactory;
        private SceneTransitionManager _sceneTransitionManager;
        private PageState.Controller _pageStateController;
        private SceneTransitionFactory _sceneTransitionFactory;
        
        [CanBeNull] private State _state = null;
        private static readonly int IsActive = Animator.StringToHash("IsActive");

        [CanBeNull]
        private State State
        {
            get => _state;
            set
            {
                _state = value;
                SyncProjectState();
            }
        }

        private void SyncProjectState()
        {
            if (State == null) return;

            _bindings.projectLocation.InputFieldText = State.ProjectPath?.ToString() ?? "";
            _bindings.mapFileLocation.InputFieldText = State.MapFilePath?.ToString() ?? "";
        }

        /// <summary>
        /// Initialise this <see cref="Controller"/> by injecting its dependencies.
        /// </summary>
        /// <param name="bindings">The NewProjectPage.Bindings.</param>
        /// <param name="newProjectStateFactory">The factory to create <see cref="State"/>.</param>
        /// <param name="sceneTransitionManager">The scene transition manager.</param>
        /// <param name="pageStateController">The page state controller.</param>
        /// <param name="sceneTransitionFactory"></param>
        [Inject]
        public void Init(Bindings bindings,
                         State.Factory newProjectStateFactory,
                         SceneTransitionManager sceneTransitionManager,
                         PageState.Controller pageStateController,
                         SceneTransitionFactory sceneTransitionFactory)
        {
            _bindings = bindings;
            _stateFactory = newProjectStateFactory;
            _sceneTransitionManager = sceneTransitionManager;
            _pageStateController = pageStateController;
            _sceneTransitionFactory = sceneTransitionFactory;
        }

        private void Start()
        {
            _bindings.projectLocation.Handler = new ProjectLocationHandler(this);
            _bindings.mapFileLocation.Handler = new MapFileLocationHandler(this);
            _bindings.createProjectButton.onClick.AddListener(OnCreateProjectButtonClick);
            _bindings.backButton.onClick.AddListener(OnBackButtonClick);
        }

        public override void Activate()
        {
            State = _stateFactory.Create();
            _bindings.animator.SetBool(IsActive, true);
        }

        public override void Deactivate()
        {
            _bindings.animator.SetBool(IsActive, false);
            State = null;
        }

        private class ProjectLocationHandler : IFileSelectionHandler
        {
            private readonly Controller _parent;

            public ProjectLocationHandler(Controller parent) => _parent = parent;
            
            public FileDialogConfiguration Configuration { get; } =
                new FileDialogConfiguration
                { 
                    Title = "Select seagull project location",
                    FileDialogType = FileDialogType.Save,
                    ExtensionFilters = new[] 
                    {
                        ExtensionFilter.Predefined.SeagullProjectFiles, 
                        ExtensionFilter.Predefined.AllFiles,
                    }
                };

            public void HandleInvalidOperation([CanBeNull] IPath path) 
            { 
                // no-op
            }

            public void HandleValidOperation(IPath path) 
            {
                if (_parent._state != null)
                {
                    _parent._state.ProjectPath = path;
                }
            }

            public bool ValidatePath(IPath path) => !path.Exists();

            public IPath TransformPath(IPath path)
            {
                path = AddProjectExtension(path);

                if (_parent._state?.ShouldCreateNewSolutionDirectory ?? false)
                {
                    path = AddProjectDir(path);
                }

                return path;
            }

            private static IPath AddProjectExtension(IPath path) => 
                path.WithExtension(".seagull");
            private static IPath AddProjectDir(IPath path) => 
                path.Parent().Join(path.Basename).Join(path.Filename);
        }

        private class MapFileLocationHandler : IFileSelectionHandler
        {
            private readonly Controller _parent;

            public MapFileLocationHandler(Controller parent) => _parent = parent;
            
            public FileDialogConfiguration Configuration { get; } = 
                new FileDialogConfiguration
                { 
                    Title = "Select map file",
                    ExtensionFilters = new[] 
                    {
                        ExtensionFilter.Predefined.NetcdfFiles, 
                        ExtensionFilter.Predefined.AllFiles,
                    }
                };
                
            public void HandleInvalidOperation(IPath path)
            {
                // no-op
            }

            public void HandleValidOperation(IPath path)
            {
                if (_parent._state != null)
                {
                    _parent._state.MapFilePath = path;
                }
            }

            public bool ValidatePath(IPath path) => true;
            public IPath TransformPath(IPath path) => path;
        }

        private void OnCreateProjectButtonClick()
        {
            if (State != null)
            {
                _sceneTransitionManager.LoadScene(_sceneTransitionFactory.GetCreateProjectTransition(State));
            }
        }

        private void OnBackButtonClick() =>
            _pageStateController.Activate(Page.OpeningPage);
    }
}
