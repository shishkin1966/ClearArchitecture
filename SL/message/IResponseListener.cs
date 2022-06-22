namespace ClearArchitecture.SL
{
    public interface IResponseListener
    {
        /**
        * Событие - пришел ответ с результатами запроса
        *
        * @param result - результат
        */
        void Response(ExtResult result);
    }
}
