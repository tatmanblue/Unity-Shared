using UnityEngine;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// Adds Type support for views using data models
    ///
    /// Use when type support is needed but the view doesn't need to refreshed
    /// because the underlying data is not dynamic.  To refresh when ViewData is
    /// changed, override <inheritdoc cref="protected void OnViewDataChanged(old, data)"/> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StaticViewModel<T> : ViewController
    {
        /// <summary>
        /// TODO should this be public? in Max Capacity, some kind of public is needed.  Better
        /// TODO approach would be make setter protected/private and implement some
        /// TODO type of factory (or related) pattern
        /// </summary>
        public T ViewData 
        {
            get
            {
                return data;
            }
            set
            {
                T old = data;
                data = value;
                OnViewDataChanged(old, data);
            }
        }

        private T data = default(T);
        
        /// <summary>
        /// allows derived types ability to react when ViewData has changed
        /// </summary>
        /// <param name="old"></param>
        /// <param name="data"></param>
        protected virtual void OnViewDataChanged(T old, T data = default(T)) {}
    }
}