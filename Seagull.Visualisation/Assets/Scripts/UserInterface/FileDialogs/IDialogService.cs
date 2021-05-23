using System.Collections.Generic;
using JetBrains.Annotations;

namespace Seagull.Visualisation.UserInterface.FileDialogs
{
    public interface IDialogService
    {
        [CanBeNull]
        IEnumerable<string> OpenFileDialog(FileDialogConfiguration configuration);
    }
}
