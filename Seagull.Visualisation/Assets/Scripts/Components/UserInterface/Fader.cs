using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Seagull.Visualisation.Components.UserInterface
{
    /// <summary>
    /// <see cref="Fader"/> is a behaviour to fade an <see cref="Image"/>
    /// attached to the  <see cref="GameObject"/> from the current alpha,
    /// to a given other alpha over a set amount of time.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class Fader : MonoBehaviour
    {
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        /// <summary>
        /// Coroutine to fade the image of this <see cref="Fader"/> to the
        /// specified <paramref name="alphaVal"/> over the specified amount
        /// of time.
        /// </summary>
        /// <param name="alphaVal">The goal alpha value.</param>
        /// <param name="totalTime">The amount of time over which to fade.</param>
        /// <returns>
        /// The coroutine to fade the image over the specified amount of time
        /// to the specified alpha value.
        /// </returns>
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
