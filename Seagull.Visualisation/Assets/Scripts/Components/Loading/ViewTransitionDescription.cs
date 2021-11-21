using System.Collections.Generic;
using System.Linq;
using Seagull.Visualisation.Components.Common;

namespace Seagull.Visualisation.Components.Loading
{
    public class ViewTransitionDescription : IViewTransitionDescription
    {
        private readonly IReadOnlyList<IPublishableMessage> _loadMessages;
        private readonly IReadOnlyList<IPublishableMessage> _postLoadMessages;

        public ViewTransitionDescription(IEnumerable<IPublishableMessage> loadMessages, 
                                         IEnumerable<IPublishableMessage> postLoadMessages)
        {
            _loadMessages = loadMessages.ToArray();
            _postLoadMessages = postLoadMessages.ToArray();
        }

        public IEnumerable<IPublishableMessage> LoadMessages => _loadMessages;
        public IEnumerable<IPublishableMessage> PostLoadMessages => _postLoadMessages;
    }
}