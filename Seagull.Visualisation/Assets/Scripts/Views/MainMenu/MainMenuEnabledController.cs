using Seagull.Visualisation.Components.Loading.Messages;
using UniRx;
using UnityEngine;

namespace Seagull.Visualisation.Views.MainMenu
{
    /// <summary>
    /// <see cref="MainMenuEnabledController"/> controls whether the main menu
    /// is visible or not.
    /// </summary>
    public class MainMenuEnabledController : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void Start()
        {
            MessageBroker.Default.Receive<MainMenuToggleMessage>()
                                 .Subscribe(SetIsActive)
                                 .AddTo(this);
        }

        private void SetIsActive(MainMenuToggleMessage msg) => canvas.SetActive(msg.ShouldBeActive);
    }
}
