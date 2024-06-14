using Domain.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExternalServices.Db;

public class LilitooCrawlerBusiness: ILilitooCrawlerBusiness
{
    private readonly LilitooCrawlerDbContext _context;

    public LilitooCrawlerBusiness(LilitooCrawlerDbContext context)
    {
        _context = context;
    }
    public void InsertProduct(Product product)
    {
        ProductIns ins = new ProductIns()
        {
            Name = product?.Name,
            IsExist = product?.IsExist,
            Description = JsonConvert.SerializeObject(product?.Description),
            Price = product?.Price,
            OldPrice = product?.OldPrice,
            NewPrice = product?.NewPrice,
            ImageUrls = JsonConvert.SerializeObject(product?.ImageUrls)
        };

        _context.Products.Add(ins);
        _context.SaveChanges();
    }
}
public class ProductIns
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Price { get; set; }
    public string? OldPrice { get; set; }
    public string? NewPrice { get; set; }
    public bool? IsExist { get; set; }
    //public List<string>? Images { get; set; }
    public string? ImageUrls { get; set; }
    public string? Description { get; set; }
}