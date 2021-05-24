using System;
using System.Collections.Generic;

namespace Seagull.Visualisation.UserInterface.FileDialogs
{
    /// <summary>
    /// The <see cref="FileDialogConfiguration"/> defines the values used to
    /// configure a file dialog.
    /// </summary>
    public sealed class FileDialogConfiguration
    {
        /// <summary>
        /// The type of dialog being opened.
        /// </summary>
        /// <remarks>
        /// The <see cref="FileDialogType"/> defaults to <see cref="FileDialogType.Open"/>
        /// </remarks>
        public FileDialogType FileDialogType { get; set; } = 
            FileDialogType.Open;

        /// <summary>
        /// The extension filters used to determine which files to show.
        /// </summary>
        /// <remarks>
        /// The <see cref="ExtensionFilters"/> defaults to a single filter for all files.
        /// </remarks>
        public IList<ExtensionFilter> ExtensionFilters { get; set; } =
            new[] {ExtensionFilter.Predefined.AllFiles};
        
        /// <summary>
        /// The initial directory in which the dialog is opened.
        /// </summary>
        /// <remarks>
        /// The <see cref="InitialDirectory"/> defaults to the home folder.
        /// </remarks>
        public string InitialDirectory { get; set; } = 
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// Whether multiple files can be selected in the file dialog.
        /// </summary>
        /// <remarks>
        /// The <see cref="HasMultiSelect"/> defaults to false.
        /// </remarks>
        public bool HasMultiSelect { get; set; } = false;
        
        /// <summary>
        /// The title of the dialog.
        /// </summary>
        /// <remarks>
        /// The <see cref="Title"/> defaults to "Open File".
        /// </remarks>
        public string Title { get; set; } = 
            "Open File";
    }
}