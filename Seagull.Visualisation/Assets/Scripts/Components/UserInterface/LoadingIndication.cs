namespace Seagull.Visualisation.Components.UserInterface
{
    /// <summary>
    /// <see cref="LoadingIndication"/> contains relevant classes and logic
    /// to starting and stopping the loading indication.
    /// </summary>
    public static class LoadingIndication
    {
        /// <summary>
        /// <see cref="StartMessage"/> indicates that something started loading.
        /// </summary>
        public class StartMessage { }
        
        /// <summary>
        /// <see cref="StartMessage"/> indicates that something stopped loading.
        /// </summary>
        public class StopMessage { }
    }
}