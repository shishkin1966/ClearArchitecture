
namespace ClearArchitecture.SL
{
    public interface ILifecycle
    {
        /**
         * Получить состояние объекта
         *
         * @return the state
         */
        int GetState();

        /**
         * Установить состояние объекта
         *
         * @param state the state
         */
        void SetState(int state);
    }
}
