using System;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Arguments event args class
	/// </summary>
	class ArgumentsEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// The argument set
		/// </summary>
		public Argument ArgumentSet { get; protected set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pArgumentSet">The argument set</param>
		public ArgumentsEventArgs(Argument pArgumentSet)
			: base()
		{
			this.ArgumentSet = pArgumentSet;
		}

		#endregion

	}

}
