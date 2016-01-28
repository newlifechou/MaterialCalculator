using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Resource
{
    public static class ValidatonRuleFactory
    {
        public static CustomValidationItem NotEmpty()
        {
            CustomValidationItem validation = new CustomValidationItem();
            validation.ErrorMessage = "Can Not Be Empty";
            validation.validationExpression = new CustomValidation().IsNotEmpty;
            return validation;
        }
    }
}
