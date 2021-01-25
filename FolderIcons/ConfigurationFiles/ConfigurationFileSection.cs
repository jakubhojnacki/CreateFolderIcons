using System.Collections.Generic;
using System.Linq;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file section class
	/// </summary>
	class ConfigurationFileSection
	{

		#region General properties

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A list of properties
		/// </summary>
		public List<ConfigurationFileProperty> Properties { get; protected set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public ConfigurationFileSection()
		{
			this.Name = string.Empty;
			this.Properties = new List<ConfigurationFileProperty>();
		}

		/// <summary>
		/// Constructor with name
		/// </summary>
		/// <param name="pName">The name</param>
		public ConfigurationFileSection(string pName)
			: this()
		{
			this.Name = pName;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Returning if the section has property defined
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		/// <returns>Result</returns>
		public bool HasProperty(string pPropertyName)
		{
			return (this.FindProperty(pPropertyName) != null);
		}

		/// <summary>
		/// Getting property
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		/// <returns>Property value</returns>
		public string GetProperty(string pPropertyName)
		{
			ConfigurationFileProperty lProperty = this.FindProperty(pPropertyName);
			if (lProperty != null)
				return lProperty.Value;
			else
				return string.Empty;
		}

		/// <summary>
		/// Setting property
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		/// <param name="pPropertyValue">Property value</param>
		public void SetProperty(string pPropertyName, string pPropertyValue)
		{
			ConfigurationFileProperty lProperty = this.FindProperty(pPropertyName);
			if (lProperty != null)
				lProperty.Value = pPropertyValue;
			else
				this.Properties.Add(new ConfigurationFileProperty(pPropertyName, pPropertyValue));
		}

		#endregion

		#region Internal methods
		
		/// <summary>
		/// Trying to find a property
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		/// <returns>The property</returns>
		protected ConfigurationFileProperty FindProperty(string pPropertyName)
		{
			pPropertyName = pPropertyName.Trim();
			return this.Properties.FirstOrDefault<ConfigurationFileProperty>(pPropert => pPropert.Name == pPropertyName);
		}

		#endregion

	}

}
