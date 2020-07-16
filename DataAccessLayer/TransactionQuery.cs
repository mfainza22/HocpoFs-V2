using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccessLayer
{
    public class TransactionQuery
    {
        public TransactionQuery()
        {

        }

        public StringBuilder Query { get; set; }

        public List<ParameterInfo> Parameters { get; set; }

        public bool WithResult { get; set; }

        public CommandType CommandType { get; set; }
    }
}