using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Seagull.Visualisation.Components.Loading
{
    /// <summary>
    /// <see cref="SceneTransitionManager"/> implements the logic to transition from
    /// one scene to another with the use of a loading screen.
    /// </summary>
    public sealed class SceneTransitionManager : MonoBehaviour
    {
        public float fadeInTime = 0.5F; 
        public float fadeOutTime = 0.35F;

        private Fader _fader;

        private void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("SceneTransitionManager");

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            _fader = GetComponentInChildren<Fader>();
        }

        /// <summary>
        /// Load the scene described by the <paramref name="sceneTransitionDescription"/>.
        /// </summary>
        /// <param name="sceneTransitionDescription">
        /// The description of the transition.
        /// </param>
        public void LoadScene(ISceneTransitionDescription sceneTransitionDescription) =>
            StartCoroutine(LoadSceneAsync(sceneTransitionDescription));

        private const string LoadScreenName = "LoadScreen";

        private IEnumerator LoadSceneAsync(ISceneTransitionDescription sceneTransitionBehaviour)
        {
            // Load loading screen
            yield return StartCoroutine(_fader.FadeTo(1.0F, fadeInTime));
            yield return SceneManager.LoadSceneAsync(LoadScreenName,
                                                     LoadSceneMode.Single);
            yield return StartCoroutine(_fader.FadeTo(0.0F, fadeOutTime));

            // Load actual scene
            yield return Resources.UnloadUnusedAssets();
            yield return StartCoroutine(sceneTransitionBehaviour.PreSceneLoadCoroutine);
            yield return SceneManager.LoadSceneAsync(sceneTransitionBehaviour.SceneName,
                                                     LoadSceneMode.Additive);
            yield return StartCoroutine(sceneTransitionBehaviour.PostSceneLoadCoroutine);
            
            // Unload loading screen
            yield return StartCoroutine(_fader.FadeTo(1.0F, fadeInTime));
            yield return SceneManager.UnloadSceneAsync(LoadScreenName);
            yield return StartCoroutine(_fader.FadeTo(0.0F, fadeOutTime));
        }
    }
}