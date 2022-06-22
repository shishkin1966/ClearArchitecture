
using System;

namespace ClearArchitecture.SL
{
    public abstract class AbsProvider : AbsSubscriber, IProvider
    {
        protected AbsProvider(string name) : base(name)
        {
        }

        public abstract int CompareTo(IProvider other);

        public bool IsPersistent()
        {
            return false;
        }

        public void OnRegister()
        {
            //Console.WriteLine("OnRegister provider " + GetName());
        }

        public void OnUnRegister()
        {
            //Console.WriteLine("OnUnRegister provider " + GetName());
        }

        public void Stop()
        {
            OnUnRegister();
            Console.WriteLine("Stop provider " + GetName());
        }
    }
}
