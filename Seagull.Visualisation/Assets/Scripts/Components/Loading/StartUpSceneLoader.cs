using UnityEngine;
#if !UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

namespace Seagull.Visualisation.Components.Loading
{
    /// <summary>
    /// <see cref="StartUpSceneLoader"/> loads all the scenes which will never be
    /// unloaded.
    /// </summary>
    public class StartUpSceneLoader : MonoBehaviour
    {
        /// <summary>
        /// The scenes to load at start up.
        /// </summary>
        [SerializeField] private string[] startUpScenes;

#if !UNITY_EDITOR
        private void Awake()
        {
            foreach (string scene in startUpScenes)
            { 
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
#endif
    }
}
