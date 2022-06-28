using System.Collections.Generic;

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

        /**
         * Получить список заголовков моделей
         * @return список заголовков
         */
        List<string> GetTitles();

        /**
         * Получить модель по ее заголовку
         *
         * @param title заголовок модели
         * @return модель
         */
        IModelSubscriber GetModelByTile(string title);
    }
}
