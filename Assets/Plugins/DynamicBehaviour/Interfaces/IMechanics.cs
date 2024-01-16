namespace Plugins.DynamicBehaviour.Interfaces
{
    public interface IMechanics : IEnable, IDisable
    {
        bool Enabled { get; }
    }
}