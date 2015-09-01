namespace System.Xml.Linq
{
	/// <summary>
	/// 
	/// </summary>
	internal static class XAttributeExtensions
	{
		/// <summary>
		/// Gets the value of the attribute as a DataTime.
		/// </summary>
		/// <param name="attribute">The attribute.</param>
		/// <returns></returns>
		public static DateTime AsDateTime(this XAttribute attribute)
		{
			return (DateTime)attribute;
		}

		/// <summary>
		/// Gets the value of the attribute as a DataTime?.
		/// </summary>
		/// <param name="attribute">The attribute.</param>
		/// <returns></returns>
		public static DateTime? AsNullableDateTime(this XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (DateTime)attribute;
		}

		public static XAttribute ToXAttribute(this bool value, XName name)
		{
			return new XAttribute(name, value);
		}

		public static XAttribute ToXAttribute(this double value, XName name)
		{
			return new XAttribute(name, value);
		}

		public static XAttribute ToXAttribute(this float value, XName name)
		{
			return new XAttribute(name, value);
		}

		public static XAttribute ToXAttribute(this int value, XName name)
		{
			return new XAttribute(name, value);
		}

		public static XAttribute ToXAttribute(this long value, XName name)
		{
			return new XAttribute(name, value);
		}

		public static XAttribute ToXAttribute(this string value, XName name)
		{
			if (value == null)
			{
				throw new XmlException(string.Format("Can not create attribute '{0}', the value was null.", name));
			}
			return new XAttribute(name, value);
		}

		/// <summary>
		/// Creates an XAttribute representing the specified value in UTC time in the xsd:Date format.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XAttribute ToXAttributeDate(this DateTime? value, XName name)
		{
			return (value.HasValue) ? ToXAttributeDate(value.Value, name) : null;
		}

		/// <summary>
		/// Creates an XAttribute representing the specified value in UTC time in the xsd:Date format.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XAttribute ToXAttributeDate(this DateTime value, XName name)
		{
			if (value.Kind != DateTimeKind.Utc)
			{
				value = value.ToUniversalTime();
			}
			return new XAttribute(name, value.ToString(Formats.XmlDate));
		}

		/// <summary>
		/// Creates an XAttribute representing the specified value in UTC time in the xsd:DateTime format.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XAttribute ToXAttributeDateTime(this DateTime? value, XName name)
		{
			return (value.HasValue) ? ToXAttributeDateTime(value.Value, name) : null;
		}

		/// <summary>
		/// Creates an XAttribute representing the specified value in UTC time in the xsd:DateTime format.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XAttribute ToXAttributeDateTime(this DateTime value, XName name)
		{
			if (value.Kind != DateTimeKind.Utc)
			{
				value = value.ToUniversalTime();
			}
			return new XAttribute(name, value.ToString(Formats.XmlDateTime));
		}

		/// <summary>
		/// Creates an XAttribute if the value is not null or whitespace
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XAttribute ToXAttributeIfNotEmpty(this string value, XName name)
		{
			return string.IsNullOrWhiteSpace(value) ? null : new XAttribute(name, value);
		}
	}
}