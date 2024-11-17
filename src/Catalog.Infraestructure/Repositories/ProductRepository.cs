using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Dtos.Response;

namespace Catalog.Infraestructure.Repositories;

internal class ProductRepository : IProductRepository
{

    private readonly CatalogContext _context;

    public ProductRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.SingleOrDefaultAsync(e => e.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<PaginatedData<Product>> GetAsync(int pageNumber = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {

        var totalItems = await _context.Products
            .Where(p => p.IsActive)
           .LongCountAsync();

        var items = await _context.Products
         .Where(p => p.IsActive)
         .OrderBy(e => e.Id)
         .Skip((pageNumber - 1) * pageSize)
         .Take(pageSize)
         .ToListAsync();

        return new PaginatedData<Product>(pageNumber, pageSize, totalItems, items);
    }

    public async Task<Product> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.Where(p => p.IsActive).SingleOrDefaultAsync(e => e.Id == id);
        if (product != null)
            return product;

        throw new KeyNotFoundException($"product {id} not found");
    }

    public async Task<Product> SaveAsync(Product product, CancellationToken cancellationToken = default)
    {
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        product.UpdatedAt = DateTime.UtcNow; 
        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return true;
    }
}
