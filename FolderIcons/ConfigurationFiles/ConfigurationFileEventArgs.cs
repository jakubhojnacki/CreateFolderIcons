using System;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file event arguments class
	/// </summary>
	class ConfigurationFileEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// File
		/// </summary>
		public ConfigurationFile File { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pFile">File</param>
		public ConfigurationFileEventArgs(ConfigurationFile pFile)
		{
			this.File = pFile;
		}

		#endregion

	}

}
