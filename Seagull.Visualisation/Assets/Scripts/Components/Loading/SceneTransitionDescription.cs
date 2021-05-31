using System.Collections;

namespace Seagull.Visualisation.Components.Loading
{
    public class SceneTransitionDescription : ISceneTransitionDescription
    {
        public SceneTransitionDescription(string sceneName, 
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