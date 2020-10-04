using JwtProje.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProje.DataAccess.Interfaces
{
    public interface IProductDal : IGenericDal<Product>
    {
        void Test();
    }
}
