using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Models;

namespace Order.Domain.Contracts.Repositories;

public interface IProductRepository
{
    public Task<Product> GetByIdAsync(long id);
}
