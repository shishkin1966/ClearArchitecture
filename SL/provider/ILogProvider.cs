namespace ClearArchitecture.SL
{
    public interface ILogProvider : IProvider
    {
        void AddError(ExtError error);

        void AddMessage(string message);
    }
}
