using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IPool<T> 
    {
        List<T> Get(int count);

        void Release(T item);

        void Release(List<T> items);
    }
}
