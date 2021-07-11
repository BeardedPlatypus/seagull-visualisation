using Cinemachine;
using UnityEngine;
using UniRx;
using Zenject;

namespace Seagull.Visualisation.Components.Camera
{
    public class EditorCameraController : MonoBehaviour
    {
        [SerializeField] private float zoomFactor = 0.005F;
        [SerializeField] private float panFactor = 0.005F;
        [SerializeField] private float orbitFactor = 0.05F;
        [SerializeField] private float rotationFactor = 0.005F;

        private Transform _virtualCameraTransform;
        private EditorCameraInputBindings _bindings;
        
        private int _terrainLayerMask;

        [Inject]
        private void Init(EditorCameraInputBindings bindings)
        {
            _bindings = bindings;
        }
        
        private void Awake()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _virtualCameraTransform = virtualCamera.transform;
        }

        private void Start()
        {
            _terrainLayerMask = LayerMask.GetMask("Terrain");

            ConfigureSubscriptions();
        }

        private void ConfigureSubscriptions()
        {
            _bindings.Zoom.Subscribe(OnZoom)
                          .AddTo(this);
            _bindings.Rotate.Subscribe(OnRotate)
                            .AddTo(this);
            _bindings.Orbit.Subscribe(OnOrbit)
                           .AddTo(this);
            _bindings.PanXZ.Subscribe(OnPanXZ)
                           .AddTo(this);
            _bindings.PanXY.Subscribe(OnPanXY)
                           .AddTo(this);
        }
        
        private void OnZoom(float zoom)
        {
            var localTranslation = new Vector3(0F, 0F, zoom * zoomFactor);
            _virtualCameraTransform.Translate(localTranslation);
        }
        
        private void OnRotate(Vector2 inputTranslation)
        {
            Vector2 translation = inputTranslation * rotationFactor;

            _virtualCameraTransform.Rotate(Vector3.down, translation.x, Space.World);
            _virtualCameraTransform.Rotate(Vector3.right, translation.y, Space.Self);
        }
        
        private void OnOrbit(Vector2 inputTranslation)
        {
            Vector2 translation = inputTranslation * orbitFactor;

            var orbitCenter = CalculateOrbitCentre();
            var worldX = _virtualCameraTransform.TransformVector(Vector3.left);
            _virtualCameraTransform.RotateAround(orbitCenter, worldX, translation.y);
            
            _virtualCameraTransform.RotateAround(orbitCenter, Vector3.up, translation.x);
        }
        
        private Vector3 CalculateOrbitCentre()
        {
            Vector3 direction = _virtualCameraTransform.TransformDirection(Vector3.forward);
            const float maxDistance = 10F;

            Vector3 origin = _virtualCameraTransform.position;

            return Physics.Raycast(origin, direction, out var hit, maxDistance, _terrainLayerMask) 
                ? hit.point 
                : origin + maxDistance * direction;
        }

        
        private void OnPanXY(Vector2 inputTranslation)
        {
            Vector2 translation = inputTranslation * panFactor;

            var localTranslation = new Vector3(-translation.x, -translation.y, 0F);
            _virtualCameraTransform.Translate(localTranslation);
        }
        
        private void OnPanXZ(Vector2 inputTranslation)
        {
            Vector2 translation = inputTranslation * panFactor;

            var xAxisTranslation = new Vector3(-translation.x, 0F, 0);
            var zAxisTranslation = CalculateProjectedZDirection() * -translation.y;

            _virtualCameraTransform.Translate(xAxisTranslation + zAxisTranslation);
        }
        
        private Vector3 CalculateProjectedZDirection()
        {
            var worldForward = _virtualCameraTransform.TransformDirection(Vector3.forward);
            worldForward[1] = 0F;

            return _virtualCameraTransform.InverseTransformDirection(worldForward.normalized);
        }
    }
}
