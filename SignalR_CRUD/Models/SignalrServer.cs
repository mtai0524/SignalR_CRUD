using Microsoft.AspNetCore.SignalR;
using SignalR_CRUD.Controllers;

namespace SignalR_CRUD.Models
{
    public class SignalrServer: Hub
    {
        private readonly ApplicationDbContext _context;
        public SignalrServer(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SendProducts()
        {
            await Clients.All.SendAsync("ReceiveProducts");
        }
    }
}
