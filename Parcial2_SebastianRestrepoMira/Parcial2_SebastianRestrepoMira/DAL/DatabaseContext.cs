using Microsoft.EntityFrameworkCore;
using Parcial2_SebastianRestrepoMira.DAL.Entities;

namespace Parcial2_SebastianRestrepoMira.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Ticket> Tickets { get; set; }  
    }
}
