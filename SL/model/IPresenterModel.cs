namespace ClearArchitecture.SL
{
    public interface IPresenterModel<T> where T : IPresenterSubscriber
    {
        /**
        * Установить презентер модели
        *
        * @param presenter презентер
        */
        void SetPresenter(T presenter);

        /**
        * Получить презентер модели
        *
        * @return презентер
        */
        T GetPresenter();
    }
}
