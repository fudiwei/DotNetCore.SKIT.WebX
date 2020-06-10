using System;
using System.ComponentModel.DataAnnotations;

namespace STEP.WebX.RESTful.DataAnnotations
{
    /// <summary>
    /// Specifies the numeric range constraints for the value of a data field.
    /// </summary>
    public class FieldInRangeAttribute : RangeAttribute, IFieldAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool NoActionInterrupt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether can equal to the minimum (default: true).
        /// </summary>
        public bool AllowEqualToMinimum { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether can equal to the maximum (default: true).
        /// </summary>
        public bool AllowEqualToMaximum { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public FieldInRangeAttribute(double minimum, double maximum) 
            : base(minimum, maximum)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public FieldInRangeAttribute(int minimum, int maximum) 
            : base(minimum, maximum)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public FieldInRangeAttribute(Type type, string minimum, string maximum) 
            : base(type, minimum, maximum)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (base.IsValid(value))
            {
                if (!AllowEqualToMinimum && object.Equals(value, Minimum))
                    return false;
                
                if (!AllowEqualToMaximum && object.Equals(value, Maximum))
                    return false;

                return true;
            }

            return false;
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
                return new ValidationResult($"The value of \"{memberName}\" is out of range.");

            if (validationContext.IsMemberFromQuery())
                throw new Exceptions.BadRequest400InvalidQueryException(memberName);
            else
                throw new Exceptions.BadRequest400InvalidParameterException(memberName);
        }
    }
}
