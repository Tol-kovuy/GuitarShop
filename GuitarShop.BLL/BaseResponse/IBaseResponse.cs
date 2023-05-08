using GuitarShop.BLL.Enum;

namespace GuitarShop.BLL;

public interface IBaseResponse<T>
{
    T Data { get; set; }
    string Description { get; set; }
    StatusCode StatusCode { get; set; }
}