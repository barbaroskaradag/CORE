using System.Threading.Tasks;
using AutoMapper;
using JwtProje.Business.Interfaces;
using JwtProje.Business.StringInfos;
using JwtProje.Entities.Concrete;
using JwtProje.Entities.Dtos.ProductDtos;
using JwtProje.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace JwtProje.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// api/products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = RoleInfo.Admin + "," + RoleInfo.Member)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// api/products/3
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = RoleInfo.Admin)]
        [ServiceFilter(typeof(ValidId<Product>))]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpPost]
        [Authorize(Roles = RoleInfo.Admin)]
        [ValidModel]
        public async Task<IActionResult> Add([FromForm] ProductAddDto product)
        {
            await _productService.Add(_mapper.Map<Product>(product));
            return Created("", product);
        }

        [HttpPut]
        [Authorize(Roles = RoleInfo.Admin)]
        [ValidModel]
        public async Task<IActionResult> Update([FromForm] ProductUpdateDto product)
        {
            await _productService.Update(_mapper.Map<Product>(product));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleInfo.Admin)]
        [ServiceFilter(typeof(ValidId<Product>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Remove(new Product() { Id = id });
            return NoContent();
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerFeature>();
            //errorInfo.Error.Message
            return Problem(detail: "api da bir hata olustu. En kısa zamanda düzeltilecek.");
        }
    }
}
