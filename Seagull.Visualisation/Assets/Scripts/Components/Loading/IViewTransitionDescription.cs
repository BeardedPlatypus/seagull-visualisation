using System.Collections;

namespace Seagull.Visualisation.Components.Loading
{
    /// <summary>
    /// <see cref="IViewTransitionDescription"/> describes the variable information
    /// necessary for the <see cref="ViewTransitionManager"/> to execute a transition.
    /// </summary>
    public interface IViewTransitionDescription
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