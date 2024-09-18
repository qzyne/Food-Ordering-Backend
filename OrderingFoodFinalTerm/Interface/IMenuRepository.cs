using OrderingFoodFinalTerm.DTO;

namespace OrderingFoodFinalTerm.Interface
{
    public interface IMenuRepository
    {
        ICollection<Menu> GetAll();
        Menu GetById(int id);
        MenuDTO Add(MenuDTO menu); 
        void Update(MenuDTO menu);
        void Delete(int id);
        Menu AddProduct(MenuProductDTO request);
        Menu RemoveProduct(MenuProductDTO request);
        bool CheckExistMenu(int id);
        public List<Menu> Search(string key);
    }
}
