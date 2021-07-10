using Seagull.Visualisation.Views.MainMenu.PageState;
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
        
        private static readonly int IsActive = Animator.StringToHash("IsActive");

        [Inject]
        public void Init(Controller controller)
        {
            _controller = controller;
        }

        private void Start()
        {
            createNewProjectButton.OnClickAsObservable()
                                  .Select(_ => new ChangePageMessage(PageState.Page.NewProjectPage))
                                  .Subscribe(MessageBroker.Default.Publish)
                                  .AddTo(this);

            loadProjectButton.OnClickAsObservable()
                             .Select(_ => _controller.RequestProjectPath())
                             .Where(p => p != null)
                             .Subscribe(_controller.OnLoadProject)
                             .AddTo(this);
            
            selectDemoProject.OnClickAsObservable()
                             .Subscribe(_ => _controller.OnSelectDemoProject())
                             .AddTo(this);

            _controller.IsActive.Subscribe(val => animator.SetBool(IsActive, val))
                                .AddTo(this) ;
        }
    }
}