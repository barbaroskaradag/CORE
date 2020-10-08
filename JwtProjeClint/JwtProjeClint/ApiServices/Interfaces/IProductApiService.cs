using JwtProjeClint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjeClint.ApiServices.Interfaces
{
    public interface IProductApiService
    {
        Task<List<ProductList>> GetAllAsync();
        Task AddAsync(ProductAdd productAdd);
        Task<ProductList> GetByIdAsync(int id);
        Task UpdateAsync(ProductList productList);
        Task DeleteAsync(int id);
    }
}
