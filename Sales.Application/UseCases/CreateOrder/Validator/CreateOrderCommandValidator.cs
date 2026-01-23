namespace Sales.Application.UseCases.CreateOrder.Validator
{
    public class CreateOrderCommandValidator
    {
        private readonly CreateOrderItemValidator _itemValidator;

        public CreateOrderCommandValidator()
        {
            _itemValidator = new CreateOrderItemValidator();
        }

        public ValidatorResult Validate(CreateOrderCommand command)
        {
            var validator = new ValidatorResult();

            if (command.Items == null || command.Items.Count <= 0)
            {
                validator.Errors.Add("Order must contain at least one item.");
                return validator;
            }            

            if (command.DiscountValue.HasValue)
            {
                if (command.DiscountValue <= 0)
                {
                    validator.Errors.Add("Order discount value must be greater than zero.");
                }
            }

            if (string.IsNullOrWhiteSpace(command.DiscountType))
            {
                validator.Errors.Add("Order discount type must be specified.");
            }

            foreach (var item in command.Items)
            {
                _itemValidator.Validate(item, validator);
            } 
            
            return validator;
        }
    }
}
