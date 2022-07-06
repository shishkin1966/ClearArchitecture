using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface ISubscriberObservable : ISubscriber
    {
        /**
        * Добавить слушателя к слушаемому объекту
        *
        * @param subscriber слушатель
        */
        void AddObserver(IObservableSubscriber subscriber);

        /**
        * Удалить слушателя у слушаемого объекта
        *
        * @param subscriber слушатель
        */
        void RemoveObserver(IObservableSubscriber subscriber);

        /**
        * Событие - зарегестрирован первый слушаемый объект. Вызывается при появлении
        * первого слушателя
        */
        void OnRegisterFirstObserver();

        /**
        * Событие - зарегестрирован слушаемый объект. 
        */
        void OnRegisterObserver(IObservableSubscriber subscriber);

        /**
        * Событие - отмена регистрации последнего слушаемого объекта. Вызывается при удалении
        * последнего слушателя
        */
        void OnUnRegisterLastObserver();

        /**
        * Событие - отменена регистрации слушаемого объекта. 
        */
        void OnUnRegisterObserver(IObservableSubscriber subscriber);

        /**
        * Событие - в слушаемом объекте произошли изменения
        *
        * @param obj объект изменения
        */
        void OnChangeObservable(object obj);

        /**
        * Получить список слушателей
        *
        * @return список слушателей
        */
        List<IObservableSubscriber> GetObservers();

        /**
        * Получить слушателя
        *
        * @param name имя слушателя
        * @return слушатель
        */
        IObservableSubscriber GetObserver(string name);

        /**
        * Остановить 
        */
        void Stop();

    }
}
