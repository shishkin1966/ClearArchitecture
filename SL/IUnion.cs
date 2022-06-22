using System;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс объединения подписчиков
    */
    public interface IUnion : ISmallUnion
    {
        /*
         * Установить текущего специалиста
         *
         * @param subscriber специалист
         */
        bool SetCurrentSubscriber(IProviderSubscriber subscriber);

        /*
         * Получить текущего специалиста
         *
         * @return специалист
         */
        IProviderSubscriber GetCurrentSubscriber();

    }
}
