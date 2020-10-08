using JwtProjeClint.ApiServices.Interfaces;
using JwtProjeClint.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JwtProjeClint.ApiServices.Concrete
{
    public class ProductApiManager : IProductApiService
    {
        private readonly IHttpContextAccessor _accessor;
        public ProductApiManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task AddAsync(ProductAdd productAdd)
        {
            var token = _accessor.HttpContext.Session.GetString("token");
            if (String.IsNullOrEmpty(token) == false)
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(productAdd);

                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://localhost:63641/api/products", stringContent);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var token = _accessor.HttpContext.Session.GetString("token");
            if (String.IsNullOrEmpty(token) == false)
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                await httpClient.DeleteAsync("http://localhost:63641/api/products/" + id);
            }
        }

        public async Task<List<ProductList>> GetAllAsync()
        {
            var token = _accessor.HttpContext.Session.GetString("token");

            if(String.IsNullOrEmpty(token) == false)
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await httpClient.GetAsync("http://localhost:63641/api/products");

                if(response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<ProductList>>(await response.Content.ReadAsStringAsync());
                }
            }
            return null;
        }

        public async Task<ProductList> GetByIdAsync(int id)
        {
            var token = _accessor.HttpContext.Session.GetString("token");
            if (String.IsNullOrEmpty(token) == false)
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync("http://localhost:63641/api/products/"+id);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ProductList>(await response.Content.ReadAsStringAsync());
                }
            }
            return null;
        }

        public async Task UpdateAsync(ProductList productList)
        {
            var token = _accessor.HttpContext.Session.GetString("token");
            if (String.IsNullOrEmpty(token) == false)
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(productList);

                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                await httpClient.PutAsync("http://localhost:63641/api/products", stringContent);
            }
        }
    }
}
