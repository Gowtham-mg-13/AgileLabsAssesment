using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;

namespace ECommerceEmailNotification.ConsoleApp
{
    class Program
    {
        private static readonly List<string> processedTicketReferences = new List<string>();
        private static readonly List<Ticket> tickets = new List<Ticket>(); 

        static void Main(string[] args)
        {
            Timer timer = new Timer(ProcessTickets, null, 0, 60000); 
            Console.WriteLine("Console app running... Press [Enter] to exit.");
            Console.ReadLine(); 
        }

        private static void ProcessTickets(object state)
        {
            var highPriorityTickets = tickets.Where(t => t.Priority == "High" && !processedTicketReferences.Contains(t.OrderId)).ToList();

            foreach (var ticket in highPriorityTickets)
            {
                SendEmailAlert(ticket);
                processedTicketReferences.Add(ticket.OrderId); 
            }
        }

        private static void SendEmailAlert(Ticket ticket)
        {
            var mailMessage = new MailMessage("your-email@example.com", "admin-email@example.com")
            {
                Subject = $"High Priority Ticket: {ticket.Title}",
                Body = $"Details: {ticket.Description}\nOrder ID: {ticket.OrderId}",
                IsBodyHtml = true
            };

            using (var smtpClient = new SmtpClient("smtp.example.com"))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-email-password");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
        }
    }

    public class Ticket
    {
        public string UserId { get; set; }
        public string Priority { get; set; }
        public string Module { get; set; }
        public string Title { get; set; }
        public string OrderId { get; set; }
        public string Description { get; set; }
    }
}
