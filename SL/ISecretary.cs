using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс секретаря объединения
    */
    public interface ISecretary<T>
    {
        /*
        * удалить члена объединения
        *
        * @param key - имя члена объединения
        */
        void Remove(string key);

        /*
        * получить размер объединения
        *
        * @return - получить размер объединения
        */
        int Size();

        /*
        * добавить члена объединения
        *
        * @param key - имя члена объединения
        * @param value - член объединения
        */
        void Put(string key, T value);

        /*
        * проверить наличие члена объединения
        *
        * @param key - имя члена объединения
        */
        bool ContainsKey(string key);

        /*
        * получить члена объединения
        *
        * @param key - имя члена объединения
        */
        T GetValue(string key);

        /*
        * получить список членов объединения
        */
        List<T> Values();

        /*
        * проверить объединение на наличие членов
        *
        * @return true - объединение не пустое
        */
        bool IsEmpty();

        /*
        * очистить объединение
        */
        void Clear();

        /*
        * получить список имен членов объединения
        * @return список имен
        */
        List<string> Keys();
    }
}
