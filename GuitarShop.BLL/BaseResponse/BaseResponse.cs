﻿using GuitarShop.BLL.Enum;

namespace GuitarShop.BLL;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}
