using UnityEngine;

namespace Seagull.Visualisation.Components.UserInterface
{
    public class LoaderRotation : MonoBehaviour
    {
        public float rotationSpeed = 125f;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _rectTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}
