using System.Collections.Generic;
using JetBrains.Annotations;

namespace Seagull.Visualisation.UserInterface.FileDialogs
{
    public interface IFileDialogService
    {
        [CanBeNull] 
        IEnumerable<string> Open(FileDialogConfiguration configuration);
    }
}