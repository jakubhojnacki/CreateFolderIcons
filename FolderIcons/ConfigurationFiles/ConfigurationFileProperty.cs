namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file property class
	/// </summary>
	class ConfigurationFileProperty
	{

		#region General properties

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public ConfigurationFileProperty()
		{
			this.Name = string.Empty;
			this.Value = string.Empty;
		}
		
		/// <summary>
		/// Constructor with name
		/// </summary>
		/// <param name="pName">The name</param>
		public ConfigurationFileProperty(string pName)
			: this()
		{
			this.Name = pName;
		}

		/// <summary>
		/// Constructor with name and value
		/// </summary>
		/// <param name="pName">The name</param>
		/// <param name="pValue">The value</param>
		public ConfigurationFileProperty(string pName, string pValue)
			: this(pName)
		{
			this.Value = pValue;
		}

		#endregion

	}

}
