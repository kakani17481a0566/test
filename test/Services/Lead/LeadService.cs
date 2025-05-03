using System.Globalization;
using Microsoft.EntityFrameworkCore;
using test.Data;
using test.Models;
using test.ViewModels.Lead;

namespace test.Services.Lead
{
    public class LeadService
    {
        private readonly ApplicationDbContext _context;

        public LeadService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Get All Leads
        public async Task<List<LeadVM>> GetLeadsAsync()
        {
            var leads = await _context.leads
                .Include(l => l.LeadSource)
                .Include(l => l.LeadType)
                .Include(l => l.Status)
                .Include(l => l.LeadList)
                .Include(l => l.Owner)
                .Include(l => l.Branch)
                .Include(l => l.School)
                .ToListAsync();

            var leadVMs = leads.Select(l => new LeadVM
            {
                LeadName = l.Name,
                ContactNumber = l.ContactNumber,
                LeadSourceId = l.LeadSourceId,
                LeadSourceName = l.LeadSource.Name,
                BranchId = l.BranchId,
                BranchName = l.Branch.BranchName,
                SchoolId = l.SchoolId,
                SchoolName = l.School.Name,
                LeadTypeId = l.LeadTypeId,
                LeadTypeName = l.LeadType.Type,
                DateTime = l.DateTime,
                Converted = l.Converted,
                SalesPersonId = l.SalesPersonId,
                LeadListId = l.LeadListId,
                StatusId = l.StatusId,
                StatusName = l.Status.Name,
                OwnerId = l.OwnerId,
                OwnerName = $"{l.Owner.FirstName} {l.Owner.LastName}"
            }).ToList();

            return leadVMs;
        }
        #endregion

        //region Get Lead by ID
        public async Task<LeadVM> GetLeadByIdAsync(int id)
        {
            var lead = await _context.leads
                .Include(l => l.LeadSource)
                .Include(l => l.LeadType)
                .Include(l => l.Status)
                .Include(l => l.LeadList)
                .Include(l => l.Owner)
                .Include(l => l.Branch)
                .Include(l => l.School)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lead == null)
            {
                return null; // Handle not found case appropriately
            }

            var leadVM = new LeadVM
            {
                LeadName = lead.Name,
                ContactNumber = lead.ContactNumber,
                LeadSourceId = lead.LeadSourceId,
                LeadSourceName = lead.LeadSource.Name,
                BranchId = lead.BranchId,
                BranchName = lead.Branch.BranchName,
                SchoolId = lead.SchoolId,
                SchoolName = lead.School.Name,
                LeadTypeId = lead.LeadTypeId,
                LeadTypeName = lead.LeadType.Type,
                DateTime = lead.DateTime,
                Converted = lead.Converted,
                SalesPersonId = lead.SalesPersonId,
                LeadListId = lead.LeadListId,
                StatusId = lead.StatusId,
                StatusName = lead.Status.Name,
                OwnerId = lead.OwnerId,
                OwnerName = $"{lead.Owner.FirstName} {lead.Owner.LastName}"
            };

            return leadVM;
        }
        //#endregion

        //region Create Lead
        public async Task<LeadEntity> CreateLeadAsync(LeadVMPost leadVMPost)
        {
            // Specify the exact format you expect (e.g., without "T")
            string dateFormat = "yyyy-MM-dd HH:mm:ss";
            DateTime parsedDateTime;

            bool isValidDateTime = DateTime.TryParseExact(leadVMPost.DateTime, dateFormat,
                                                         CultureInfo.InvariantCulture,
                                                         DateTimeStyles.None, out parsedDateTime);

            if (!isValidDateTime)
            {
                // Handle invalid date format appropriately
                throw new ArgumentException("Invalid date format", nameof(leadVMPost.DateTime));
            }

            var leadEntity = new LeadEntity
            {
                Name = leadVMPost.LeadName,
                ContactNumber = leadVMPost.ContactNumber,
                LeadSourceId = leadVMPost.LeadSourceId,
                BranchId = leadVMPost.BranchId,
                LeadTypeId = leadVMPost.LeadTypeId,
                DateTime = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc),
                Converted = leadVMPost.Converted,
                SalesPersonId = leadVMPost.SalesPersonId,
                LeadListId = leadVMPost.LeadListId,
                StatusId = leadVMPost.StatusId,
                OwnerId = leadVMPost.OwnerId,
                SchoolId = leadVMPost.SchoolId
            };

            _context.leads.Add(leadEntity);
            await _context.SaveChangesAsync();

            return leadEntity;
        }


        //#endregion

        //region Update Lead
        public async Task<LeadEntity> UpdateLeadAsync(int id, LeadVMPost leadVMPost)
        {
            var leadEntity = await _context.leads.FindAsync(id);

            if (leadEntity == null)
            {
                return null; // Handle not found case
            }

            // Parse and convert DateTime to UTC
            var parsedDateTime = DateTime.Parse(leadVMPost.DateTime).ToUniversalTime();

            leadEntity.Name = leadVMPost.LeadName;
            leadEntity.ContactNumber = leadVMPost.ContactNumber;
            leadEntity.LeadSourceId = leadVMPost.LeadSourceId;
            leadEntity.BranchId = leadVMPost.BranchId;
            leadEntity.LeadTypeId = leadVMPost.LeadTypeId;
            leadEntity.DateTime = parsedDateTime; // Set the parsed DateTime (now in UTC)
            leadEntity.Converted = leadVMPost.Converted;
            leadEntity.SalesPersonId = leadVMPost.SalesPersonId;
            leadEntity.LeadListId = leadVMPost.LeadListId;
            leadEntity.StatusId = leadVMPost.StatusId;
            leadEntity.OwnerId = leadVMPost.OwnerId;

            _context.leads.Update(leadEntity);
            await _context.SaveChangesAsync();

            return leadEntity;
        }


        //#endregion

        //region Delete Lead
        public async Task<bool> DeleteLeadAsync(int id)
        {
            var leadEntity = await _context.leads.FindAsync(id);

            if (leadEntity == null)
            {
                return false; // Handle not found case
            }

            _context.leads.Remove(leadEntity);
            await _context.SaveChangesAsync();

            return true;
        }
        //#endregion
    }
}
