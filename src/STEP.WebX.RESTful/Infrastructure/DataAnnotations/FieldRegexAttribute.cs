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
        public bool NoActionInterrupt { get; set; }

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

            string memberName = validationContext.GetActualMemberName();

            if (IsValid(value))
                return ValidationResult.Success;

            if (NoActionInterrupt)
                return new ValidationResult($"The value of \"{memberName}\" is invalid.");

            if (validationContext.IsMemberFromQuery())
                throw new Exceptions.BadRequest400InvalidQueryException(memberName);
            else
                throw new Exceptions.BadRequest400InvalidParameterException(memberName);
        }
    }
}
