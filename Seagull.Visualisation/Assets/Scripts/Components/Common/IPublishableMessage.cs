namespace Seagull.Visualisation.Components.Common
{
    /// <summary>
    /// <see cref="IPublishableMessage"/> defines the publish messages, such that
    /// messages of specific types get published as their own type, while still
    /// providing a common interface with which to store the messages.
    /// </summary>
    public interface IPublishableMessage
    {
        /// <summary>
        /// Publish this <see cref="IPublishableMessage"/> through the message broker.
        /// </summary>
        /// <remarks>
        /// This method needs to be implemented in the actual message (and not a parent
        /// class), in order to ensure the right type is used.
        /// </remarks>
        void Publish();

    }
}