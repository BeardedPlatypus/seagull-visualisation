using System.Linq;
using Seagull.Visualisation.Components.FileDialogs;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        private IDialogService _dialogService;

        // TODO: introduce some underlying state for this?
        public GameObject recentProjectsMenu;
        public GameObject createNewProjectMenu;
        
        [Inject]
        public void Init(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private void Start()
        {
            SetIsActiveMenu(recentProjectsMenu);
        }

        public void CreateNewProject_Click() =>
            SetIsActiveMenu(createNewProjectMenu);


        private void SetIsActiveMenu(GameObject menu)
        {
            recentProjectsMenu.SetActive(menu == recentProjectsMenu);
            createNewProjectMenu.SetActive(menu == createNewProjectMenu);
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
        
        // TODO: Create a separate controller per menu?
        // CreateNewProjectButtons
        public void CreateNewProjectMenu_Back_Click() =>
            SetIsActiveMenu(recentProjectsMenu);

        public TMPro.TMP_InputField mapFilePath;
        
        public void SelectMapFile_Click()
        {
            var configuration = new FileDialogConfiguration
            { 
                ExtensionFilters = new[] 
                {
                    ExtensionFilter.Predefined.NetcdfFiles, 
                    ExtensionFilter.Predefined.AllFiles,
                }
            };

            var result = _dialogService.OpenFileDialog(configuration).FirstOrDefault();
            mapFilePath.text = result ?? mapFilePath.text;
        }
    }
}
