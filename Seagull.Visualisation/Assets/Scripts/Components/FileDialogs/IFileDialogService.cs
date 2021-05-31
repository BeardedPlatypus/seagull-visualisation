using System.Collections.Generic;
using PathLib;

namespace Seagull.Visualisation.Components.FileDialogs
{
    public interface IFileDialogService
    {
        IEnumerable<IPath> Open(FileDialogConfiguration configuration);
    }
}