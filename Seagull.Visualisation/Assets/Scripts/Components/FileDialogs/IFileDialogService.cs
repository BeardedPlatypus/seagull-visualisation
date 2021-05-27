using System.Collections.Generic;

namespace Seagull.Visualisation.Components.FileDialogs
{
    public interface IFileDialogService
    {
        IEnumerable<string> Open(FileDialogConfiguration configuration);
    }
}