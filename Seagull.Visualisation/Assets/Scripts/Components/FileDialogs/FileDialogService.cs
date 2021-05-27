using System.Collections.Generic;
using System.Linq;
using BeardedPlatypus.FileBrowser;

namespace Seagull.Visualisation.Components.FileDialogs
{
    public sealed class FileDialogService : IFileDialogService
    {
        public IEnumerable<string> Open(FileDialogConfiguration configuration) =>
            FileBrowserService.OpenFileDialog(Convert(configuration));

        private static BeardedPlatypus.FileBrowser.Configurations.FileDialogConfiguration Convert(
            FileDialogConfiguration configuration) =>
            new BeardedPlatypus.FileBrowser.Configurations.FileDialogConfiguration
            {
                Title = configuration.Title,
                InitialDirectory = configuration.InitialDirectory,
                ExtensionFilters = configuration.ExtensionFilters.Select(Convert).ToArray(),
                Multiselect = configuration.HasMultiSelect,
            };
        
        private static BeardedPlatypus.FileBrowser.ExtensionFilter Convert(ExtensionFilter filter) => 
            new BeardedPlatypus.FileBrowser.ExtensionFilter(filter.FileTypeDescription,
                                                            filter.AssociatedFileExtensions.ToArray());
    }
}