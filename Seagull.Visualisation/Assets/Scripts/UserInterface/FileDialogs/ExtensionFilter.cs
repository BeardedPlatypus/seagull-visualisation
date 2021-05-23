using System.Collections.Generic;

namespace Seagull.Visualisation.UserInterface.FileDialogs
{
    public sealed class ExtensionFilter
    {
        public ExtensionFilter(string fileTypeDescription,
                               params string[] fileExtensions)
        {
            FileTypeDescription = fileTypeDescription;
            AssociatedFileExtensions = fileExtensions;
        }
        
        public string FileTypeDescription { get; }
        public IReadOnlyCollection<string> AssociatedFileExtensions { get; }

    }
}