using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace FrameWork
{
	public static class PersianDateExtensionMethods
	{
		private static CultureInfo? _Culture;

		public static CultureInfo GetPersianCulture()
		{
			if (_Culture == null)
			{
				_Culture = new CultureInfo("fa-IR");
				DateTimeFormatInfo formatInfo = _Culture.DateTimeFormat;

				// Set day names
				formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
				formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };

				// Set month names
				var monthNames = new[]
				{
					"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
					"مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", ""
				};
				formatInfo.AbbreviatedMonthNames =
				formatInfo.MonthNames =
				formatInfo.MonthGenitiveNames =
				formatInfo.AbbreviatedMonthGenitiveNames = monthNames;

				// Set designators
				formatInfo.AMDesignator = "ق.ظ";
				formatInfo.PMDesignator = "ب.ظ";

				// Set date patterns
				formatInfo.ShortDatePattern = "yyyy/MM/dd";
				formatInfo.LongDatePattern = "dddd، dd MMMM، yyyy"; // Fixed formatting (removed extra comma)

				// Set first day of week
				formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;

				// Set calendar
				Calendar cal = new PersianCalendar();

				// Reflection to set private 'calendar' fields
				FieldInfo? fieldInfo = _Culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
				if (fieldInfo != null)
					fieldInfo.SetValue(_Culture, cal);

				FieldInfo? info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
				if (info != null)
					info.SetValue(formatInfo, cal);

				// Number format settings
				_Culture.NumberFormat.NumberDecimalSeparator = "/";
				_Culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
				_Culture.NumberFormat.NumberNegativePattern = 0;
			}
			return _Culture;
		}

		public static string ToPersianString(this DateTime date, string format = "yyyy/MM/dd")
		{
			return date.ToString(format, GetPersianCulture());
		}
	}

	public static class EnumExtensions
	{
		public static string GetDisplayName(this Enum enumValue)
		{
			return enumValue.GetType()
						   .GetMember(enumValue.ToString())
						   .First()
						   .GetCustomAttribute<DisplayAttribute>()
						   ?.GetName() ?? enumValue.ToString();
		}
	}
}