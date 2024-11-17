using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Domain.Models;
namespace Catalog.Domain.Contracts.Repositories;
public interface IProductRepository
{
    public Task<Product> SaveAsync(Product product, CancellationToken cancellationToken = default);
    public Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    public Task<ICollection<Product>> GetAsync(int pageNumber = 0, int pageSize = 10, CancellationToken cancellationToken = default);

    public Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default); 
    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
