namespace ClearArchitecture.SL
{
    public interface IFormUnion : ISmallUnion
    {
        /**
         * Получить Form
         *
         * @param name имя Form
         * @return Form
         */
        IFormSubscriber GetForm(string name);
    }
}
