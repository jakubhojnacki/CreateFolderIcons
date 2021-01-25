using System;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Folder status event args class
	/// </summary>
	public class FolderStatusEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// The folder icon
		/// </summary>
		public FolderStatus FolderStatus { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pFolderStatus">The folder status</param>
		public FolderStatusEventArgs(FolderStatus pFolderStatus)
			: base()
		{
			this.FolderStatus = pFolderStatus;
		}

		#endregion

	}

}
