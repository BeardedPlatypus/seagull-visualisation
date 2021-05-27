using System.Collections;

namespace Seagull.Visualisation.Components.Loading
{
    public interface ISceneTransitionDescription
    {
        string SceneName { get; }
        IEnumerator PreSceneLoadCoroutine();
        IEnumerator PostSceneLoadCoroutine();
    }
}