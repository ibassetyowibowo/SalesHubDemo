using System.Globalization;
using System.Text.RegularExpressions;

namespace KSO_SalesHub.Utilities
{
	public static class StringExtensions
	{
		public static string Terbilang(this decimal d)
		{
			bool minus = false;
			if (d < 0)
			{
				d = d * -1;
				minus = true;
			}
			string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
			string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
			string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
			string[] ribuan = new string[6] { "", "ribu", "juta", "milyar", "triliyun", "kuadriliun" };


			string strHasil = "";
			Decimal frac = d - Decimal.Truncate(d);

			int xDigit = 0;
			int xPosisi = 0;

			string strTemp = Decimal.Truncate(d).ToString();
			for (int i = strTemp.Length; i > 0; i--)
			{
				string tmpx = "";
				xDigit = Convert.ToInt32(strTemp.Substring(i - 1, 1));
				xPosisi = (strTemp.Length - i) + 1;
				switch (xPosisi % 3)
				{
					case 1:
						bool allNull = false;
						if (i == 1)
							tmpx = satuan[xDigit] + " ";
						else if (strTemp.Substring(i - 2, 1) == "1")
							tmpx = belasan[xDigit] + " ";
						else if (xDigit > 0)
							tmpx = satuan[xDigit] + " ";
						else
						{
							allNull = true;
							if (i > 1)
								if (strTemp.Substring(i - 2, 1) != "0")
									allNull = false;
							if (i > 2)
								if (strTemp.Substring(i - 3, 1) != "0")
									allNull = false;
							tmpx = "";
						}

						if ((!allNull) && (xPosisi > 1))
							if ((strTemp.Length == 4) && (strTemp.Substring(0, 1) == "1"))
								tmpx = "se" + ribuan[int.Parse(Decimal.Round((decimal)(xPosisi / 3)).ToString())] + " ";
							else
								tmpx = tmpx + ribuan[int.Parse(Decimal.Round((decimal)(xPosisi / 3)).ToString())] + " ";
						strHasil = tmpx + strHasil;
						break;
					case 2:
						if (xDigit > 0)
							strHasil = puluhan[xDigit] + " " + strHasil;
						break;
					case 0:
						if (xDigit > 0)
							if (xDigit == 1)
								strHasil = "seratus " + strHasil;
							else
								strHasil = satuan[xDigit] + " ratus " + strHasil;
						break;
				}
			}
			strHasil = strHasil.Trim().ToLower();
			if (strHasil.Length > 0)
			{
				strHasil = strHasil.Substring(0, 1).ToUpper() +
				  strHasil.Substring(1, strHasil.Length - 1);
			}
			if (minus)
			{
				strHasil = "Minus " + strHasil;
			}

			return strHasil.ToTitleCase();
		}

		public static string ToTitleCase(this string input)
		{
			var TxtInfo = new CultureInfo("en-US", false).TextInfo;
			return input.Length == 0 ? "" : TxtInfo.ToTitleCase(input);
		}

		public static string FirstCharToUpper(this string input) =>
				input switch
				{
					null => throw new ArgumentNullException(nameof(input)),
					"" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
					_ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
				};


		public static float ToFloat(this object value)
		{
			return Convert.ToSingle(value);
		}

		public static bool IsDBNull(this object str) => str == DBNull.Value;
		public static bool IsDBNull(this string? str)
		{
			return str == null || str.Length == 0 ? true : false;
		}

		public static bool IsBool(this string? str)
		{
			if (str == null || str.Length == 0) return false;
			if (str.ToLower() == "true")
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static long ToInt(this string value)
		{
			return value == null ? 0 : value.Length == 0 ? 0 : Convert.ToInt64(value);
		}

		public static int ToInt32(this string value)
		{
			return string.IsNullOrEmpty(value) ? 0 : value.Length == 0 ? 0 : Convert.ToInt32(value);
		}

		public static decimal ToDecimal(this string value)
		{
			return Convert.ToDecimal(value);
		}
		public static decimal? ToDecimalCustome(this string value)
		{
			value = value.Replace(".", "").Replace(",", "");
			return value.Length >= 3 ? Convert.ToDecimal(value.Substring(0, value.Length - 2) + "," + value.Substring(value.Length - 2)) : value.Length == 0 ? null : Convert.ToDecimal(value);
		}

		public static string ToFormatIDStringDate(this DateTime? value)
		{
			if (value == null) return "";
			return value.Value.ToString(
				"dd MMMM yyyy HH:mm:ss");
		}
		public static string ToFormatIDStringDate(this DateTime value)
		{
			return value.ToString(
				"dd MMMM yyyy HH:mm:ss");
		}
		public static string ToFormatIDStringDateShort(this DateTime? value)
		{
			if (value == null) return "";
			return value.Value.ToString(
				"dd/MM/yyyy");
		}
		public static string ToFormatIDStringDateShort(this DateTime value)
		{
			return value.ToString(
				"dd/MM/yyyy");
		}
		public static string ToFormatIDStringDateShort2(this DateTime value)
		{
			return value.ToString(
				"yyyy-MM-dd");
		}

		public static string ToFormatUSStringDate(this DateTime? value)
		{
			if (value == null) return "";
			return value.Value.ToString(
				"yyyy/MM/dd HH:mm:ss",
				new System.Globalization.CultureInfo("es-ES"));
		}
		public static string ToFormatUSStringDate(this DateTime value)
		{
			return value.ToString(
				"yyyy/MM/dd HH:mm:ss",
				new System.Globalization.CultureInfo("es-ES"));
		}
		public static string ToFormatUSStringDateShort(this DateTime? value)
		{
			if (value == null) return "";
			return value.Value.ToString(
				"yyyy/MM/dd",
				new System.Globalization.CultureInfo("es-ES"));
		}
		public static string ToFormatUSStringDateShort(this DateTime value)
		{
			return value.ToString(
				"yyyy/MM/dd",
				new System.Globalization.CultureInfo("es-ES"));
		}
		public static string ToNamaHari(this string DateValue)
		{
			DateTime TanggalNya = DateTime.Now;
			if (DateValue == null || DateValue == "")
			{
				TanggalNya = DateTime.Now;
			}
			else
			{
				if (!DateTime.TryParse(DateValue, out TanggalNya))
				{
					return DateValue;
				}
				else
				{
					TanggalNya = DateTime.Parse(DateValue);
				}
			}

			var myCulture = new System.Globalization.CultureInfo("id-ID");
			var dayOfWeek = myCulture.Calendar.GetDayOfWeek(TanggalNya);
			string dayName = myCulture.DateTimeFormat.GetDayName(dayOfWeek);

			return dayName;
		}
		public static string ToTanggalIDShort(this string DateValue)
		{
			DateTime TanggalNya = DateTime.Now;
			if (DateValue == null || DateValue == "")
			{
				TanggalNya = DateTime.Now;
			}
			else
			{
				if (!DateTime.TryParse(DateValue, out TanggalNya))
				{
					return DateValue;
				}
				else
				{
					TanggalNya = DateTime.Parse(DateValue);
				}
			}

			var myCulture = new System.Globalization.CultureInfo("id-ID");

			return TanggalNya.ToString("dd-MM-yyyy", myCulture);
		}
		public static string ToNamaBulan(this string DateValue)
		{
			DateTime TanggalNya = DateTime.Now;
			if (DateValue == null || DateValue == "")
			{
				TanggalNya = DateTime.Now;
			}
			else
			{
				if (!DateTime.TryParse(DateValue, out TanggalNya))
				{
					return DateValue;
				}
				else
				{
					TanggalNya = DateTime.Parse(DateValue);
				}
			}

			var myCulture = new System.Globalization.CultureInfo("id-ID");
			var MonthOfDate = myCulture.Calendar.GetMonth(TanggalNya);
			string monthName = myCulture.DateTimeFormat.GetMonthName(MonthOfDate);

			return monthName;
		}
		public static string ToTanggalPicker(this string DateValue)
		{
			DateTime TanggalNya = DateTime.Now;
			if (DateValue == null || DateValue == "")
			{
				TanggalNya = DateTime.Now;
			}
			else
			{
				if (!DateTime.TryParse(DateValue, out TanggalNya))
				{
					return DateValue;
				}
				else
				{
					TanggalNya = DateTime.Parse(DateValue);
				}
			}

			string Rs = TanggalNya.Year + "-" + (TanggalNya.Month < 10 ? "0" + TanggalNya.Month : TanggalNya.Month) + "-" + (TanggalNya.Day < 10 ? "0" + TanggalNya.Day : TanggalNya.Day);

			return Rs;
		}
		public static string ToFormatIDStringDateShort2(this string DateValue)
		{
			DateTime TanggalNya = DateTime.Now;
			if (DateValue == null || DateValue == "")
			{
				TanggalNya = DateTime.Now;
			}
			else
			{
				if (!DateTime.TryParse(DateValue, out TanggalNya))
				{
					return DateValue;
				}
				else
				{
					TanggalNya = DateTime.Parse(DateValue);
				}
			}
			return TanggalNya.ToString(
				"yyyy-MM-dd");
		}
		public static string ToTanggalIndonesia(this string DateValue)
		{

			DateTime TanggalNya = DateTime.Now;
			if (DateValue == null || DateValue == "")
			{
				TanggalNya = DateTime.Now;
			}
			else
			{
				if (!DateTime.TryParse(DateValue, out TanggalNya))
				{
					return DateValue;
				}
				else
				{
					TanggalNya = DateTime.Parse(DateValue);
				}
			}

			string[] ArrBulan = {"Januari",
			"Februari",
			"Maret",
			"April",
			"Mei",
			"Juni",
			"Juli",
			"Agustus",
			"September",
			"Oktober",
			"November",
			"Desember" };

			string Rs = TanggalNya.Day + " " + ArrBulan[TanggalNya.Month - 1] + " " + TanggalNya.Year;

			return Rs;

		}

		public static string ToTanggalIndonesiaWithTime(this string DateValue)
		{
			DateTime TanggalNya = DateTime.Now;
			if (string.IsNullOrEmpty(DateValue))
			{
				TanggalNya = DateTime.Now;
			}
			else
			{
				if (!DateTime.TryParse(DateValue, out TanggalNya))
				{
					return DateValue; // Return original string if parsing fails
				}
			}

			string[] ArrBulan = {
				"Januari", "Februari", "Maret", "April", "Mei", "Juni",
				"Juli", "Agustus", "September", "Oktober", "November", "Desember"
			};

			string Rs = $"{TanggalNya.Day} {ArrBulan[TanggalNya.Month - 1]} {TanggalNya.Year} at {TanggalNya:HH:mm}";

			return Rs;
		}

		public static string IntToRoman(this int num)
		{
			string romanResult = string.Empty;
			string[] romanLetters = {
			"M",
			"CM",
			"D",
			"CD",
			"C",
			"XC",
			"L",
			"XL",
			"X",
			"IX",
			"V",
			"IV",
			"I"
			};
			int[] numbers = {
				1000,
				900,
				500,
				400,
				100,
				90,
				50,
				40,
				10,
				9,
				5,
				4,
				1
			};
			int i = 0;
			while (num != 0)
			{
				if (num >= numbers[i])
				{
					num -= numbers[i];
					romanResult += romanLetters[i];
				}
				else
				{
					i++;
				}
			}
			return romanResult;
		}
		public static string ToNormalWa(this string val)
		{
			val = val.Trim();
			val = val.Replace(" ", "");
			val = val.Replace("-", "");
			val = val.Replace("+", "");
			val = Left(val, 1) == "0" ? "62" + val.Substring(1) : val;

			return val;
		}

		public static string Left(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;
			maxLength = Math.Abs(maxLength);

			return (value.Length <= maxLength
				   ? value
				   : value.Substring(0, maxLength)
				   );
		}
		public static string StripHtmlTags(this string source)
		{
			return Regex.Replace(source, "<.*?>|&.*?;", string.Empty);
		}
	}
}
