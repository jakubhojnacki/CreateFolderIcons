using System;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Drive event args class
	/// </summary>
	public class DriveEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// The drive
		/// </summary>
		public Drive Drive { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pDrive">The drive</param>
		public DriveEventArgs(Drive pDrive)
			: base()
		{
			this.Drive = pDrive;
		}

		#endregion

	}

}
