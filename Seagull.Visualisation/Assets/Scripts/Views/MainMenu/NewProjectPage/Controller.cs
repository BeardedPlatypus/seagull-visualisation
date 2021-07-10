using JetBrains.Annotations;
using PathLib;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Components.UserInterface;
using Seagull.Visualisation.Views.MainMenu.Common;
using Seagull.Visualisation.Views.MainMenu.PageState;
using UniRx;

namespace Seagull.Visualisation.Views.MainMenu.NewProjectPage
{
    /// <summary>
    /// <see cref="Controller"/> is responsible for
    /// managing the 
    /// </summary>
    public sealed class Controller : IPageController
    {
        private readonly SceneTransitionManager _sceneTransitionManager;
        private readonly SceneTransitionFactory _sceneTransitionFactory;
        
        /// <summary>
        /// Initialise this <see cref="Controller"/> by injecting its dependencies.
        /// </summary>
        /// <param name="newProjectStateFactory">The factory to create <see cref="State"/>.</param>
        /// <param name="sceneTransitionManager">The scene transition manager.</param>
        /// <param name="sceneTransitionFactory"></param>
        public Controller(State.Factory newProjectStateFactory,
                          SceneTransitionManager sceneTransitionManager,
                          SceneTransitionFactory sceneTransitionFactory)
        {
            _sceneTransitionManager = sceneTransitionManager;
            _sceneTransitionFactory = sceneTransitionFactory;
            
            IsActive.Where(isActive => isActive)
                    .Subscribe(_ => State.Value = newProjectStateFactory.Create());
            IsActive.Where(isActive => !isActive)
                    .Subscribe(_ => State.Value = null);

            ProjectLocationHandler = new ProjectLocationHandlerImplementation(this);
            MapLocationHandler = new MapFileLocationHandlerImplementation(this);
        }

        private ReactiveProperty<State> State { get; } = new ReactiveProperty<State>(null);
        
        public ReactiveProperty<bool> IsActive { get; } = new ReactiveProperty<bool>(false);

        public IFileSelectionHandler ProjectLocationHandler { get; } 
        public IFileSelectionHandler MapLocationHandler { get; } 

        // TODO: see whether these can be made reactive as well
        private class ProjectLocationHandlerImplementation : IFileSelectionHandler
        {
            private readonly Controller _parent;

            public ProjectLocationHandlerImplementation(Controller parent) => _parent = parent;
            
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

            public void HandleInvalidOperation([CanBeNull] IPath path) { 
                // no-op
            }

            public void HandleValidOperation(IPath path) 
            {
                if (_parent.State.Value != null)
                {
                    _parent.State.Value.ProjectPath = path;
                }
            }

            public bool ValidatePath(IPath path) => !path.Exists();

            public IPath TransformPath(IPath path)
            {
                path = AddProjectExtension(path);

                if (_parent.State.Value?.ShouldCreateNewSolutionDirectory ?? false)
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

        private class MapFileLocationHandlerImplementation : IFileSelectionHandler
        {
            private readonly Controller _parent;

            public MapFileLocationHandlerImplementation(Controller parent) => _parent = parent;
            
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
                if (_parent.State.Value != null)
                {
                    _parent.State.Value.MapFilePath = path;
                }
            }

            public bool ValidatePath(IPath path) => true;
            public IPath TransformPath(IPath path) => path;
        }

        public void OnCreateProject()
        {
            if (State != null)
            {
                _sceneTransitionManager.LoadScene(_sceneTransitionFactory.GetCreateProjectTransition(State.Value));
            }
        }
    }
}
