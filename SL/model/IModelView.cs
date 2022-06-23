namespace ClearArchitecture.SL
{
    public interface IModelView<V> : IValidated, ILifecycleObservable
    {
        /**
        * Закрыть ModelView объект
        */
        void Stop();

        /**
        * Получить модель
        *
        * @return модель
        */
        IModel<V> GetModel();

        /**
        * Установить модель
        *
        * @param model модель
        */
        void SetModel(IModel<V> model);
    }
}
