using Seagull.Visualisation.UserInterface.FileDialogs;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.UserInterface
{
    public class MainMenuController : MonoBehaviour
    {
        private IDialogService _dialogService;
        
        [Inject]
        public void Init(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        
        public void CreateNewProject_Click()
        {
            Debug.Log("Clicked 'Create new project'");
        }
        
        public void LoadProject_Click()
        {
            var configuration = new FileDialogConfiguration
            {
                ExtensionFilters = new[]
                {
                    ExtensionFilter.Predefined.SeagullProjectFiles,
                    ExtensionFilter.Predefined.AllFiles,
                }
            };
            
            var _ = _dialogService.OpenFileDialog(configuration);
        }

        public void SelectDemoProject_Click()
        {
            Debug.Log("Clicked 'Select demo project'");
        }
    }
}
