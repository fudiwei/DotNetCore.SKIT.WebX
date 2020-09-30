using System;
using System.ComponentModel.DataAnnotations;

namespace STEP.WebX.RESTful.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value must match the specified regular expression.
    /// </summary>
    public class FieldRegexAttribute : RegularExpressionAttribute, IFieldAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool ThrowOnFailures { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        public FieldRegexAttribute(string pattern) 
            : base(pattern)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
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

            if (IsValid(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                string memberName = validationContext.GetActualMemberName();
                Exception exception;

                if (validationContext.IsMemberFromQuery())
                    exception = new Exceptions.BadRequest400InvalidQueryException(memberName);
                else
                    exception = new Exceptions.BadRequest400InvalidParameterException(memberName);

                if (ThrowOnFailures)
                {
                    throw exception;
                }

                return new ValidationResult($"The format of \"{memberName}\" is invalid.");
            }
        }
    }
}
