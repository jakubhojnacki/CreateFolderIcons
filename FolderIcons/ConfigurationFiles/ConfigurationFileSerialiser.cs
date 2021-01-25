using System.IO;
using System.Text.RegularExpressions;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file serialiser class (static)
	/// </summary>
	class ConfigurationFileSerialiser
	{

		#region Constants

		/// <summary>
		/// Section regular expression pattern
		/// </summary>
		protected const string SectionRegexPattern = @"\[(?<Name>.+)\]";

		/// <summary>
		/// Property regular expression pattern
		/// </summary>
		protected const string PropertyRegexPattern = @"(?<Name>.+)=(?<Value>.+)";

		#endregion

		#region General properties

		/// <summary>
		/// Configuration file
		/// </summary>
		public ConfigurationFile ConfigurationFile { get; set; }

		/// <summary>
		/// Section regex
		/// </summary>
		protected Regex SectionRegex { get; set; }
		
		/// <summary>
		/// Property regex
		/// </summary>
		protected Regex PropertyRegex { get; set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public ConfigurationFileSerialiser()
		{
			this.ConfigurationFile = null;
			this.SectionRegex = null;
			this.PropertyRegex = null;
		}

		#endregion

		#region General methods
		
		/// <summary>
		/// Reading configuration file
		/// </summary>
		/// <param name="pPath">Path to configuration file</param>
		/// <returns>Configuration file read</returns>
		public ConfigurationFile Read(string pPath)
		{
			this.ConfigurationFile = new ConfigurationFile();
			StreamReader lStreamReader = new StreamReader(pPath);
			this.SectionRegex = new Regex(ConfigurationFileSerialiser.SectionRegexPattern);
			this.PropertyRegex = new Regex(ConfigurationFileSerialiser.PropertyRegexPattern);
			try
			{
				string lLine = string.Empty;
				while(!lStreamReader.EndOfStream)
				{
					lLine = lStreamReader.ReadLine().Trim();
					if (lLine.Length > 0)
						if (!this.ProcessSection(lLine))
							if (!this.ProcessProperty(lLine))
								this.ProcessNameOnlyProperty(lLine);
				}
			}
			finally
			{ 
				lStreamReader.Close();
				lStreamReader.Dispose();
				this.SectionRegex = null;
				this.PropertyRegex = null;
			}
			return this.ConfigurationFile;
		}

		/// <summary>
		/// Writing configuration file
		/// </summary>
		/// <param name="pConfigurationFile">The file</param>
		/// <param name="pPath">Path to write to</param>
		public void Write(ConfigurationFile pConfigurationFile, string pPath)
		{
			this.ConfigurationFile = pConfigurationFile;
			StreamWriter lStreamWriter = new StreamWriter(pPath);
			try
			{
				foreach (ConfigurationFileSection lSection in this.ConfigurationFile.Sections)
				{
					if (lSection.Name.Length > 0)
						lStreamWriter.WriteLine(string.Format("[{0}]", lSection.Name));
					foreach (ConfigurationFileProperty lProperty in lSection.Properties)
						if (lProperty.Name.Length > 0)
							if (lProperty.Value.Length > 0)
								lStreamWriter.WriteLine(string.Format("{0}={1}", lProperty.Name, lProperty.Value));
							else
								lStreamWriter.WriteLine(lProperty.Name);
				}	
			}
			finally
			{
				lStreamWriter.Close();
				lStreamWriter.Dispose();
			}
		}

		#endregion

		#region Tool methods

		/// <summary>
		/// Trying to process the current line as a section
		/// </summary>
		/// <param name="pLine">The line</param>
		/// <returns>true = line processed as a section, false = line is not a section</returns>
		protected bool ProcessSection(string pLine)
		{
			Match lMatch = this.SectionRegex.Match(pLine);
			if (lMatch.Success)
			{
				string lSectionName = lMatch.Groups["Name"].Value;
				this.ConfigurationFile.SetSection(lSectionName);
			}
			return lMatch.Success;
		}

		/// <summary>
		/// Trying to process the current line as a property
		/// </summary>
		/// <param name="pLine">The line</param>
		/// <returns>true = line processed as a property, false = line is not a property</returns>
		protected bool ProcessProperty(string pLine)
		{
			Match lMatch = this.PropertyRegex.Match(pLine);
			if (lMatch.Success)
			{
				string lPropertyName = lMatch.Groups["Name"].Value;
				string lPropertyValue = lMatch.Groups["Value"].Value;
				this.ConfigurationFile.SetProperty(lPropertyName, lPropertyValue);
			}
			return lMatch.Success;
		}

		/// <summary>
		/// Processing the current line as a name-only property
		/// </summary>
		/// <param name="pLine">The line</param>
		protected void ProcessNameOnlyProperty(string pLine)
		{
			this.ConfigurationFile.SetProperty(pLine);
		}

		#endregion

	}

}
