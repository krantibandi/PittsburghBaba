using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PittsburghBabaTemple.Web.Attributes
{
    public class DataTypeAttributeAdapter : DataAnnotationsModelValidator<DataTypeAttribute>
    {
        public DataTypeAttributeAdapter(ModelMetadata metadata, ControllerContext context, DataTypeAttribute attribute)
            : base(metadata, context, attribute) { }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (Attribute.DataType == DataType.Date)
            {
                return new[] { new ModelClientValidationDateRule(Attribute.FormatErrorMessage(Metadata.GetDisplayName())) };
            }

            return base.GetClientValidationRules();
        }
    }

    public class ModelClientValidationDateRule : ModelClientValidationRule
    {
        public ModelClientValidationDateRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "ceddate";
        }
    }
}