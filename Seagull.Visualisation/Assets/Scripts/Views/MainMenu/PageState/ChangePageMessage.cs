namespace Seagull.Visualisation.Views.MainMenu.PageState
{
    public sealed class ChangePageMessage
    {
        public ChangePageMessage(Page goalPage) => GoalPage = goalPage;
        
        public Page GoalPage { get; }
    }
}