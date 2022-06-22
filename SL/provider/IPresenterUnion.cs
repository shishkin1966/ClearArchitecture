namespace ClearArchitecture.SL
{
    interface IPresenterUnion : ISmallUnion
    {
        /**
         * Получить presenter
         *
         * @param name имя презентера
         * @return презентер
         */
        IPresenterSubscriber GetPresenter(string name);
    }
}
