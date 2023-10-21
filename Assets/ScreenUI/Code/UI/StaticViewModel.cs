using UnityEngine;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// Adds Type support for views using data models
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StaticViewModel<T> : ViewController
    {
        /// <summary>
        /// TODO should this be public? in Max Capacity, some kind of public is needed.  Better
        /// TODO approach would be make setter protected/private and implement some
        /// TODO type of factory (or related) pattern
        /// </summary>
        public T ViewData { get; set; } = default(T);
    }
}