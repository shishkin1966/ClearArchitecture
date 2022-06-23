namespace ClearArchitecture.SL
{
    public interface IModelPresenter<out V>
    {
        /**
         * Получить модель презентера
         *
         * @return the model
         */
        IModel<V> GetModel();

        /**
        * Получить View модели
        *
        * @return the view model
        */
        V GetView();
    }
}
