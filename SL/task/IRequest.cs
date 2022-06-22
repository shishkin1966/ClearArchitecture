using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IRequest : INamed, IValidated
    {
        /**
        * Запустить запрос
        */
        void Execute(object obj);

        /**
        * Получить имя источника запроса
        *
        * @return имя источника запроса
        */
        string GetSender();

        /**
        * Получить данные запроса
        *
        * @return данные запроса
        */
        object GetData();

        /**
        * Получить id запроса
        *
        * @return id запроса
        */
        int GetId();

        /**
        * Установить id запроса
        *
        * @param id запроса
        * @return запрос
        */
        IRequest SetId(int id);

        /**
        * Установить Executor запроса
        *
        * @param Executor запроса
        * @return запрос
        */
        IRequest SetExecutor(IExecutorProvider executor);

        /**
        * Установить флаг - запрос прерван
        */
        IRequest SetCanceled();

        /**
        * Проверить прерван ли запрос
        *
        * @return true - запрос прерван
        */
        bool IsCancelled();

        /**
        * Проверить является ли запрос уникальным
        *
        * @return true - при запуске все предыдущие такие же запросы будут прерваны
        */
        bool IsDistinct();

        /**
        * Проверить является ли запрос одиночным
        *
        * @return true - при запуске если он выполняется - то не запускается на выполнение
        */
        bool IsSingle();

        /**
        * Что делать если такой запрос уже есть
        *
        * @return 0 - удалить старые запросы, 1 = не выполнять
        */
        int GetAction(IRequest oldRequest);

        /**
        * Удалить из очереди Executor
        */
        void RemoveRequest();

        /**
        * Получить список получателей запроса
        *
        * @return список получателей запроса
        */
        List<string> GetReceiver();

        /**
        * Установить результат запроса
        */
        IRequest SetResult(ExtResult result);

        /**
        * Получить результат запроса
        */
        ExtResult GetResult();

        /**
        * Отослать результат запроса
        */
        void SendResult();

        /**
        * Добавить получателей в запрос
        *
        * @param список получателей запроса
        */
        IRequest AddReceiver(List<string> receiver);

    }
}
