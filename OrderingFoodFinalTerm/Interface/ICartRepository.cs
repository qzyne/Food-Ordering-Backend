using OrderingFoodFinalTerm;
using OrderingFoodFinalTerm.DTO;

namespace OrderingFoodFinalTerm.Interface
{
    public interface ICartRepository
    {
        
        void AddProduct(int idProduct, int userId, int quantity);
        ICollection<CartItem> GetCartItemById(int userId);

        Cart getCartByUserId(int userId);
        void Checkout(int userId, OrderDTO order);
        void ClearCart(int cartId);
        void removeCartItem(int idCartItem);
        void EditQuantityProduct(int cartId, int cartProductId, int quantity);
        void SaveChange();
    }
}
