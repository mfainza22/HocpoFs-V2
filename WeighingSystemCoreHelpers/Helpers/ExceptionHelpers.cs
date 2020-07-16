using System;
using System.Text;

namespace WeighingSystemCoreHelpers.Helpers
{
	public static class ExceptionHelpers
	{
		public static string FlattenExceptions(AggregateException ex)
		{
			StringBuilder strE = new StringBuilder();
			foreach (var e in ex.InnerExceptions)
			{
				// Handle the custom exception.
				if (e is CustomException)
				{
					strE.AppendLine(e.Message);
				}
				// Rethrow any other exception.
				else
				{
					strE.AppendLine(e.Message);
				}
			}

			return strE.ToString();
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

	public class CustomException : Exception
	{
		public CustomException(String message)
			: base(message)
		{ }

		
	}
}
