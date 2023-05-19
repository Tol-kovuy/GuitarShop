using AutoMapper;
using GuitarShop.BLL.Dtos;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Repositories.CartRepository;
using GuitarShop.DAL.Repositories.ProductRepository;

namespace GuitarShop.BLL.Servisec.CartService;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IMapper mapper
        )
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task AddToCart(AddToCartDto dto)
    {
        var product = _productRepository.GetAll().SingleOrDefault(p => p.Id == dto.ProductId);
        var cart = _cartRepository.GetAll().SingleOrDefault(cart => cart.UserId == dto.UserId);
        if (cart == null)
        {
            var newCart = new Cart
            {
                UserId = dto.UserId,
                CartItems = new List<CartItem>()
            };
            await _cartRepository.CreateAsync(newCart);

            cart = newCart;
        }

        var existProduct = cart.CartItems.SingleOrDefault(p => p.Product.Id == dto.ProductId);
        if (existProduct == null)
        {
            var cartItem = new CartItem
            {
                //ProductId = dto.ProductId,
                CreatedDate = DateTime.Now,
                Product = product,
                Quantity = 1,
                CartId = dto.UserId
            };
            cart.CartItems.Add(cartItem);
        }
        else
        {
            // TODO: move to variable // How can i do this?
            cart.CartItems.SingleOrDefault(cartItem => cartItem.Product.Id == dto.ProductId).Quantity++;
        }
        await _cartRepository.UpdateAsync(cart);
    }

    public async Task CreateAsync(Cart cart)
    {
        await _cartRepository.CreateAsync(cart);
    }

    public async Task DeleteItem(long id)
    {
        var cart = _cartRepository.GetAll().SingleOrDefault(c => c.Id == id);
        await _cartRepository.DeleteAsync(cart);
    }
    public async Task DeleteCartItem(CartItem cartItem)
    {
        var cart = _cartRepository.GetAll().SingleOrDefault(c => c.Id == cartItem.CartId);
        //await _cartRepository.UpdateAsync(cart);
        await _cartRepository.DeleteCartItemAsync(cartItem);
    }

    public Cart GetByUserId(long id)
    {
        var cart = _cartRepository.GetAll().FirstOrDefault(cart => cart.UserId == id);
        return cart;
    }
}
