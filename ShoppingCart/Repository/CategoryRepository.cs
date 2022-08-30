﻿using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Category obj)
        {
            _db.Category.Update(obj);
        }
    }
}