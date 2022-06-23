namespace ClearArchitecture.SL
{
    public interface IModel<out V> : IModelSubscriber
    {
        /**
        * Получить View объект модели
        *
        * @return View объект модели
        */
        V GetView();
    }
}
