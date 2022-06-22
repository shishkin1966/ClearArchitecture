using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IMessage : IAction
    {
        /**
         * Получить id сообщения
         *
         * @return id сообщения
         */
        int GetMessageId();

        /**
         * Установить id сообщения
         *
         * @param id id сообщения
         * @return сообщение
         */
        IMessage SetMessageId(int id);

        /**
         * Прочитать письмо.
         *
         * @param subscriber подписчик получения почты
         */
        void Read(IMessengerSubscriber subscriber);

        /**
        * Проверить наличие адреса. Проверяются поля Получатель и CopyTo
        *
        * @param address адрес
        * @return true если адрес найден
        */
        bool ContainsAddress(string address);

        /**
        * Скопировать письмо
        *
        * @return письмо
        */
        IMessage Copy();

        /**
        * Получить поле CopyTo
        *
        * @return поле CopyTo
        */
        List<string> GetCopyTo();

        /**
        * Установить поле CopyTo
        *
        * @param copyTo список адресов
        * @return письмо
        */
        IMessage SetCopyTo(List<string> copyTo);

        /**
        * Получить адрес получателя
        *
        * @return адрес
        */
        string GetAddress();

        /**
        * Установить адрес получателя
        *
        * @param address адрес получателя
        * @return письмо
        */
        IMessage SetAddress(string address);

        /**
        * Флаг - контролировать сервером наличие копий письма.
        * При добавлении письма все старые копии стираются
        *
        * @return true - контролировать на дубликаты
        */
        bool IsCheckDublicate();

        /**
        * Флаг - при удалении подписчика сообщение с сервера не удаляется
        *
        * @return true - при удалении подписчика сообщение с сервера не удаляется
        */
        bool IsSticky();

        /**
        * Получить время окончания жизни письма
        *
        * @return время окончания жизни письма
        */
        long GetEndTime();

        /**
        * Установить время окончания жизни письма. При чтении почты с сервера,
        * просроченные сообщения удаляются
        *
        * @param keepAliveTime время окончания жизни письма
        * @return письмо
        */
        IMessage SetEndTime(long keepAliveTime);

        /**
        * Установить тему письма
        *
        * @param name тема письма
        * @return письмо
        */
        IMessage SetSubj(string subj);

        /**
        * Получить тему письма
        *
        * @return тема письма
        */
        string GetSubj();
    }
}
