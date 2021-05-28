using System.Collections;
using Seagull.Visualisation.Components.Loading;

namespace Seagull.Visualisation.Views.MainMenu
{
    public class CreateProjectToProjectTransition : ISceneTransitionDescription
    {
        public string SceneName => "ProjectEditor";
        
        public IEnumerator PreSceneLoadCoroutine()
        {
            yield break;
        }

        public IEnumerator PostSceneLoadCoroutine()
        {
            yield break;
        }
    }
}