using System.Text.Json;
using BuildingManagerIImporter;

namespace JsonImporter
{
    public class JsonImporter : IImporter
    {
        public string Name => "DefaultJson";

        public JsonImporter() { }

        public List<ImporterBuilding> Import(string data, Guid companyId)
        {
            List<ImporterBuilding> buildings = new List<ImporterBuilding>();
            List<JsonBuilding> jsonBuildings = JsonSerializer.Deserialize<List<JsonBuilding>>(data);

            foreach (JsonBuilding buildingData in jsonBuildings)
            {
                ImporterBuilding building = new ImporterBuilding
                {
                    Name = buildingData.nombre,
                    Address = $"({buildingData.direccion.calle_principal} {buildingData.direccion.numero_puerta}, {buildingData.direccion.calle_secundaria})",
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

        private class JsonBuilding
        {
            public string nombre { get; set; }
            public JsonAddress direccion { get; set; }
            public string encargado { get; set; }
            public JsonGps gps { get; set; }
            public long gastos_comunes { get; set; }
            public List<JsonApartment> departamentos { get; set; }
        }

        private class JsonAddress
        {
            public string calle_principal { get; set; }
            public int numero_puerta { get; set; }
            public string calle_secundaria { get; set; }
        }

        private class JsonGps
        {
            public double latitud { get; set; }
            public double longitud { get; set; }
        }

        private class JsonApartment
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
