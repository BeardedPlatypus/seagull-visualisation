using Seagull.Visualisation.Components.Common;
using UniRx;

namespace Seagull.Visualisation.Components.Loading.Messages
{
    /// <summary>
    /// <see cref="EditorUserInterfaceToggleMessage"/> defines the message used to toggle the
    /// Editor UI on and off.
    /// </summary>
    public sealed class EditorUserInterfaceToggleMessage : IPublishableMessage
    {
        /// <summary>
        /// Creates a new <see cref="EditorUserInterfaceToggleMessage"/> with the given
        /// <paramref name="shouldBeActive"/>.
        /// </summary>
        /// <param name="shouldBeActive">
        /// Whether the editor UI should be active after executing this message.
        /// </param>
        public EditorUserInterfaceToggleMessage(bool shouldBeActive) => 
            ShouldBeActive = shouldBeActive;
        
        /// <summary>
        /// Gets whether the editor UI should be active after executing this message.
        /// </summary>
        public bool ShouldBeActive { get; }

        /// <inheritdoc cref="IPublishableMessage.Publish"/>
        public void Publish() => MessageBroker.Default.Publish(this);
    }
}
