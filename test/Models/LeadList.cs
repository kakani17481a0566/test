using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    [Table("lead_lists")]  // Table Name for LeadList
    public class LeadList
    {
        [Column("id")]  // Column Name for Id
        public int Id { get; set; }

        [Column("name")]  // Column Name for Name
        public string Name { get; set; }

        // Navigation Property for Leads (One-to-Many Relationship with LeadEntity)
        public ICollection<LeadEntity> Leads { get; set; }
    }
}
