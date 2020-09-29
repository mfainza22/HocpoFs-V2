using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;

namespace WeighingSystemCore.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        public WarehouseRepository()
        {

        }

        public IEnumerable<Warehouse> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from Warehouses";
            var results = DBContext.GetRecords<Models.Warehouse>(qry.ToString());
            return results;
        }

        public Warehouse Get(long id)
        {
            if (id == 0) return null;
            string qry = $"Select top 1 * from Warehouses where {nameof(Warehouse.WarehouseId)}= '{id}'";
            var result = DBContext.GetRecord<Models.Warehouse>(qry);
            return result;
        }

        public Warehouse GetByWarehouseCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return null;
            string qry = $"Select top 1 * from Warehouses where {nameof(Warehouse.WarehouseCode)}= '{code}'";
            var result = DBContext.GetRecord<Models.Warehouse>(qry);
            return result;
        }
        public Warehouse GetByWarehouseName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            string qry = $"Select top 1 * from Warehouses where {nameof(Warehouse.WarehouseName)}= '{name}'";
            var result = DBContext.GetRecord<Models.Warehouse>(qry);
            return result;
        }

        public Warehouse Create(Warehouse warehouse)
        {
            warehouse.WarehouseCode = warehouse.WarehouseCode.ToUpperCase();
            warehouse.WarehouseName = warehouse.WarehouseName.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(warehouse.WarehouseId).Parameterize(), ParameterValue = warehouse.WarehouseId },
                new ParameterInfo() { ParameterName = nameof(warehouse.WarehouseCode).Parameterize(), ParameterValue = warehouse.WarehouseCode },
                new ParameterInfo() { ParameterName = nameof(warehouse.WarehouseName).Parameterize(), ParameterValue = warehouse.WarehouseName }
            };

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into Warehouses (");
            qry.AppendLine(nameof(warehouse.WarehouseCode) + (char)44);
            qry.AppendLine(nameof(warehouse.WarehouseName));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(warehouse.WarehouseCode).Parameterize() + (char)44);
            qry.AppendLine(nameof(warehouse.WarehouseName).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            warehouse.WarehouseId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return warehouse;
        }

        public Warehouse Update(Warehouse warehouseChanges)
        {
            warehouseChanges.WarehouseCode = warehouseChanges.WarehouseCode.ToUpperCase();
            warehouseChanges.WarehouseName = warehouseChanges.WarehouseName.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(warehouseChanges.WarehouseId).Parameterize(), ParameterValue = warehouseChanges.WarehouseId },
                new ParameterInfo() { ParameterName = nameof(warehouseChanges.WarehouseCode).Parameterize(), ParameterValue = warehouseChanges.WarehouseCode.ToUpper() },
                new ParameterInfo() { ParameterName = nameof(warehouseChanges.WarehouseName).Parameterize(), ParameterValue = warehouseChanges.WarehouseName.ToUpper() }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update Warehouses set");
            qry.AppendLine($"{nameof(warehouseChanges.WarehouseCode)}={nameof(warehouseChanges.WarehouseCode).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(warehouseChanges.WarehouseName)}={nameof(warehouseChanges.WarehouseName).Parameterize()}");
            qry.AppendLine($"where {nameof(warehouseChanges.WarehouseId)} = {nameof(warehouseChanges.WarehouseId).Parameterize()}");
            DBContext.ExecuteQuery(qry.ToString(), parameters);
            return warehouseChanges;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from Warehouses where WarehouseId in  ({0})", strIds));
            DBContext.ExecuteQuery(qry.ToString());
        }
       
        public Warehouse GetDefault(long currentId = 0)
        {
            string qry = $"Select top 1 * from Warehouses where IsDefault = '1' and WarehouseId <> '{currentId}'";

            var result = DBContext.GetRecord<Models.Warehouse>(qry);

            return result;
        }

    }
}