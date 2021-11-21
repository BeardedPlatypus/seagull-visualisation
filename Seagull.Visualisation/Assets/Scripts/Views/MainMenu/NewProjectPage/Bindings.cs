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
        [SerializeField] private FileSelectionRowController projectLocation;
        
        /// <summary>
        /// The <see cref="Button"/> to return to the <see cref="OpeningPage"/>.
        /// </summary>
        [SerializeField] private Button backButton;
        
        /// <summary>
        /// The <see cref="Button"/> to create a new project.
        /// </summary>
        [SerializeField] private Button createProjectButton;
        
        [SerializeField] private Animator animator;

        private Controller _controller;
        
        private static readonly int IsActive = Animator.StringToHash("IsActive");

        [Inject]
        public void Init(Controller controller)
        {
            _controller = controller;
        }

        private void Start()
        {
            projectLocation.Handler = _controller.ProjectLocationHandler;

            createProjectButton.OnClickAsObservable()
                               .Subscribe(_ => _controller.OnCreateProject())
                               .AddTo(this);
            
            backButton.OnClickAsObservable()
                      .Select(_ => new ChangePageMessage(PageState.Page.OpeningPage))
                      .Subscribe(MessageBroker.Default.Publish)
                      .AddTo(this);
            
            _controller.IsActive.Subscribe(val => animator.SetBool(IsActive, val))
                                .AddTo(this);
        }
    }
}
