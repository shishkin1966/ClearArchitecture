namespace ClearArchitecture.SL
{
    public interface ICommented
    {
        /*
        * Добавить комментарий подписчику
        */
        void AddComment(string comment);

        /*
        * Получить комментарий подписчика
        * @return string - комментарий подписчика
        */
        string GetComment();
    }
}
