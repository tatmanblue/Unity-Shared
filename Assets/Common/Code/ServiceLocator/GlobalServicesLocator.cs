namespace TatmanGames.Common.ServiceLocator
{
    /// <summary>
    /// Provides a global instance of the ServicesLocator
    /// </summary>
    public static class GlobalServicesLocator
    {
        public static ServicesLocator Instance { get; private set; }

        static GlobalServicesLocator()
        {
            Instance = new ServicesLocator();
        }

        /// <summary>
        /// this is a cheesy way to make it easier to identify when/where the
        /// GlobalServiceLocator is changed
        /// </summary>
        /// <param name="instance"></param>
        public static void AssignNewServiceLocator(ServicesLocator instance)
        {
            GlobalServicesLocator.Instance = instance;
        }
    }
}