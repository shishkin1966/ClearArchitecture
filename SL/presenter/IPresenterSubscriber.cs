namespace ClearArchitecture.SL
{
    public interface IPresenterSubscriber : IMessengerSubscriber, ILifecycleListener, IActionListener, IActionHandler
    {
        /**
        * Флаг - регистрировать презентер в объединении презентеров
        *
        * @return true - регистрировать (презентер - глобальный)
        */
        bool IsRegister();

        /**
        * Событие - при готовности View объекта 
        *
        */
        void OnStart();
    }
}
