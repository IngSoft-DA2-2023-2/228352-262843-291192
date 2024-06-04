using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;

namespace BuildingManagerILogic
{
    public interface IImporterLogic
    {
        List<Building> ImportData(string importerName, string path, Guid companyAdminSessionToken);
        List<string> ListImporters();
    }
}