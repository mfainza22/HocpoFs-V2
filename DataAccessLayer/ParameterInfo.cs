
using System.Data;


namespace DataAccessLayer {
    public class ParameterInfo {
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
        
        public DbType ParameterDbType {
            get;  set; }
    }
}