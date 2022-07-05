using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface ISimpleObservable 
    {
        /**
        * Добавить слушателя к слушаемому объекту
        *
        * @param subscriber слушатель
        */
        void AddObserver(IObserver subscriber);

        /**
        * Удалить слушателя у слушаемого объекта
        *
        * @param subscriber слушатель
        */
        void RemoveObserver(IObserver subscriber);


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
        List<IObserver> GetObservers();

        /**
        * Получить слушателя
        *
        * @param name имя слушателя
        * @return слушатель
        */
        IObserver GetObserver(string name);

        /**
        * Остановить 
        */
        void Stop();

    }
}
