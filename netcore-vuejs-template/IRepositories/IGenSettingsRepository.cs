using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IGeneralSettingsRepository
{
    GeneralSettings Get(Int64 id = 1);
    GeneralSettings Update(GeneralSettings genSettingsChanges);

}