using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Folder icon changer state class
	/// </summary>
	public class FolderIconChangerState
	{

		#region General properties

		/// <summary>
		/// Content type
		/// </summary>
		public FolderIconChangerStateContentType ContentType { get; protected set; }

		/// <summary>
		/// Content
		/// </summary>
		public IFolderIconChangerStateContent Content { get; protected set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pContentType">Content type</param>
		/// <param name="pContent">Content</param>
		public FolderIconChangerState(FolderIconChangerStateContentType pContentType, IFolderIconChangerStateContent pContent)
		{
			this.ContentType = pContentType;
			this.Content = pContent;
		}

		#endregion

	}

}
