using System;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Folder event args class
	/// </summary>
	public class FolderEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// The folder
		/// </summary>
		public Folder Folder { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pFolder">The folder</param>
		public FolderEventArgs(Folder pFolder)
			: base()
		{
			this.Folder = pFolder;
		}

		#endregion

	}

}
