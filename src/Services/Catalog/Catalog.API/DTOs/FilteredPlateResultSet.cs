namespace Catalog.API.DTOs
{
    public struct FilteredPlateResultSet
    {
        public FilteredPlateResultSet(List<PlateDto> plateDtos, int totalPlates) 
        {
            Plates = plateDtos;
            TotalPlates = totalPlates;
        }

        public List<PlateDto> Plates { get; set; }

        public int TotalPlates { get; set; }
    }
}
