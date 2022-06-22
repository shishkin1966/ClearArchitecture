using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IObservableSubscriber : ILifecycle, IProviderSubscriber
    {
        /**
        * Получить список имен слушаемых объектов
        *
        * @return список имен слушаемых объектов
        */
        List<string> GetObservable();

        /**
        * Событие - слушаемый объект изменен
        *
        * @param name имя слушаемого объекта
        * @param obj значение слушаемого объекта
        */
        void OnChangeObservable(string observable, object obj);

        /*
        * Событие - слушаемый объект прекратил работу
        */
        void OnStopObservable(string observable);


    }
}
