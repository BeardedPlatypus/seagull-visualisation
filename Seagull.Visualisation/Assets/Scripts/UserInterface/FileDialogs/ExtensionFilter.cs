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

        public static class Predefined
        {
            public static ExtensionFilter SeagullProjectFiles =>
                new ExtensionFilter("Seagull project files", "seagull");

            public static ExtensionFilter NetcdfFiles =>
                new ExtensionFilter("NetCDF files", "nc");
            
            public static ExtensionFilter AllFiles =>
                new ExtensionFilter("All Files", "*");
        }
    }
}