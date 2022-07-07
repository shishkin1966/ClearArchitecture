using System;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс провайдера - объекта предоставлющий сервис
    */

    public interface IProvider : ISubscriber, IComparable<IProvider>
    {
        /*
         * Получить тип провайдера - постоянный или нет
         *
         * @return true - не будет удаляться администратором
         */

        bool IsPersistent();

        /*
         * Событие - отключить регистрацию провайдера
         */

        void OnUnRegister();

        /*
        * Событие - регистрация провайдера
        */

        void OnRegister();

        /*
        * Остановитить работу провайдера
        */

        void Stop();

        /*
        * Добавить слушателя провайдера
        */

        void AddObserver(IObserver observer);

        /*
        * Получить Observable провайдера
        * @return IObserverObservable - Observable провайдера
        */

        IObserverObservable GetObservable();
    }
}