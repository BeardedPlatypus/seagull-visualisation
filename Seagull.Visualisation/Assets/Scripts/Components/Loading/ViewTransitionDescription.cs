using System.Collections;

namespace Seagull.Visualisation.Components.Loading
{
    public class ViewTransitionDescription : IViewTransitionDescription
    {
        public ViewTransitionDescription(string sceneName, 
                                          IEnumerator preSceneLoadCoroutine, 
                                          IEnumerator postSceneLoadCoroutine)
        {
            SceneName = sceneName;
            PreSceneLoadCoroutine = preSceneLoadCoroutine;
            PostSceneLoadCoroutine = postSceneLoadCoroutine;
        }
        
        public string SceneName { get; }
        public IEnumerator PreSceneLoadCoroutine { get; }
        public IEnumerator PostSceneLoadCoroutine { get; }
    }
}