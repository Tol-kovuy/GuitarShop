﻿namespace GuitarShop.DAL.Entities;


public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageData { get; set; }
    public long CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
