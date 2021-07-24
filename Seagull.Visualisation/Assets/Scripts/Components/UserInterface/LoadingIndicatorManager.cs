using System;
using System.Collections;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Seagull.Visualisation.Components.UserInterface
{
    /// <summary>
    /// <see cref="LoadingIndicatorManager"/> is responsible for showing a
    /// loader indicator within the screen, when loading is triggered.
    /// </summary>
    public class LoadingIndicatorManager : MonoBehaviour
    {
        [SerializeField] private GameObject logo;
        private Fader _logoFader;
        [SerializeField] private float logoFadeInTime = 0.35F;
        [SerializeField] private float logoFadeOutTime = 0.4F;
        [CanBeNull] private IDisposable _logoCancellationToken = null;
        
        [SerializeField] private GameObject spinner;
        private Fader _spinnerFader;
        [SerializeField] private float spinnerFadeInTime = 0.5F;
        [SerializeField] private float spinnerFadeOutTime = 0.2F;
        [CanBeNull] private IDisposable _spinnerCancellationToken = null;

        private int NActiveLoading { get; set; } = 0;

        private void Start()
        {
            logo.SetActive(false);
            _logoFader = logo.GetComponent<Fader>();
            
            spinner.SetActive(false);
            _spinnerFader = spinner.GetComponent<Fader>();

            MessageBroker.Default.Receive<LoadingIndication.StartMessage>()
                                 .Subscribe(IncrementActiveLoading);
            MessageBroker.Default.Receive<LoadingIndication.StopMessage>()
                                 .Subscribe(DecrementActiveLoading);
        }

        private void IncrementActiveLoading(LoadingIndication.StartMessage _)
        {
            if (NActiveLoading == 0)
                StartIndicator();

            NActiveLoading += 1;
        }

        private void DecrementActiveLoading(LoadingIndication.StopMessage _)
        {
            if (NActiveLoading == 1)
                StopIndicator();

            NActiveLoading -= 1;
        }

        private void StartIndicator()
        {
            DisposeTokens();
            
            logo.SetActive(true);
            spinner.SetActive(true);

            var fadeInLogo = _logoFader.FadeTo(1.0F, logoFadeInTime).ToObservable();
            var fadeInSpinner = _spinnerFader.FadeTo(1.0F, spinnerFadeInTime).ToObservable();

            _logoCancellationToken = fadeInLogo.Subscribe();
            _spinnerCancellationToken = fadeInSpinner.Subscribe();
        }

        private void StopIndicator()
        {
            DisposeTokens();

            var fadeOutLogo = _logoFader.FadeTo(0.0F, logoFadeOutTime).ToObservable();
            var fadeOutSpinner = _spinnerFader.FadeTo(0.0F, spinnerFadeOutTime).ToObservable();

            _logoCancellationToken = 
                fadeOutLogo
                    .SelectMany(DisableGameObject(logo).ToObservable())
                    .Subscribe();
            _spinnerCancellationToken = 
                fadeOutSpinner
                    .SelectMany(DisableGameObject(spinner))
                    .Subscribe();
        }

        private static IEnumerator DisableGameObject(GameObject obj)
        {
            obj.SetActive(false);
            yield break;
        }

        private void DisposeTokens()
        {
            _logoCancellationToken?.Dispose();
            _spinnerCancellationToken?.Dispose();
        }
    }
}
