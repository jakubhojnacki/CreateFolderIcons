using System;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file property event arguments class
	/// </summary>
	class ConfigurationFilePropertyEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// Property
		/// </summary>
		public ConfigurationFileProperty Property { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pProperty">Property</param>
		public ConfigurationFilePropertyEventArgs(ConfigurationFileProperty pProperty)
		{
			this.Property = pProperty;
		}

		#endregion

	}

}
