using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Error event args class
	/// </summary>
	public class ErrorEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// Error message
		/// </summary>
		public string ErrorMessage { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pErrorMessage">Error message</param>
		public ErrorEventArgs(string pErrorMessage)
			: base()
		{
			this.ErrorMessage = pErrorMessage;
		}

		/// <summary>
		/// Constructor with exception
		/// </summary>
		/// <param name="pException"></param>
		public ErrorEventArgs(Exception pException)
			: this(ErrorToolkit.CreateErrorMessage(pException))
		{
		}

		#endregion

	}

}
