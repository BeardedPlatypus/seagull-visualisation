using System.Collections.Generic;

namespace Seagull.Visualisation.Components.FileDialogs
{
    public interface IDialogService
    {
        IEnumerable<string> OpenFileDialog(FileDialogConfiguration configuration);
    }
}
