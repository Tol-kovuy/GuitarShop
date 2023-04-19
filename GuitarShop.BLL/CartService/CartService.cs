using GuitarShop.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarShop.BLL.CartService;

public class CartService : ICartService
{
    public Task<IBaseResponse<bool>> AddToCart(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IBaseResponse<bool>> DeleteItem(int id)
    {
        throw new NotImplementedException();
    }
}
