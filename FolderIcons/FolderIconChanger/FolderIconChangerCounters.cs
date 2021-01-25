using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Folder icon changer counters class
	/// </summary>
	public class FolderIconChangerCounters
	{

		#region General properties

		/// <summary>
		/// Currently processing
		/// </summary>
		public string CurrentlyProcessing { get; set; }

		/// <summary>
		/// Drives processed counter
		/// </summary>
		public int DrivesProcessed { get; set; }

		/// <summary>
		/// Folders processed counter
		/// </summary>
		public int FoldersProcessed { get; set; }

		/// <summary>
		/// Icons already OK counter
		/// </summary>
		public int IconsAlreadyOk { get; set; }

		/// <summary>
		/// Icons changed counter
		/// </summary>
		public int IconsChanged { get; set; }

		/// <summary>
		/// Errors counter
		/// </summary>
		public int Errors { get; set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public FolderIconChangerCounters()
		{
			this.CurrentlyProcessing = string.Empty;
			this.DrivesProcessed = 0;
			this.FoldersProcessed = 0;
			this.IconsAlreadyOk = 0;
			this.IconsChanged = 0;
			this.Errors = 0;
		}

		#endregion

	}
}
