using JetBrains.Annotations;
using PathLib;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.NewProjectPage
{
    public sealed class State
    {
        [CanBeNull] public IPath ProjectPath { get; private set; } = null;

        public void ConfigureProjectPath(IPath projectPath)
        {
            projectPath = projectPath.WithExtension(".seagull");

            if (ShouldCreateNewSolutionDirectory)
            {
                projectPath = AddProjectDirToPath(projectPath);
            }

            ProjectPath = projectPath;
        }

        public bool ShouldCreateNewSolutionDirectory { get; private set; } = false;

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

        [CanBeNull] public IPath MapFilePath { get; set; } = null;
        
        public class Factory : PlaceholderFactory<State> { }
    }
}
