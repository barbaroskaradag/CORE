using JwtProje.DataAccess.Interfaces;
using JwtProje.Entities.Concrete;

namespace JwtProje.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfProductRepository : EfGenericRepository<Product>, IProductDal
    {
        public void Test()
        {
            
        }
    }
}
