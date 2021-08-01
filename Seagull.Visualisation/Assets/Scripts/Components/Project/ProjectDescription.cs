using PathLib;
using Seagull.Visualisation.Core.Application.Model;

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
        /// <param name="modelRepository">The model repository</param>
        public ProjectDescription(IPath modelPath, IModelRepository modelRepository)
        {
            ModelPath = modelPath;
            ModelRepository = modelRepository;
        }

        /// <summary>
        /// Gets the model path of this project.
        /// </summary>
        public IPath ModelPath { get; }
        
        /// <summary>
        /// Gets the <see cref="IModelRepository"/> of this project.
        /// </summary>
        public IModelRepository ModelRepository { get; }
    }
}
