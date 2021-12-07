using TatmanGames.ScreenUI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TatmanGames.ScreenUI.UI
{
    public class PopupHandler : IPopupHandler
    {
        private GameObject background;
        public float destroyTime = 0.5f;
        private bool handlingClose = false;
        private GameObject activeDialog = null;
        
        public Canvas Canvas { get; set; }
        public bool IsDialogActive { get; private set; } = false;
        public Color BackgroundColor { get; set; } = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);
        public void ShowDialog(GameObject dialog)
        {
            if (true == IsDialogActive) return;
            
            IsDialogActive = true;
            activeDialog = dialog;
            ShowDialogPrefab(dialog);
            AddBackground();
        }

        public void CloseDialog()
        {
            if (false == IsDialogActive) return;

            IsDialogActive = false;
            RemoveBackground();
            Canvas.Destroy(background);
        }
        
        /// <summary>
        /// This adds shading to the rest of the screen to make it looked somewhat greyed out
        /// </summary>
        private void AddBackground()
        {
            var backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, BackgroundColor);
            backgroundTexture.Apply();

            background = new GameObject("PopupBackground");
            var image = background.AddComponent<Image>();
            var rect = new Rect(0, 0, backgroundTexture.width, backgroundTexture.height);
            var sprite = Sprite.Create(backgroundTexture, rect, new Vector2(0.5f, 0.5f), 1);
            image.material.mainTexture = backgroundTexture;
            image.sprite = sprite;
            var newColor = image.color;
            image.color = newColor;
            image.canvasRenderer.SetAlpha(0.0f);
            image.CrossFadeAlpha(1.0f, 0.4f, false);
            
            background.transform.localScale = new Vector3(1, 1, 1);
            background.GetComponent<RectTransform>().sizeDelta = Canvas.GetComponent<RectTransform>().sizeDelta;
            background.transform.SetParent(Canvas.transform, false);
            background.transform.SetSiblingIndex(Canvas.transform.GetSiblingIndex());
        }
        
        
        /// <summary>
        /// inverse of AddBackground()
        /// </summary>
        private void RemoveBackground()
        {
            if (null == background)
                return;

            var image = background.GetComponent<Image>();
            if (image != null)
                image.CrossFadeAlpha(0.0f, 0.2f, false);
        }
        
        private void ShowDialogPrefab(GameObject which)
        {
            if (null == which)
                return;

            var popup = Canvas.Instantiate(which) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;

            if (null != Canvas)
                popup.transform.SetParent(Canvas.transform, false);
            
        }
    }
}