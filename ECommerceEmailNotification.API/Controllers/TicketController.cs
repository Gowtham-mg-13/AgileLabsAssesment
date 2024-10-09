using Microsoft.AspNetCore.Mvc;
using ECommerceEmailNotification.API.Models;

namespace ECommerceEmailNotification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("RaiseTicket")]
        public IActionResult RaiseTicket([FromBody] Ticket ticket)
        {
            var ticketReference = _ticketService.RaiseTicket(ticket);
            return Ok(new { success = true, message = $"Ticket raised successfully. Ticket Reference #{ticketReference}" });
        }
    }
}
