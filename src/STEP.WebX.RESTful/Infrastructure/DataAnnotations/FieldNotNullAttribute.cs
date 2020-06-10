using System;
using System.ComponentModel.DataAnnotations;

namespace STEP.WebX.RESTful.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value is required and not nullable.
    /// </summary>
    public class FieldNotNullAttribute : RequiredAttribute, IFieldAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool NoActionInterrupt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (!AllowEmptyStrings && value is string)
                return !string.IsNullOrWhiteSpace(value?.ToString());

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
                throw new ArgumentNullException(nameof(validationContext));

            string memberName = validationContext.GetActualMemberName();

            if (IsValid(value))
                return ValidationResult.Success;

            if (NoActionInterrupt)
                return new ValidationResult($"The value of \"{memberName}\" can not be null or empty.");

            if (value == null)
            {
                if (validationContext.IsMemberFromQuery())
                    throw new Exceptions.BadRequest400LackOfQueryException(memberName);
                else
                    throw new Exceptions.BadRequest400LackOfParameterException(memberName);
            }
            else
            {
                if (validationContext.IsMemberFromQuery())
                    throw new Exceptions.BadRequest400InvalidQueryException(memberName);
                else
                    throw new Exceptions.BadRequest400InvalidParameterException(memberName);
            }
        }
    }
}
