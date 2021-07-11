using System;
using Seagull.Visualisation.Components.Common;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Seagull.Visualisation.Components.Camera
{
    public class EditorCameraInputBindings : MonoBehaviour
    {
        private MouseControls _mouseControls;

        [Inject]
        private void Init(MouseControls mouseControls)
        {
            _mouseControls = mouseControls;
        }

        private void Awake()
        {
            ConfigureObservables();
        }

        private void ConfigureObservables()
        {
            IObservable<Vector2> dragStream = 
                _mouseControls.Mouse.Drag.ActionAsObservable()
                                         .Select(InterpretAs<Vector2>);
            Zoom = _mouseControls.Mouse.Zoom.ActionAsObservable()
                                            .Select(InterpretAs<float>);
            IObservable<bool> isRotatingStream = 
                _mouseControls.Mouse.RotateActive.ActionAsObservable()
                                                 .Select(InterpretAsBool)
                                                 .DistinctUntilChanged();
            IObservable<bool> isPanningStream =
                _mouseControls.Mouse.PanActive.ActionAsObservable()
                                              .Select(InterpretAsBool)
                                              .DistinctUntilChanged();
            IObservable<bool> isAlternativeMoveStream = 
                _mouseControls.Mouse.AlternativeMoveActive.ActionAsObservable()
                                                          .Select(InterpretAsBool)
                                                          .StartWith(false)
                                                          .DistinctUntilChanged();
            
            var isRotatingActiveStream =
                isRotatingStream.CombineLatest(isAlternativeMoveStream,
                                               (isRot, isAlt) => isRot && isAlt)
                                .DistinctUntilChanged();
            Rotate = GetFilteredDragBasedStream(dragStream, isRotatingActiveStream);

            var isOrbitingActiveStream =
                isRotatingStream.CombineLatest(isAlternativeMoveStream,
                                               (isRot, isAlt) => isRot && !isAlt)
                                .DistinctUntilChanged();
            Orbit = GetFilteredDragBasedStream(dragStream, isOrbitingActiveStream);

            var isPanningXZ =
                isPanningStream.CombineLatest(isAlternativeMoveStream,
                                              (isPan, isAlt) => isPan && !isAlt);
            PanXZ = GetFilteredDragBasedStream(dragStream, isPanningXZ);

            var isPanningXY =
                isPanningStream.CombineLatest(isAlternativeMoveStream,
                                              (isPan, isAlt) => isPan && isAlt);
            PanXY = GetFilteredDragBasedStream(dragStream, isPanningXY);
        }

        private static IObservable<Vector2> GetFilteredDragBasedStream(IObservable<Vector2> dragStream,
                                                                       IObservable<bool> isActiveStream) =>
            isActiveStream.DistinctUntilChanged()
                          .CombineLatest(dragStream, 
                                         (isActive, direction) => (isActive, direction))
                          .Where(x => x.isActive)
                          .Select(x => x.direction);

        private static T InterpretAs<T>(InputAction.CallbackContext context) where T : struct =>
            context.ReadValue<T>();
        
        private static bool InterpretAsBool(InputAction.CallbackContext context) =>
            context.ReadValue<float>() != 0F;
        
        public IObservable<Vector2> PanXZ { get; private set; }
        
        public IObservable<Vector2> PanXY { get; private set; }
        
        
        public IObservable<Vector2> Rotate { get; private set; }
        
        public IObservable<Vector2> Orbit { get; private set; }
        
        public IObservable<float> Zoom { get; private set; }
        
        private void OnEnable()
        {
            _mouseControls.Mouse.Enable();
        }

        private void OnDisable()
        {
            _mouseControls.Mouse.Disable();
        }
    }
}
