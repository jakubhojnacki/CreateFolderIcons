using System;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file iterator class
	/// </summary>
	class ConfigurationFileIterator
	{

		#region General properties

		/// <summary>
		/// Configuration file
		/// </summary>
		public ConfigurationFile ConfigurationFile { get; protected set; }

		#endregion

		#region Events

		/// <summary>
		/// Start event
		/// </summary>
		public event EventHandler<ConfigurationFileEventArgs> Start;

		/// <summary>
		/// Section event
		/// </summary>
		public event EventHandler<ConfigurationFileSectionEventArgs> Section;

		/// <summary>
		/// Property event
		/// </summary>
		public event EventHandler<ConfigurationFilePropertyEventArgs> Property;

		/// <summary>
		/// Finish event
		/// </summary>
		public event EventHandler<ConfigurationFileEventArgs> Finish;

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pConfigurationFile">Configuration file</param>
		public ConfigurationFileIterator(ConfigurationFile pConfigurationFile)
		{
			this.ConfigurationFile = pConfigurationFile;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Running the iterator
		/// </summary>
		public void Run()
		{
			if (this.ConfigurationFile == null)
				throw new Exception(Resources.ConfigurationFiles.ConfigurationFileNotProvided);

			this.OnStart();
			foreach (ConfigurationFileSection lSection in this.ConfigurationFile.Sections)
			{
				this.OnSection(lSection);
				foreach (ConfigurationFileProperty lProperty in lSection.Properties)
					this.OnProperty(lProperty);
			}
			this.OnFinish();
		}

		#endregion

		#region Event triggers

		/// <summary>
		/// On start
		/// </summary>
		protected void OnStart()
		{
			if (this.Start != null)
				this.Start(this, new ConfigurationFileEventArgs(this.ConfigurationFile));
		}

		/// <summary>
		/// On section
		/// </summary>
		/// <param name="pSection">Section</param>
		protected void OnSection(ConfigurationFileSection pSection)
		{
			if (this.Section != null)
				this.Section(this, new ConfigurationFileSectionEventArgs(pSection));
		}

		/// <summary>
		/// On property
		/// </summary>
		/// <param name="pProperty">Property</param>
		protected void OnProperty(ConfigurationFileProperty pProperty)
		{
			if (this.Property != null)
				this.Property(this, new ConfigurationFilePropertyEventArgs(pProperty));
		}

		/// <summary>
		/// On finish
		/// </summary>
		protected void OnFinish()
		{
			if (this.Finish != null)
				this.Finish(this, new ConfigurationFileEventArgs(this.ConfigurationFile));
		}

		#endregion

	}

}
