using System.Net.Sockets;

namespace ECommerceEmailNotification.API.Services
{
    public interface ITicketService
    {
        string RaiseTicket(Ticket ticket);
    }
}
