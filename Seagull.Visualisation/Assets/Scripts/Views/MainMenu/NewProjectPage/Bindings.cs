using Seagull.Visualisation.Components.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace Seagull.Visualisation.Views.MainMenu.NewProjectPage
{
    /// <summary>
    /// <see cref="Bindings"/> implements components accessible to a controller
    /// to visualise the state.
    /// </summary>
    public class Bindings : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="FileSelectionRowController"/> describing the project 
        /// location.
        /// </summary>
        public FileSelectionRowController projectLocation;
        
        /// <summary>
        /// The <see cref="FileSelectionRowController"/> describing the map file
        /// location.
        /// </summary>
        public FileSelectionRowController mapFileLocation;
        
        /// <summary>
        /// The <see cref="Button"/> to return to the <see cref="OpeningPage"/>.
        /// </summary>
        public Button backButton;
        
        /// <summary>
        /// The <see cref="Button"/> to create a new project.
        /// </summary>
        public Button createProjectButton;
        
        public Animator animator;
    }
}
