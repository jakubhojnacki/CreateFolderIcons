using System.Xml.Serialization;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Folder class
	/// </summary>
	public class Folder : IFolderIconChangerStateContent
	{

		#region General properties

		/// <summary>
		/// Name
		/// </summary>
		[XmlAttribute]
		public string Name { get; set; }

		/// <summary>
		/// Icon
		/// </summary>
		[XmlAttribute]
		public string Icon { get; set; }

		/// <summary>
		/// Sub folder processing
		/// </summary>
		[XmlAttribute]
		public SubFolderProcessing SubFolderProcessing { get; set; }

		/// <summary>
		/// Sub folder depth
		/// </summary>
		[XmlAttribute]
		public int SubFolderDepth { get; set; }

		#endregion

		#region Internal properties

		/// <summary>
		/// Selected icon
		/// </summary>
		[XmlIgnore]
		protected string SelectedIcon { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public Folder()
		{
			this.Name = string.Empty;
			this.Icon = string.Empty;
			this.SubFolderProcessing = SubFolderProcessing.No;
			this.SubFolderDepth = 0;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Returning string representing the object
		/// </summary>
		/// <returns>The string</returns>
		public override string ToString()
		{
			DescriptionBuilder lDescriptionBuilder = new DescriptionBuilder(this.Name);
			lDescriptionBuilder.AddTextAttribute(Resources.FileSystem.FolderIcon, this.Icon, true);
			if (this.SubFolderProcessing != FolderIcons.SubFolderProcessing.Null)
				lDescriptionBuilder.AddTextAttribute(Resources.FileSystem.FolderSubFolderProcessing, this.SubFolderProcessing.ToString(), false);
			lDescriptionBuilder.AddIntegerAttribute(Resources.FileSystem.FolderSubFolderDepth, this.SubFolderDepth, true);
			return lDescriptionBuilder.Description;
		}

		/// <summary>
		/// Setting selected icon
		/// </summary>
		/// <param name="pParentFolderIcon">Parent folder icon</param>
		/// <param name="pIconSet">Icon set</param>
		public void SetSelectedIcon(string pParentFolderIcon, Icons pIconSet)
		{
			this.SelectedIcon = string.Empty;
			if (this.Icon.Length > 0)
				this.SelectedIcon = this.Icon;
			if (this.SelectedIcon.Length == 0)
				this.SelectedIcon = pParentFolderIcon;
			if (this.SelectedIcon.Length == 0)
				this.SelectedIcon = pIconSet.FindIcon(this.Name);
		}

		#endregion

	}

}
