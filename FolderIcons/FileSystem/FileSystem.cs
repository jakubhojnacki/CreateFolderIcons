using System.Collections.Generic;
using System.Xml.Serialization;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// File system class
	/// </summary>
	public class FileSystem
	{

		#region General properties
		
		/// <summary>
		/// File path
		/// </summary>
		[XmlIgnore]
		public string FilePath { get; set; }
		
		/// <summary>
		/// A list of drives
		/// </summary>
		[XmlElement("Drive")]
		public List<Drive> Drives { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public FileSystem()
		{
			this.FilePath = string.Empty;
			this.Drives = new List<Drive>();
		}

		#endregion

		#region General methods

		/// <summary>
		/// Returning string representing the object
		/// </summary>
		/// <returns>The string</returns>
		public override string ToString()
		{
			int lFoldersCount = 0;
			foreach (Drive lDrive in this.Drives)
				lFoldersCount += lDrive.Folders.Count;
			return string.Format(Resources.FileSystem.FileSystemText, this.Drives.Count, lFoldersCount);
		}

		#endregion

	}

}
