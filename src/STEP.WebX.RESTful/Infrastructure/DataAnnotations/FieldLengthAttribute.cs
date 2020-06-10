using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace STEP.WebX.RESTful.DataAnnotations
{
    /// <summary>
    /// Specifies the minimum and maximum length of elements that are allowed in a data field.
    /// </summary>
    public class FieldLengthAttribute : ValidationAttribute, IFieldAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool NoActionInterrupt { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of a string, or the maximum count of a collection.
        /// </summary>
        public int MaximumLength { get; }

        /// <summary>
        /// Gets or sets the minimum length of a string, or the minimum count of a collection.
        /// </summary>
        public int MinimumLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLen"></param>
        public FieldLengthAttribute(int maxLen)
        {
            if (maxLen < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLen));

            MaximumLength = maxLen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minLen"></param>
        /// <param name="maxLen"></param>
        public FieldLengthAttribute(int minLen, int maxLen)
            : this(maxLen)
        {
            if (minLen < 0 || minLen < maxLen)
                throw new ArgumentOutOfRangeException(nameof(minLen));

            MinimumLength = minLen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            int len = (value is ICollection) ? ((ICollection)this).Count : ((string)value).Length;
            return len >= MinimumLength && len <= MaximumLength;
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
                return new ValidationResult($"The length of \"{memberName}\" is invalid.");

            if (validationContext.IsMemberFromQuery())
                throw new Exceptions.BadRequest400InvalidQueryException(memberName);
            else
                throw new Exceptions.BadRequest400InvalidParameterException(memberName);
        }
    }
}
