using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс администратора(Service Locator)
    */
    public interface IServiceLocator : INamed
    {
        /*
        * Проверить существование провайдера
        *
        * @param name имя провайдера
        * @return true - провайдер существует
        */
        bool ExistsProvider(string name);

        /*
        * Получить провайдера
        *
        * @param Any  тип провайдера
        * @param name имя провайдера
        * @return провайдер
        */
        IProvider GetProvider(string name);

        /*
        * Зарегистрировать провайдера
        *
        * @param provider провайдер
        * @return флаг - операция завершена успешно
        */
        bool RegisterProvider(IProvider provider);

        /*
        * Зарегистрировать провайдера
        *
        * @param name имя провайдера
        * @return флаг - операция завершена успешно
        */
        bool RegisterProvider(string name);

        /*
        * Отменить регистрацию провайдера
        *
        * @param name имя провайдера
        * @return флаг - операция завершена успешно
        */
        void UnRegisterProvider(string name);

        /*
        * Зарегистрировать подписчика провайдера
        *
        * @param subscriber подписчик провайдера
        * @return флаг - операция завершена успешно
        */
        bool RegisterSubscriber(IProviderSubscriber subscriber);

        /*
        * Отменить регистрацию подписчика провайдера
        *
        * @param subscriber подписчик провайдера
        * @return флаг - операция завершена успешно
        */
        void UnRegisterSubscriber(IProviderSubscriber subscriber);

        /*
        * Установить подписчика текущим
        *
        * @param subscriber подписчик
        * @return флаг - операция завершена успешно
        */
        bool SetCurrentSubscriber(IProviderSubscriber subscriber);

        /*
        * Остановитить работу service locator
        */
        void Stop();

        /*
        * Запустить работу service locator
        */
        void Start();

        /*
        * Получить список провайдеров
        *
        * @return список провайдеров
        */
        List<IProvider> GetProviders();

        /*
        * Получить фабрику провайдеров
        *
        * @return фабрика провайдеров
        */
        IProviderFactory GetProviderFactory();  
    }
}
