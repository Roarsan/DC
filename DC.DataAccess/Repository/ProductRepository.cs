using DC.DataAccess.Repository.IRepository;
using DC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DC.DataAccess.Repository
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  

        public void Update(Product obj)
        {
           var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Colour = obj.Colour;
                objFromDb.Price = obj.Price;
                objFromDb.Size = obj.Size;
                objFromDb.Material = obj.Material;
                objFromDb.Type = obj.Type;
                objFromDb.DiscountedPrice = obj.DiscountedPrice;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                if(objFromDb.ImageURL != null)
                {
                    objFromDb.ImageURL = obj.ImageURL;
                }
            }
        }

      
    }
}
