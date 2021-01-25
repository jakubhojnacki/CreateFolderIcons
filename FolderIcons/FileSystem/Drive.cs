using System.Collections.Generic;
using System.Xml.Serialization;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Drive class
	/// </summary>
	public class Drive : IFolderIconChangerStateContent
	{

		#region General properties

		/// <summary>
		/// Name
		/// </summary>
		[XmlAttribute]
		public string Name { get; set; }

		/// <summary>
		/// A list of folders
		/// </summary>
		[XmlElement("Folder")]
		public List<Folder> Folders { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public Drive()
		{
			this.Name = string.Empty;
			this.Folders = new List<Folder>();
		}

		#endregion

		#region General methods

		/// <summary>
		/// Returning string representing the object
		/// </summary>
		/// <returns>The string</returns>
		public override string ToString()
		{
			return string.Format(Resources.FileSystem.DriveText, this.Name);
		}

		#endregion

	}

}
