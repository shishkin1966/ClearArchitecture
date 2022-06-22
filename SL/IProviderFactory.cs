using System;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс Фабрики поставщиков услуг
    */
    public interface IProviderFactory
    {
        // Создать провайдера
        IProvider Create(string name); 
    }
}
