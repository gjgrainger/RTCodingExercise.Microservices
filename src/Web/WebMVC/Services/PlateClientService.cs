using Catalog.API.DTOs;
using Newtonsoft.Json;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class PlateClientService : IPlateClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7160/plate";
        
        public PlateClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlateDto>> GetPlates(int pageIndex, int pageSize, string searchTerm)
        {
            List<PlateDto> plateDtos = new List<PlateDto>();

            // We don't use the `HttpClient` inside a using here as the socket is not immediately released
            // NB: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net
            HttpClient client = new HttpClient();
            using (Stream s = await client.GetStreamAsync(BuildGetPlatesQuery(pageIndex, pageSize, searchTerm)))
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                // We deserialize into a stream here to minimize garbage collection. 
                // "is especially important when working with JSON documents
                // greater than 85kb in size to avoid the JSON string ending up in the large object heap"
                // NB: https://www.newtonsoft.com/json/help/html/Performance.htm#MemoryUsage
                JsonSerializer serializer = new JsonSerializer();
                plateDtos = serializer.Deserialize<List<PlateDto>>(reader);
            }

            return plateDtos;
        }

        public async Task CreatePlate(PlateDto dto)
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsJsonAsync(_apiUrl, dto);
            response.EnsureSuccessStatusCode();
        }

        private string BuildGetPlatesQuery(int pageIndex, int pageSize, string? searchTerm)
            => string.IsNullOrEmpty(searchTerm)
                ? $"{_apiUrl}?pageIndex={pageIndex}&pageSize={pageSize}"
                : $"{_apiUrl}?pageIndex={pageIndex}&pageSize={pageSize}&searchTerm={searchTerm}";

    }
}
