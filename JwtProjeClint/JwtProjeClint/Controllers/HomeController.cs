using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JwtProjeClint.ApiServices.Interfaces;
using JwtProjeClint.CustomFilter;
using JwtProjeClint.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtProjeClint.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _apiService;
        public HomeController(IProductApiService apiService)
        {
            _apiService = apiService;
        }

        [JwtAuthorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Index()
        {
            return View(await _apiService.GetAllAsync());
        }

        [JwtAuthorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductAdd productAdd)
        {
            if (ModelState.IsValid)
            {
                await _apiService.AddAsync(productAdd);
                return RedirectToAction("Index", "Home");
            }
            return View(productAdd);
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _apiService.GetByIdAsync(id));
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ProductList productList)
        {
            if (ModelState.IsValid)
            {
                await _apiService.UpdateAsync(productList);
                return RedirectToAction("Index", "Home");
            }
            return View(productList);
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _apiService.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
