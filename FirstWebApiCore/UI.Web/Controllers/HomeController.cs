using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    /// <summary>
    /// apiler ile content nesneleri üzerinden çalışılır.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue();
            var response = await httpClient.GetAsync("http://localhost:63227/api/categories");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // başarılı
                var stringJson = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(stringJson);
                return View(categories);
            }
            else if (response.IsSuccessStatusCode)
            {
                // başarlı
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            using var httpClient = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(category);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); ;
            var result = await httpClient.PostAsync("http://localhost:63227/api/categories", stringContent);

            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("","Bir sorun oluştu");
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:63227/api/categories/"+ id);
            if (response.IsSuccessStatusCode)
            {
                var jsonCategory = await response.Content.ReadAsStringAsync();
                Category category = JsonConvert.DeserializeObject<Category>(jsonCategory);
                return View(category);
               
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            using var httpClient = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(category);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); ;
            var result = await httpClient.PutAsync("http://localhost:63227/api/categories", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Bir sorun oluştu");
            return View(category);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            //dosyanın byte arrayi alınır.
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            ByteArrayContent fileContext = new ByteArrayContent(memoryStream.ToArray());

            //file nesnesinin content type bilgisini doldurur. dosya tip kontrolünde kullanılabilir.
            fileContext.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            //dosya göndermeyi sağlayan yardımcı nesneye byte array verilir.
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(fileContext, "file", file.FileName);

            //ilgili apiye gönderilir.
            using var httpClient = new HttpClient();
            var result = await httpClient.PostAsync("http://localhost:63227/api/categories/upload", formData);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Bir sorun oluştu");
            return View(file);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
