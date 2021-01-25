using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Folder icon changer counters event args class
	/// </summary>
	public class FolderIconChangerCountersEventArgs : EventArgs
	{

		#region General properties

		/// <summary>
		/// Counters
		/// </summary>
		public FolderIconChangerCounters Counters { get; protected set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pCounters">Counters</param>
		public FolderIconChangerCountersEventArgs(FolderIconChangerCounters pCounters)
			: base()
		{
			this.Counters = pCounters;
		}

		#endregion

	}

}
