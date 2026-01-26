using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.CreateOrder.Validator
{
    public class CreateOrderItemValidator
    {
        public void Validate(CreateOrderItemDto item, ValidatorResult result)
        {
            if (item.ProductId <= 0)
                result.Errors.Add("Item must have a valid ProductId.");

            if (item.Quantity <= 0)
                result.Errors.Add($"Item {item.ProductId}: quantity must be greater than zero.");

            if (item.UnitPrice <= 0)
                result.Errors.Add($"Item {item.ProductId}: unit price must be greater than zero.");

            if (item.DiscountValue.HasValue)
            {
                if (item.DiscountValue <= 0)
                {
                    result.Errors.Add($"Item {item.ProductId}: discount value must be greater than zero.");
                }
                
                if (string.IsNullOrWhiteSpace(item.DiscountType))
                {
                    result.Errors.Add($"Item {item.ProductId}: discount type must be specified.");
                }                    
            }            
        }
    }
}
