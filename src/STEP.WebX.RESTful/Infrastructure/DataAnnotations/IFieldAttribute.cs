using System;

namespace STEP.WebX.RESTful.DataAnnotations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFieldAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether should catch exceptions if the value of a data field is invalid (default: false).
        /// If this is set to true, it will throw an <see cref="Exceptions.Api400BadRequestException"/>;
        /// otherwise, it returns a <see cref="System.ComponentModel.DataAnnotations.ValidationResult"/>.
        /// </summary>
        bool ThrowOnFailures { get; set; }
    }
}
