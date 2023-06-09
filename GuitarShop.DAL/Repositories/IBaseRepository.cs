﻿namespace GuitarShop.DAL.Repositories;

public interface IBaseRepository<T>
{
    Task CreateAsync(T entity);
    Task DeleteAsync(T entity);
    IQueryable<T> GetAll();
    Task UpdateAsync(T entity);
}
