using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IObservableUnion : ISmallUnion
    {
        /**
        * Получить список слушаемых объектов
        *
        * @return список слушаемых(IObservable) объектов
        */
        List<IObservable> GetObservables();

        /**
        * Зарегестрировать слушаемый объект
        *
        * @param observable слушаемый объект
        */
        bool RegisterObservable(IObservable observable);

        /**
        * Отменить регистрацию слушаемего объекта
        *
        * @param observable слушаемый объект
        */
        bool UnRegisterObservable(IObservable observable);

        /**
        * Событие - изменился слушаемый объект
        *
        * @param name имя слушаемого объекта
        * @param obj новое значение
        */
        void OnChangeObservable(string name, object obj);

        /**
        * Получить слушаемый объект
        *
        * @param name имя слушаемого объекта
        * @return слушаемый объект
        */
        IObservable GetObservable(string name);

    }
}
