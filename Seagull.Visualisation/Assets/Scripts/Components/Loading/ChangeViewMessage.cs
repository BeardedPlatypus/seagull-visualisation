namespace Seagull.Visualisation.Components.Loading
{
    public sealed class ChangeViewMessage
    {
        public ChangeViewMessage(IViewTransitionDescription viewTransitionDescription) =>
            ViewTransitionDescription = viewTransitionDescription;

        public IViewTransitionDescription ViewTransitionDescription { get; }
    }
}