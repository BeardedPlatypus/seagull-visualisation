using UniRx;

namespace Seagull.Visualisation.Views.MainMenu.Common
{
    public interface IPageController
    {
        ReactiveProperty<bool> IsActive { get; }
    }
}