using System;
using BeardedPlatypus.OrbitCamera.Core;
using Seagull.Visualisation.Components.Camera.Messages;
using Seagull.Visualisation.Components.Common;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Seagull.Visualisation.Components.Camera
{
    // TODO: set up the messaging to handle the Set operations.
    /// <summary>
    /// <see cref="Bindings"/> implements the boilerplate to connect the InputActions
    /// to the OrbitCamera's <see cref="IBindings"/> interface.
    /// </summary>
    public class CameraBindings : MonoBehaviour, IBindings
    {
        private CameraInputActions _inputActions;
 
        [Inject]
        private void Init(CameraInputActions inputActions)
        {
            _inputActions = inputActions;
        }
        
        public IObservable<Vector2> Orbit { get; private set; }
        public IObservable<Vector3> Translate { get; private set; }
        public IObservable<float> Zoom { get; private set; }

        public IObservable<Vector2> SetOrbit { get; private set; }
        public IObservable<Vector3> SetPosition { get; private set; }
        public IObservable<float> SetZoom { get; private set; }

        private void Awake()
        {
            ConfigureObservables();
        }

        private void Start()
        {
            _inputActions.Enable();
        }

        private void ConfigureObservables()
        {
            IObservable<bool> isActive = 
                MessageBroker.Default.Receive<SetIsActiveMessage>()
                                     .Select(msg => msg.Value)
                                     .DistinctUntilChanged()
                                     .StartWith(false);
            
            IObservable<Vector2> dragStream = _inputActions.Camera.Drag.ActionAsObservable()
                .Select(InterpretAs<Vector2>);

            ConfigureOrbitObservable(dragStream, isActive);
            ConfigureTranslateObservable(dragStream, isActive);
            ConfigureZoomObservable(isActive);

            ConfigureSetObservables(isActive);
        }
        
        private void ConfigureOrbitObservable(IObservable<Vector2> dragStream,
                                              IObservable<bool> isActiveStream)
        {

            IObservable<bool> isOrbitingStream = 
                _inputActions.Camera.OrbitActive.ActionAsObservable()
                                                .Select(InterpretAsBool)
                                                .DistinctUntilChanged();

            IObservable<bool> isOrbitingAndActiveStream =
                isOrbitingStream.CombineLatest(isActiveStream, (isOrbiting, isActive) => isOrbiting && isActive)
                                .DistinctUntilChanged();

            Orbit = isOrbitingAndActiveStream
                .CombineLatest(dragStream, (isActive, direction) => (isActive, direction))
                .Where(x => x.isActive)
                .Select(x => x.direction);
        }

        private void ConfigureTranslateObservable(IObservable<Vector2> dragStream,
                                                  IObservable<bool> isActiveStream)
        {
            IObservable<bool> isTranslatingStream = _inputActions.Camera.TranslateActive.ActionAsObservable()
                .Select(InterpretAsBool)
                .DistinctUntilChanged()
                .CombineLatest(isActiveStream, (isTranslating, isActive) => isTranslating && isActive)
                .DistinctUntilChanged();

            IObservable<bool> isAlternativeStream = _inputActions.Camera.TranslateAlt.ActionAsObservable()
                .Select(InterpretAsBool)
                .StartWith(false)
                .DistinctUntilChanged()
                .CombineLatest(isActiveStream, (isAltTranslating, isActive) => isAltTranslating && isActive)
                .DistinctUntilChanged();

            Translate = isAlternativeStream
                .CombineLatest(dragStream, ToTranslation)
                .CombineLatest(isTranslatingStream, (translation, isActive) => (isActive, translation))
                .Where(x => x.isActive)
                .Select(x => x.translation);
        }

        private static Vector3 ToTranslation(bool isAlt, Vector2 dragDirection) =>
            isAlt ? new Vector3(dragDirection.x, dragDirection.y, 0F)
                  : new Vector3(dragDirection.x, 0F, dragDirection.y);

        private void ConfigureZoomObservable(IObservable<bool> isActiveStream)
        {
            IObservable<float> zoomStream = 
                _inputActions.Camera.Zoom.ActionAsObservable()
                                         .Select(InterpretAs<float>);

            Zoom = isActiveStream.CombineLatest(zoomStream, (isActive, zoom) => (isActive, zoom))
                                 .Where(x => x.isActive)
                                 .Select(x => x.zoom);
        }

        private static T InterpretAs<T>(InputAction.CallbackContext context) where T : struct => 
            context.ReadValue<T>();
        private static bool InterpretAsBool(InputAction.CallbackContext context) => 
            context.ReadValue<float>() != 0F;

        private void ConfigureSetObservables(IObservable<bool> isActive)
        {
            // In the future other streams that work with the set behaviour can be mixed in here.
            SetOrbit = Observable.Empty<Vector2>();
            SetPosition = Observable.Empty<Vector3>();
            SetZoom = Observable.Empty<float>();
        }
    }
}
