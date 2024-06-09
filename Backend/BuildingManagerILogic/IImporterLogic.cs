using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;
using BuildingManagerIImporter;

namespace BuildingManagerILogic
{
    public interface IImporterLogic
    {
        List<Building> ImportData(string importerName, string data, Guid companyAdminSessionToken);
        List<IImporter> ListImporters();
        List<string> ListImportersNames();
    }
}