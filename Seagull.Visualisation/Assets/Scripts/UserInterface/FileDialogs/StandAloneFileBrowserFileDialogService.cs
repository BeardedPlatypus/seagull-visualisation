using System.Collections.Generic;
using System.Linq;

namespace Seagull.Visualisation.UserInterface.FileDialogs
{
    public sealed class StandAloneFileBrowserFileDialogService : IFileDialogService
    {
        public IEnumerable<string> Open(FileDialogConfiguration configuration) =>
            StandaloneFileBrowser.StandaloneFileBrowser.OpenFilePanel(configuration.Title, 
                                                                      configuration.InitialDirectory,
                                                                      configuration.ExtensionFilters
                                                                          .Select(ConvertFilter)
                                                                          .ToArray(),
                                                                      configuration.HasMultiSelect);

        private static StandaloneFileBrowser.ExtensionFilter ConvertFilter(ExtensionFilter filter) =>
            new StandaloneFileBrowser.ExtensionFilter(filter.FileTypeDescription,
                                                      filter.AssociatedFileExtensions.ToArray());
    }
}