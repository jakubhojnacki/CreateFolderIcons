using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// XML serialiser class
	/// </summary>
	public static class XmlSerialiser<TClass>
	{

		#region XML reader / writer support

		/// <summary>
		/// Reading object from XML reader
		/// </summary>
		/// <param name="pXmlReader">XML reader</param>
		/// <returns>Object</returns>
		public static TClass ReadFromXmlReader(XmlReader pXmlReader)
		{
			XmlSerializer lXmlSerializer = new XmlSerializer(typeof(TClass));
			TClass lObject = (TClass)lXmlSerializer.Deserialize(pXmlReader);
			return lObject;
		}

		/// <summary>
		/// Writing object to XML writer
		/// </summary>
		/// <param name="pObject">Object</param>
		/// <param name="pXmlWriter">XML writer</param>
		public static void WriteToXmlWriter(TClass pObject, XmlWriter pXmlWriter)
		{
			XmlSerializer lXmlSerializer = new XmlSerializer(typeof(TClass));
			XmlSerializerNamespaces lXmlSerializerNamespaces = new XmlSerializerNamespaces();
			lXmlSerializerNamespaces.Add(string.Empty, string.Empty);
			lXmlSerializer.Serialize(pXmlWriter, pObject, lXmlSerializerNamespaces);
		}

		#endregion

		#region XML content support

		/// <summary>
		/// Reading object from XML content
		/// </summary>
		/// <param name="pXmlContent">XML content</param>
		/// <returns>Object</returns>
		public static TClass ReadFromXmlContent(string pXmlContent)
		{
			XmlReader lXmlReader = XmlReader.Create(new StringReader(pXmlContent));
			TClass lObject = XmlSerialiser<TClass>.ReadFromXmlReader(lXmlReader);
			lXmlReader.Close();
			return lObject;
		}

		/// <summary>
		/// Reading object from XML content (with encoding)
		/// </summary>
		/// <param name="pXmlContent">XML content</param>
		/// <param name="pEncoding">Encoding</param>
		/// <returns>Object</returns>
		public static TClass ReadFromXmlContent(string pXmlContent, Encoding pEncoding)
		{
			MemoryStream lMemoryStream = new MemoryStream(pEncoding.GetBytes(pXmlContent));
			XmlTextWriter lXmlTextWriter = new XmlTextWriter(lMemoryStream, pEncoding);
			XmlSerializer lXmlSerializer = new XmlSerializer(typeof(TClass));
			return (TClass)lXmlSerializer.Deserialize(lMemoryStream);
		}

		/// <summary>
		/// Writing object to XML content
		/// </summary>
		/// <param name="pObject">Object</param>
		/// <returns>XML content</returns>
		public static string WriteToXmlContent(TClass pObject)
		{
			StringWriter lStringWriter = new StringWriter(CultureInfo.CurrentCulture);
			XmlWriter lXmlWriter = new XmlTextWriter(lStringWriter);
			XmlSerialiser<TClass>.WriteToXmlWriter(pObject, lXmlWriter);
			lXmlWriter.Close();
			return lStringWriter.ToString();
		}

		/// <summary>
		/// Writing object to XML content (with encoding)
		/// </summary>
		/// <param name="pObject">Object</param>
		/// <param name="pEncoding">Encoding</param>
		/// <returns>XML content</returns>
		public static string WriteToXmlContent(TClass pObject, Encoding pEncoding)
		{
			MemoryStream lMemoryStream = new MemoryStream();
			XmlTextWriter lXmlTextWriter = new XmlTextWriter(lMemoryStream, pEncoding);
			XmlSerialiser<TClass>.WriteToXmlWriter(pObject, lXmlTextWriter);
			lXmlTextWriter.Flush();
			lMemoryStream.Seek(0, System.IO.SeekOrigin.Begin);
			StreamReader lStreamReader = new System.IO.StreamReader(lMemoryStream, pEncoding);
			string lXmlContent = lStreamReader.ReadToEnd();
			return lXmlContent;
		}

		#endregion

		#region XML file support

		/// <summary>
		/// Reading object from file
		/// </summary>
		/// <param name="pXmlFilePath">XML file path</param>
		/// <returns>Object</returns>
		public static TClass ReadFromXmlFile(string pXmlFilePath)
		{
			XmlTextReader lXmlTextReader = new XmlTextReader(pXmlFilePath);
			TClass lObject = XmlSerialiser<TClass>.ReadFromXmlReader(lXmlTextReader);
			lXmlTextReader.Close();
			return lObject;
		}

		/// <summary>
		/// Saving data
		/// </summary>
		/// <param name="pObject">Object</param>
		/// <param name="pXmlFilePath">XML file path</param>
		public static void WriteToXmlFile(TClass pObject, string pXmlFilePath)
		{
			XmlTextWriter lXmlTextWriter = new XmlTextWriter(pXmlFilePath, System.Text.Encoding.Default);
			XmlSerialiser<TClass>.WriteToXmlWriter(pObject, lXmlTextWriter);
			lXmlTextWriter.Close();
		}

		#endregion

	}

}
