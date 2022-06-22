using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /**
    * Интерфейс объединения, предоставляющего Messager сервис подписчикам
    */
    public interface IMessengerUnion : ISmallUnion
    {
        /**
        * Получить сообщения подписчика
        *
        * @param subscriber подписчик
        * @return список сообщений
        */
        List<IMessage> GetMessages(IMessengerSubscriber subscriber);

        /**
        * Добавить почтовое сообщение
        *
        * @param message сообщение
        */
        void AddMessage(IMessage message);

        /**
        * Добавить почтовое сообщение, только в случае если провайдер присутствует
        *
        * @param message the message
        */
        void AddNotMandatoryMessage(IMessage message);

        /**
        * Удалить почтовое сообщение
        *
        * @param message the message
        */
        void RemoveMessage(IMessage message);

        /**
        * Удалить все сообщения
        */
        void ClearMessages();

        /**
        * Удалить сообщения подписчика
        *
        * @param subscriber имя подписчика
        */
        void ClearMessages(string subscriber);

        /**
        * Читать почту подписчика
        *
        * @param subscriber почтовый подписчик
        */
        void ReadMessages(IMessengerSubscriber subscriber);

        /**
        * Добавить список рассылки
        *
        * @param name      имя списка рассылки
        * @param addresses список рассылки
        */
        void AddMessagingList(string name, List<string> addresses);

        /**
        * Удалить список рассылки
        *
        * @param name имя списка рассылки
        */
        void RemoveMessagingList(string name);

        /**
        * Получить список рассылки
        *
        * @return список рассылки
        */
        List<string> GetMessagingList(string name);

    }
}
