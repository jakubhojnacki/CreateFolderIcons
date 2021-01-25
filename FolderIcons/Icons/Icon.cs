using System.Xml.Serialization;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Icon class
	/// </summary>	
	public class Icon
	{

		#region General properties

		/// <summary>
		/// Name
		/// </summary>
		[XmlAttribute]
		public string Name { get; set; }

		/// <summary>
		/// A list of keywords
		/// </summary>
		[XmlAttribute]
		public string Keywords { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public Icon()
		{
			this.Name = string.Empty;
			this.Keywords = string.Empty;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Returning string representing the object
		/// </summary>
		/// <returns>The string</returns>
		public override string ToString()
		{
			return string.Format(Resources.Icons.IconString, this.Name, this.Keywords);
		}

		#endregion

	}

}
