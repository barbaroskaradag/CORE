using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using FirstWebApiCore.DAL.Context;
using FirstWebApiCore.DAL.Entities;
using FirstWebApiCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using var context = new WebApiContext();
            return Ok(context.Categories.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using var context = new WebApiContext();
            var result = context.Categories.Find(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put(Category category)
        {
            using var context = new WebApiContext();
            //var response = context.Categories.Find(category.Id);
            var response = context.Find<Category>(category.Id);
            if (response == null)
                return NotFound();

            response.Name = category.Name;
            context.Update(response);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new WebApiContext();
            var result = context.Categories.Find(id);

            if(result == null)
                return NotFound();

            context.Remove(result);
            context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Add(Category category) // iste bodyden gelecek demektir
        {
            using var context = new WebApiContext();
            context.Categories.Add(category);
            context.SaveChanges();

            return Created("", category);
        }

        [HttpGet("{id}/blogs")]
        public IActionResult GetWithBlogsById(int id) // id bilgisi route şemasından gelecek
        {
            using var context = new WebApiContext();
            var result = context.Categories.Find(id);
            if (result == null)
                return NotFound();

            var kategoriler = context.Categories.Where(x => x.Id == id).Include(x => x.Blogs).ToList();

            return Ok(kategoriler);
        }

        //api/categories/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            if (file.ContentType == "image/jpeg")
                return BadRequest();
            var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents/" + newFileName);
            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return Created("", file);
        }

        #region WepApi Attributes

        //api/categories/TestFromQuery?id=1&Name=yavuz
        [HttpGet("[action]")]
        public IActionResult TestFromQuery([FromQuery]Category category) 
        {
            return Ok();
        }

        //api/categories/test/1 için  [HttpGet("[action/{id}]")] -> ([FromRoute]int id) 
        //api/categories/test?id=1 için  [HttpGet("[action]")] -> ([FromQuery]int id) 
        [HttpGet("[action]")]
        public IActionResult TestFromQuery([FromQuery]int id) 
        {
            return Ok();
        }

        //web api çağrılma header bilgilerine erişmeyi sağlar.
        [HttpGet("[action]")]
        public IActionResult TestFromHeader([FromHeader]string Auth)
        {
            var deger = HttpContext.Request.Headers["Authentication"];
            return Ok();
        }

        //fromdan gelen dataları alabiliriz.
        [HttpGet("[action]")]
        public IActionResult TestFromForm([FromForm] string Auth)
        {
            var deger = HttpContext.Request.Form["Auth"];
            return Ok();
        }

        //bir servisin metotlarını api içerisinde depency injection ile çağırabiliriz.
        [HttpGet("[action]")]
        public IActionResult TestFromServices([FromServices]IUserService userService)
        {
            var deger = userService.GetName("barbaros");
            return Ok();
        }

        #endregion
    }
}
