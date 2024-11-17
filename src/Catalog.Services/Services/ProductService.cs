using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Models;
using Catalog.Services.Dtos.Products;
using Shared.Domain.Dtos.Response;

namespace Catalog.Services.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> SaveAsync(Product product, CancellationToken cancellationToken = default)
    {
        return await _productRepository.SaveAsync(product, cancellationToken);   
    }
    public async Task<PaginatedData<Product>> GetAsync(int pageNumber = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetAsync(pageNumber, pageSize, cancellationToken);

    }

    public async Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Product> UpdateAsync(long id, UpdateProductOptions updateProductOptions, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"product {id} not found");

        if(!string.IsNullOrEmpty( updateProductOptions.Name))
            product.Name = updateProductOptions.Name;
        if (!string.IsNullOrEmpty(updateProductOptions.Description))
            product.Description = updateProductOptions.Description;
        if (updateProductOptions.Price.HasValue)
            product.Price = updateProductOptions.Price.Value;
        if (updateProductOptions.IsActive.HasValue)
            product.IsActive = updateProductOptions.IsActive.Value;

        await _productRepository.UpdateAsync(product, cancellationToken);

        return product;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"product {id} not found");

        return await _productRepository.DeleteAsync(id, cancellationToken);
    }
}
