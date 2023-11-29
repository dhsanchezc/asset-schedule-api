using System.ComponentModel.DataAnnotations;

namespace AssetScheduleApi.Models.Entities
{
    public class Asset
    {
        public long Id { get; private set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string? Name { get; set; }


        // check ef
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // periodicity

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