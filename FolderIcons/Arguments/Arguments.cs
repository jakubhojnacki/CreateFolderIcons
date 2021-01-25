using System.Collections.Generic;
using System.Linq;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Arguments class
	/// </summary>
	public class Arguments
	{

		#region General properties

		/// <summary>
		/// List of items (arguments)
		/// </summary>
		public List<Argument> Items { get; protected set; }

		// Name indexer
		public Argument this[string pName]
		{
			get
			{
				pName = pName.Trim().ToLower();
				Argument lArgument = this.Items.FirstOrDefault<Argument>(pArgument => pArgument.Name == pName);
				if (lArgument == null)
					lArgument = new Argument(pName);
				return lArgument;
			}
		}

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public Arguments()
		{
			this.Items = new List<Argument>();
		}

		#endregion
		
		#region General methods
		
		/// <summary>
		/// Reading arguments into a class
		/// </summary>
		/// <param name="pArguments">Array of arguments</param>
		/// <returns>The class</returns>
		public static Arguments Read(string[] pArguments)
		{
			Arguments lArguments = new Arguments();
			foreach (string lArgumentText in pArguments)
			{
				Argument lArgument = Argument.Read(lArgumentText);
				if (lArgument != null)
					lArguments.Items.Add(lArgument);
			}
			return lArguments;
		}

		/// <summary>
		/// Returning if argument is defined
		/// </summary>
		/// <param name="pName">Argument name</param>
		/// <returns>If argument is defined</returns>
		public bool Defined(string pName)
		{
			pName = pName.Trim().ToLower();
			return (this.Items.FirstOrDefault<Argument>(pArgument => pArgument.Name == pName) != null);
		}

		#endregion

	}

}
