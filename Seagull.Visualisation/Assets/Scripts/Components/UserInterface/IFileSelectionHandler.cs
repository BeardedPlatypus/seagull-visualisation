using JetBrains.Annotations;
using PathLib;
using Seagull.Visualisation.Components.FileDialogs;

namespace Seagull.Visualisation.Components.UserInterface
{
    public interface IFileSelectionHandler
    {
        public FileDialogConfiguration Configuration { get; }
        void HandleInvalidOperation([CanBeNull] IPath path);
        void HandleValidOperation(IPath path);
        bool ValidatePath(IPath path);
        IPath TransformPath(IPath path);
    }
}