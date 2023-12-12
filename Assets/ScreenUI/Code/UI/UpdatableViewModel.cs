using System;
using TatmanGames.Common;
using UnityEngine;
using TatmanGames.Common.Interfaces;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// For data driven views that need to be refreshed at regular intervals but not when 
    /// the underlying data actually changed. Updates will be triggeredIGameTimeManager implementation.
    /// Requires IGameTimeManager implementation
    ///
    /// When inheriting from this type, do not use Update()
    /// override DoUIUpdate() to handle UI refreshes
    ///
    /// When overriding any of the protected void Do...() methods, make sure to
    /// call base.Do...() or this model breaks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdatableViewModel<T> : StaticViewModel<T>
    {
        private IGameTimeManager gameTimeManager;
        private bool doUIRefresh = true;
        
        public void InvalidateView()
        {
            doUIRefresh = true;
        }

        private void Update()
        {
            // This method hides Update in base classes but that
            // should be ok because neither StaticView nor ViewController
            // implement it
            if (null == ViewData || false == doUIRefresh) return;
            doUIRefresh = false;

            DoUIUpdate();
        }

        protected override void DoAwake()
        {
            try
            {
                gameTimeManager = this.GetService<IGameTimeManager>();
                gameTimeManager.OnGameTimeInterval += OnGameTimeInterval;
            }
            catch (ServiceLocatorException)
            {
                logger?.LogWarning("View will be inactive");
            }
        }

        protected override void DoOnDestroy()
        {
            if (null == gameTimeManager) return;
            gameTimeManager.OnGameTimeInterval -= OnGameTimeInterval;
        }
        
        private void OnGameTimeInterval(GameTimeIntervalUpdate data)
        {
            doUIRefresh = true;
        }
    }
}