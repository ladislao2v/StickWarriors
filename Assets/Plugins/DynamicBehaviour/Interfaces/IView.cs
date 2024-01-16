namespace Plugins.DynamicBehaviour.Interfaces
{
    public interface IView : IEnable, IDisable
    {
        public bool Enabled { get; }
    }
}