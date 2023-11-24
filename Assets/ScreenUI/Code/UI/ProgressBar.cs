using System;
using UnityEngine;
using UnityEngine.UI;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// ViewModel for a progress bar.  It expects there is a child
    /// image object called "Fill"
    ///
    /// </summary>
    public class ProgressBar : StaticViewModel<float>
    {
        private Image mask;
        private bool doRefresh = false;

        /// <summary>
        /// Update() here is due to a design short coming in that StaticViewModel
        /// and ViewController do not trigger DoUIUpdate and while its not needed here
        /// we want to stick the pattern 
        /// </summary>
        private void Update()
        {
            if (false == doRefresh) return;
            doRefresh = false;
            DoUIUpdate();
        }

        protected override void DoStart()
        {
            GameObject fill = SearchFor("Fill");
            mask = fill.GetComponent<Image>();
            doRefresh = true;
        }

        protected override void DoUIUpdate()
        {
            mask.fillAmount = ViewData;
        }

        protected override void OnViewDataChanged(float old, float data = default(float))
        {
            doRefresh = true;
        }
    }
}