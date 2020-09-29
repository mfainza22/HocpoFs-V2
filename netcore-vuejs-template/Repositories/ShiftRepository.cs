using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;
using System;

namespace WeighingSystemCore.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        public ShiftRepository()
        {

        }

        public Shift Get(long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from Shifts where ShiftId= '" + id + "'";
            var result = DBContext.GetRecord<Models.Shift>(qry);
            return result;
        }

        public Shift GetByCode(string code)
        {
            if (String.IsNullOrEmpty(code)) return null;
            string qry = $"Select top 1 * from Shifts where {nameof(Shift.ShiftCode)}= '{code}'";
            var result = DBContext.GetRecord<Models.Shift>(qry);
            return result;
        }

        public Shift GetByDesc(string desc)
        {
            if (String.IsNullOrEmpty(desc)) return null;
            string qry = $"Select top 1 * from Shifts where {nameof(Shift.ShiftDesc)}= '{desc}'";
            var result = DBContext.GetRecord<Models.Shift>(qry);
            return result;
        }

        public IEnumerable<Shift> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from Shifts";
            var results = DBContext.GetRecords<Models.Shift>(qry.ToString());
            return results;
        }

        public Shift Create(Shift shift)
        {
            shift.ShiftCode = shift.ShiftCode.ToUpperCase();
            shift.ShiftDesc = shift.ShiftDesc.ToUpperCase();
            shift.TimeFrom = shift.TimeFrom.ToUpperCase();
            shift.TimeTo = shift.TimeTo.ToUpperCase();

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shift.ShiftId).Parameterize(), ParameterValue = shift.ShiftId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shift.ShiftCode).Parameterize(), ParameterValue = shift.ShiftCode  });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shift.ShiftDesc).Parameterize(), ParameterValue = shift.ShiftDesc  });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shift.TimeFrom).Parameterize(), ParameterValue = shift.TimeFrom  });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shift.TimeTo).Parameterize(), ParameterValue = shift.TimeTo  });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("insert into Shifts (");
            qry.AppendLine(nameof(shift.ShiftCode) + (char)44);
            qry.AppendLine(nameof(shift.ShiftDesc) + (char)44);
            qry.AppendLine(nameof(shift.TimeFrom) + (char)44);
            qry.AppendLine(nameof(shift.TimeTo));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(shift.ShiftCode).Parameterize() + (char)44);
            qry.AppendLine(nameof(shift.ShiftDesc).Parameterize() + (char)44);
            qry.AppendLine(nameof(shift.TimeFrom).Parameterize() + (char)44);
            qry.AppendLine(nameof(shift.TimeTo).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            shift.ShiftId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return shift;
        }

        public Shift Update(Shift shiftChanges)
        {
            shiftChanges.ShiftCode = shiftChanges.ShiftCode.ToUpperCase();
            shiftChanges.ShiftDesc = shiftChanges.ShiftDesc.ToUpperCase();
            shiftChanges.TimeFrom = shiftChanges.TimeFrom.ToUpperCase();
            shiftChanges.TimeTo = shiftChanges.TimeTo.ToUpperCase();

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shiftChanges.ShiftId).Parameterize(), ParameterValue = shiftChanges.ShiftId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shiftChanges.ShiftCode).Parameterize(), ParameterValue = shiftChanges.ShiftCode  });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shiftChanges.ShiftDesc).Parameterize(), ParameterValue = shiftChanges.ShiftDesc  });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shiftChanges.TimeFrom).Parameterize(), ParameterValue = shiftChanges.TimeFrom  });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(shiftChanges.TimeTo).Parameterize(), ParameterValue = shiftChanges.TimeTo  });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update Shifts set");
            qry.AppendLine($"{nameof(shiftChanges.ShiftCode)}={nameof(shiftChanges.ShiftCode).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(shiftChanges.ShiftDesc)}={nameof(shiftChanges.ShiftDesc).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(shiftChanges.TimeFrom)}={nameof(shiftChanges.TimeFrom).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(shiftChanges.TimeTo)}={nameof(shiftChanges.TimeTo).Parameterize()}");
            qry.AppendLine($"where {nameof(shiftChanges.ShiftId)} = {nameof(shiftChanges.ShiftId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return shiftChanges;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine($"Delete from Shifts where {nameof(Shift.ShiftId)} in  ({strIds})");
            int success = DBContext.ExecuteQuery(qry.ToString());

        }
        public Shift GetCurrentShift(DateTime dt)
        {
            //dt = DateTime.Now.Date + new TimeSpan(0, 1, 0);
            var currentTime = dt.ToString("HH:mm");

            StringBuilder str = new StringBuilder();
            str.AppendLine("  ");
            str.AppendLine($" declare @currentTime datetime; set @currentTime = (SELECT DATEDIFF(dd, 0,GETDATE()) + Convert(datetime,'{currentTime}')); ");
            str.AppendLine(" declare @ShiftId bigint;  ");
            str.AppendLine(" set @ShiftId = ISNULL((select top 1 ShiftId from Shifts where  ");
            str.AppendLine(" 	@currentTime between  ");
            str.AppendLine(" 	(SELECT DATEDIFF(dd, 0,GETDATE()) + Convert(datetime,TimeFrom)) and  ");
            str.AppendLine(" 	(SELECT DATEDIFF(dd, 0,GETDATE()) + Convert(datetime,TimeTo))),(select top 1 ShiftId from Shifts order by ShiftId desc)) ");
            str.AppendLine(" select top 1 * from Shifts where ShiftId = @ShiftId ");

            var result = DBContext.GetRecord<Shift>(str.ToString());
            var timeTo = Convert.ToDateTime(result.TimeTo);
      
            if (result.ShiftId == 2 && dt.TimeOfDay.TotalHours >= 0 && dt.TimeOfDay.TotalHours <= timeTo.TimeOfDay.TotalHours)
            {
                result.ShiftDate = dt.AddDays(-1).Date;
            } else
            {
                result.ShiftDate = DateTime.Now.Date;
            }

            return result;
        }

    }
}