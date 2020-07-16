using System;
using System.Text;

namespace WeighingSystemCoreHelpers.Helpers
{
	public static class MonthLetter
	{
		public static string GetMonthLetter(int month)
		{
            var monthLetter = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "L", "M" };
            return monthLetter[month-1];
		}

		public static Exception CheckDeleteConstraint(Exception ex)
		{
			if (ex.Message.Contains("The DELETE statement conflicted"))
			{
				return new Exception("Cannot delete record that is linked to another record/s");
			}
			else
			{
				return new Exception(ex.Message);
			}
		}

	}
	
}
