using System;
using System.IO;
using System.Reflection;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Application toolkit class
	/// </summary>
	static class AssemblyToolkit
	{

		#region General properties

		/// <summary>
		/// Assembly title
		/// </summary>
		public static string AssemblyTitle 
		{ 
			get 
			{ 
				AssemblyTitleAttribute lAttribute = (AssemblyTitleAttribute)AssemblyToolkit.GetCustomAttribute<AssemblyTitleAttribute>();
				return lAttribute != null ? lAttribute.Title : string.Empty; 
			} 
		}

		/// <summary>
		/// Assembly version
		/// </summary>
		public static string AssemblyVersion 
		{ 
			get 
			{ 
				return Assembly.GetCallingAssembly().GetName().Version.ToString(); 
			} 
		}

		/// <summary>
		/// Assembly product
		/// </summary>
		public static string AssemblyProduct 
		{ 
			get 
			{
				AssemblyProductAttribute lAttribute = (AssemblyProductAttribute)AssemblyToolkit.GetCustomAttribute<AssemblyProductAttribute>();
				return lAttribute != null ? lAttribute.Product : string.Empty;
			} 
		}

		/// <summary>
		/// Assembly description
		/// </summary>
		public static string AssemblyDescription 
		{ 
			get 
			{
				AssemblyDescriptionAttribute lAttribute = (AssemblyDescriptionAttribute)AssemblyToolkit.GetCustomAttribute<AssemblyDescriptionAttribute>();
				return lAttribute != null ? lAttribute.Description : string.Empty;
			} 
		}

		/// <summary>
		/// Assembly copyright
		/// </summary>
		public static string AssemblyCopyright 
		{ 
			get 
			{
				AssemblyCopyrightAttribute lAttribute = (AssemblyCopyrightAttribute)AssemblyToolkit.GetCustomAttribute<AssemblyCopyrightAttribute>();
				return lAttribute != null ? lAttribute.Copyright : string.Empty;
			} 
		}

		/// <summary>
		/// Assembly company
		/// </summary>
		public static string AssemblyCompany 
		{ 
			get 
			{ 
				AssemblyCompanyAttribute lAttribute = (AssemblyCompanyAttribute)AssemblyToolkit.GetCustomAttribute<AssemblyCompanyAttribute>();
				return lAttribute != null ? lAttribute.Company : string.Empty;
			} 
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Returning custom attribute for given attribute type
		/// </summary>
		/// <typeparam name="AttributeType">Attribute type</typeparam>
		/// <returns>Attribute</returns>
		private static Attribute GetCustomAttribute<AttributeType>()
		{
			Attribute lAttribute = null;
			object[] lAttributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AttributeType), false);
			if (lAttributes.Length > 0)
				lAttribute = (Attribute)lAttributes[0];
			return lAttribute;
		}

		#endregion

	}

}
