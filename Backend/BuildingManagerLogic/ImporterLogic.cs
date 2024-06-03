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

        public List<string> ListImporters()
        {
            return Importers.Select(i => i.Name).ToList();
        }
    }
}
