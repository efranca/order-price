using System;

namespace OrderPrice.Services.BusinessExceptions
{
    public class ProductOutOfStockException : Exception
    {
        public ProductOutOfStockException(string message): base(message)
        {
            //
        }
    }
}