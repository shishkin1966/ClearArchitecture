namespace ClearArchitecture.SL
{
    public interface IModelUnion : ISmallUnion
    {
        /**
         * Получить модель
         *
         * @param name имя модели
         * @return модель
         */
        IModelSubscriber GetModel(string name);
    }
}
