namespace TatmanGames.ScreenUI.Interfaces
{
    /// <summary>
    /// this abstracts out the view model public contract so that we do not
    /// need to know the actual implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewModel<T>
    {
        void SetViewData(T data);
    }
}