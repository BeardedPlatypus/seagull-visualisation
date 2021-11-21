using Seagull.Visualisation.Components.Common;
using UniRx;

namespace Seagull.Visualisation.Components.Loading.Messages
{
    /// <summary>
    /// <see cref="MainMenuToggleMessage"/> defines the message used to toggle the
    /// MainMenu on and off.
    /// </summary>
    public sealed class MainMenuToggleMessage : IPublishableMessage
    {
        /// <summary>
        /// Creates a new <see cref="MainMenuToggleMessage"/> with the given
        /// <paramref name="shouldBeActive"/>.
        /// </summary>
        /// <param name="shouldBeActive">
        /// Whether the main menu should be active after executing this message.
        /// </param>
        public MainMenuToggleMessage(bool shouldBeActive) => 
            ShouldBeActive = shouldBeActive;
        
        /// <summary>
        /// Gets whether the main menu should be active after executing this message.
        /// </summary>
        public bool ShouldBeActive { get; }

        /// <inheritdoc cref="IPublishableMessage.Publish"/>
        public void Publish() => MessageBroker.Default.Publish(this);
    }
}
