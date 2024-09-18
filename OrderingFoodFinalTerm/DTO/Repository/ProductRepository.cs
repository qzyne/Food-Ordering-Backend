using OrderingFoodFinalTerm.Interface;
using OrderingFoodFinalTerm.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace OrderingFoodFinalTerm.Repository
{
    public class ProductRepository : IProductRepository

    {
        private readonly MainDbContext _context;

        public ProductRepository(MainDbContext context) 
        {
            _context = context;
        }
        //Add
        public Product Add(ProductDTO product)
        {
            var ImagePath = UploadFile(product.file);

            var _product = new Product
            {
                ProductName = product.ProductName,
                ImagePath = ImagePath,
                Price = product.Price,
                Description = product.Description,
                IsActive = product.IsActive,
                CategoryId = product.CategoryId,
            };
            _context.Add(_product);
            _context.SaveChanges();
            return _product;
        }


        public void Delete(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if(product != null)
            {
                Common.DeleteImage(product.ImagePath);
                _context.Remove(product);
                _context.SaveChanges();
            }
        }

        // Get all product
        public ICollection<Product> GetAll()
        {
            var products = _context.Products.Include(c => c.Category).Select(e => new Product()
            {
                Id = e.Id,
                ProductName = e.ProductName,
                Price = e.Price,
                Description = e.Description,
                Category = e.Category,
                IsActive = e.IsActive,
                CategoryId = e.CategoryId,
                ImagePath = String.Format("http://localhost:5143/images/" + e.ImagePath)
            }).ToList();
            return products;
        }

        // Get product by id
        public Product GetById(int id)
        {
            var product = _context.Products.Include(c => c.Category).SingleOrDefault(p => p.Id == id);
            if(product != null)
            {
                return product;
            }
            return null;
        }


        // Update product
        public void Update(ProductDTO product)
        {
            
            var ImagePath = UploadFile(product.file);
            
            var _product = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            _product.ProductName = product.ProductName;
            _product.Price = product.Price;
            _product.Description = product.Description;
            _product.IsActive = product.IsActive;
            _product.ImagePath = ImagePath;
            _product.CategoryId = product.CategoryId;
            _context.SaveChanges();
        }

        public void UpdateIsActive(int id, int status)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            product.IsActive = status;
            _context.SaveChanges();
            
        }

        public string UploadFile(IFormFile file)
        {
            string filename = "";
            try 
            {
                FileInfo fileinfo = new FileInfo(file.FileName);
                filename = file.FileName; 
                var getFilePath = Common.GetFilePath(filename);
                
                using (var  stream = new FileStream(getFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return filename;
            }

            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public List<Product> Search(string key)
        {
            var products = _context.Products.AsQueryable();
            if (key != null)
            {
                products = products.Where(c => c.ProductName.Contains(key)).Include(c => c.Category);

            }
            var result = products.Select(e => new Product
            {
                Id = e.Id,
                ProductName = e.ProductName,
                Price = e.Price,
                Description = e.Description,
                Category = e.Category,
                IsActive = e.IsActive,
                CategoryId = e.CategoryId,
                ImagePath = String.Format("http://localhost:5143/images/" + e.ImagePath)
            });
            return result.ToList();
            

        }

    }
}
