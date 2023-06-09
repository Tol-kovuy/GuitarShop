﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuitarShop.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string FileName { get; set; }
    public IFormFile ImageData { get; set; }
    public CategoryViewModel Category { get; set; }
}
