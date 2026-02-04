using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class DomainException(string message) : Exception(message)
    {
    }
}
