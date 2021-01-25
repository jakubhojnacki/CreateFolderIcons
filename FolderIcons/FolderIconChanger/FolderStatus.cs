using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Folder status class
	/// </summary>
	public class FolderStatus : IFolderIconChangerStateContent
	{

		#region General properties

		/// <summary>
		/// Process flag
		/// </summary>
		public bool Process { get; set; }

		/// <summary>
		/// Path
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Icon
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// Icon path
		/// </summary>
		public string IconPath { get; set; }

		/// <summary>
		/// Sub folder processing
		/// </summary>
		public SubFolderProcessing SubFolderProcessing { get; set; }

		/// <summary>
		/// Sub folder depth
		/// </summary>
		public int SubFolderDepth { get; set; }

		/// <summary>
		/// Returning if folder icon tells to process sub-folders
		/// </summary>
		public bool ProcessSubFolders { get { return ((this.SubFolderProcessing == SubFolderProcessing.Icons) || (this.SubFolderProcessing == SubFolderProcessing.SameIcon)); } }

		/// <summary>
		/// Icon already OK flag
		/// </summary>
		public bool IconAlreadyOk { get; set; }

		/// <summary>
		/// Icon saved flag
		/// </summary>
		public bool IconChanged { get; set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public FolderStatus()
		{
			this.Process = false;
			this.Path = string.Empty;
			this.Icon = string.Empty;
			this.IconPath = string.Empty;
			this.SubFolderProcessing = FolderIcons.SubFolderProcessing.Null;
			this.SubFolderDepth = 0;
			this.IconAlreadyOk = false;
			this.IconChanged = false;
		}

		/// <summary>
		/// Constructor with path
		/// </summary>
		/// <param name="pPath">Path</param>
		public FolderStatus(string pPath)
			: this()
		{
			this.Path = pPath;
		}

		#endregion

	}

}
