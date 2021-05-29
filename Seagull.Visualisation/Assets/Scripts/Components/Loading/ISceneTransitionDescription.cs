using System.Collections;

namespace Seagull.Visualisation.Components.Loading
{
    /// <summary>
    /// <see cref="ISceneTransitionDescription"/> describes the variable information
    /// necessary for the <see cref="SceneTransitionManager"/> to execute a transition.
    /// </summary>
    public interface ISceneTransitionDescription
    {
        /// <summary>
        /// Get the name of the scene to transition to. 
        /// </summary>
        string SceneName { get; }
        
        /// <summary>
        /// Get the coroutine executed before a scene is loaded.
        /// </summary>
        IEnumerator PreSceneLoadCoroutine { get; } 
        
        /// <summary>
        /// Get the coroutine executed after a scene is loaded.
        /// </summary>
        IEnumerator PostSceneLoadCoroutine { get; }
    }
}