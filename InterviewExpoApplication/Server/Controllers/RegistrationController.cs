using InterviewExpoApplication.Server.Services;
using InterviewExpoApplication.Shared.EventRegistration;
using Microsoft.AspNetCore.Mvc;

namespace InterviewExpoApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEvent([FromBody]CreateEventRegistrationDto eventRegistrationDto)
        {
            if (!registrationService.IsRegistrationAvailable())
            {
                return BadRequest("Registration is closed");
            }

            if(ModelState.IsValid)
            {
                try
                {
                    await registrationService.RegisterEvent(eventRegistrationDto);
                }
                catch (AggregateException ex)
                {
                    return StatusCode(500, string.Join(", ", ex.InnerExceptions.Select(ie=>ie.Message)));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message.ToString());
                }
                return Ok(eventRegistrationDto);
            }


            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<EventRegistrationsResult>> GetEventRegistrationDetails()
        {
            var summary = await registrationService.GetEventRegistrationSummary();
            return Ok(summary);
        }

        [HttpGet("available")]
        public ActionResult<bool> GetRegistrationAvailable()
        {
            return Ok(registrationService.IsRegistrationAvailable());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEventRegistrations()
        {
            await registrationService.ClearEventRegistrations();
            return Ok();
        }

    }
}
