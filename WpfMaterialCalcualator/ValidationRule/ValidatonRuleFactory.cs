using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.ValidationRule
{
    public static class ValidatonRuleFactory
    {
        public static ValidationItem NotEmpty()
        {
            ValidationItem validation = new ValidationItem();
            validation.ErrorMessage = "Can Not Be Empty";
            validation.ValidationExpression = new CustomValidation().IsNotEmpty;
            return validation;
        }

        private static ValidationItem MustPositive()
        {
            ValidationItem validation = new ValidationItem();
            validation.ErrorMessage = "Must be a positive Number";
            validation.ValidationExpression = new CustomValidation().IsPositiveNumber;
            return validation;
        }

        public static List<ValidationItem> NotEmptyAndMustPositive()
        {
            List<ValidationItem> validations = new List<ValidationItem>();
            validations.Add(NotEmpty());
            validations.Add(MustPositive());
            return validations;
        }
    }
}
