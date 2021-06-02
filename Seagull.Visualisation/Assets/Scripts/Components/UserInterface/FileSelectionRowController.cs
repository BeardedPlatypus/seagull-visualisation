using System.Linq;
using JetBrains.Annotations;
using PathLib;
using Seagull.Visualisation.Components.FileDialogs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Seagull.Visualisation.Components.UserInterface
{
    public class FileSelectionRowController : MonoBehaviour
    {
        [CanBeNull]
        public IFileSelectionHandler Handler
        {
            get => _handler;
            set
            {
                _handler = value;
                fileSelectionButton.interactable = _handler != null && _isInteractableButton;
            }
        }

        public string LabelText
        {
            get => label.text;
            set => label.text = value;
        }

        public string InputFieldText
        {
            get => inputField.text;
            set => inputField.text = value;
        }

        public bool IsInteractableButton
        {
            get => _isInteractableButton;
            set
            {
                _isInteractableButton = value;
                fileSelectionButton.interactable = Handler != null && value;
            }
        }

        public bool HasInteractableButton() => fileSelectionButton.interactable;

        public TMPro.TMP_Text label;
        public TMPro.TMP_InputField inputField;
        public Button fileSelectionButton;

        private IDialogService _dialogService;
        [CanBeNull] private IFileSelectionHandler _handler;
        private bool _isInteractableButton = true;

        [Inject]
        private void Init(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        
        private void Start()
        {
            fileSelectionButton.onClick.AddListener(OnButtonClick);
        }
        
        private void OnButtonClick()
        {
            if (!HasInteractableButton()) return;
            
            IPath result = _dialogService.OpenFileDialog(Handler.Configuration)
                                         .FirstOrDefault();

            if (!IsValidPath(result))
            {
                Handler.HandleInvalidOperation(result);
                return;
            }
            
            result = Handler.TransformPath(result);

            InputFieldText = result.ToString();
            Handler.HandleValidOperation(result);
        }

        private bool IsValidPath([CanBeNull] IPath path) =>
            path != null && Handler != null && Handler.ValidatePath(path);
    }
}
