
namespace Catalog.API.Request.Product;

public class CreateProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public float? Price { get; set; } = 0;
    public bool? IsActive { get; set; } = true;
}
