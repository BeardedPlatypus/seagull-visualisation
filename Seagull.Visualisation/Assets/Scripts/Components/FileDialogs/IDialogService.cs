using System.Collections.Generic;
using PathLib;

namespace Seagull.Visualisation.Components.FileDialogs
{
    public interface IDialogService
    {
        IEnumerable<IPath> OpenFileDialog(FileDialogConfiguration configuration);
    }
}
