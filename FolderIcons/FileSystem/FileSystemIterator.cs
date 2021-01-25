using System;
using System.IO;
using System.Linq;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// File system iterator class
	/// </summary>
	class FileSystemIterator
	{

		#region General properties

		/// <summary>
		/// File system
		/// </summary>
		protected FileSystem FileSystem { get; set; }

		/// <summary>
		/// Drive
		/// </summary>
		protected Drive Drive { get; set; }

		#endregion

		#region Events
		
		/// <summary>
		/// Folder event
		/// </summary>
		public event EventHandler<FolderEventArgs> OnFolder;
		public void TriggerOnFolder(Folder pFolder)
		{
			if (this.OnFolder != null)
				this.OnFolder(this, new FolderEventArgs(pFolder));
		}

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public FileSystemIterator(FolderIconChanger pFolderIconChanger)
		{
			this.FileSystem = pFolderIconChanger.FileSystem;
			this.Drive = null;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Running the iterator
		/// </summary>
		public void Run()
		{
			foreach (Drive lDrive in this.FileSystem.Drives)
			{
				this.Drive = lDrive;
				DriveInfo lDriveInfo = new DriveInfo(this.Drive.Name);
				this.ProcessDirectory(lDriveInfo.RootDirectory, string.Empty);
			}
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Processing a directory
		/// </summary>
		/// <param name="pDirectoryInfo">The directory info</param>
		/// <param name="pParentIconName">Parent icon name</param>
		protected void ProcessDirectory(DirectoryInfo pDirectoryInfo, string pParentIconName)
		{
			if (pDirectoryInfo == null)
				return;
			string lDirectoryName = pDirectoryInfo.Name;
			Folder lFolder = this.Drive.Folders.FirstOrDefault<Folder>(pFolder => pFolder.Name == lDirectoryName);
			if (lFolder == null)
				return;
			


			this.TriggerOnFolder(lFolder);
			foreach (DirectoryInfo lDirectoryInfo in pDirectoryInfo.GetDirectories())
			{

			}			
		}

		#endregion

		#region Tool methods
		


		#endregion

	}

}
