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
                using (TextReader reader = new StringReader(data))
                {
                    xmlBuildings = (XmlImporterBuilding)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                throw new NotCorrectImporterException(e, "Error while parsing the JSON data");
            }

            foreach (XmlBuilding buildingData in xmlBuildings.edificios)
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

        [XmlRoot]
        public class XmlImporterBuilding
        {
            [XmlArray("edificios")]
            public XmlBuilding[] edificios { get; set; }
        }
        public class XmlBuilding
        {
            [XmlElement]
            public string nombre { get; set; }

            [XmlElement]
            public XmlAddress direccion { get; set; }

            [XmlElement]
            public string encargado { get; set; }

            [XmlElement]
            public XmlGps gps { get; set; }

            [XmlElement]
            public long gastos_comunes { get; set; }

            [XmlArray("departamentos")]
            public XmlApartment[] departamentos { get; set; }
        }

        public class XmlAddress
        {
            [XmlElement]
            public string calle_principal { get; set; }

            [XmlElement]
            public int numero_puerta { get; set; }

            [XmlElement]
            public string calle_secundaria { get; set; }
        }

        public class XmlGps
        {
            [XmlElement]
            public double latitud { get; set; }

            [XmlElement]
            public double longitud { get; set; }
        }

        public class XmlApartment
        {
            [XmlElement]
            public int piso { get; set; }

            [XmlElement]
            public int numero_puerta { get; set; }

            [XmlElement]
            public int habitaciones { get; set; }

            [XmlElement]
            public bool conTerraza { get; set; }

            [XmlElement]
            public int baños { get; set; }

            [XmlElement]
            public string propietarioEmail { get; set; }
        }
    }
}
