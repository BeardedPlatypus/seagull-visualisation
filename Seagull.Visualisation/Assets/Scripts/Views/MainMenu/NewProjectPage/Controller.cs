using JetBrains.Annotations;
using PathLib;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Components.UserInterface;
using Seagull.Visualisation.Views.MainMenu.Common;
using UniRx;

namespace Seagull.Visualisation.Views.MainMenu.NewProjectPage
{
    /// <summary>
    /// <see cref="Controller"/> is responsible for
    /// managing the 
    /// </summary>
    public sealed class Controller : IPageController
    {
        private readonly ViewTransitionFactory _viewTransitionFactory;
        
        /// <summary>
        /// Initialise this <see cref="Controller"/> by injecting its dependencies.
        /// </summary>
        /// <param name="newProjectStateFactory">The factory to create <see cref="State"/>.</param>
        /// <param name="viewTransitionFactory"></param>
        public Controller(State.Factory newProjectStateFactory,
                          ViewTransitionFactory viewTransitionFactory)
        {
            _viewTransitionFactory = viewTransitionFactory;
            
            IsActive.Where(isActive => isActive)
                    .Subscribe(_ => State.Value = newProjectStateFactory.Create());
            IsActive.Where(isActive => !isActive)
                    .Subscribe(_ => State.Value = null);

            ProjectLocationHandler = new ProjectLocationHandlerImplementation(this);
        }

        private ReactiveProperty<State> State { get; } = new ReactiveProperty<State>(null);
        
        public ReactiveProperty<bool> IsActive { get; } = new ReactiveProperty<bool>(false);

        public IFileSelectionHandler ProjectLocationHandler { get; } 

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
            if (State == null) return;
            
            var transitionDescription = _viewTransitionFactory.GetCreateProjectTransition(State.Value);
            var msg = new ChangeViewMessage(transitionDescription);
            MessageBroker.Default.Publish(msg);
        }
    }
}
