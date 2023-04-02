using Parcial2_SebastianRestrepoMira.DAL.Entities;

namespace Parcial2_SebastianRestrepoMira.DAL
{
    public class SeederDb
    { 
        private readonly DatabaseContext _context;

        public SeederDb(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateTicketsAsync();
            await _context.SaveChangesAsync();
        }

        private async Task PopulateTicketsAsync()
        {
            if(!_context.Tickets.Any())
            {
                for (int i = 0; i < 50000; i++)
                {
                    _context.Tickets.Add(new Ticket { IsUsed = false, UseDate = null, EntranceGate = null });
                }
            }
        }
    }
}
