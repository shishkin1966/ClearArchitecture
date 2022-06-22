namespace ClearArchitecture.SL
{
    public interface IModel : IValidated
    {
        /**
        * Получить View объект модели
        *
        * @return View объект модели
        */
        T GetView<T>();

       /**
        * Добавить слушателя к модели
        *
        * @param stateable stateable объект
        */
       void AddLifecycleObserver(ILifecycle stateable);
}
}
