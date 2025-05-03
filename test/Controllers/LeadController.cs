using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test.Services.Lead;
using test.ViewModels.Lead;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly LeadService _leadService;

        // Constructor to inject the LeadService into the controller
        public LeadController(LeadService leadService)
        {
            _leadService = leadService;
        }

        #region Get All Leads
        // GET: api/lead
        // Fetches a list of all leads
        [HttpGet]
        public async Task<ActionResult<List<LeadVM>>> GetLeads()
        {
            var leads = await _leadService.GetLeadsAsync();
            return Ok(leads); // Returns 200 OK with the list of leads
        }
        #endregion

        #region Get Lead by ID
        // GET: api/lead/{id}
        // Fetches a lead by its ID
        [HttpGet("{id}")]
        public async Task<ActionResult<LeadVM>> GetLeadById(int id)
        {
            var lead = await _leadService.GetLeadByIdAsync(id);
            if (lead == null)
            {
                return NotFound(); // Returns 404 if the lead is not found
            }
            return Ok(lead); // Returns 200 OK with the lead details
        }
        #endregion

        #region Create Lead
        // POST: api/lead
        // Creates a new lead
        [HttpPost]
        public async Task<ActionResult> CreateLead([FromBody] LeadVMPost leadVMPost)
        {
            if (leadVMPost == null)
            {
                return BadRequest("Lead data is null"); // Returns 400 BadRequest if the input is null
            }
            var leadEntity = await _leadService.CreateLeadAsync(leadVMPost);
            return Ok(new { Message = "Lead created successfully", LeadId = leadEntity.Id }); // Returns success message and the new LeadId
        }
        #endregion

        #region Update Lead
        // PUT: api/lead/{id}
        // Updates an existing lead by its ID
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLead(int id, [FromBody] LeadVMPost leadVMPost)
        {
            if (leadVMPost == null)
            {
                return BadRequest("Lead data is null"); // Returns 400 BadRequest if the input is null
            }
            var leadEntity = await _leadService.UpdateLeadAsync(id, leadVMPost);
            if (leadEntity == null)
            {
                return NotFound(); // Returns 404 if the lead is not found
            }
            return Ok(new { message = "Lead updated successfully", updatedLead = leadEntity }); // Returns success message with updated lead details
        }
        #endregion

        #region Delete Lead
        // DELETE: api/lead/{id}
        // Deletes a lead by its ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLead(int id)
        {
            var success = await _leadService.DeleteLeadAsync(id);
            if (!success)
            {
                return NotFound(); // Returns 404 if the lead was not found
            }
            return NoContent(); // Returns 204 NoContent if the lead was successfully deleted
        }
        #endregion
    }
}
