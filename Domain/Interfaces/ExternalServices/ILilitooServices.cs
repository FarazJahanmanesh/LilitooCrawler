using Domain.Entities;

namespace Domain.Interfaces.ExternalServices;
public interface ILilitooServices
{
    Task<List<Product>> GetProducts(string url);
}
