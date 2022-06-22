using System;

namespace ClearArchitecture.SL
{
    public class LogProvider : AbsProvider, ILogProvider
    {
        public const string NAME = "LogProvider";

        public LogProvider(string name) : base(name)
        {
        }

        public void AddError(ExtError error)
        {
            if (error == null) return;

            Console.WriteLine(error.GetErrorText());
        }

        public void AddMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            Console.WriteLine(DateTime.Now.ToString("G") + ": " + message);
        }

        public override int CompareTo(IProvider other)
        {
            if (other is ILogProvider)
            { return 0; }
            else
            { return 1; }
        }
    }
}
