using Seagull.Visualisation.Core.Domain.Model;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Components.Model.Geometry
{
    /// <summary>
    /// <see cref="Grid"/> defines a 
    /// </summary>
    public class Mesh2DGeometry : MonoBehaviour
    {
        private IMesh2D _mesh2D;
        
        [Inject]
        public void Init(IMesh2D mesh2D)
        {
            _mesh2D = mesh2D;
        }
    }
}
