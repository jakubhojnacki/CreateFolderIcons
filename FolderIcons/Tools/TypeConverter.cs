using System;
using System.Globalization;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Type converter class
	/// </summary>
	public static class TypeConverter
	{

		#region Boolean

		/// <summary>
		/// Converting to boolean
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <returns>Value converted</returns>
		public static bool ToBoolean(object pValue)
		{
			return TypeConverter.ToBoolean(pValue, false);
		}

		/// <summary>
		/// Converting to boolean with default value
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Value converted</returns>
		public static bool ToBoolean(object pValue, bool pDefaultValue)
		{
			bool lValue = false;
			if (!TypeConverter.TryToBoolean(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to boolean
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToBoolean(object pValue, out bool pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToBoolean(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToBoolean(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to boolean with conversion type
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pTypeConversion">Conversion type</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToBoolean(object pValue, TypeConversion pTypeConversion, out bool pOutputValue)
		{
			pOutputValue = false;
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is bool)
							{
								pOutputValue = (bool)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = bool.Parse(pValue.ToString());
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Integer

		/// <summary>
		/// Converting to integer
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <returns>Value converted</returns>
		public static int ToInteger(object pValue)
		{
			return TypeConverter.ToInteger(pValue, 0);
		}

		/// <summary>
		/// Converting to int with default value
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Value converted</returns>
		public static int ToInteger(object pValue, int pDefaultValue)
		{
			int lValue = 0;
			if (!TypeConverter.TryToInteger(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to integer
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToInteger(object pValue, out int pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToInteger(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToInteger(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to integer with conversion type
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pTypeConversion">Conversion type</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToInteger(object pValue, TypeConversion pTypeConversion, out int pOutputValue)
		{
			pOutputValue = 0;
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is int)
							{
								pOutputValue = (int)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = int.Parse(pValue.ToString(), CultureInfo.CurrentCulture);
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Double

		/// <summary>
		/// Converting to double
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <returns>Value converted</returns>
		public static double ToDouble(object pValue)
		{
			return TypeConverter.ToDouble(pValue, 0);
		}

		/// <summary>
		/// Converting to float with default value
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Value converted</returns>
		public static double ToDouble(object pValue, double pDefaultValue)
		{
			double lValue = 0;
			if (!TypeConverter.TryToDouble(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to float
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToDouble(object pValue, out double pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToDouble(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToDouble(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to float with conversion type
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pTypeConversion">Conversion type</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToDouble(object pValue, TypeConversion pTypeConversion, out double pOutputValue)
		{
			pOutputValue = 0;
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is double)
							{
								pOutputValue = (double)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = double.Parse(pValue.ToString(), CultureInfo.CurrentCulture);
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Decimal

		/// <summary>
		/// Converting to decimal
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <returns>Value converted</returns>
		public static decimal ToDecimal(object pValue)
		{
			return TypeConverter.ToDecimal(pValue, 0);
		}

		/// <summary>
		/// Converting to decimal with default value
		/// </summary>
		/// <param name="pValue">Value to convert</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Value converted</returns>
		public static decimal ToDecimal(object pValue, decimal pDefaultValue)
		{
			decimal lValue = 0;
			if (!TypeConverter.TryToDecimal(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to decimal
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToDecimal(object pValue, out decimal pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToDecimal(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToDecimal(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to decimal
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToDecimal(object pValue, TypeConversion pTypeConversion, out decimal pOutputValue)
		{
			pOutputValue = 0;
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is decimal)
							{
								pOutputValue = (decimal)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = decimal.Parse(pValue.ToString(), CultureInfo.CurrentCulture);
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region String

		/// <summary>
		/// Returning non-null string
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <returns>String</returns>
		public static string ToString(object pValue)
		{
			return TypeConverter.ToString(pValue, string.Empty);
		}

		/// <summary>
		/// Returning non-null string
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>String</returns>
		public static string ToString(object pValue, string pDefaultValue)
		{
			return pValue != null ? pValue.ToString() : pDefaultValue;
		}

		/// <summary>
		/// Trying to convert to string
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToString(object pValue, out string pOutputValue)
		{
			pOutputValue = string.Empty;
			if (pValue != null)
			{
				pOutputValue = pValue.ToString();
				return true;
			}
			else
				return false;
		}

		#endregion

		#region Datetime

		/// <summary>
		/// Converting to datetime
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <returns>Date time</returns>
		public static DateTime ToDateTime(object pValue)
		{
			return TypeConverter.ToDateTime(pValue, new DateTime());
		}

		/// <summary>
		/// Converting to datetime with default value
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Date time</returns>
		public static DateTime ToDateTime(object pValue, DateTime pDefaultValue)
		{
			DateTime lValue = new DateTime();
			if (!TypeConverter.TryToDateTime(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to datetime
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToDateTime(object pValue, out DateTime pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToDateTime(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToDateTime(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to datetime
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pTypeConversion">Conversion type</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToDateTime(object pValue, TypeConversion pTypeConversion, out DateTime pOutputValue)
		{
			pOutputValue = new DateTime();
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is DateTime)
							{
								pOutputValue = (DateTime)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = DateTime.Parse(pValue.ToString(), CultureInfo.CurrentCulture);
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Datetime with date parser

		/// <summary>
		/// Converting to datetime with format
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pFormat">The format</param>
		/// <returns>Date time</returns>
		public static DateTime ToDateTime(object pValue, string pFormat)
		{
			return TypeConverter.ToDateTime(pValue, new DateTime(), pFormat);
		}

		/// <summary>
		/// Converting to datetime with default value and format
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <param name="pFormat">The format</param>
		/// <returns>Date time</returns>
		public static DateTime ToDateTime(object pValue, DateTime pDefaultValue, string pFormat)
		{
			DateTime lValue = new DateTime();
			if (!TypeConverter.TryToDateTime(pValue, out lValue, pFormat))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to datetime with format
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <param name="pFormat">The format</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToDateTime(object pValue, out DateTime pOutputValue, string pFormat)
		{
			pOutputValue = new DateTime();
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					pOutputValue = DateTime.ParseExact(pValue.ToString(), pFormat, null);
				lSuccess = true;
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Nullable datetime

		/// <summary>
		/// Converting to nullable date time
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <returns>Nullable date time</returns>
		public static DateTime? ToNullableDateTime(object pValue)
		{
			return TypeConverter.ToNullableDateTime(pValue, null);
		}

		/// <summary>
		/// Converting to nullable datetime with default value
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Nullable date time</returns>
		public static DateTime? ToNullableDateTime(object pValue, DateTime? pDefaultValue)
		{
			DateTime? lValue = null;
			if (!TypeConverter.TryToNullableDateTime(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to nullable datetime
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToNullableDateTime(object pValue, out DateTime? pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToNullableDateTime(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToNullableDateTime(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to nullable datetime with conversion type
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pTypeConversion">Conversion type</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToNullableDateTime(object pValue, TypeConversion pTypeConversion, out DateTime? pOutputValue)
		{
			pOutputValue = null;
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is DateTime?)
							{
								pOutputValue = (DateTime?)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = new DateTime?(DateTime.Parse(pValue.ToString(), CultureInfo.CurrentCulture));
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Time span

		/// <summary>
		/// Converting to timespan
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <returns>Date time</returns>
		public static TimeSpan ToTimeSpan(object pValue)
		{
			return TypeConverter.ToTimeSpan(pValue, new TimeSpan());
		}

		/// <summary>
		/// Converting to timespan with default value
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Date time</returns>
		public static TimeSpan ToTimeSpan(object pValue, TimeSpan pDefaultValue)
		{
			TimeSpan lValue = new TimeSpan();
			if (!TypeConverter.TryToTimeSpan(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to timespan
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToTimeSpan(object pValue, out TimeSpan pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToTimeSpan(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToTimeSpan(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to timespan with conversion type
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pTypeConversion">Conversion type</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToTimeSpan(object pValue, TypeConversion pTypeConversion, out TimeSpan pOutputValue)
		{
			pOutputValue = new TimeSpan();
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is TimeSpan)
							{
								pOutputValue = (TimeSpan)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = TimeSpan.Parse(pValue.ToString(), CultureInfo.CurrentCulture);
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Nullable time span

		/// <summary>
		/// Converting to nullable time span
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <returns>Nullable time span</returns>
		public static TimeSpan? ToNullableTimeSpan(object pValue)
		{
			return TypeConverter.ToNullableTimeSpan(pValue, null);
		}

		/// <summary>
		/// Converting to nullable time span with default value
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Nullable time span</returns>
		public static TimeSpan? ToNullableTimeSpan(object pValue, TimeSpan? pDefaultValue)
		{
			TimeSpan? lValue = null;
			if (!TypeConverter.TryToNullableTimeSpan(pValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to nullable timespan
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToNullableTimeSpan(object pValue, out TimeSpan? pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToNullableTimeSpan(pValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToNullableTimeSpan(pValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to nullable timespan with conversion type
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pTypeConversion">Conversion type</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToNullableTimeSpan(object pValue, TypeConversion pTypeConversion, out TimeSpan? pOutputValue)
		{
			pOutputValue = null;
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is TimeSpan?)
							{
								pOutputValue = (TimeSpan?)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = new TimeSpan?(TimeSpan.Parse(pValue.ToString(), CultureInfo.CurrentCulture));
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

		#region Enum

		/// <summary>
		/// Converting to enum with default value
		/// </summary>
		/// <typeparam name="EnumType">Enum type</typeparam>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <returns>Converted value</returns>
		public static TEnumType ToEnum<TEnumType>(object pValue, TEnumType pDefaultValue)
		{
			TEnumType lValue = pDefaultValue;
			if (!TypeConverter.TryToEnum<TEnumType>(pValue, pDefaultValue, out lValue))
				lValue = pDefaultValue;
			return lValue;
		}

		/// <summary>
		/// Trying to convert to enum
		/// </summary>
		/// <param name="pValue">Value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToEnum<TEnumType>(object pValue, TEnumType pDefaultValue, out TEnumType pOutputValue)
		{
			bool lSuccess = TypeConverter.TryToEnum<TEnumType>(pValue, pDefaultValue, TypeConversion.Object, out pOutputValue);
			if (!lSuccess)
				lSuccess = TypeConverter.TryToEnum<TEnumType>(pValue, pDefaultValue, TypeConversion.String, out pOutputValue);
			return lSuccess;
		}

		/// <summary>
		/// Trying to convert to enum with conversion type
		/// </summary>
		/// <typeparam name="EnumType">Enum type</typeparam>
		/// <param name="pValue">Value</param>
		/// <param name="pDefaultValue">Default value</param>
		/// <param name="pOutputValue">Output value</param>
		/// <returns>Conversion successful / not</returns>
		public static bool TryToEnum<TEnumType>(object pValue, TEnumType pDefaultValue, TypeConversion pTypeConversion,
			out TEnumType pOutputValue)
		{
			pOutputValue = pDefaultValue;
			bool lSuccess = false;
			try
			{
				if (pValue != null)
					switch (pTypeConversion)
					{
						case TypeConversion.Object:
							if (pValue is TEnumType)
							{
								pOutputValue = (TEnumType)pValue;
								lSuccess = true;
							}
							break;
						case TypeConversion.String:
							{
								pOutputValue = (TEnumType)Enum.Parse(typeof(TEnumType), pValue.ToString(), true);
								lSuccess = true;
							}
							break;
					}
			}
			catch (Exception)
			{
			}
			return lSuccess;
		}

		#endregion

	}

}
