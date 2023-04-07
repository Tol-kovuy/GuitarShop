﻿namespace GuitarShop.DAL;

public interface IBaseRepository<T>
{
    Task CreateAsync(T entity);
    Task DeleteAsync(T entity);
    IQueryable<T> GetAll();
    Task<T> UpdateAsync(T entity);
}
