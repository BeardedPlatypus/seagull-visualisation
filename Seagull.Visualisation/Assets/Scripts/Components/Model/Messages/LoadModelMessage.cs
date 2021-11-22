using PathLib;
using Seagull.Visualisation.Components.Common;
using UniRx;

namespace Seagull.Visualisation.Components.Model.Messages
{
    /// <summary>
    /// <see cref="LoadModelMessage"/> indicates that the model loader should load the
    /// model at the specified <see cref="LoadModelMessage.ModelPath"/>
    /// </summary>
    public sealed class LoadModelMessage : IPublishableMessage
    {
        /// <summary>
        /// Creates a new <see cref="LoadModelMessage"/> with the given
        /// <paramref name="modelPath"/>.
        /// </summary>
        /// <param name="modelPath">The <see cref="IPath"/> to the model.</param>
        public LoadModelMessage(IPath modelPath)
        {
            ModelPath = modelPath;
        }
        
        /// <summary>
        /// Gets the path to the model to load.
        /// </summary>
        public IPath ModelPath { get; }

        /// <inheritdoc cref="IPublishableMessage.Publish"/>
        public void Publish() => MessageBroker.Default.Publish(this);
    }
}