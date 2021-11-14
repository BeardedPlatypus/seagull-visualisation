using System.Linq;
using Seagull.Visualisation.Components.Project;
using Seagull.Visualisation.Components.UserInterface;
using Seagull.Visualisation.Core.Domain.Model;
using Seagull.Visualisation.Core.Domain.Model.Grid;
using UniRx;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Components.Model
{
    public class ModelLoader : MonoBehaviour
    {
        private ProjectDescription _projectDescription;
        
        [Inject]
        public void Init(ProjectDescription projectDescription)
        {
            _projectDescription = projectDescription;
        }
        
        private void Start()
        {
            MessageBroker.Default.Publish(new LoadingIndication.StartMessage());

            IMesh2D[] mesh2d = _projectDescription.ModelRepository.RetrieveMeshes2D().ToArray();

            var retrieveVertices = Observable.Start(RetrieveVertices);
            Observable.WhenAll(retrieveVertices)
                      .ObserveOnMainThread()
                      .Subscribe(HandleLoadedVertices);
        }

        private Vertex2D[] RetrieveVertices() =>
            _projectDescription
                .ModelRepository
                .RetrieveMeshes2D()
                .First()
                .RetrieveVertices()
                .ToArray();

        private void HandleLoadedVertices(Vertex2D[][] array)
        {
            MessageBroker.Default.Publish(new LoadingIndication.StopMessage());
        }
    }
}
