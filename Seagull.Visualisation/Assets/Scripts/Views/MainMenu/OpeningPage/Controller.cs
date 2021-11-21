using System.Linq;
using PathLib;
using Seagull.Visualisation.Components.FileDialogs;
using Seagull.Visualisation.Components.Loading;
using Seagull.Visualisation.Views.MainMenu.Common;
using UniRx;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage
{
    public sealed class Controller : IPageController
    {
        private readonly ViewTransitionFactory _viewTransitionFactory;
        private readonly IDialogService _dialogService;

        public Controller(ViewTransitionFactory viewTransitionFactory,
                          IDialogService dialogService)
        {
            _dialogService = dialogService;
            _viewTransitionFactory = viewTransitionFactory;
        }

        public IPath RequestProjectPath()
        {
            var configuration = new FileDialogConfiguration
            {
                ExtensionFilters = new[]
                {
                    ExtensionFilter.Predefined.SeagullProjectFiles,
                    ExtensionFilter.Predefined.AllFiles,
                }
            };
            
            return _dialogService.OpenFileDialog(configuration).FirstOrDefault();
        }
        
        public void OnLoadProject(IPath path)
        {
            var description = _viewTransitionFactory.GetLoadProjectTransition(path);
            var msg = new ChangeViewMessage(description);
            MessageBroker.Default.Publish(msg);
        }
        
        public void OnSelectDemoProject() { }

        public ReactiveProperty<bool> IsActive { get; } = new ReactiveProperty<bool>(true);
    }
}