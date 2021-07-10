using Seagull.Visualisation.Components.UserInterface;
using Seagull.Visualisation.Views.MainMenu.PageState;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        private Controller _controller;
        private PageState.Controller _pageStateController;
        
        private static readonly int IsActive = Animator.StringToHash("IsActive");

        [Inject]
        public void Init(Controller controller, 
                         PageState.Controller pageStateController)
        {
            _controller = controller;
            _pageStateController = pageStateController;
        }

        private void Start()
        {
            projectLocation.Handler = _controller.ProjectLocationHandler;
            mapFileLocation.Handler = _controller.MapLocationHandler;

            createProjectButton.OnClickAsObservable()
                               .Subscribe(_ => _controller.OnCreateProject())
                               .AddTo(this);

            _pageStateController.RegisterPageObservable(
                backButton.OnClickAsObservable().Select(_ => Page.OpeningPage));
            
            _controller.IsActive.Subscribe(val => animator.SetBool(IsActive, val))
                                .AddTo(this);
        }
    }
}
