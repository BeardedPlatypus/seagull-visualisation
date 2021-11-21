using System;
using System.Collections;
using System.Linq;
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
        [SerializeField] private float fadeInTime = 0.5F; 
        [SerializeField] private float fadeOutTime = 0.35F;

        private Fader _fader;

        private ZenjectSceneLoader _sceneLoader;

        [Inject]
        private void Init(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
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

        private static IObservable<Unit> LoadNextViewAsync(IViewTransitionDescription viewTransitionBehaviour) =>
            viewTransitionBehaviour.LoadMessages.Select(ExecuteMsg).WhenAll().ObserveOnMainThread();

        private static IObservable<Unit> ExecuteMsg(object msg) =>
            Observable.Start(() => { MessageBroker.Default.Publish(msg); });

        private IEnumerator PostLoadNextViewAsync(IViewTransitionDescription viewTransitionDescription)
        {
            foreach (var msg in viewTransitionDescription.PostLoadMessages)
            {
                MessageBroker.Default.Publish(msg);
                yield return null;
            }
        }

        private IEnumerator LoadLoadingScreenAsync()
        {
            yield return _sceneLoader.LoadSceneAsync(LoadScreenName, LoadSceneMode.Additive);
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
                .SelectMany(LoadNextViewAsync(viewTransitionBehaviour))
                .SelectMany(() => PostLoadNextViewAsync(viewTransitionBehaviour))
                .SelectMany(fadeIn)
                .SelectMany(UnloadLoadingScreenAsync)
                .SelectMany(fadeOut)
                .Subscribe();
        }
    }
}