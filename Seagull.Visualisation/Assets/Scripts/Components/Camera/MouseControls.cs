// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Components/Camera/MouseControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Seagull.Visualisation.Components.Camera
{
    public class @MouseControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MouseControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MouseControls"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""714534af-4dee-4e2a-b0eb-1ed6bce26686"",
            ""actions"": [
                {
                    ""name"": ""PanActive"",
                    ""type"": ""PassThrough"",
                    ""id"": ""807e668c-22ea-48ac-8ee2-1afacbbb53d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""19ad1943-db14-49c5-81c9-1ee424d1c72a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a71d16a3-7d5b-462d-9689-43c724793af1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateActive"",
                    ""type"": ""PassThrough"",
                    ""id"": ""63851f1f-84a6-4510-be48-25beb0560357"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AlternativeMoveActive"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6499118a-b06f-462f-98c2-e37b126abdbd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4de4cf43-3ca4-4a75-99af-72f72a4cc9cb"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""069d4306-8a42-460b-932f-c1dbaf6a583e"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""457d6b27-6e2f-4628-b793-8250ae108ca8"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PanActive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b30b89d-e79b-4a0f-a949-a6419b010065"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateActive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d3f75f0-419d-49df-9088-05c374206540"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AlternativeMoveActive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Mouse
            m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
            m_Mouse_PanActive = m_Mouse.FindAction("PanActive", throwIfNotFound: true);
            m_Mouse_Drag = m_Mouse.FindAction("Drag", throwIfNotFound: true);
            m_Mouse_Zoom = m_Mouse.FindAction("Zoom", throwIfNotFound: true);
            m_Mouse_RotateActive = m_Mouse.FindAction("RotateActive", throwIfNotFound: true);
            m_Mouse_AlternativeMoveActive = m_Mouse.FindAction("AlternativeMoveActive", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Mouse
        private readonly InputActionMap m_Mouse;
        private IMouseActions m_MouseActionsCallbackInterface;
        private readonly InputAction m_Mouse_PanActive;
        private readonly InputAction m_Mouse_Drag;
        private readonly InputAction m_Mouse_Zoom;
        private readonly InputAction m_Mouse_RotateActive;
        private readonly InputAction m_Mouse_AlternativeMoveActive;
        public struct MouseActions
        {
            private @MouseControls m_Wrapper;
            public MouseActions(@MouseControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @PanActive => m_Wrapper.m_Mouse_PanActive;
            public InputAction @Drag => m_Wrapper.m_Mouse_Drag;
            public InputAction @Zoom => m_Wrapper.m_Mouse_Zoom;
            public InputAction @RotateActive => m_Wrapper.m_Mouse_RotateActive;
            public InputAction @AlternativeMoveActive => m_Wrapper.m_Mouse_AlternativeMoveActive;
            public InputActionMap Get() { return m_Wrapper.m_Mouse; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
            public void SetCallbacks(IMouseActions instance)
            {
                if (m_Wrapper.m_MouseActionsCallbackInterface != null)
                {
                    @PanActive.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnPanActive;
                    @PanActive.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnPanActive;
                    @PanActive.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnPanActive;
                    @Drag.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnDrag;
                    @Drag.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnDrag;
                    @Drag.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnDrag;
                    @Zoom.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                    @RotateActive.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnRotateActive;
                    @RotateActive.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnRotateActive;
                    @RotateActive.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnRotateActive;
                    @AlternativeMoveActive.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnAlternativeMoveActive;
                    @AlternativeMoveActive.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnAlternativeMoveActive;
                    @AlternativeMoveActive.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnAlternativeMoveActive;
                }
                m_Wrapper.m_MouseActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PanActive.started += instance.OnPanActive;
                    @PanActive.performed += instance.OnPanActive;
                    @PanActive.canceled += instance.OnPanActive;
                    @Drag.started += instance.OnDrag;
                    @Drag.performed += instance.OnDrag;
                    @Drag.canceled += instance.OnDrag;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                    @RotateActive.started += instance.OnRotateActive;
                    @RotateActive.performed += instance.OnRotateActive;
                    @RotateActive.canceled += instance.OnRotateActive;
                    @AlternativeMoveActive.started += instance.OnAlternativeMoveActive;
                    @AlternativeMoveActive.performed += instance.OnAlternativeMoveActive;
                    @AlternativeMoveActive.canceled += instance.OnAlternativeMoveActive;
                }
            }
        }
        public MouseActions @Mouse => new MouseActions(this);
        public interface IMouseActions
        {
            void OnPanActive(InputAction.CallbackContext context);
            void OnDrag(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
            void OnRotateActive(InputAction.CallbackContext context);
            void OnAlternativeMoveActive(InputAction.CallbackContext context);
        }
    }
}
