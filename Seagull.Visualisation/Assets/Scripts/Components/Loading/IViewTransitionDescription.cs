using System.Collections.Generic;
using Seagull.Visualisation.Components.Common;

namespace Seagull.Visualisation.Components.Loading
{
    /// <summary>
    /// <see cref="IViewTransitionDescription"/> describes the variable information
    /// necessary for the <see cref="ViewTransitionManager"/> to execute a transition.
    /// </summary>
    public interface IViewTransitionDescription
    {
        /// <summary>
        /// Gets the messages executed during the loading of a view transition.
        /// </summary>
        /// <remarks>
        /// It is not guaranteed these messages are executed in order.
        /// </remarks>
        IEnumerable<IPublishableMessage> LoadMessages { get; }
        
        /// <summary>
        /// Gets the messages executed after loading a view transition.
        /// </summary>
        IEnumerable<IPublishableMessage> PostLoadMessages { get; }
    }
}