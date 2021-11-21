using System.Collections.Generic;
using System.Linq;

namespace Seagull.Visualisation.Components.Loading
{
    public class ViewTransitionDescription : IViewTransitionDescription
    {
        private readonly IReadOnlyList<object> _loadMessages;
        private readonly IReadOnlyList<object> _postLoadMessages;

        public ViewTransitionDescription(IEnumerable<object> loadMessages, 
                                         IEnumerable<object> postLoadMessages)
        {
            _loadMessages = loadMessages.ToArray();
            _postLoadMessages = postLoadMessages.ToArray();
        }

        public IEnumerable<object> LoadMessages => _loadMessages;
        public IEnumerable<object> PostLoadMessages => _postLoadMessages;
    }
}