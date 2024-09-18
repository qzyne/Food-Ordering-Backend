using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderingFoodFinalTerm.Interface;

namespace OrderingFoodFinalTerm.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MainDbContext _context;

        public OrderRepository(MainDbContext context)
        {
            _context = context;
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public ICollection<Order> GetOrderByUserID(int userId)
        {
            return _context.Orders.Where(c => c.UserId == userId).ToList(); 
        }

        public Order GetOrderByID(int Orderid)
        {
            return _context.Orders.FirstOrDefault(c => c.Id == Orderid);
        }
        


        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public ICollection<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
    }
}
