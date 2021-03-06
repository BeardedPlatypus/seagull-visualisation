using Seagull.Visualisation.Components.Common;
using UniRx;

namespace Seagull.Visualisation.Components.Camera.Messages
{
    /// <summary>
    /// <see cref="SetIsActiveMessage"/> is used to indicate whether to enable and
    /// disables camera controls.
    /// </summary>
    public sealed class SetIsActiveMessage : IPublishableMessage
    {
        /// <summary>
        /// Creates a new <see cref="SetIsActiveMessage"/>.
        /// </summary>
        /// <param name="value">Whether the Camera controls should be active.</param>
        public SetIsActiveMessage(bool value) => Value = value;
        
        /// <summary>
        /// Gets the value of this <see cref="SetIsActiveMessage"/>.
        /// </summary>
        public bool Value { get; }

        /// <inheritdoc cref="IPublishableMessage.Publish"/>
        public void Publish() => MessageBroker.Default.Publish(this);
    }
}
