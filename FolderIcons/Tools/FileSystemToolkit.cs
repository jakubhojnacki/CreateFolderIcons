using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// File system toolkit class (static)
	/// </summary>
	class FileSystemToolkit
	{

		#region General methods

		/// <summary>
		/// Returning path without drive
		/// </summary>
		/// <param name="pPath">The path</param>
		/// <returns>Path without drive</returns>
		public static string GetPathWithoutDrive(string pPath)
		{
			string lRootPath = Path.GetPathRoot(pPath);
			if (pPath.Substring(0, lRootPath.Length) == lRootPath)
				pPath = pPath.Substring(lRootPath.Length);
			return pPath;
		}

		/// <summary>
		/// Returnign path identifier
		/// </summary>
		/// <param name="pPath">The path</param>
		/// <returns>Path identifier</returns>
		public static string GetPathIdentifier(string pPath)
		{
			StringBuilder lStringBuilder = new StringBuilder();
			foreach (char lChar in pPath.ToLower().Trim())
				if (((lChar >= 'a') && (lChar <= 'z')) || ((lChar >= '0') && (lChar <= '9')) || (lChar == '\\'))
					lStringBuilder.Append(lChar);
			return lStringBuilder.ToString();
		}

		#endregion

	}

}
