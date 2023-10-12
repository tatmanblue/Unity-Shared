using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TatmanGames.Common;
using TatmanGames.Common.Interfaces;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.ScreenUI.Interfaces;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// Most of the views should be constructed the same and share some behaviors
    ///
    /// Do not implement Awake, Start, Update etc in the derived class
    ///
    /// Override DoAwake(), DoStart(), DoUIUpdate()
    ///
    /// TODO: move to TatmanGames.ScreenUI, some concerns: IGameTimeManager, ILogger
    /// </summary>
    public abstract class BaseViewModel<T> : MonoBehaviour, IViewModel<T>
    {
        private List<GameObject> matchedChildren = new ();
        private IGameTimeManager gameTimeManager;
        private bool doUIRefresh = true;
        protected ILogger logger;
        protected T viewData = default(T);

        public void InvalidateView()
        {
            doUIRefresh = true;
        }
        
        public virtual void SetViewData(T data)
        {
            viewData = data;
            InvalidateView();
        }

        /// <summary>
        /// TEMP TODO hack and need to use a better means to expose data
        /// </summary>
        /// <returns></returns>
        public T ViewData()
        {
            return viewData;
        }
        
        private void Awake()
        {
            logger = this.GetService<ILogger>();
            try
            {
                // TODO this is ugly and should be refactored in such a way not 
                // TODO all ViewModels need gametime manager
                gameTimeManager = this.GetService<IGameTimeManager>();
                gameTimeManager.OnGameTimeInterval += OnGameTimeInterval;
            }
            catch (ServiceLocatorException)
            {
                logger?.Debug("View will be inactive");
            }

            DoAwake();
        }

        private void Start()
        {
            if (null == logger)
                logger = this.GetService<ILogger>();
            
            DoStart();
        }

        private void Update()
        {
            if (null == viewData || false == doUIRefresh) return;
            doUIRefresh = false;
            DoUIUpdate();
        }

        private void OnDestroy()
        {
            DoOnDestroy();
            if (null == gameTimeManager) return;
            gameTimeManager.OnGameTimeInterval -= OnGameTimeInterval;
        }

        private void OnGameTimeInterval(GameTimeIntervalUpdate data)
        {
            doUIRefresh = true;
        }
        
        /// <summary>
        /// if this method is called then its assumed GameObject has not been found
        /// previously and does not exist in matchedChildren
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private GameObject SearchFor(Transform parent, string name)
        {
            if (parent == null) return null;
            GameObject item = null;
            
            for (int count = 0; count < parent.childCount; count++)
            {
                Transform t = parent.GetChild(count);
                if (t.name == name)
                {
                    matchedChildren.Add(t.gameObject);
                    item = t.gameObject;
                    break;
                }

                item = SearchFor(t, name);
                if (null != item)
                    break;
            }

            return item;
        }
        
        private IEnumerator ShowWithFadeIn(GameObject control)
        {
            CanvasGroup cg = control.GetComponent<CanvasGroup>();
            while (cg.alpha < 1.0f)
            {
                cg.alpha += 0.105f;
                yield return new WaitForSeconds(0.025f);
            }
            
            yield return null;
        }

        #region overridable functions
        protected virtual void DoAwake() {}
        protected virtual void DoStart() {}
        protected virtual void DoUIUpdate() {}
        protected virtual void DoOnDestroy() {}
        #endregion
        
        /// <summary>
        /// Get a GameObject that is child by name.  Search results are cached thus subsequent
        /// calls returned the cached GameObject.
        ///
        /// Rules!  Each GameObject in the hierarchy must have a unique name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected GameObject SearchFor(string name)
        {
            GameObject item = matchedChildren.Find(o => o.name == name);
            if (item == null)
            {
                item = SearchFor(this.transform, name);
            }

            return item;
        }
        
        #region UI setter functions

        protected void Hide(string fieldName)
        {
            GameObject child = SearchFor(fieldName);
            CanvasGroup cg = child.GetComponent<CanvasGroup>();
            cg.alpha = 0.0f;
        }

        protected void Show(string fieldName)
        {
            GameObject child = SearchFor(fieldName);
            StartCoroutine(ShowWithFadeIn(child));
        }
        
        protected void EnableButton(string fieldName, bool isEnabled)
        {
            GameObject child = SearchFor(fieldName);
            Button btn = child.GetComponent<Button>();
            btn.interactable = isEnabled;
        }
        
        protected void SetText(string fieldName, string data)
        {
            GameObject child = SearchFor(fieldName);
            
            TMP_Text text = child.GetComponent(typeof(TMP_Text)) as TMP_Text;
            if (null == text) return;

            text.text = data;
        }
        
        protected void SetInput(string fieldName, string data)
        {
            GameObject child = SearchFor(fieldName);
            
            TMP_InputField text = child.GetComponent<TMP_InputField>();
            if (null == text) return;
            text.interactable = true;
            text.text = data;
        }

        protected void SetImage(string fieldName, Sprite sprite)
        {
            GameObject child = SearchFor(fieldName);
            Image image = child.GetComponent<Image>();
            if (null == image) return;
            image.overrideSprite = sprite;
        }
        
        protected void SelectDropItemItem(string fieldName, string matching)
        {
            GameObject child = SearchFor(fieldName);
            TMP_Dropdown itemsDropDown = child.GetComponentInChildren<TMP_Dropdown>();
            if (null == itemsDropDown) return;
            for (int count = 0; count < itemsDropDown.options.Count; count++)
            {
                string optionText = itemsDropDown.options[count].text;
                if (optionText == matching)
                {
                    itemsDropDown.value = count;
                    break;
                }
            }
        }

        /// <summary>
        /// for adding a child to a scroll view content
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="prefab"></param>
        /// <returns></returns>
        protected GameObject AddRowToListView(string fieldName, GameObject prefab)
        {
            GameObject listView = SearchFor(fieldName);
            ScrollRect scrollRect = listView.GetComponentInChildren<ScrollRect>();
            GameObject row = Instantiate(prefab, scrollRect.content);

            return row;
        }
        
        /// <summary>
        /// for clearing a vertical scroll view of its contents
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="callback">callback for doing something with the row GameObject prior to deletion</param>
        protected void ClearListResultsV(string fieldName, Action<GameObject> callback = null)
        {
            GameObject listView = SearchFor(fieldName);
            VerticalLayoutGroup vlg = listView.GetComponentInChildren<VerticalLayoutGroup>();
            GameObject content = vlg.gameObject;
            while(0 < content.transform.childCount)
            {
                GameObject row = content.transform.GetChild(0).gameObject;
                if (null != callback)
                    callback(row);
                
                DestroyImmediate(row);
            }
        }
        
        protected void ClearListResultsH(string fieldName, Action<GameObject> callback = null)
        {
            GameObject listView = SearchFor(fieldName);
            HorizontalLayoutGroup vlg = listView.GetComponentInChildren<HorizontalLayoutGroup>();
            GameObject content = vlg.gameObject;
            while(0 < content.transform.childCount)
            {
                GameObject row = content.transform.GetChild(0).gameObject;
                if (null != callback)
                    callback(row);
                
                DestroyImmediate(row);
            }
        }
        #endregion
        
        protected string GetDialogName()
        {
            string dlgName = this.name;
            int indexOf = dlgName.IndexOf("(Clone)");
            if (0 >= indexOf)
                return dlgName;
            return dlgName.Remove(indexOf);
        }
        
        private T GetService<T>()
        {
            return GlobalServicesLocator.Instance.GetService<T>();
        }
    }
}