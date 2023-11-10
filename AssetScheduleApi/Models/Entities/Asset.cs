namespace AssetScheduleApi.Models.Entities
{
    public class Asset
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; }
    }
}