using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seagull.Visualisation.UserInterface
{
    public class MainMenuController : MonoBehaviour
    {
        public void CreateNewProject_Click()
        {
            Debug.Log("Clicked 'Create new project'");
            
        }
        
        public void LoadProject_Click()
        {
            Debug.Log("Clicked 'Load project'");
        }

        public void SelectDemoProject_Click()
        {
            Debug.Log("Clicked 'Select demo project'");
        }
    }
}
