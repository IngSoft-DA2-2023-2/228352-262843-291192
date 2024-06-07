using System;
using System.Collections.Generic;
using BuildingManagerIImporter;

namespace BuildingManagerILogic
{
    public interface IImporterLogic
    {
        List<ImporterBuilding> ImportData(string importerName, string data, Guid companyAdminSessionToken);
        List<IImporter> ListImporters();
        List<string> ListImportersNames();
    }
}