using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer {
    public static class ServerUtils {
        public static String ConnectionString (string name = "DeveloperConnection") {

            IConfigurationBuilder builder = new ConfigurationBuilder ();
            builder.AddJsonFile (Path.Combine (Directory.GetCurrentDirectory (), "appsettings.json"));

            var root = builder.Build ();
            var connectionString = root.GetConnectionString (name);
            return connectionString;
        }

          public static DynamicParameters ToDynamicParams(this List<ParameterInfo> ps)
        {
            DynamicParameters p = new DynamicParameters();
            if (ps == null) { return p; }
            foreach (var param in ps)
            {
                p.Add("@" + param.ParameterName, param.ParameterValue);
            }

            return p;
        }
    }
}