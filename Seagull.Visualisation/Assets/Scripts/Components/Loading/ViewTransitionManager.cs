using System.Collections;
using Seagull.Visualisation.Components.UserInterface;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Seagull.Visualisation.Components.Loading
{
    /// <summary>
    /// <see cref="ViewTransitionManager"/> implements the logic to transition from
    /// one scene to another with the use of a loading screen.
    /// </summary>
    public sealed class ViewTransitionManager : MonoBehaviour
    {
        public float fadeInTime = 0.5F; 
        public float fadeOutTime = 0.35F;

        private Fader _fader;

        private ZenjectSceneLoader _sceneLoader;

        [Inject]
        private void Init(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
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

            MessageBroker.Default
                         .Receive<ChangeViewMessage>()
                         .Subscribe(msg => LoadSceneAsync(msg.ViewTransitionDescription))
                         .AddTo(this);
        }

        private const string LoadScreenName = "LoadScreen";

        private IEnumerator LoadNextSceneAsync(IViewTransitionDescription viewTransitionBehaviour)
        {
            yield return Resources.UnloadUnusedAssets();
            yield return StartCoroutine(viewTransitionBehaviour.PreSceneLoadCoroutine);
            yield return _sceneLoader.LoadSceneAsync(viewTransitionBehaviour.SceneName,
                                                     LoadSceneMode.Additive);
            yield return StartCoroutine(viewTransitionBehaviour.PostSceneLoadCoroutine);
        }

        private IEnumerator LoadLoadingScreenAsync()
        {
            yield return _sceneLoader.LoadSceneAsync(LoadScreenName, LoadSceneMode.Single);
        }
        
        private IEnumerator UnloadLoadingScreenAsync()
        {
            yield return SceneManager.UnloadSceneAsync(LoadScreenName);
        }

        private void LoadSceneAsync(IViewTransitionDescription viewTransitionBehaviour)
        {
            // Load loading screen
            var fadeIn = Observable.FromCoroutine(() => _fader.FadeTo(1.0F, fadeInTime));
            var fadeOut = Observable.FromCoroutine(() => _fader.FadeTo(0.0F, fadeOutTime));

            fadeIn
                .SelectMany(LoadLoadingScreenAsync)
                .SelectMany(fadeOut)
                .SelectMany(() => LoadNextSceneAsync(viewTransitionBehaviour))
                .SelectMany(fadeIn)
                .SelectMany(UnloadLoadingScreenAsync)
                .SelectMany(fadeOut)
                .Subscribe();
        }
    }
}