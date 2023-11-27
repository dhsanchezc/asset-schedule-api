namespace AssetScheduleApi.Models.Entities
{
    public class Asset
    {
        public long Id { get; private set; }

        private string? name;
        public string? Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or whitespace.");
                name = value;
            }
        }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Asset()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}