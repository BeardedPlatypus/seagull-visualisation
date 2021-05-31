using PathLib;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.CreateProjects
{
    public sealed class NewProjectState
    {
        public IPath ProjectPath { get; private set; }

        public void ConfigureProjectPath(IPath projectPath)
        {
            projectPath = projectPath.WithExtension(".seagull");

            if (ShouldCreateNewSolutionDirectory)
            {
                projectPath = AddProjectDirToPath(projectPath);
            }

            ProjectPath = projectPath;
        }
        
        public bool ShouldCreateNewSolutionDirectory { get; private set; }

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
        
        public IPath MapFilePath { get; set; }
        
        public class Factory : PlaceholderFactory<NewProjectState> { }
    }
}
