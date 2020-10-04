using JwtProje.Business.Interfaces;
using JwtProje.DataAccess.Interfaces;
using JwtProje.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProje.Business.Contrete
{
    public class ProductManager :  GenericManager<Product>, IProductService
    {
        private readonly IProductDal _productDal;
        public ProductManager(IGenericDal<Product> genericDal, IProductDal productDal) : base (genericDal)
        {
            _productDal = productDal;
        }

        public void Test()
        {
            _productDal.Test();
        }
    }
}
