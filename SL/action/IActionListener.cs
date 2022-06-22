namespace ClearArchitecture.SL
{
    public interface IActionListener
    {
        /**
        * Добавить новое действие к исполнению
        *
        * @param action действие
        */
        void AddAction(IAction action);
    }
}
