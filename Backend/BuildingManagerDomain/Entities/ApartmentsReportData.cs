namespace BuildingManagerDomain.Entities
{
    public struct ApartmentsReportData
    {
        public int ApartmentFloor { get; set; }
        public int ApartmentNumber { get; set; }
        public string  OwnerName { get; set; }
        public ApartmentsReportData(int apartmentFloor, int apartmentNumber, string ownerName)
        {
            ApartmentFloor = apartmentFloor;
            ApartmentNumber = apartmentNumber;
            OwnerName = ownerName;
        }
    }
}
