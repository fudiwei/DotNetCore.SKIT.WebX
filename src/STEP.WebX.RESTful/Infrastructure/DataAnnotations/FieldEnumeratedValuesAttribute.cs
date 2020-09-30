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
        public bool ThrowOnFailures { get; set; }

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
        public FieldEnumeratedValuesAttribute(params Byte[] values)
        {
            OperandType = typeof(Byte);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params Char[] values)
        {
            OperandType = typeof(Char);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params Decimal[] values)
        {
            OperandType = typeof(Decimal);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params Double[] values)
        {
            OperandType = typeof(Double);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params Int16[] values)
        {
            OperandType = typeof(Int16);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params Int32[] values)
        {
            OperandType = typeof(Int32);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params Int64[] values)
        {
            OperandType = typeof(Int64);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params SByte[] values)
        {
            OperandType = typeof(SByte);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params Single[] values)
        {
            OperandType = typeof(Single);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params UInt16[] values)
        {
            OperandType = typeof(UInt16);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params UInt32[] values)
        {
            OperandType = typeof(UInt32);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params UInt64[] values)
        {
            OperandType = typeof(UInt64);
            EnumeratedValues = values?.Distinct()?.Select(e => (object)e)?.ToArray() ?? new object[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public FieldEnumeratedValuesAttribute(params String[] values)
        {
            OperandType = typeof(String);
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

            if (OperandType == typeof(Byte))
            {
                return EnumeratedValues.Any(e => Byte.TryParse(value.ToString(), out Byte val) && val == (Byte)e);
            }
            else if (OperandType == typeof(Char))
            {
                return EnumeratedValues.Any(e => Char.TryParse(value.ToString(), out Char val) && val == (Char)e);
            }
            else if (OperandType == typeof(Decimal))
            {
                return EnumeratedValues.Any(e => Decimal.TryParse(value.ToString(), out Decimal val) && val == (Decimal)e);
            }
            else if (OperandType == typeof(Double))
            {
                return EnumeratedValues.Any(e => Double.TryParse(value.ToString(), out Double val) && val == (Double)e);
            }
            else if (OperandType == typeof(Int16))
            {
                return EnumeratedValues.Any(e => Int16.TryParse(value.ToString(), out Int16 val) && val == (Int16)e);
            }
            else if (OperandType == typeof(Int32))
            {
                return EnumeratedValues.Any(e => Int32.TryParse(value.ToString(), out Int32 val) && val == (Int32)e);
            }
            else if (OperandType == typeof(Int64))
            {
                return EnumeratedValues.Any(e => Int64.TryParse(value.ToString(), out Int64 val) && val == (Int64)e);
            }
            else if (OperandType == typeof(Single))
            {
                return EnumeratedValues.Any(e => Single.TryParse(value.ToString(), out Single val) && val == (Single)e);
            }
            else if (OperandType == typeof(UInt16))
            {
                return EnumeratedValues.Any(e => UInt16.TryParse(value.ToString(), out UInt16 val) && val == (UInt16)e);
            }
            else if (OperandType == typeof(UInt32))
            {
                return EnumeratedValues.Any(e => UInt32.TryParse(value.ToString(), out UInt32 val) && val == (UInt32)e);
            }
            else if (OperandType == typeof(UInt64))
            {
                return EnumeratedValues.Any(e => UInt64.TryParse(value.ToString(), out UInt64 val) && val == (UInt64)e);
            }
            else if (OperandType == typeof(String))
            {
                return EnumeratedValues.Any(e => String.Equals(e?.ToString(), value.ToString(), StringComparison.InvariantCultureIgnoreCase));
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

            if (IsValid(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                string memberName = validationContext.GetActualMemberName();
                if (ThrowOnFailures)
                {
                    if (validationContext.IsMemberFromQuery())
                        throw new Exceptions.BadRequest400InvalidQueryException(memberName);
                    else
                        throw new Exceptions.BadRequest400InvalidParameterException(memberName);
                }

                return new ValidationResult($"The value of \"{memberName}\" is not defined.");
            }
        }
    }
}
