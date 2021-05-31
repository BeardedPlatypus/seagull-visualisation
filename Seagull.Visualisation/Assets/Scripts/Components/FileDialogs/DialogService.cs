using System.Collections.Generic;
using PathLib;

namespace Seagull.Visualisation.Components.FileDialogs
{
    public sealed class DialogService : IDialogService
    {
        private readonly IFileDialogService _fileDialogService;

        public DialogService(IFileDialogService fileDialogService)
        {
            _fileDialogService = fileDialogService;
        }

        public IEnumerable<IPath> OpenFileDialog(FileDialogConfiguration configuration) =>
            _fileDialogService.Open(configuration);
    }
}