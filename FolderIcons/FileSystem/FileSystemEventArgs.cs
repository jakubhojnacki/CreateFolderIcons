using System;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// File system event args class
	/// </summary>
	public class FileSystemEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// The file system
		/// </summary>
		public FileSystem FileSystem { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pFileSystem">The file system</param>
		public FileSystemEventArgs(FileSystem pFileSystem)
			: base()
		{
			this.FileSystem = pFileSystem;
		}

		#endregion

	}

}
