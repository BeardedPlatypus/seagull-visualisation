using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Seagull.Visualisation.Components.Camera
{
    public class EditorCameraController : MonoBehaviour, MouseControls.IMouseActions
    {
        [SerializeField] private float zoomFactor = 0.005F;
        [SerializeField] private float panFactor = 0.005F;
        [SerializeField] private float orbitFactor = 0.05F;
        [SerializeField] private float rotationFactor = 0.005F;

        private Transform _virtualCameraTransform;
        private MouseControls _mouseControls;

        [SerializeField] private bool isPanning = false;
        [SerializeField] private bool isAlternativeMove  = false;
        [SerializeField] private bool isRotating = false;

        [CanBeNull]
        [SerializeField] private Vector3? orbitCenter = null;

        private int terrainLayerMask;
        
        private void Awake()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _virtualCameraTransform = virtualCamera.transform;
        }

        private void Start()
        {
            terrainLayerMask = LayerMask.GetMask("Terrain");
        }

        private void OnEnable()
        {
            if (_mouseControls == null)
            {
                _mouseControls = new MouseControls();
                _mouseControls.Mouse.SetCallbacks(this);
            }
            
            _mouseControls.Mouse.Enable();
        }

        private void OnDisable()
        {
            _mouseControls.Mouse.Disable();
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if (isRotating)
            {
                if (!isAlternativeMove) HandleOrbit(context);
                else HandleRotate(context);
            }
            else if (isPanning)
            {
                if (!isAlternativeMove) HandlePanXZ(context);
                else HandlePanXY(context);
            }
        }

        private void HandleOrbit(InputAction.CallbackContext context)
        {
            if (!orbitCenter.HasValue) return;
            
            Vector2 translation = context.ReadValue<Vector2>() * orbitFactor;
            
            var worldX = _virtualCameraTransform.TransformVector(Vector3.left);
            _virtualCameraTransform.RotateAround(orbitCenter.Value, worldX, translation.y);
            
            _virtualCameraTransform.RotateAround(orbitCenter.Value, Vector3.up, translation.x);
        }

        private void HandleRotate(InputAction.CallbackContext context)
        {
            Vector2 translation = context.ReadValue<Vector2>() * rotationFactor;

            _virtualCameraTransform.Rotate(Vector3.down, translation.x, Space.World);
            _virtualCameraTransform.Rotate(Vector3.right, translation.y, Space.Self);
        }
        
        private void HandlePanXY(InputAction.CallbackContext context)
        {
            Vector2 translation = context.ReadValue<Vector2>() * panFactor;

            var localTranslation = new Vector3(-translation.x, -translation.y, 0F);
            _virtualCameraTransform.Translate(localTranslation);
        }
        
        private void HandlePanXZ(InputAction.CallbackContext context)
        {
            Vector2 translation = context.ReadValue<Vector2>() * panFactor;

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

        public void OnZoom(InputAction.CallbackContext context)
        {
            var scroll = context.ReadValue<float>();
            if (scroll == 0F) return;

            var localTranslation = new Vector3(0F, 0F, scroll * zoomFactor);
            _virtualCameraTransform.Translate(localTranslation);
        }

        public void OnRotateActive(InputAction.CallbackContext context)
        {
            
            var newIsRotating = context.ReadValue<float>() != 0F;

            if (!isRotating && newIsRotating) CalculateOrbitFocus();

            isRotating = newIsRotating;
        }

        private void CalculateOrbitFocus()
        {
            var direction = _virtualCameraTransform.TransformDirection(Vector3.forward);
            const float maxDistance = 10F;

            var origin = _virtualCameraTransform.position;

            orbitCenter = 
                Physics.Raycast(origin, direction, out var hit, maxDistance, terrainLayerMask)
                ? hit.point 
                : origin + maxDistance * direction;
        }

        public void OnAlternativeMoveActive(InputAction.CallbackContext context) =>
            isAlternativeMove = context.ReadValue<float>() != 0F;
        
        public void OnPanActive(InputAction.CallbackContext context) =>
            isPanning = context.ReadValue<float>() != 0F;
    }
}
