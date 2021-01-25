using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Folder icons exception class
	/// </summary>
	public class FolderIconsException : Exception, IFolderIconChangerStateContent
	{

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pMessage">Message</param>
		public FolderIconsException(string pMessage)
			: base(pMessage)
		{
		}

		/// <summary>
		/// Constructor with exception
		/// </summary>
		/// <param name="pException">The exception</param>
		public FolderIconsException(Exception pException)
			: this(ErrorToolkit.CreateErrorMessage(pException))
		{
		}

		#endregion

	}

}
