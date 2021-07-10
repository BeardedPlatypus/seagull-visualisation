using UniRx;
using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace Seagull.Visualisation.Views.MainMenu.OpeningPage
{
    public class Bindings : MonoBehaviour
    {
        // Bindable objects
        public Button createNewProjectButton;
        public Button loadProjectButton;
        public Button selectDemoProject;
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
            _pageStateController.RegisterPageObservable(
                createNewProjectButton.OnClickAsObservable()
                                      .Select(_ => PageState.Page.NewProjectPage));

            loadProjectButton.OnClickAsObservable()
                             .Subscribe(_ => _controller.OnLoadProject())
                             .AddTo(this);
            selectDemoProject.OnClickAsObservable()
                             .Subscribe(_ => _controller.OnSelectDemoProject())
                             .AddTo(this);

            _controller.IsActive.Subscribe(val => animator.SetBool(IsActive, val))
                                .AddTo(this) ;
        }
    }
}