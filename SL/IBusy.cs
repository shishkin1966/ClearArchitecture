namespace ClearArchitecture.SL
{
    public interface IBusy
    {
        /*
        * Получить состояние - занятый или нет
        */
        bool IsBusy();

        /*
        * Установить состояние - занятый
        */
        void SetBusy();

        /*
        * Установить состояние - свободный
        */
        void SetUnBusy();
    }
}
