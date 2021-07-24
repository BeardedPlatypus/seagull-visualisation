using Seagull.Visualisation.Components.UserInterface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Seagull.Visualisation.Development
{
    public class LoadingButton : MonoBehaviour
    {
        public Button startStopButton;
        public TMP_Text startStopText;
        public bool isLoading = false;

        private void Start()
        {
            startStopButton.OnClickAsObservable()
                           .Subscribe(_ => OnClick())
                           .AddTo(this);
        }

        private void OnClick()
        {
            isLoading = !isLoading;
            startStopText.text = isLoading ? "Stop Loading" : "Start Loading";

            if (isLoading)
                MessageBroker.Default.Publish(new LoadingIndication.StartMessage());
            else 
                MessageBroker.Default.Publish(new LoadingIndication.StopMessage());
        }
    }
}
