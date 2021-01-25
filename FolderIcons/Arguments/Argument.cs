namespace JHJ.FolderIcons
{
	
	/// <summary>
	/// Argument class
	/// </summary>
	public class Argument
	{

		#region General properties

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Boolean value
		/// </summary>
		public bool BooleanValue { get { return TypeConverter.ToBoolean(this.Value); } }

		/// <summary>
		/// Integer value
		/// </summary>
		public int IntegerValue { get { return TypeConverter.ToInteger(this.Value); } }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public Argument()
		{
			this.Name = string.Empty;
			this.Value = string.Empty;
		}

		/// <summary>
		/// Constructor with name
		/// </summary>
		/// <param name="pName">Name</param>
		public Argument(string pName)
			: this()
		{
			this.Name = pName;
		}

		/// <summary>
		/// Constructor with name and value
		/// </summary>
		/// <param name="pName">Name</param>
		/// <param name="pValue">Value</param>
		public Argument(string pName, string pValue)
			: this(pName)
		{
			this.Value = pValue;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Reading string argument to argument class
		/// </summary>
		/// <param name="pArgument">String argument</param>
		/// <returns>The class (or null if argument is empty)</returns>
		public static Argument Read(string pArgument)
		{
			Argument lArgument = null;
			string[] lArgumentParts = pArgument.Split(new char[] { '=' });
			if (lArgumentParts.Length > 0)
			{
				string lName = lArgumentParts[0].Trim().ToLower();
				string lValue = lArgumentParts.Length > 1 ? lArgumentParts[1] : string.Empty;
				lValue = Argument.RemoveQuotes(lValue);
				if ((lName != string.Empty) || (lValue != string.Empty))
					lArgument = new Argument(lName, lValue);
			}
			return lArgument;
		}

		/// <summary>
		/// Removing quotes from the string
		/// </summary>
		/// <param name="pString">The string</param>
		/// <returns>String without quotes</returns>
		private static string RemoveQuotes(string pString)
		{
			if (pString != string.Empty)
				if (pString[0] == '"')
					pString = pString.Substring(1);
			if (pString != string.Empty)
				if (pString[pString.Length - 1] == '"')
					pString = pString.Substring(0, pString.Length - 1);
			return pString;
		}

		/// <summary>
		/// Returning default value if the value is empty or null
		/// </summary>
		/// <param name="pValue">The value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Result</returns>
		public static string Default(string pValue, string pDefaultValue)
		{
			return !string.IsNullOrEmpty(pValue) ? pValue : pDefaultValue;
		}

		#endregion

	}

}
