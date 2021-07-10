using System;
using System.Collections.Generic;
using Seagull.Visualisation.Views.MainMenu.Common;
using UniRx;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.PageState
{
    public sealed class Controller : MonoBehaviour
    {
        private IReadOnlyDictionary<Page, IPageController> _controllers;
        private Page _activePage = Page.OpeningPage;

        [Inject]
        public void Init(NewProjectPage.Controller newProjectController,
                         OpeningPage.Controller openingPageController)
        {
            _controllers = new Dictionary<Page, IPageController>()
            {
                { Page.NewProjectPage, newProjectController },
                { Page.OpeningPage, openingPageController },
            };
            
        }

        public void RegisterPageObservable(IObservable<Page> observable) =>
            observable.Subscribe(Activate).AddTo(this);

        private void Activate(Page page)
        {
            if (page == _activePage) return;
            
            _controllers[_activePage].IsActive.Value = false;
            _activePage = page;
            _controllers[_activePage].IsActive.Value = true;
        }
    }
}