using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;

namespace WeighingSystemCore.Repositories
{
    public class GeneralSettingsRepository : IGeneralSettingsRepository
    {
        public GeneralSettingsRepository()
        {

        }
        public GeneralSettings Get(long id = 1)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from GeneralSettings";
            var result = DBContext.GetRecord<Models.GeneralSettings>(qry);
            return result;
        }

        public GeneralSettings Update(GeneralSettings genSettingsChanges)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(GeneralSettings.SettingsId).Parameterize(), ParameterValue = genSettingsChanges.SettingsId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(GeneralSettings.Tolerance).Parameterize(), ParameterValue = genSettingsChanges.Tolerance });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(GeneralSettings.ClientCode).Parameterize(), ParameterValue = genSettingsChanges.ClientCode.ToUpper() });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(GeneralSettings.ClientName).Parameterize(), ParameterValue = genSettingsChanges.ClientName.ToUpper() });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(GeneralSettings.ClientAddress).Parameterize(), ParameterValue = genSettingsChanges.ClientAddress.ToUpper() });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update GeneralSettings set");
            qry.AppendLine($"{nameof(genSettingsChanges.Tolerance)}={nameof(genSettingsChanges.Tolerance).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(genSettingsChanges.ClientCode)}={nameof(genSettingsChanges.ClientCode).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(genSettingsChanges.ClientName)}={nameof(genSettingsChanges.ClientName).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(genSettingsChanges.ClientAddress)}={nameof(genSettingsChanges.ClientAddress).Parameterize()}");
            qry.AppendLine($"where {nameof(genSettingsChanges.SettingsId)} = {nameof(genSettingsChanges.SettingsId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return genSettingsChanges;
        }



    }
}