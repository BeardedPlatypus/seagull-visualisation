using System.Collections.Generic;
using PathLib;
using Seagull.Visualisation.Core.Application.World;
using Seagull.Visualisation.Core.Persistence.World;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Root.Views.ProjectEditor
{
    public class TerrainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ITileSourceRepository repository = new TileSourceRepository(GetTileSources());
            Container.Bind<ITileSourceRepository>()
                     .FromInstance(repository)
                     .AsSingle()
                     .NonLazy();
        }

        private static IEnumerable<TileSource> GetTileSources()
        {
            var tilePath = Paths.Create(Application.persistentDataPath);

            var eoRootPath = tilePath.Join("world", "terrain", "eo_tiles");
            yield return new TileSource("eo_tiles", 2, 6,
                                        new LocalBehaviourImageSharp(eoRootPath),
                                        new RemoteBehaviourNone());
        }
    }
}