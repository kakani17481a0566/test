namespace test.ViewModels.Lead
{
    public class LeadVMPost
    {
        public string LeadName { get; set; }
        public string ContactNumber { get; set; }
        public int LeadSourceId { get; set; }
        public int BranchId { get; set; }
        public int LeadTypeId { get; set; }


        public string DateTime { get; set; }

        public bool Converted { get; set; }
        public int SalesPersonId { get; set; }
        public int LeadListId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
        public int SchoolId { get; set; }
    }
}
