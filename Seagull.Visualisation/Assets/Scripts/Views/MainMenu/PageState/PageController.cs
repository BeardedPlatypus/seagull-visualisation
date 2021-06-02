using UnityEngine;

namespace Seagull.Visualisation.Views.MainMenu.PageState
{
    public abstract class PageController : MonoBehaviour
    {
        public abstract void Activate();
        public abstract void Deactivate();
    }
}