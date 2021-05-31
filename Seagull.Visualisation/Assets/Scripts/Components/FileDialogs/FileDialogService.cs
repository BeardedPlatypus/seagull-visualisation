using System;
using System.Collections.Generic;
using System.Linq;
using BeardedPlatypus.FileBrowser;
using PathLib;

namespace Seagull.Visualisation.Components.FileDialogs
{
    public sealed class FileDialogService : IFileDialogService
    {
        public IEnumerable<IPath> Open(FileDialogConfiguration configuration) =>
            FileBrowserService.OpenFileDialog(Convert(configuration))
                              .Select(PathLib.Paths.Create);

        private static BeardedPlatypus.FileBrowser.Configurations.FileDialogConfiguration Convert(
            FileDialogConfiguration configuration) =>
            new BeardedPlatypus.FileBrowser.Configurations.FileDialogConfiguration
            {
                Title = configuration.Title,
                InitialDirectory = configuration.InitialDirectory,
                FileDialogType = Convert(configuration.FileDialogType),
                ExtensionFilters = configuration.ExtensionFilters.Select(Convert).ToArray(),
                Multiselect = configuration.HasMultiSelect,
            };
        
        private static BeardedPlatypus.FileBrowser.ExtensionFilter Convert(ExtensionFilter filter) => 
            new BeardedPlatypus.FileBrowser.ExtensionFilter(filter.FileTypeDescription,
                                                            filter.AssociatedFileExtensions.ToArray());

        private static BeardedPlatypus.FileBrowser.FileDialogType Convert(FileDialogType t) =>
            t switch
            {
                FileDialogType.Open => BeardedPlatypus.FileBrowser.FileDialogType.Open,
                FileDialogType.Save => BeardedPlatypus.FileBrowser.FileDialogType.Save,
                _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
            };
    }
}