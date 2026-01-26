using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Exceptions
{
    public class CreateOrderInvalidInputsException : Exception
    {
        public CreateOrderInvalidInputsException(List<string> errors) : base($"Invalid order inputs: {string.Join(", ", errors)}")
        {
        }
    }
}
