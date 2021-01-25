using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Description builder class
	/// </summary>
	public class DescriptionBuilder
	{

		#region Genearal properties

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; protected set; }

		/// <summary>
		/// Attributes (counter)
		/// </summary>
		protected int Attributes { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="pName"></param>
		public DescriptionBuilder(string pName)
		{
			this.Description = pName;
			this.Attributes = 0;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Adding text attribute
		/// </summary>
		/// <param name="pName">Name</param>
		/// <param name="pValue">Value</param>
		/// <param name="pIgnoreIfEmpty">Ignore if empty flag</param>
		public void AddTextAttribute(string pName, string pValue, bool pIgnoreIfEmpty)
		{
			if ((string.IsNullOrEmpty(pValue)) && (pIgnoreIfEmpty))
				return;
			this.BeginAttribute();
			this.AddAttribute(pName, pValue);
			this.EndAttribute();
		}

		/// <summary>
		/// Adding integer attribute
		/// </summary>
		/// <param name="pName">Name</param>
		/// <param name="pValue">Value</param>
		/// <param name="pIgnoreIfEmpty">Ignore if empty flag</param>
		public void AddIntegerAttribute(string pName, int pValue, bool pIgnoreIfEmpty)
		{
			if ((pValue == 0) && (pIgnoreIfEmpty))
				return;
			this.BeginAttribute();
			this.AddAttribute(pName, pValue);
			this.EndAttribute();
		}

		/// <summary>
		/// Adding boolean attribute
		/// </summary>
		/// <param name="pName">Name</param>
		/// <param name="pValue">Value</param>
		/// <param name="pIgnoreIfEmpty">Ignore if empty flag</param>
		public void AddBooleanAttribute(string pName, bool pValue, bool pIgnoreIfEmpty)
		{
			if ((!pValue) && (pIgnoreIfEmpty))
				return;
			this.BeginAttribute();
			this.AddAttribute(pName, pValue);
			this.EndAttribute();
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Beginning an attribute
		/// </summary>
		protected void BeginAttribute()
		{
			if (this.Description.EndsWith(")"))
			{
				this.Description = this.Description.Substring(0, this.Description.Length - 1);
				if (this.Attributes > 0)
					this.Description += "; ";
			}
			else
				this.Description += " (";
		}

		/// <summary>
		/// Adding an attribute
		/// </summary>
		/// <param name="pName">Name</param>
		/// <param name="pValue">Value</param>
		protected void AddAttribute(string pName, object pValue)
		{
			this.Description += string.Format("{0}: {1}", pName, TypeConverter.ToString(pValue));
		}

		/// <summary>
		/// Ending an attribute
		/// </summary>
		protected void EndAttribute()
		{
			this.Description += ")";
			this.Attributes ++;
		}

		#endregion

	}

}
