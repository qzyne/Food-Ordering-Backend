using Microsoft.EntityFrameworkCore;
using OrderingFoodFinalTerm.DTO;
using OrderingFoodFinalTerm.Interface;
using System.ComponentModel;

namespace OrderingFoodFinalTerm.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MainDbContext _context;

        public CartRepository(MainDbContext context)
        {
            _context = context;
        }
        
        public void AddProduct(int idProduct, int userId, int quantity)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            if(cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            var cartItem = _context.CartItems.FirstOrDefault(p => p.CartId == cart.Id && p.ProductId == idProduct);
            var product = _context.Products.Find(idProduct);
            if(product == null)
            {
                throw new Exception("Not Found");
            }
            if(cartItem != null)
            {
                cartItem.Quantity += quantity;
                cartItem.TotalPrice = cartItem.Quantity * _context.Products.Find(idProduct).Price;
                _context.SaveChanges();
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = idProduct,
                    Quantity = quantity,
                    TotalPrice = _context.Products.Find(idProduct).Price * quantity
                };
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
            }
        }


        public void EditQuantityProduct(int cartId, int cartProductId, int quantity)
        {
            // kiểm tra xem có productID cần tìm hay không
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == cartProductId && c.CartId == cartId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                cartItem.TotalPrice = quantity * _context.Products.Find(cartProductId).Price;
                _context.SaveChanges();
            }
        }

        public ICollection<CartItem> GetCartItemById(int userId)
        {
            var cart = _context.Carts.Include(p => p.CartItems).ThenInclude(item => item.Product).FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                var cartItem = cart.CartItems.ToList();
                return cartItem;
            }
            return null;
        }

        public Cart getCartByUserId(int userId)
        {
            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c=> c.UserId == userId);
            return cart;
        }

        public void ClearCart(int cartId)
        {
            var cartItems = _context.CartItems.Where(c => c.CartId == cartId);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public void Checkout(int userId, OrderDTO order)
        {
            var cart = getCartByUserId(userId);
            if (cart != null)
            {
                var _order = new Order
                {
                    CustomerName = order.CustomerName,
                    CustomerPhone = order.CustomerPhone,
                    CustomerAddress = order.CustomerAddress,
                    TotalPrice = cart.CartItems.Sum(c => c.TotalPrice),
                    Quantity = cart.CartItems.Sum(c => c.Quantity),
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    Status = 0,
                };
                _context.Add(_order);
                _context.SaveChanges();

                ClearCart(cart.Id);
            }
        }

        public void removeCartItem(int idCartItem)
        {
            var cartItem = _context.CartItems.Find(idCartItem);
            
            if(cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}
