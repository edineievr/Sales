using Sales.Application.UseCases.CreateOrder;
using Sales.Application.UseCases.CreateOrder.Validator;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Tests.Validators
{

    [TestFixture]
    public class CreateOrderCommandValidatorTests
    { 
        [Test]
        public void When_ValidCommand_Should_PassValidation()
        {
            
            var validator = new CreateOrderCommandValidator();

            var command = new CreateOrderCommand
            {
                Items = new List<CreateOrderItemDto>
                    {
                        new() { ProductId = 1, Quantity = 2, UnitPrice = 50m }
                    },
                DiscountValue = 10m,
                DiscountType = "Percentage"
            };

            var result = validator.Validate(command);


            result.IsValid.ShouldBeTrue();
            result.Errors.Count.ShouldBe(0);
        }
        [Test]
        public void When_CommandHasNoItems_Should_FailValidation()
        {
            var validator = new CreateOrderCommandValidator();

            var command = new CreateOrderCommand
            {
                Items = new List<CreateOrderItemDto>(),
                DiscountValue = null,
                DiscountType = null
            };

            var result = validator.Validate(command);
            
            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void When_CommandHasInvalidDiscount_Should_FailValidation()
        {
            var validator = new CreateOrderCommandValidator();

            var command = new CreateOrderCommand
            {
                Items = new List<CreateOrderItemDto>
                    {
                        new() { ProductId = 1, Quantity = 2, UnitPrice = 50m }
                    },
                DiscountValue = -5m,
                DiscountType = ""
            };

            var result = validator.Validate(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void When_ItemsAreInvalid_Should_FailValidation()
        {
            var validator = new CreateOrderCommandValidator();
            var command = new CreateOrderCommand
            {
                Items = new List<CreateOrderItemDto>
                    {
                        new() { ProductId = 0, Quantity = -5, UnitPrice = -10m }
                    },
                DiscountValue = null,
                DiscountType = null
            };

            var result = validator.Validate(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public void When_DiscountIsInvalid_Should_FailValidation()
        {
            var validator = new CreateOrderCommandValidator();
            var command = new CreateOrderCommand
            {
                Items = new List<CreateOrderItemDto>
                    {
                        new() { ProductId = 1, Quantity = 2, UnitPrice = 50m }
                    },
                DiscountValue = 0m,
                DiscountType = ""
            };
            var result = validator.Validate(command);
            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void When_DiscountTypeIsMissingWithDiscountValue_Should_FailValidation()
        {
            var validator = new CreateOrderCommandValidator();
            var command = new CreateOrderCommand
            {
                Items = new List<CreateOrderItemDto>
                    {
                        new() { ProductId = 1, Quantity = 2, UnitPrice = 50m }
                    },
                DiscountValue = 10m,
                DiscountType = null
            };

            var result = validator.Validate(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
        }

    }
}

