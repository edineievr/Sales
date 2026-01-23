using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.CreateOrder.Validator
{
    public class ValidatorResult
    {
        public bool IsValid  => Errors.Count == 0;
        public List<string> Errors { get; set; } = [];
    }
}
