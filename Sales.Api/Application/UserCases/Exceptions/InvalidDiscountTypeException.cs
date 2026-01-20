namespace Sales.Api.Application.UserCases.Exceptions
{
    public class InvalidDiscountTypeException : Exception
    {
        public InvalidDiscountTypeException(string discountType) : base($"Invalid discount type: {discountType}")
        {
        }
    }
}
