using Microsoft.EntityFrameworkCore;
using OrderingFoodFinalTerm.DTO;
using OrderingFoodFinalTerm.Interface;

namespace OrderingFoodFinalTerm.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly MainDbContext _context;

        public MenuRepository(MainDbContext context) 
        {
            _context = context;
        }

        // Add
        public MenuDTO Add(MenuDTO menu)
        {
            var _menu = new Menu
            {
                Id = menu.Id,
                MenuName = menu.MenuName,
                MenuDescription = menu.MenuDescription,
                IsActive = menu.IsActive
            };
            _context.Add(_menu);
            _context.SaveChanges();
            return new MenuDTO 
            { 
                Id = menu.Id, 
                MenuName = menu.MenuName, 
                MenuDescription = menu.MenuDescription ,
                IsActive = menu.IsActive
            };
        }


        // Delete
        public void Delete(int id)
        {
            var menu = _context.Menus.SingleOrDefault(m => id == m.Id);
            if (menu != null)
            {
                _context.Remove(menu);
                _context.SaveChanges();
            }
        }

        // Get By Id
        public Menu GetById(int id)
        {
            var menu = _context.Menus.Include(p => p.Products).ThenInclude(c => c.Category).SingleOrDefault(m => m.Id == id);
            if ( menu != null)
            {
                return menu;

            }
            return null;
        }

        // Get all
        public ICollection<Menu> GetAll()
        {
            var menus = _context.Menus.Include(p => p.Products).ThenInclude(c => c.Category).ToList();

            return menus;
        }

        public void Update(MenuDTO menu)
        {
            var _menu = _context.Menus.SingleOrDefault(m => m.Id == menu.Id);
            if (menu != null)
            {
                _menu.MenuName = menu.MenuName;
                _menu.MenuDescription = menu.MenuDescription;
                _menu.IsActive = menu.IsActive;
                _context.SaveChanges();
            }
        }

        public Menu AddProduct(MenuProductDTO request)
        {
            var menu = _context.Menus.Where(c => c.Id == request.MenuId).Include(c => c.Products).ThenInclude(c => c.Category).FirstOrDefault();
            var product = _context.Products.Where(c => c.Id == request.ProductId).FirstOrDefault();
            // .Product là tham chiếu đến Products mới thêm product được
            menu.Products.Add(product);
            _context.SaveChanges();
            return menu;

        }

        // nếu tìm thấy id thì trả về true
        public bool CheckExistMenu(int id)
        {
            // kiểm tra id Menu có tồn tại kh
            return _context.Menus.Any(c => c.Id == id);
       
        }

        public Menu RemoveProduct(MenuProductDTO request)
        {
            var menu = _context.Menus.Where(c => c.Id == request.MenuId).Include(c => c.Products).FirstOrDefault();
            var product = _context.Products.Where(c => c.Id == request.ProductId).FirstOrDefault();
            // .Product là tham chiếu đến Products mới thêm product được

            menu.Products.Remove(product);
            _context.SaveChanges();
            return menu;
        }

        public List<Menu> Search(string key)
        {
            var menus = _context.Menus.AsQueryable();
            if (key != null)
            {
                menus = menus.Where(c => c.MenuName.Contains(key));

            }
            return menus.ToList();

        }
    }
}
