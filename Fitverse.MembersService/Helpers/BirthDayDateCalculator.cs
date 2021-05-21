using System;

namespace Fitverse.MembersService.Helpers
{
	public static class BirthDayDateCalculator
	{
		public static DateTime ExtractFromPesel(string pesel)
		{
			var year = CharToInt(pesel[0]) * 10 + CharToInt(pesel[1]) + (CharToInt(pesel[2]) / 2 + 1) % 5 * 100 + 1800;
			var month = CharToInt(pesel[2]) % 2 * 10 + CharToInt(pesel[3]);
			var day = CharToInt(pesel[4]) * 10 + CharToInt(pesel[5]);

			var birthDay = new DateTime(year, month, day);
			return birthDay;
		}

		private static int CharToInt(char c)
		{
			return c - '0';
		}
	}
}