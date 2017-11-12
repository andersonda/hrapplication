using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class PositionContext : DbContext
    {
        public PositionContext(DbContextOptions<PositionContext> options)
            : base(options)
        {
        }

        public DbSet<Position> Position { get; set; }
        public DbSet<Application> Application { get; set; }
    }
}
