namespace Domain.Entities;
public class Product
{
    public string? Name { get; set; } 
    public string? Price { get; set; }
    public string? OldPrice { get; set; }
    public string? NewPrice { get; set; }
    public bool? IsExist { get; set; }
    public List<string>? Images { get; set; }
    public List<string>? ImageUrls { get; set; }
    public List<string>? Description { get; set; }
}
