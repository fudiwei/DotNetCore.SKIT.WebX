using System;

namespace STEP.WebX.RESTful.Paging
{
    /// <summary>
    /// Represents the information about how to sort data in an individual field.
    /// </summary>
    public struct PagingSortField : IComparable, IComparable<PagingSortField>, IEquatable<PagingSortField>
    {
        private const string DEFAULT_FIELD_NAME = "_default";

        /// <summary>
        /// Represents the empty desending sort field. This field is read-only.
        /// </summary>
        public readonly static PagingSortField DefaultDesc = new PagingSortField(DEFAULT_FIELD_NAME, PagingSortMode.Desc);

        /// <summary>
        /// Represents the empty ascending sort field. This field is read-only.
        /// </summary>
        public readonly static PagingSortField DefaultAsc = new PagingSortField(DEFAULT_FIELD_NAME, PagingSortMode.Asc);

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the sort mode for the field.
        /// </summary>
        public PagingSortMode Mode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal PagingSortField(string name)
            : this(name, PagingSortMode.Desc)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        internal PagingSortField(string name, PagingSortMode mode)
        {
            Name = string.IsNullOrEmpty(name) ? "_default": name;
            Mode = mode;
        }

        #region Implements System.IComparable
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CompareTo(object value)
        {
            if (value == null)
                return 1;

            if (value is PagingSortField field)
            {
                return CompareTo(field);
            }

            throw new ArgumentException(nameof(value));
        }
        #endregion

        #region Implements System.IComparable<PagingSortField>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CompareTo(PagingSortField value)
        {
            return
                object.ReferenceEquals(value, this) ? 0 :
                Name != value.Name ? Name.CompareTo(value.Name) :
                Mode != value.Mode ? Mode.CompareTo(value.Mode) :
                0;
        }
        #endregion

        #region Implements System.IEquatable<PagingSortField>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(PagingSortField obj)
        {
            return object.ReferenceEquals(obj, this) ||
                (string.Equals(Name, obj.Name, StringComparison.InvariantCultureIgnoreCase) && Mode == obj.Mode);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name.ToLowerInvariant(), Mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PagingSortField))
                return false;

            return Equals((PagingSortField)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static bool operator ==(PagingSortField f1, PagingSortField f2)
        {
            return ReferenceEquals(f2, f1) ? true : f2.Equals(f1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static bool operator !=(PagingSortField f1, PagingSortField f2)
        {
            return !(f1 == f2);
        }
    }
}
