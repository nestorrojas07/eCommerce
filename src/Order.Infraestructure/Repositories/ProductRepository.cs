using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
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
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<Product> GetByIdAsync(long id)
    {

        Console.WriteLine($"Consultando Product {_httpClient.BaseAddress}/api/products/{id}");
        var response = await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");

        return response;
    }
}
