using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class UserProfileContext : DbContext
    {
        public UserProfileContext(DbContextOptions<UserProfileContext> options)
            : base(options)
        {

        }

       public DbSet<UserProfile> UserProfile { get; set; }
    }
}
