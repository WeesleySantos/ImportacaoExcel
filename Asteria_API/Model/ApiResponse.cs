namespace Asteria_API.Model
{
    public class ApiResponse
    {
        public IEnumerable<DataModel> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
