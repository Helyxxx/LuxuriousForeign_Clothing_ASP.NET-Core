using ShoppingCart.Data;
using ShoppingCart.Models;
using ShoppingCart.Repository;
using System.Linq.Expressions;

namespace ShoppingCart.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Product GetFirstOrDefault(Expression<Func<Product, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Product> entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Product.FirstOrDefault(u => u.ProductId == obj.ProductId);
            if (objFromDb != null)
            {
                objFromDb.ProductName = obj.ProductName;
                objFromDb.Price = obj.Price;
                objFromDb.Category = obj.Category;
                objFromDb.Description = obj.Description;
              
                if (obj.ImgUrl != null)
                {
                    objFromDb.ImgUrl = obj.ImgUrl;
                }
            }
        }
    }
}
