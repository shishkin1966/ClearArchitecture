namespace ClearArchitecture.SL
{
    /**
    * Интерфейс слушателя View объекта, имеющего жизненный цикл
    */
    public interface ILifecycleListener : ILifecycle
    {
        /**
        * Событие - view на этапе создания
        */
        void OnCreateView();

        /**
        * Событие - view готово к использованию
        */
        void OnReadyView();

        /**
        * Событие - уничтожение view
        */
        void OnDestroyView();

    }
}
