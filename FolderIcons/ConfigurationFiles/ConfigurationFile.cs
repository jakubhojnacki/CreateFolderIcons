using System.Linq;
using System.Collections.Generic;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Configuration file class
	/// </summary>
  class ConfigurationFile
  {

		#region General properties

		/// <summary>
		/// A list of sections
		/// </summary>
		public List<ConfigurationFileSection> Sections { get; protected set; }

		/// <summary>
		/// Current section
		/// </summary>
		protected ConfigurationFileSection CurrentSection { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public ConfigurationFile()
		{
			this.Sections = new List<ConfigurationFileSection>();
			this.CurrentSection = null;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Setting a section
		/// </summary>
		/// <param name="pSectionName">Section name</param>
		public void SetSection(string pSectionName)
		{
			this.CurrentSection = this.Sections.FirstOrDefault<ConfigurationFileSection>(pSection => pSection.Name == pSectionName);
			if (this.CurrentSection == null)
			{
				this.CurrentSection = new ConfigurationFileSection(pSectionName);
				this.Sections.Add(this.CurrentSection);
			}
		}

		/// <summary>
		/// Returning if the current section has the property defined
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		/// <returns>Answer</returns>
		public bool HasProperty(string pPropertyName)
		{
			this.VerifySection();
			return this.CurrentSection.HasProperty(pPropertyName);
		}

		/// <summary>
		/// Getting property
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		/// <returns>Property value</returns>
		public string GetProperty(string pPropertyName)
		{
			this.VerifySection();
			return this.CurrentSection.GetProperty(pPropertyName);
		}

		/// <summary>
		/// Setting a property
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		/// <param name="pPropertyValue">Property value</param>
		public void SetProperty(string pPropertyName, string pPropertyValue)
		{
			this.VerifySection();
			this.CurrentSection.SetProperty(pPropertyName, pPropertyValue);
		}

		/// <summary>
		/// Setting name-only property
		/// </summary>
		/// <param name="pPropertyName">Property name</param>
		public void SetProperty(string pPropertyName)
		{
			this.SetProperty(pPropertyName, string.Empty);
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Verifying section (if not found - creating one)
		/// </summary>
		protected void VerifySection()
		{
			if (this.CurrentSection == null)
			{
				this.CurrentSection = new ConfigurationFileSection();
				this.Sections.Add(this.CurrentSection);
			}
		}

		#endregion

  }

}
