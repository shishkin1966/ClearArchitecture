namespace ClearArchitecture.SL
{
    public interface IModel<out V> : IValidated, ILifecycleListener
    {
        /**
        * Получить View объект модели
        *
        * @return View объект модели
        */
        V GetView();
    }
}
