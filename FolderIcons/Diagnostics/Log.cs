using System.Text;

namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Log class
	/// </summary>
	public class Log
	{

		#region General properties

		/// <summary>
		/// Staring builder
		/// </summary>
		protected StringBuilder StringBuilder { get; set; }

		/// <summary>
		/// Log content
		/// </summary>
		public string Content { get { return this.StringBuilder.ToString(); } }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public Log()
		{
			this.StringBuilder = new StringBuilder();
		}

		#endregion

		#region General methods

		/// <summary>
		/// Clearing the log
		/// </summary>
		public void Clear()
		{
			this.StringBuilder.Clear();
		}

		/// <summary>
		/// Adding to the log
		/// </summary>
		public void Add(string pMessage)
		{
			this.StringBuilder.Append(pMessage + "\r\n");
		}

		#endregion

	}

}
