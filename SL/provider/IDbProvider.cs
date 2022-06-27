using System.Data.SqlClient;

namespace ClearArchitecture.SL
{
    public interface IDbProvider
    {
        /**
         * Получить соединение БД
         *
         * @param databaseName имя БД
         */
        SqlConnection GetDb(DbConficuration conficuration);

        void Disconnect(string databaseName);
    }
}
