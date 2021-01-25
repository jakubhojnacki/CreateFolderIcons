using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Icons class
	/// </summary>
	public class Icons
	{

		#region Constants

		/// <summary>
		/// Icon extension
		/// </summary>
		private const string IconExtension = "ico";

		#endregion

		#region General properties

		/// <summary>
		/// File path
		/// </summary>
		[XmlIgnore]
		public string FilePath { get; set; }

		/// <summary>
		/// Folder
		/// </summary>
		[XmlAttribute]
		public string Folder { get; set; }

		/// <summary>
		/// A list of items (icons)
		/// </summary>
		[XmlElement("Icon")]
		public List<Icon> Items { get; set; }

		/// <summary>
		/// Dictionary of icons
		/// </summary>
		[XmlIgnore]
		public Dictionary<string, Icon> Dictionary { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public Icons()
		{
			this.FilePath = string.Empty;
			this.Folder = string.Empty;
			this.Items = new List<Icon>();
			this.Dictionary = null;
		}

		#endregion

		#region General method

		/// <summary>
		/// Returning string representing the object
		/// </summary>
		/// <returns>The string</returns>
		public override string ToString()
		{
			return string.Format(Resources.Icons.IconsString, this.Items.Count);
		}

		/// <summary>
		/// Trying to find an icom
		/// </summary>
		/// <param name="pKeyword">Keyword</param>
		/// <returns>Icon found</returns>
		public string FindIcon(string pKeyword)
		{
			return string.Empty;
		}

		/// <summary>
		/// Building dictionary
		/// </summary>
		public void BuildDictionary()
		{
			this.Dictionary = new Dictionary<string,Icon>();
			foreach (Icon lIcon in this.Items)
			{
				string[] lKeywords = lIcon.Keywords.ToLower().Trim().Split(new char[] { ',', ';'});
				foreach (string lKeywordPart in lKeywords)
				{
					string lKeyword = lKeywordPart.Trim();
					if (!this.Dictionary.ContainsKey(lKeyword))
						this.Dictionary.Add(lKeyword, lIcon);
				}
			}
		}

		/// <summary>
		/// Returning full icon path
		/// </summary>
		/// <param name="pIcon">The icon</param>
		/// <returns>Icon path</returns>
		public string IconPath(Icon pIcon)
		{
			return Path.Combine(this.Folder, pIcon.Name) + "." + Icons.IconExtension;
		}

		/// <summary>
		/// Returning icon configuration file property value
		/// </summary>
		/// <param name="pIconPath">Icon path</param>
		/// <returns>The value</returns>
		public static string IconConfigurationFilePropertyValue(string pIconPath)
		{
			return pIconPath + ",0";
		}

		#endregion

	}

}
