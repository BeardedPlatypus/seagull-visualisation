using PathLib;

namespace Seagull.Visualisation.Components.Project
{
    /// <summary>
    /// <see cref="ProjectDescription"/> describes a single project.
    /// </summary>
    public sealed class ProjectDescription
    {
        /// <summary>
        /// Creates a new <see cref="ProjectDescription"/>.
        /// </summary>
        /// <param name="modelPath">The path to the model</param>
        public ProjectDescription(IPath modelPath)
        {
            ModelPath = modelPath;
        }

        /// <summary>
        /// Gets the model path of this project.
        /// </summary>
        public IPath ModelPath { get; }
    }
}
