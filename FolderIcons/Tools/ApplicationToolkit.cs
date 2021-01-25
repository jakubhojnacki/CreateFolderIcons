using System;
using System.IO;
using System.Reflection;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Application toolkit class
	/// </summary>
	static class ApplicationToolkit
	{

		#region General properties
	
		/// <summary>
		/// Returning application path
		/// </summary>
		/// <returns>The path</returns>
		public static string ApplicationPath
		{
			get 
			{
				string lCodeBase = Assembly.GetExecutingAssembly().Location;
				return Path.GetDirectoryName(lCodeBase);
			}
		}
		
		#endregion

		#region General methods

		/// <summary>
		/// Converting file path to application-related file path
		/// </summary>
		/// <param name="pFilePath">The file path</param>
		/// <returns>Application file path</returns>
		public static string ApplicationFilePath(string pFilePath)
		{
			return Path.Combine(ApplicationToolkit.ApplicationPath, pFilePath);
		}

		/// <summary>
		/// Verifying file path and adding - if necessary - application path at the front
		/// </summary>
		/// <param name="pFilePath">File path</param>
		/// <returns>File path verified</returns>
		public static string VerifyFilePath(string pFilePath)
		{
			if (Path.GetDirectoryName(pFilePath).Length == 0)
				pFilePath = ApplicationToolkit.ApplicationFilePath(pFilePath);
			return pFilePath;
		}

		#endregion

	}

}
