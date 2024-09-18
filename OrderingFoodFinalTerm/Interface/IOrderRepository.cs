namespace OrderingFoodFinalTerm.Interface
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Order GetOrderByID(int id);
        ICollection<Order> GetOrderByUserID(int userId);
        ICollection<Order> GetAll();
        void SaveChange();
    }
}
