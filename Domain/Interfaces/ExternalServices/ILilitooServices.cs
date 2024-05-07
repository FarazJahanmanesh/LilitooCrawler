namespace Domain.Interfaces.ExternalServices;
public interface ILilitooServices
{
    Task GetCosmeticProduct();
    Task GetSkinCareProduct();
    Task GetHairCareProduct();
    Task GetMouthAndToothProduct();
    Task<string> GetUncategorizedProduct();
}
