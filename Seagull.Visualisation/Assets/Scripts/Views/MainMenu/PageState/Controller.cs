using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Seagull.Visualisation.Views.MainMenu.PageState
{
    public class Controller : MonoBehaviour
    {
        private IReadOnlyCollection<Tuple<Page, GameObject>> _pages;
        private IReadOnlyDictionary<Page, PageController> _controllers;

        public PageController newProjectPageController;
        public PageController openingPageController;

        private Page _activePage = Page.OpeningPage;

        [Inject]
        public void Init(NewProjectPage.Bindings newProjectPage,
                          OpeningPage.Bindings openingPage)
        {
            _pages = new[]
            {
                new Tuple<Page, GameObject>(Page.NewProjectPage, newProjectPage.gameObject),
                new Tuple<Page, GameObject>(Page.OpeningPage, openingPage.gameObject),
            };
            
        }
        
        private void Start()
        {
            _controllers = new Dictionary<Page, PageController>
            {
                {Page.OpeningPage, openingPageController},
                {Page.NewProjectPage, newProjectPageController},
            };
        }

        public void Activate(Page page)
        {
            if (page == _activePage) return;
            
            _controllers[_activePage].Deactivate();
            _activePage = page;
            _controllers[_activePage].Activate();
        }
    }
}