using System.Linq;
using Seagull.Visualisation.Core.Application.World;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.ProjectEditor.World
{
    public class TerrainManager : MonoBehaviour
    {
        public GameObject tilePrefab;
        
        private ITileSourceRepository _repository;
        private TileSourceKey _sourceKey;
        
        private static readonly int BaseColorMap = Shader.PropertyToID("_BaseMap");
        
        [Inject]
        public void Init(ITileSourceRepository tileSourceRepository)
        {
            _repository = tileSourceRepository;
        }

        private void Start()
        {
            _sourceKey = _repository.RetrieveTileSourceKeys().First();
            
            for (int i = 0; i < 4; i++) 
            for (int j = 0; j < 4; j++) 
            { 
                InstantiateTile(i, j, 2); 
            }
        }
        
        private GameObject InstantiateTile(int x, int y, int zoomLevel)
        {
            GameObject tile = Instantiate(tilePrefab, new Vector3(x - 2 , 0, y - 2 ), Quaternion.Euler(90.0F, 0.0F, 0.0F));
            tile.name = $"tile_{x}_{y}";
            tile.transform.parent = transform;
            
            SetTileTexture(tile, x, y, zoomLevel);
     
            return tile;
        }
     
        private void SetTileTexture(GameObject tile, int x, int y, int zoomLevel)
        {
            TileSource source = _repository.RetrieveTileSource(_sourceKey);
            
            var terrainTex = new Texture2D(1, 1);
     
            var terrainTexData = source.RetrieveTile(x, y, zoomLevel);
            terrainTex.LoadImage(terrainTexData, false);
     
            var tileRenderer = tile.GetComponent<MeshRenderer>();
            tileRenderer.material.SetTexture(BaseColorMap, terrainTex);
        }
    }
}
