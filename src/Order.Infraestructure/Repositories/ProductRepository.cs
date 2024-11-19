using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Contracts.Repositories;
using Order.Domain.Models;

namespace Order.Infraestructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly HttpClient _httpClient;

    public ProductRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Product> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
