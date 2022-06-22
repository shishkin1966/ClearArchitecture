using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IObservable : ISubscriber
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
        * Зарегестрировать слушаемый объект. Вызывается при появлении
        * первого слушателя
        */
        void OnRegisterFirstObserver();

        /**
        * Отменить регистрацию слушаемого объекта. Вызывается при удалении
        * последнего слушателя
        */
        void OnUnRegisterLastObserver();

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
