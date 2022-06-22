using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс объекта, который регистрируется у провайдеров для получения/предоставления сервиса
    */
    public interface IProviderSubscriber : ISubscriber
    {
        /*
        * Получить список имен провайдеров, у которых должен быть зарегистрирован объект
        *
        * @return список имен провайдеров
        */
        List<string> GetProviderSubscription();

        /*
        * Событие - провайдер прекратил работу
        */
        void OnStopProvider(string provider);

        /*
        * Остановить работу объекта
        */
        void Stop();

        /*
        * Получить список имен провайдеров подписчика
        *
        * @return список имен провайдера подписчика
        */
        List<string> GetProviders();

        /*
        * Установить провайдера подписчика
        * @param sring провайдер
        */
        void SetProvider(string provider);

        /*
        * Очистить провайдера подписчика
        */
        void RemoveProvider(string provider);

        /*
        * Событие - установлен провайдера подписчика
        * @param sring провайдер
        */
        void OnSetProvider(string provider);

        /*
        * Событие - провайдера подписчика очищен
        */
        void OnRemoveProvider(string provider);
    }
}
