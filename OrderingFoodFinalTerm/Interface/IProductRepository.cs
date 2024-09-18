

using Microsoft.AspNetCore.Mvc;
using OrderingFoodFinalTerm.DTO;


namespace OrderingFoodFinalTerm.Interface
{
    public interface IProductRepository
    {
        // lấy tất cả
        ICollection<Product> GetAll();
        // lấy data theo id
        Product GetById(int id);
        // thêm product
        Product Add(ProductDTO product);
        // sửa product
        void Update(ProductDTO product);
        // xóa product theo id
        void Delete(int id);

        void UpdateIsActive(int id, int status);
        string UploadFile(IFormFile file);
        public List<Product> Search(string key);
    }
}
