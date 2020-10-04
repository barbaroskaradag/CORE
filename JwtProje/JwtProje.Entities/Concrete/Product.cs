using JwtProje.Entities.Interfaces;

namespace JwtProje.Entities.Concrete
{
    public class Product : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
