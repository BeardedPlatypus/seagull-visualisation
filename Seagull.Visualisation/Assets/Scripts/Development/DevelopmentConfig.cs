using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PathLib;
using UnityEngine;

namespace Seagull.Visualisation.Development
{
    /// <summary>
    /// <see cref="DevelopmentConfig"/> defines several properties
    /// relevant to development and debug purposes.
    /// </summary>
    public static class DevelopmentConfig
    {
        private static readonly IPath ConfigPath = Paths.Create("development-config.json");
        
        /// <summary>
        /// Initializes this <see cref="DevelopmentConfig"/>.
        /// </summary>
        static DevelopmentConfig()
        {
            if (!ConfigPath.Exists())
            {
                Debug.LogWarning($"Could not find {ConfigPath}.");
            }
            
            using var file = new StreamReader(ConfigPath.Open(FileMode.Open));
            using var reader = new JsonTextReader(file);
            
            var data = (JObject) JToken.ReadFrom(reader);
            TestModelPath = Paths.Create((string) data["TestModel"]);
        }

        /// <summary>
        /// Get the path to the default test model.
        /// </summary>
        public static IPath TestModelPath { get; }
    }
}