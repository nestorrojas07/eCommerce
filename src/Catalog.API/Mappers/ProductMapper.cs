using Catalog.API.Request.Product;
using Catalog.Domain.Models;
using Catalog.Services.Dtos.Products;

namespace Catalog.API.Mappers;

public static class ProductMapper
{
    public static Product ToProduct(this CreateProductRequest product)
    {
        return new Product { 
            Name = product.Name!,
            Description = product.Description!,
            IsActive = product.IsActive ?? true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Price = product.Price ?? 0
        };
    }

    public static UpdateProductOptions ToUpdateProductOptions(this ProductUpdateRequest UpdateProd)
    {
        return new UpdateProductOptions(UpdateProd.Name, UpdateProd.Description, UpdateProd.Price,UpdateProd.IsActive);
    }

}
