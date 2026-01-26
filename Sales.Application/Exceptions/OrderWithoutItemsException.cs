using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Exceptions
{
    public class OrderWithoutItemsException : Exception
    {  
        public OrderWithoutItemsException() : base("The order must have at least one item.")
        {
        }
    }
}
