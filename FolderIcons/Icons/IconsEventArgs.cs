using System;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Icons event args class
	/// </summary>
	public class IconsEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// The icon set
		/// </summary>
		public Icons IconSet { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pIconSet">The icon set</param>
		public IconsEventArgs(Icons pIconSet)
			: base()
		{
			this.IconSet = pIconSet;
		}

		#endregion

	}

}
