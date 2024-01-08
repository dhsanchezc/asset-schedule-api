namespace AssetScheduleApi.Models.DTOs
{
    public class AssetOutput : AssetInput, IBaseEntity
    {
        //public long Id { get; }
        //public DateTime CreatedAt { get; }
        //public DateTime UpdatedAt { get; }

        public long Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
