using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Seagull.Visualisation.Components.Loading
{
    [RequireComponent(typeof(Image))]
    public class Fader : MonoBehaviour
    {
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public IEnumerator FadeTo(float alphaVal, float totalTime)
        {
            var timeFactor = 1.0F / totalTime;

            var origColor = _image.color;
            var goalColor = new Color(_image.color.r,
                                      _image.color.g,
                                      _image.color.b,
                                      alphaVal);

            for (var t = 0.0F; t < 1.0F; t += Time.deltaTime * timeFactor)
            {
                _image.color = Color.Lerp(origColor, goalColor, t);
                yield return null;
            }
        }
    }
}
