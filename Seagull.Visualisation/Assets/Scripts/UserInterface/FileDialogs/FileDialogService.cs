using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StandaloneFileBrowser;

namespace Seagull.Visualisation.UserInterface.FileDialogs
{
    public class StandAloneFileBrowserFileDialogService : IFileDialogService
    {
        private readonly IStandaloneFileBrowser _browser;
        public StandAloneFileBrowserFileDialogService(IStandaloneFileBrowser browser)
        {
            _browser = browser;
        }
        
        public IEnumerable<string> Open(FileDialogConfiguration configuration) =>
            _browser.OpenFilePanel(configuration.Title,
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