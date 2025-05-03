using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    public class LeadEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("lead_name")]
        public string Name { get; set; }

        [Column("contact_number")]
        public string ContactNumber { get; set; }

        [Column("lead_source_id")]
        public int LeadSourceId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("lead_type_id")]
        public int LeadTypeId { get; set; }

        [Column("lead_date_time")]
        public DateTime DateTime { get; set; }

        [Column("is_converted")]
        public bool Converted { get; set; }

        [Column("sales_person_id")]
        public int SalesPersonId { get; set; }

        [Column("lead_list_id")]
        public int LeadListId { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }

        [Column("school_id")]
        public int SchoolId { get; set; }

        // --- Navigation Properties ---
        public LeadType LeadType { get; set; }
        public Source LeadSource { get; set; }
        public Status Status { get; set; }
        public LeadList LeadList { get; set; }
        public UserModel Owner { get; set; }
        public Branch Branch { get; set; }
        public School School { get; set; }
        public SalesPerson SalesPerson { get; set; }
    }
}
