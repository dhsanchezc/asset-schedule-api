namespace AssetScheduleApi.Models.DTOs
{
    public interface IBaseEntity
    {
        public long Id { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
    }
}