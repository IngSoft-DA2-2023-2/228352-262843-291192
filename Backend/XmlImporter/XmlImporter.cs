using BuildingManagerIImporter;
using BuildingManagerIImporter.Exceptions;
using System.Xml.Serialization;
using System.IO;

namespace XmlImporter
{
    public class XmlImporter : IImporter
    {
        public string Name => "DefaultXml";

        public XmlImporter() { }

        public List<ImporterBuilding> Import(string data, Guid companyId)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlImporterBuilding));
            List<ImporterBuilding> buildings = new List<ImporterBuilding>();
            XmlImporterBuilding xmlBuildings;
            try
            {
                using (StringReader reader = new StringReader(data))
                {
                    xmlBuildings = (XmlImporterBuilding)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                throw new NotCorrectImporterException(e, "Error while parsing the JSON data");
            }

            foreach (XmlBuilding buildingData in xmlBuildings.edificios.building)
            {
                ImporterBuilding building = new ImporterBuilding
                {
                    Name = buildingData.nombre,
                    Address = $"{buildingData.direccion.calle_principal} {buildingData.direccion.numero_puerta}, {buildingData.direccion.calle_secundaria}",
                    Location = $"({buildingData.gps.latitud},{buildingData.gps.longitud})",
                    CommonExpenses = buildingData.gastos_comunes,
                    Manager = buildingData.encargado,
                    Apartments = new List<ImporterApartment>()
                };

                foreach (var apartmentData in buildingData.departamentos)
                {
                    ImporterApartment apartment = new ImporterApartment
                    {
                        OwnerEmail = apartmentData.propietarioEmail,
                        Floor = apartmentData.piso,
                        Number = apartmentData.numero_puerta,
                        Rooms = apartmentData.habitaciones,
                        Bathrooms = apartmentData.baños,
                        HasTerrace = apartmentData.conTerraza
                    };
                    building.Apartments.Add(apartment);
                }

                buildings.Add(building);
            }
            return buildings;
        }

        public class XmlImporterBuilding
        {
            public XmlBuildings edificios { get; set; }
        }
        public class XmlBuildings
        {
            public List<XmlBuilding> building { get; set; }
        }
        public class XmlBuilding
        {
            public string nombre { get; set; }
            public XmlAddress direccion { get; set; }
            public string encargado { get; set; }
            public XmlGps gps { get; set; }
            public long gastos_comunes { get; set; }
            public List<XmlApartment> departamentos { get; set; }
        }

        public class XmlAddress
        {
            public string calle_principal { get; set; }
            public int numero_puerta { get; set; }
            public string calle_secundaria { get; set; }
        }

        public class XmlGps
        {
            public double latitud { get; set; }
            public double longitud { get; set; }
        }

        public class XmlApartment
        {
            public int piso { get; set; }
            public int numero_puerta { get; set; }
            public int habitaciones { get; set; }
            public bool conTerraza { get; set; }
            public int baños { get; set; }
            public string propietarioEmail { get; set; }
        }
    }
}
