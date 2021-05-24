using System;
using Seagull.Visualisation.UserInterface.FileDialogs;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.UserInterface
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

        public void CreateNewProjectMenu_Back_Click() =>
            SetIsActiveMenu(recentProjectsMenu);

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
    }
}
