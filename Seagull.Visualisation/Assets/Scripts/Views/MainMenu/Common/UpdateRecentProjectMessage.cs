using Seagull.Visualisation.Components.Common;
using Seagull.Visualisation.Core.Domain;
using UniRx;

namespace Seagull.Visualisation.Views.MainMenu.Common
{
    /// <summary>
    /// <see cref="UpdateRecentProjectMessage"/> defines a message used to
    /// update the contained <see cref="RecentProject"/>.
    /// </summary>
    public sealed class UpdateRecentProjectMessage : IPublishableMessage
    {
        /// <summary>
        /// Creates a new <see cref="UpdateRecentProjectMessage"/>.
        /// </summary>
        /// <param name="recentProject">The recent project to update.</param>
        public UpdateRecentProjectMessage(RecentProject recentProject)
        {
            RecentProject = recentProject;
        }
        
        /// <summary>
        /// Gets the <see cref="RecentProject"/> to update.
        /// </summary>
        public RecentProject RecentProject { get; }

        /// <inheritdoc cref="IPublishableMessage.Publish"/>
        public void Publish() => MessageBroker.Default.Publish(this);
    }
}