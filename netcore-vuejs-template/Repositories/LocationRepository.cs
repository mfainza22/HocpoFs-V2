using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;

namespace WeighingSystemCore.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public LocationRepository()
        {

        }

        public Location Create(Location location)
        {
            location.LocationName = location.LocationName.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(location.LocationId).Parameterize(), ParameterValue = location.LocationId },
                new ParameterInfo() { ParameterName = nameof(location.LocationName).Parameterize(), ParameterValue = location.LocationName }
            };

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into Locations (");
            qry.AppendLine(nameof(location.LocationName));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(location.LocationName).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            location.LocationId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return location;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from Locations where LocationId in  ({0})", strIds));
            DBContext.ExecuteQuery(qry.ToString());
        }

        public Location Get(long id)
        {
            if (id == 0) return null;
            string qry = $"Select top 1 * from Locations where {nameof(Location.LocationId)}= '{id}'";
            var result = DBContext.GetRecord<Models.Location>(qry);
            return result;
        }

        public Location Get(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            string qry = $"Select top 1 * from Locations where {nameof(Location.LocationName)}= '{name.Trim()}'";
            var result = DBContext.GetRecord<Models.Location>(qry);
            return result;
        }

        public IEnumerable<Location> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from Locations";
            var results = DBContext.GetRecords<Models.Location>(qry.ToString());
            return results;
        }

        public Location Update(Location locationChanges)
        {
            locationChanges.LocationName = locationChanges.LocationName.ToUpperCase();
            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(locationChanges.LocationId).Parameterize(), ParameterValue = locationChanges.LocationId },
                new ParameterInfo() { ParameterName = nameof(locationChanges.LocationName).Parameterize(), ParameterValue = locationChanges.LocationName }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update Locations set");
            qry.AppendLine($"{nameof(locationChanges.LocationName)}={nameof(locationChanges.LocationName).Parameterize()}");
            qry.AppendLine($"where {nameof(locationChanges.LocationId)} = {nameof(locationChanges.LocationId).Parameterize()}");
            DBContext.ExecuteQuery(qry.ToString(), parameters);
            return locationChanges;
        }

        public Location GetDefault(long currentId = 0)
        {
            string qry = $"Select top 1 * from Locations where IsDefault = '1' and LocationId <> '{currentId}'";

            var result = DBContext.GetRecord<Models.Location>(qry);

            return result;
        }

    }
}