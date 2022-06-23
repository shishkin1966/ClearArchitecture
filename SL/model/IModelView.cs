namespace ClearArchitecture.SL
{
    public interface IModelView<M,V> : IValidated, ILifecycleObservable where M : IModel<V>
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
        M GetModel();

        /**
        * Установить модель
        *
        * @param model модель
        */
        M SetModel(M model);
    }
}
