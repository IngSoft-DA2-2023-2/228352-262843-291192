namespace BuildingManagerDomain.Entities
{
    public struct ApartmentsReportData
    {
        public int ApartmentFloor { get; set; }
        public ApartmentsReportData(int apartmentFloor)
        {
            ApartmentFloor = apartmentFloor;
        }
    }
}
