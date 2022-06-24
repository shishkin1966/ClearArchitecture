
using System;

namespace ClearArchitecture.SL
{
    public abstract class AbsProvider : AbsSubscriber, IProvider
    {
        protected AbsProvider(string name) : base(name)
        {
        }

        public abstract int CompareTo(IProvider other);

        public virtual bool IsPersistent()
        {
            return false;
        }

        public virtual void OnRegister()
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": OnRegister provider " + GetName());
        }

        public virtual void OnUnRegister()
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": OnUnRegister provider " + GetName());
        }

        public virtual void Stop()
        {
            OnUnRegister();
            Console.WriteLine(DateTime.Now.ToString("G") + ": Stop provider " + GetName());
        }
    }
}
