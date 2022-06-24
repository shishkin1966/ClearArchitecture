using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsProviderSubscriber : AbsSubscriber, IProviderSubscriber
    {
        public abstract List<string> GetProviderSubscription();

        protected AbsProviderSubscriber(string name) : base(name)
        {
        }

        public virtual void Stop()
        {
            //
        }

        public virtual void SetProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            OnSetProvider(provider);
        }

        public virtual void RemoveProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            OnRemoveProvider(provider);
        }

        public virtual void OnSetProvider(string provider)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": Подключен к провайдеру " + provider + " подписчик "+ GetName());
        }

        public virtual void OnRemoveProvider(string provider)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": Отключен от провайдера " + provider + " подписчик " + GetName());
        }
    }
}
