namespace Seagull.Visualisation.Components.Loading
{
    public sealed class ChangeSceneMessage
    {
        public ChangeSceneMessage(ISceneTransitionDescription sceneTransitionDescription) =>
            SceneTransitionDescription = sceneTransitionDescription;

        public ISceneTransitionDescription SceneTransitionDescription { get; }
    }
}