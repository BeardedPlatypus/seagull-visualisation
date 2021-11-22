using Seagull.Visualisation.Components.Loading.Messages;
using UniRx;
using UnityEngine;

namespace Seagull.Visualisation.Views.ProjectEditor.UserInterface
{
    public class EditorUserInterfaceEnabledController : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void Start()
        {
            MessageBroker.Default.Receive<EditorUserInterfaceToggleMessage>()
                                 .Subscribe(SetIsActive)
                                 .AddTo(this);
        }

        private void SetIsActive(EditorUserInterfaceToggleMessage msg) => canvas.SetActive(msg.ShouldBeActive);
    }
}