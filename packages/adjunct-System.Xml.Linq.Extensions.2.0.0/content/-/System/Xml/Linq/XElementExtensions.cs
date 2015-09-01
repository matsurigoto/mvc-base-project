namespace System.Xml.Linq
{
	/// <summary>
	/// This class provides extensions to <see cref="XElement"/>.
	/// </summary>
	internal static class XElementExtensions
	{
		/// <summary>
		/// Gets the value of the attribute as a string.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static string AttributeValue(this XElement element, XName attributeName)
		{
			return element.EnsureAttribute(attributeName).Value;
		}

		/// <summary>
		/// Gets the value of the attribute as a string, or the specified default value if the attribute is missing.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static string AttributeValue(this XElement element, XName attributeName, string defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : attribute.Value;
		}

		/// <summary>
		/// Gets the value of the attribute as a Boolean.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static bool AttributeValueAsBool(this XElement element, XName attributeName)
		{
			return (bool)element.EnsureAttribute(attributeName);
		}

		/// <summary>
		/// Gets the value of the attribute as a Boolean, or the specified default value if the attribute missing.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static bool AttributeValueAsBool(this XElement element, XName attributeName, bool defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : (bool)attribute;
		}

		/// <summary>
		/// Gets the value of the attribute as a DateTime.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static DateTime AttributeValueAsDateTime(this XElement element, XName attributeName)
		{
			return (DateTime)element.EnsureAttribute(attributeName);
		}

		/// <summary>
		/// Gets the value of the attribute as a DateTime, or the specified default value if the attribute missing.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static DateTime AttributeValueAsDateTime(this XElement element, XName attributeName, DateTime defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : (DateTime)attribute;
		}

		/// <summary>
		/// Gets the value of the attribute as a double.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static double AttributeValueAsDouble(this XElement element, XName attributeName)
		{
			return (double)element.EnsureAttribute(attributeName);
		}

		/// <summary>
		/// Gets the value of the attribute as a double, or the specified default value if the attribute is missing.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static double AttributeValueAsDouble(this XElement element, XName attributeName, double defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : (double)attribute;
		}

		/// <summary>
		/// Gets the value of the attribute as a float.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static float AttributeValueAsFloat(this XElement element, XName attributeName)
		{
			return (float)element.EnsureAttribute(attributeName);
		}

		/// <summary>
		/// Gets the value of the attribute as a float, or the specified default value if the attribute missing.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static float AttributeValueAsFloat(this XElement element, XName attributeName, float defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : (float)attribute;
		}

		/// <summary>
		/// Gets the value of the attribute as an int.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static int AttributeValueAsInt(this XElement element, XName attributeName)
		{
			return (int)element.EnsureAttribute(attributeName);
		}

		/// <summary>
		/// Gets the value of the attribute as an integer, or the specified default value if the attribute is missing.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static int AttributeValueAsInt(this XElement element, XName attributeName, int defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : (int)attribute;
		}

		/// <summary>
		/// Gets the value of the attribute as a long.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static long AttributeValueAsLong(this XElement element, XName attributeName)
		{
			return (long)element.EnsureAttribute(attributeName);
		}

		/// <summary>
		/// Gets the value of the attribute as a long, or the specified default value if the attribute is missing.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static long AttributeValueAsLong(this XElement element, XName attributeName, long defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : (long)attribute;
		}

		/// <summary>
		/// Attributes the value as version.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static Version AttributeValueAsVersion(this XElement element, XName attributeName)
		{
			return new Version(element.EnsureAttribute(attributeName).Value);
		}

		/// <summary>
		/// Attributes the value as version.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <param name="defaultValue">The default value if the attribute is missing.</param>
		public static Version AttributeValueAsVersion(this XElement element, XName attributeName, Version defaultValue)
		{
			XAttribute attribute = element.Attribute(attributeName);
			return (attribute == null) ? defaultValue : new Version(attribute.Value);
		}

		/// <summary>
		/// Changes the namespace of the root element and all descendants with the same namespace to the new namespace.
		/// </summary>
		/// <param name="root">The root.</param>
		/// <param name="newNamespace">The new namespace.</param>
		public static void ChangeNamespaceTo(this XElement root, XNamespace newNamespace)
		{
			XName xmlns = "xmlns";
			XNamespace origionalNamespace = root.Name.Namespace;
			foreach (XElement element in root.DescendantsAndSelf())
			{
				if (element.Name.Namespace == origionalNamespace)
				{
					element.SetAttributeValue(xmlns, newNamespace.NamespaceName);
					element.Name = newNamespace + element.Name.LocalName;
				}
			}
		}

		public static string ChildElementValue(this XElement element, XName childName)
		{
			return element.EnsureElement(childName).Value;
		}

		public static string ChildElementValue(this XElement element, XName childName, string defaultValue)
		{
			XElement child = element.Element(childName);
			return (child == null) ? defaultValue : child.Value;
		}

		/// <summary>
		/// Ensures that an attribute with the specified name exists on the element, and return the attribute.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <exception cref="XmlException">Thrown if the attribute is missing.</exception>
		public static XAttribute EnsureAttribute(this XElement element, XName attributeName)
		{
			XAttribute attribute = element.Attribute(attributeName);
			if (attribute == null)
			{
				string message = String.Format("The required attribute '{0}' was not found on '{1}'.", attributeName, element.Name);
				throw new XmlException(message);
			}
			return attribute;
		}

		/// <summary>
		/// Ensures the child element with the specified name exists, and returns the child element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="childName">Name of the child element.</param>
		/// <returns>An <see cref="XElement"/>.</returns>
		/// <exception cref="XmlException">Thrown if the child element is missing.</exception>
		public static XElement EnsureElement(this XElement element, XName childName)
		{
			XElement child = element.Element(childName);
			if (child == null)
			{
				string message = String.Format("The required child '{0}' was not found on '{1}'.", childName, element.Name);
				throw new XmlException(message);
			}
			return child;
		}

		/// <summary>
		/// Ensures the <see cref="XElement"/> name matches the specified name.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="expectedName">The expected element name.</param>
		/// <exception cref="XmlException">Thrown if the element name does not match the expected name.</exception>
		public static void EnsureElementNameIs(this XElement element, XName expectedName)
		{
			if (element.Name == expectedName)
			{
				return;
			}
			string message = String.Format("Expected element with name '{0}' but found '{1}'.", expectedName, element.Name);
			throw new XmlException(message);
		}

		public static XElement ToXElement(this bool value, XName name)
		{
			return new XElement(name, value);
		}

		public static XElement ToXElement(this double value, XName name)
		{
			return new XElement(name, value);
		}

		public static XElement ToXElement(this float value, XName name)
		{
			return new XElement(name, value);
		}

		public static XElement ToXElement(this int value, XName name)
		{
			return new XElement(name, value);
		}

		public static XElement ToXElement(this long value, XName name)
		{
			return new XElement(name, value);
		}

		public static XElement ToXElement(this string value, XName name)
		{
			if (String.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("value is null or white space", "value");
			}
			return new XElement(name, value);
		}

		public static XElement ToXElement(this XmlElement xmlElement)
		{
			return XElement.Load(xmlElement.CreateNavigator().ReadSubtree());
		}

		/// <summary>
		/// </summary>
		/// <param name="value">The date time.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XElement ToXElementDate(this DateTime? value, XName name)
		{
			return value == null ? null : value.Value.ToXElementDate(name);
		}

		/// <summary>
		/// </summary>
		/// <param name="value">The date time.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XElement ToXElementDate(this DateTime value, XName name)
		{
			if (value.Kind != DateTimeKind.Utc)
			{
				value = value.ToUniversalTime();
			}
			return new XElement(name, value.ToString(Formats.XmlDate));
		}

		/// <summary>
		/// </summary>
		/// <param name="value">The date time.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XElement ToXElementDateTime(this DateTime? value, XName name)
		{
			return value == null ? null : value.Value.ToXElementDateTime(name);
		}

		/// <summary>
		/// </summary>
		/// <param name="value">The date time.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static XElement ToXElementDateTime(this DateTime value, XName name)
		{
			if (value.Kind != DateTimeKind.Utc)
			{
				value = value.ToUniversalTime();
			}
			return new XElement(name, value.ToString(Formats.XmlDateTime));
		}

		public static XElement ToXElementIfNotEmpty(this string value, XName name)
		{
			return String.IsNullOrWhiteSpace(value) ? null : new XElement(name, value);
		}

		public static bool ValueAsBool(this XElement element, bool defaultValue)
		{
			return (element == null) ? defaultValue : (bool)element;
		}

		public static DateTime ValueAsDateTime(this XElement element, DateTime defaultValue)
		{
			return (element == null) ? defaultValue : XmlConvert.ToDateTime(element.Value, XmlDateTimeSerializationMode.Unspecified);
		}

		public static double ValueAsDouble(this XElement element, double defaultValue)
		{
			return (element == null) ? defaultValue : (double)element;
		}

		public static float ValueAsFloat(this XElement element, float defaultValue)
		{
			return (element == null) ? defaultValue : (float)element;
		}

		public static int ValueAsInt(this XElement element, int defaultValue)
		{
			return (element == null) ? defaultValue : (int)element;
		}

		public static long ValueAsLong(this XElement element, long defaultValue)
		{
			return (element == null) ? defaultValue : (long)element;
		}

		public static Version ValueAsVersion(this XElement element, Version defaultValue = null)
		{
			if (element != null)
			{
				return Version.Parse(element.Value);
			}
			if (defaultValue != null)
			{
				return defaultValue;
			}
			throw new ArgumentNullException("element");
		}

		/// <summary>
		/// Writes as current element to the writer using IXmlSerializable.WriteXml conventions.
		/// </summary>
		/// <param name="xElement">The x element.</param>
		/// <param name="writer">The writer.</param>
		public static void WriteAsCurrentElementTo(this XElement xElement, XmlWriter writer)
		{
			foreach (XAttribute attribute in xElement.Attributes())
			{
				//todo: validate attribute prefixes are properly supported
				writer.WriteAttributeString(attribute.Name.LocalName, attribute.Name.NamespaceName, attribute.Value);
			}
			foreach (XElement child in xElement.Elements())
			{
				child.WriteTo(writer);
			}
		}
	}
}