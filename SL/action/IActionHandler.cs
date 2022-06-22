namespace ClearArchitecture.SL
{
    public interface IActionHandler
    {
        /**
        * Обработать событие
        *
        * @param action  событие
        */
        bool OnAction(IAction action);
    }
}
