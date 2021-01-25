using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Error toolkit class (static)
	/// </summary>
	class ErrorToolkit
	{

		#region General methods

		/// <summary>
		/// Creating error message
		/// </summary>
		/// <param name="pException">Exception to take error message from</param>
		/// <returns>The error message</returns>
		public static string CreateErrorMessage(Exception pException)
		{
			string lErrorMessage = string.Empty;
			Exception lException = pException;
			while (lException != null)
			{
				if (lErrorMessage.Length > 0)
					lErrorMessage += "; ";
				lErrorMessage += lException.Message;
				lException = lException.InnerException;
			}
			return lErrorMessage;
		}

		/// <summary>
		/// Showing error message
		/// </summary>
		/// <param name="pException">Exception to take error message from</param>
		public static void ShowErrorMessage(Exception pException)
		{
			MessageBox.Show(ErrorToolkit.CreateErrorMessage(pException), string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		#endregion

	}

}
