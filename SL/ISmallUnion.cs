using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс малого объединения подписчиков
    */
    public interface ISmallUnion : IProvider
    {
        /*
        * Зарегестрировать подписчика
        *
        * @param subscriber подписчик
        */
        bool RegisterSubscriber(IProviderSubscriber subscriber);

        /*
        * Отключить подписчика
        *
        * @param subscriber подписчик
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void UnRegisterSubscriber(IProviderSubscriber subscriber);

        /*
        * Отключить всех подписчиков
        */
        void UnRegisterSubscribers();

        /*
        * Отключить подписчика по его имени
        *
        * @param name имя подписчика
        * @return true - провайдер должен быть остановлен и выгружен
        */
        void UnRegisterSubscriber(string name);

        /*
        * Получить список подписчиков
        *
        * @return список подписчиков
        */
        List<IProviderSubscriber> GetSubscribers();

        /*
        * Получить проверенного подписчика
        *
        * @return проверенный подписчик
        */
        IProviderSubscriber GetValidSubscriber();

        /*
        * Получить список проверенных подписчиков
        *
        * @return список проверенных подписчиков
        */
        List<IProviderSubscriber> GetValidSubscribers();

        /*
        * Проверить наличие подписчиков
        *
        * @return true - подписчики есть
        */
        bool HasSubscribers();

        /*
        * Проверить наличие подписчика
        *
        * @param name имя подписчика
        * @return true - подписчик есть
        */
        bool HasSubscriber(string name);

        /*
        * Получить подписчика по его имени
        *
        * @param name имя подписчика
        * @return подписчик
        */
        IProviderSubscriber GetSubscriber(string name);

        /*
        * Событие - появился первый подписчик
        */
        void OnRegisterFirstSubscriber();

        /*
        * Событие - отписан последний подписчик
        */
        void OnUnRegisterLastSubscriber();

        /*
        * Событие - добавлен подписчик
        *
        * @param subscriber подписчик
        */
        void OnAddSubscriber(IProviderSubscriber subscriber);

        /*
        * Проверить регистрацию подписчика
        *
        * @param subscriber подписчик
        * @return true подписчик зарегистрирован
        */
        bool ContainsSubscriber(IProviderSubscriber subscriber);

        /*
        * Получить свободного подписчика
        *
        * @return свободный подписчик
        */
        IProviderSubscriber GetUnBusySubscriber();

        /*
        * Получить список свободных подписчиков
        *
        * @return список свободных подписчиков
        */
        List<IProviderSubscriber> GetUnBusySubscribers();

    }
}
