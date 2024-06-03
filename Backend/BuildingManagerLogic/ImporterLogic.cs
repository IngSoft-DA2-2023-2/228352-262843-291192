using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using System.Collections.Generic;
using System.Linq;

namespace BuildingManagerLogic
{
    public class ImporterLogic : IImporterLogic
    {
        public readonly List<IImporter> Importers;
        public ImporterLogic()
        {
            Importers = new List<IImporter>();
        }
        public List<Building> ImportData(string importerName, string path)
        {
            IImporter importer = Importers.Find(i => i.Name.Equals(importerName));
            return importer.Import(path);
        }

        public List<string> ListImporters()
        {
            return Importers.Select(i => i.Name).ToList();
        }
    }
}
