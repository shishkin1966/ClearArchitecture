using System;
using System.Data.SqlClient;

namespace ClearArchitecture.SL
{
    public class DbProvider : AbsProvider, IDbProvider
    {
        private readonly Secretary<SqlConnection> _secretary = new Secretary<SqlConnection>();

        public const string NAME = "DbProvider";

        public DbProvider(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IDbProvider)
            { return 0; }
            else
            { return 1; }
        }

        public SqlConnection GetDb(DbConficuration conficuration)
        {
            if (conficuration == null) return default;

            SqlConnection cnn = default;
            if (!_secretary.ContainsKey(conficuration.GetName()))
            {
                try
                {
                    cnn = new SqlConnection(conficuration.ConnectionString);
                    cnn.Open();
                    if (cnn.State != System.Data.ConnectionState.Open)
                    {
                        cnn.Close();
                        cnn = default;
                    }
                    else
                    {
                        _secretary.Put(conficuration.GetName(), cnn);
                        Console.WriteLine(DateTime.Now.ToString("G") + ": Соединение с БД " + conficuration.GetName());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(DateTime.Now.ToString("G") + ": " + e.Message);
                }
            } else
            {
                cnn = _secretary.GetValue(conficuration.GetName());
                if (cnn.State != System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                    cnn.Open();
                    if (cnn.State != System.Data.ConnectionState.Open)
                    {
                        cnn.Close();
                        cnn = default;
                        _secretary.Remove(conficuration.GetName());
                    }
                    else
                    {
                        _secretary.Put(conficuration.GetName(), cnn);
                        Console.WriteLine(DateTime.Now.ToString("G") + ": Соединение с БД " + conficuration.GetName());
                    }
                }
            }
            return cnn;
        }

        public void Disconnect(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName)) return;

            if (_secretary.ContainsKey(databaseName))
            {
                var cnn = _secretary.GetValue(databaseName);
                cnn.Close();
                Console.WriteLine(DateTime.Now.ToString("G") + ": Соединение " + databaseName + " с БД разорвано");
                _secretary.Remove(databaseName);
            }
        }

        public override void Stop()
        {
            foreach (string databaseName in _secretary.Keys())
            {
                this.Disconnect(databaseName);
            }
            _secretary.Clear();
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Очистка списка DB Connections");

            base.Stop();
        }

    }
}
