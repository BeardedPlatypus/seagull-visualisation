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

            const int zoomLevel = 3;
            const int nTiles = 2 << (zoomLevel - 1);

            for (int i = 0; i < nTiles; i++) 
            for (int j = 0; j < nTiles; j++) 
            { 
                InstantiateTile(i, j, zoomLevel); 
            }
        }
        
        private GameObject InstantiateTile(int x, int y, int zoomLevel)
        {
            int tileOffset = 2 << (zoomLevel - 2);
            
            GameObject tile = Instantiate(tilePrefab, new Vector3(x - tileOffset, 0, y - tileOffset ), Quaternion.Euler(90.0F, 0.0F, 0.0F));
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
