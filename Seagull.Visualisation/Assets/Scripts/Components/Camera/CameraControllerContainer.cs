using UnityEngine;
using BeardedPlatypus.OrbitCamera.Core;
using UniRx;
using Zenject;

namespace Seagull.Visualisation.Components.Camera
{
    /// <summary>
    /// <see cref="CameraControllerContainer"/> provides a container for the
    /// <see cref="BeardedPlatypus.OrbitCamera.Core.Controller"/>
    /// </summary>
    public sealed class CameraControllerContainer : MonoBehaviour
    {
        private Controller _controller;

        [Inject]
        private void Init(Controller controller)
        {
            _controller = controller;
        }

        private void Start()
        {
            // We assume that this script is attached to the virtual camera it
            // controls.
            _controller.VirtualCameraTransform = transform;
            _controller.AddTo(this);
        }
    }
}
