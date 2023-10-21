using System;
using TatmanGames.Common;
using UnityEngine;
using TatmanGames.Common.Interfaces;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// For data driven views that need to be refreshed at regular intervals triggered
    /// by IGameTimeManager implementation. Requires IGameTimeManager implementation
    ///
    /// When inheriting from this type, do not use Update()
    /// override DoUIUpdate() 
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

        private void Awake()
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
        
        private void Update()
        {
            if (null == ViewData || false == doUIRefresh) return;
            doUIRefresh = false;

            DoUIUpdate();
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