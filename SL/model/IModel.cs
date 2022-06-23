namespace ClearArchitecture.SL
{
    public interface IModel<out V> : IValidated
    {
        /**
        * Получить View объект модели
        *
        * @return View объект модели
        */
        V GetView();

       /**
        * Добавить слушателя к модели
        *
        * @param stateable stateable объект
        */
       void AddLifecycleObserver(ILifecycle stateable);
}
}
