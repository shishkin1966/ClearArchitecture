using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsProviderSubscriber : AbsSubscriber, IProviderSubscriber
    {
        private readonly Secretary<string> _providers = new Secretary<string>();

        public abstract List<string> GetProviderSubscription();

        protected AbsProviderSubscriber(string name) : base(name)
        {
        }

        public void OnStopProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            RemoveProvider(provider);
            OnRemoveProvider(provider);
        }

        public void Stop()
        {
            _providers.Clear();
        }

        public List<string> GetProviders()
        {
            return _providers.Values();
        }

        public void SetProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            _providers.Put(provider,provider);
            OnSetProvider(provider);
        }

        public void RemoveProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return;

            _providers.Remove(provider);
        }

        public void OnSetProvider(string provider)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": Подключен к провайдеру " + provider + " подписчик "+ GetName());
        }

        public void OnRemoveProvider(string provider)
        {
            Console.WriteLine(DateTime.Now.ToString("G") + ": Отключен от провайдера " + provider + " подписчик " + GetName());
        }
    }
}
