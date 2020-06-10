using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace STEP.WebX.RESTful.DataAnnotations
{
    /// <summary>
    /// Specifies the possible value constraints for the value of a data field.
    /// </summary>
    public class FieldEnumeratedValuesAttribute : ValidationAttribute, IFieldAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool NoActionInterrupt { get; set; }

        /// <summary>
        /// Gets or sets the option that specifies how the strings will be compared.
        /// </summary>
        public StringComparison StringComparisonType { get; set; } = StringComparison.InvariantCultureIgnoreCase;

        /// <summary>
        /// Gets the type of the data field whose value must be validated.
        /// </summary>
        public Type OperandType { get; }

        /// <summary>
        /// Gets the possible value allowed field value.
        /// </summary>
        public object[] EnumeratedValues { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params double[] values)
        {
            OperandType = typeof(double);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params int[] values)
        {
            OperandType = typeof(int);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params string[] values)
        {
            OperandType = typeof(string);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
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

            if (OperandType == typeof(string))
            {
                return EnumeratedValues.Any(e => string.Equals((string)e, (string)value, StringComparisonType));
            }

            return EnumeratedValues.Contains(value);
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
                return new ValidationResult($"The value of \"{memberName}\" is not defined.");

            if (validationContext.IsMemberFromQuery())
                throw new Exceptions.BadRequest400InvalidQueryException(memberName);
            else
                throw new Exceptions.BadRequest400InvalidParameterException(memberName);
        }
    }
}
