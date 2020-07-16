using System;
using System.Collections.Generic;
using System.Text;

namespace WeighingSystemCoreHelpers.Enums
{
    public static class Enums
    {
        public enum TransactionStatus
        {
            PENDING,
            COMPLETED,
            ALL
        }

        public enum TransactionProcess
        {
            NONE,
            WEIGH_IN,
            WEIGH_OUT,
            UPDATE_IN,
            UPDATE_OUT,
            DELETE
        }

        public enum TicketType
        {
            IN,
            OUT,
            ALL,
        }

        public static Dictionary<int, string> ReportTypes()
        {
            var strList = new Dictionary<int, string>();
            strList.Add(1, "Daily Transfer Summary");
            strList.Add(3, "Daily Transfer with Remarks");
            strList.Add(2, "General Summary");
            strList.Add(4, "Monthly General Summary (With Weigh-In)");
            strList.Add(5, "Monthly General Summary (No Weigh-In)");

            return strList;
        }

        public enum WeighStatus
        {
            NONE,
            PASSED,
            FAILED,
            OVERLOAD,
            UNDERLOAD,
        }
    }
}
