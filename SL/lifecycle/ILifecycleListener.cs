namespace ClearArchitecture.SL
{
    /**
    * Интерфейс слушателя объекта, имеющего жизненный цикл
    */
    public interface ILifecycleListener : ILifecycle
    {
        /**
        * Событие - объект на этапе создания
        */
        void OnCreate();

        /**
        * Событие - объект на этапе открытия
        */
        void OnStart();
        /**
        * Событие - объект готов к использованию
        */
        void OnReady();

        /**
        * Событие - уничтожение объекта
        */
        void OnDestroy();

    }
}
