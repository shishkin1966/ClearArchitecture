namespace ClearArchitecture.SL
{
    public interface IObserver : INamed
    {
        /**
        * Событие - слушаемый объект изменен
        *
        * @param obj значение слушаемого объекта
        */
        void OnChangeObservable(object obj);
    }
}
