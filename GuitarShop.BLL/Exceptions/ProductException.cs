﻿namespace GuitarShop.BLL.Exceptions;

public class ProductException : Exception
{
	public ProductException(string message) 
		: base(message)
	{

	}
	public ProductException(string message, Exception innerException) 
		: base(message, innerException)
    {

	}
}
