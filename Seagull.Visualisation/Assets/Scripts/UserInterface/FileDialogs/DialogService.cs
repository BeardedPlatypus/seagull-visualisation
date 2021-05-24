using System.Collections.Generic;

namespace Seagull.Visualisation.UserInterface.FileDialogs
{
    public sealed class DialogService : IDialogService
    {
        private readonly IFileDialogService _fileDialogService;

        public DialogService(IFileDialogService fileDialogService)
        {
            _fileDialogService = fileDialogService;
        }

        public IEnumerable<string> OpenFileDialog(FileDialogConfiguration configuration) =>
            _fileDialogService.Open(configuration);
    }
}