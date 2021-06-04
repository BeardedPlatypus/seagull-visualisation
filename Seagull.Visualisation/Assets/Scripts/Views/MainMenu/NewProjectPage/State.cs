using JetBrains.Annotations;
using PathLib;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.NewProjectPage
{
    /// <summary>
    /// <see cref="State"/> implements the state of a <see cref="NewProjectPage"/>.
    /// </summary>
    public sealed class State
    {
        /// <summary>
        /// The path to the project file to create.
        /// </summary>
        [CanBeNull] public IPath ProjectPath { get; set; } = null;
        
        /// <summary>
        /// The path to the map file to load.
        /// </summary>
        [CanBeNull] public IPath MapFilePath { get; set; } = null;

        /// <summary>
        /// Whether a new project should be created in a new directory.
        /// </summary>
        public bool ShouldCreateNewSolutionDirectory { get; private set; } = false;

        /// <summary>
        /// Configure whether a new project should be created in a new directory.
        /// </summary>
        /// <param name="newValue">Whether to create a new directory.</param>
        public void ConfigureCreateNewSolutionDirectory(bool newValue)
        {
            if (newValue == ShouldCreateNewSolutionDirectory)
            {
                return;
            }
            
            ShouldCreateNewSolutionDirectory = newValue;
            ProjectPath = ShouldCreateNewSolutionDirectory
                ? AddProjectDirToPath(ProjectPath)
                : RemoveProjectDirToPath(ProjectPath);
        }

        private static IPath AddProjectDirToPath(IPath path) =>
            path.Parent().Join(path.Basename).Join(path.Filename);
        
        private static IPath RemoveProjectDirToPath(IPath path) =>
            path.Parent(2).Join(path.Filename);

        /// <summary>
        /// <see cref="Factory"/> implements a factory to create <see cref="State"/> objects.
        /// </summary>
        public class Factory : PlaceholderFactory<State> { }
    }
}
