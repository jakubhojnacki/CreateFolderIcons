using System;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file section event arguments class
	/// </summary>
	class ConfigurationFileSectionEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// Section
		/// </summary>
		public ConfigurationFileSection Section { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pSection">Section</param>
		public ConfigurationFileSectionEventArgs(ConfigurationFileSection pSection)
		{
			this.Section = pSection;
		}

		#endregion

	}

}
