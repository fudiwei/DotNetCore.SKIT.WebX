using System;
using System.Collections;
using System.Collections.Generic;
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
                bool IsDefined(object obj)
                { 
                    return EnumeratedValues.Any(e => Byte.TryParse(obj.ToString(), out Byte n) && n == (Byte)e);
                }

                if (value is IEnumerable<Byte>)
                {
                    return ((IEnumerable<Byte>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(Char))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => Char.TryParse(obj.ToString(), out Char n) && n == (Char)e);
                }

                if (value is IEnumerable<Char>)
                {
                    return ((IEnumerable<Char>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(Decimal))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => Decimal.TryParse(obj.ToString(), out Decimal n) && n == (Decimal)e);
                }

                if (value is IEnumerable<Decimal>)
                {
                    return ((IEnumerable<Decimal>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(Double))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => Double.TryParse(obj.ToString(), out Double n) && n == (Double)e);
                }

                if (value is IEnumerable<Double>)
                {
                    return ((IEnumerable<Double>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(Int16))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => Int16.TryParse(obj.ToString(), out Int16 n) && n == (Int16)e);
                }

                if (value is IEnumerable<Int16>)
                {
                    return ((IEnumerable<Int16>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(Int32))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => Int32.TryParse(obj.ToString(), out Int32 n) && n == (Int32)e);
                }

                if (value is IEnumerable<Int32>)
                {
                    return ((IEnumerable<Int32>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(Int64))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => Int64.TryParse(obj.ToString(), out Int64 n) && n == (Int64)e);
                }

                if (value is IEnumerable<Int64>)
                {
                    return ((IEnumerable<Int64>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(Single))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => Single.TryParse(obj.ToString(), out Single n) && n == (Single)e);
                }

                if (value is IEnumerable<Single>)
                {
                    return ((IEnumerable<Single>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(UInt16))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => UInt16.TryParse(obj.ToString(), out UInt16 n) && n == (UInt16)e);
                }

                if (value is IEnumerable<UInt16>)
                {
                    return ((IEnumerable<UInt16>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(UInt32))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => UInt32.TryParse(obj.ToString(), out UInt32 n) && n == (UInt32)e);
                }

                if (value is IEnumerable<UInt32>)
                {
                    return ((IEnumerable<UInt32>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(UInt64))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => UInt64.TryParse(obj.ToString(), out UInt64 n) && n == (UInt64)e);
                }

                if (value is IEnumerable<UInt64>)
                {
                    return ((IEnumerable<UInt64>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
            }
            else if (OperandType == typeof(String))
            {
                bool IsDefined(object obj)
                {
                    return EnumeratedValues.Any(e => String.Equals(e?.ToString(), obj.ToString(), StringComparisonType));
                }

                if (value is IEnumerable<String>)
                {
                    return ((IEnumerable<String>)value).All(e => IsDefined(e));
                }

                return IsDefined(value);
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
                string memberName = validationContext.GetMemberName();
                Exception exception;

                if (validationContext.IsMemberFromQuery())
                    exception = new Exceptions.BadRequest400InvalidQueryException(memberName);
                else
                    exception = new Exceptions.BadRequest400InvalidParameterException(memberName);

                if (ThrowOnFailures)
                {
                    throw exception;
                }

                return new ValidationResult($"The value of \"{memberName}\" is not defined.");
            }
        }
    }
}
